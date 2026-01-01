using Microsoft.AspNetCore.Builder;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

// Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Gaming Hub API",
        Version = "v1",
        Description = "API pour la gestion de tournois esport"
    });
});

// TODO: Ajouter l'authentification JWT ici
// TODO: Ajouter FluentValidation
// TODO: Ajouter EF Core DbContext
// TODO: Ajouter les services Application et Infrastructure

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TODO: Ajouter le middleware global d'exception handling

app.UseHttpsRedirection();

// TODO: Décommenter quand l'authentification sera configurée
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting Gaming Hub API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
