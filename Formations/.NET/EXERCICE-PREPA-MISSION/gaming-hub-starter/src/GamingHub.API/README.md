# 🎯 GamingHub.API

**Couche API** - Controllers et configuration ASP.NET Core

## 📋 À implémenter ici

### Controllers (Controllers/)

- `AuthController.cs` - Register, Login
- `TournamentsController.cs` - CRUD tournois
- `TeamsController.cs` - CRUD équipes

### Middleware (Middleware/)

- `GlobalExceptionHandler.cs` - Gestion globale des erreurs

## ⚠️ Règles importantes

- ✅ **Controllers doivent être minimalistes** - Déléguer aux services
- ✅ **Utiliser les attributs [Authorize]** pour protéger les endpoints
- ✅ **Retourner des DTOs** (pas des entités Domain)
- ✅ **Logger les actions importantes** avec Serilog

## 📝 Exemple de controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class TournamentsController : ControllerBase
{
    private readonly ITournamentService _tournamentService;
    private readonly ILogger<TournamentsController> _logger;

    public TournamentsController(
        ITournamentService tournamentService,
        ILogger<TournamentsController> logger)
    {
        _tournamentService = tournamentService;
        _logger = logger;
    }

    [HttpPost]
    [Authorize(Roles = "Organizer,Admin")]
    public async Task<IActionResult> Create([FromBody] CreateTournamentDto dto)
    {
        _logger.LogInformation("Creating tournament {Name}", dto.Name);

        var tournament = await _tournamentService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = tournament.Id }, tournament);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tournament = await _tournamentService.GetByIdAsync(id);

        if (tournament == null)
            return NotFound();

        return Ok(tournament);
    }
}
```

## 📝 Middleware d'erreurs

```csharp
public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Domain exception occurred");
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = "An error occurred" });
        }
    }
}
```

## 🔧 Configuration dans Program.cs

N'oubliez pas de configurer :
- JWT Authentication
- FluentValidation
- DbContext
- Services (DI)
- Middleware global d'erreurs
