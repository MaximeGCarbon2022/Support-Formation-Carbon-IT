# 💼 Questions d'Entretien Technique - ASP.NET Core

## 📋 Organisation

Ce document contient **50+ questions techniques** organisées par niveau de difficulté pour préparer vos entretiens techniques et missions consultant ASP.NET Core.

### Niveaux de difficulté

- ⚪ **NÉCESSAIRE** - Concepts essentiels, attendus de tout développeur ASP.NET
- 🟢 **BASIQUE** - Fondamentaux ASP.NET Core
- 🟡 **INTERMÉDIAIRE** - Concepts avancés et patterns courants
- 🔴 **AVANCÉ** - Optimisations, architecture, problèmes complexes

---

## ⚪ NÉCESSAIRE (Concepts essentiels)

### Q1. Qu'est-ce qu'ASP.NET Core et en quoi diffère-t-il d'ASP.NET Framework ?

**Réponse attendue :**

**ASP.NET Core** est un framework web **open-source et cross-platform** pour construire des applications web modernes.

**Différences clés avec ASP.NET Framework :**

| Aspect | ASP.NET Core | ASP.NET Framework |
|--------|--------------|-------------------|
| **Plateforme** | Cross-platform (Windows, Linux, macOS) | Windows uniquement |
| **Open Source** | Oui (MIT License) | Partiellement |
| **Performance** | Très performant (optimisé) | Moins performant |
| **Déploiement** | Self-contained ou framework-dependent | Framework-dependent uniquement |
| **Modularité** | Packages NuGet modulaires | Monolithique |
| **Hébergement** | Kestrel, IIS, Nginx, Apache, Docker | IIS principalement |

---

### Q2. Qu'est-ce que le pattern MVC et comment fonctionne-t-il dans ASP.NET Core ?

**Réponse attendue :**

**MVC** = **Model-View-Controller**, un pattern architectural qui sépare l'application en 3 composants :

**1. Model (Modèle)** : Représente les données et la logique métier
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

**2. View (Vue)** : Interface utilisateur (fichiers .cshtml avec Razor)
```html
@model Product
<h1>@Model.Name</h1>
<p>Prix : @Model.Price €</p>
```

**3. Controller (Contrôleur)** : Gère les requêtes, coordonne Model et View
```csharp
public class ProductController : Controller
{
    public IActionResult Index()
    {
        var products = GetProducts(); // Logique métier
        return View(products); // Retourne la vue avec les données
    }
}
```

**Flux :** Requête → Routing → Controller → Model → View → Réponse

---

### Q3. Qu'est-ce que le Dependency Injection (DI) dans ASP.NET Core ?

**Réponse attendue :**

**Dependency Injection** est un pattern permettant d'injecter les dépendances d'une classe plutôt que de les créer manuellement. ASP.NET Core a un **DI container intégré**.

**Exemple :**

```csharp
// Interface
public interface IProductService
{
    List<Product> GetAll();
}

// Implémentation
public class ProductService : IProductService
{
    public List<Product> GetAll() => new List<Product>();
}

// Enregistrement dans Program.cs
builder.Services.AddScoped<IProductService, ProductService>();

// Injection dans un contrôleur
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService) // Injection
    {
        _productService = productService;
    }
}
```

**Avantages :** Testabilité, découplage, maintenance facilitée

---

### Q4. Quelle est la différence entre AddScoped, AddTransient et AddSingleton ?

**Réponse attendue :**

**Durée de vie des services injectés :**

| Lifetime | Durée de vie | Usage typique |
|----------|--------------|---------------|
| **Transient** | Nouvelle instance à chaque injection | Services légers, stateless |
| **Scoped** | Nouvelle instance par requête HTTP | DbContext, services liés à une requête |
| **Singleton** | Instance unique pour toute l'application | Configuration, cache, services coûteux |

**Exemple :**

```csharp
// Transient - nouvelle instance à chaque fois
builder.Services.AddTransient<IEmailService, EmailService>();

// Scoped - une instance par requête HTTP
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddDbContext<AppDbContext>(); // Scoped par défaut

// Singleton - instance unique
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
```

**⚠️ Attention :** Ne jamais injecter un Scoped service dans un Singleton (problème de captive dependency)

---

### Q5. Qu'est-ce que le Middleware dans ASP.NET Core ?

**Réponse attendue :**

**Middleware** = composants qui forment un **pipeline** pour traiter les requêtes/réponses HTTP.

**Pipeline :**
```
Requête → Middleware 1 → Middleware 2 → ... → Endpoint → ... → Middleware 2 → Middleware 1 → Réponse
```

**Exemple de configuration :**

```csharp
var app = builder.Build();

// Ordre important !
app.UseHttpsRedirection();     // 1. Redirection HTTPS
app.UseStaticFiles();          // 2. Fichiers statiques
app.UseRouting();              // 3. Routing
app.UseAuthentication();       // 4. Authentification
app.UseAuthorization();        // 5. Autorisation
app.MapControllers();          // 6. Endpoints

app.Run();
```

**Middleware personnalisé :**

```csharp
app.Use(async (context, next) =>
{
    // Code avant l'appel suivant
    await next(); // Appelle le middleware suivant
    // Code après l'appel suivant
});
```

**Usages :** Logging, authentification, gestion d'erreurs, compression, CORS

---

### Q6. Qu'est-ce que Routing dans ASP.NET Core ?

**Réponse attendue :**

**Routing** = système qui associe les URLs aux endpoints (contrôleurs/actions).

**Convention-based routing (MVC) :**

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// URL: /Products/Details/5
// Controller: ProductsController
// Action: Details(int id)
```

**Attribute routing (recommandé) :**

```csharp
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]                    // GET /api/products
    public IActionResult GetAll() { }

    [HttpGet("{id}")]           // GET /api/products/5
    public IActionResult Get(int id) { }

    [HttpPost]                   // POST /api/products
    public IActionResult Create([FromBody] Product product) { }
}
```

---

## 🟢 BASIQUE (Fondamentaux)

### Q7. Quelle est la différence entre ViewData, ViewBag et TempData ?

**Réponse attendue :**

Mécanismes pour passer des données du contrôleur à la vue :

| Type | Description | Durée de vie | Type |
|------|-------------|--------------|------|
| **ViewData** | Dictionnaire clé-valeur | Requête actuelle uniquement | `ViewDataDictionary` (cast requis) |
| **ViewBag** | Propriété dynamique | Requête actuelle uniquement | `dynamic` (pas de cast) |
| **TempData** | Stockage temporaire | Jusqu'à lecture (1 redirect) | `ITempDataDictionary` |

**Exemple :**

```csharp
// Controller
public IActionResult Index()
{
    ViewData["Message"] = "Hello"; // Dictionary
    ViewBag.Count = 10;            // Dynamic
    TempData["Success"] = "Sauvegardé !"; // Persiste après redirect

    return View();
}

// View
<p>@ViewData["Message"]</p>        <!-- Besoin du cast si complex type -->
<p>@ViewBag.Count</p>              <!-- Pas de cast -->
<p>@TempData["Success"]</p>        <!-- Disponible après redirect -->
```

**Usage TempData :** Messages de succès/erreur après un `RedirectToAction`

---

### Q8. Qu'est-ce qu'un ActionResult et quels sont les types courants ?

**Réponse attendue :**

**ActionResult** = type de retour d'une action de contrôleur représentant le résultat HTTP.

**Types courants :**

```csharp
// 1. ViewResult - Retourne une vue Razor
public IActionResult Index()
{
    return View(); // ou View("ViewName", model)
}

// 2. JsonResult - Retourne du JSON
public IActionResult GetData()
{
    return Json(new { Name = "John", Age = 30 });
}

// 3. RedirectToActionResult - Redirige vers une autre action
public IActionResult Create()
{
    return RedirectToAction("Index");
}

// 4. ContentResult - Retourne du texte brut
public IActionResult Text()
{
    return Content("Hello World");
}

// 5. StatusCodeResult - Retourne un code HTTP
public IActionResult NotFound()
{
    return NotFound(); // 404
    // Ou : StatusCode(404)
}

// 6. FileResult - Retourne un fichier
public IActionResult Download()
{
    return File(bytes, "application/pdf", "document.pdf");
}
```

**API Controllers :** Utiliser `ActionResult<T>` pour typage fort

```csharp
public ActionResult<Product> Get(int id)
{
    var product = _service.Find(id);
    if (product == null) return NotFound();
    return product; // Conversion implicite
}
```

---

### Q9. Comment fonctionne la validation de modèles (Model Validation) ?

**Réponse attendue :**

**Model Validation** = validation automatique des données avec des **Data Annotations**.

**Attributs courants :**

```csharp
public class Product
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [Range(0.01, 10000, ErrorMessage = "Prix entre 0.01 et 10000")]
    public decimal Price { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [RegularExpression(@"^\d{5}$", ErrorMessage = "Code postal invalide")]
    public string ZipCode { get; set; }
}
```

**Validation dans le contrôleur :**

```csharp
[HttpPost]
public IActionResult Create([FromBody] Product product)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState); // Retourne les erreurs
    }

    // Logique si valide
    _service.Add(product);
    return Ok(product);
}
```

**Dans les vues (MVC) :**

```html
@model Product

<form asp-action="Create">
    <div>
        <label asp-for="Name"></label>
        <input asp-for="Name" />
        <span asp-validation-for="Name"></span> <!-- Affiche l'erreur -->
    </div>
    <button type="submit">Envoyer</button>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

---

### Q10. Qu'est-ce que les Minimal APIs dans .NET 6+ ?

**Réponse attendue :**

**Minimal APIs** = approche simplifiée pour créer des APIs sans contrôleurs, introduite dans .NET 6.

**Exemple :**

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Définition directe des endpoints
app.MapGet("/", () => "Hello World!");

app.MapGet("/products", () => new[]
{
    new { Id = 1, Name = "Product 1" },
    new { Id = 2, Name = "Product 2" }
});

app.MapGet("/products/{id}", (int id) =>
    new { Id = id, Name = $"Product {id}" });

app.MapPost("/products", (Product product) =>
{
    // Logique de création
    return Results.Created($"/products/{product.Id}", product);
});

app.Run();
```

**Avantages :**
- Moins de boilerplate
- Performance légèrement meilleure
- Parfait pour microservices et APIs simples

**Quand utiliser :**
- ✅ APIs simples, microservices
- ❌ Applications complexes avec beaucoup de logique (préférer MVC)

---

### Q11. Comment gérer les erreurs globalement dans ASP.NET Core ?

**Réponse attendue :**

**3 approches principales :**

**1. Middleware d'exception (Development) :**

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Page détaillée
}
else
{
    app.UseExceptionHandler("/Error"); // Page custom
    app.UseHsts();
}
```

**2. Middleware personnalisé :**

```csharp
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new
        {
            Error = "Une erreur est survenue",
            Message = ex.Message
        });
    }
});
```

**3. Exception Filter (API Controllers) :**

```csharp
public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var result = new ObjectResult(new
        {
            Error = context.Exception.Message
        })
        {
            StatusCode = 500
        };

        context.Result = result;
        context.ExceptionHandled = true;
    }
}

// Enregistrement
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
```

---

### Q12. Qu'est-ce que Entity Framework Core et comment l'utiliser avec ASP.NET Core ?

**Réponse attendue :**

**EF Core** = ORM (Object-Relational Mapper) pour .NET permettant de manipuler des bases de données avec du code C#.

**Configuration :**

```csharp
// 1. Installation
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer

// 2. DbContext
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
}

// 3. Enregistrement dans Program.cs
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4. Création d'un service (bonne pratique)
public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
}

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }
}

// 5. Enregistrement du service
builder.Services.AddScoped<IProductService, ProductService>();

// 6. Injection dans le contrôleur (bonne pratique)
public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService; // ✅ Injecter un service, pas le DbContext
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync();
        return View(products);
    }
}
```

**⚠️ Important :** Ne jamais injecter `DbContext` directement dans un contrôleur. Utilisez toujours une couche service/repository pour :
- Respecter la séparation des responsabilités (SRP)
- Faciliter les tests unitaires (mock du service)
- Centraliser la logique d'accès aux données

**Migrations :**

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 🟡 INTERMÉDIAIRE (Concepts avancés)

### Q13. Expliquez le cycle de vie d'une requête ASP.NET Core (Request Pipeline).

**Réponse attendue :**

**Pipeline de requête :**

```
1. Requête HTTP arrive sur Kestrel (serveur web)
   ↓
2. Passe par les Middlewares (dans l'ordre défini)
   - UseHttpsRedirection
   - UseStaticFiles
   - UseRouting
   - UseAuthentication
   - UseAuthorization
   ↓
3. Routing identifie l'endpoint (contrôleur/action)
   ↓
4. Model Binding désérialise les données de la requête
   ↓
5. Model Validation valide les données
   ↓
6. Filters s'exécutent (Authorization → Action → Result)
   ↓
7. Action du contrôleur s'exécute
   ↓
8. ActionResult est retourné
   ↓
9. Filters post-exécution (Result → Exception)
   ↓
10. Réponse passe par les Middlewares (ordre inverse)
   ↓
11. Réponse HTTP retournée au client
```

**Schéma détaillé :**

```csharp
app.Use(async (context, next) => // Middleware 1 - début
{
    Console.WriteLine("Avant Middleware 2");
    await next(); // Passe au suivant
    Console.WriteLine("Après Middleware 2");
});

app.Use(async (context, next) => // Middleware 2 - début
{
    Console.WriteLine("Avant Endpoint");
    await next(); // Passe à l'endpoint
    Console.WriteLine("Après Endpoint");
});

app.MapGet("/", () => "Hello"); // Endpoint

// Output:
// Avant Middleware 2
// Avant Endpoint
// [Endpoint exécuté]
// Après Endpoint
// Après Middleware 2
```

---

### Q14. Qu'est-ce que les Filters dans ASP.NET Core MVC et quels sont les types ?

**Réponse attendue :**

**Filters** = attributs exécutés à différentes étapes du pipeline MVC.

**Types de filters (ordre d'exécution) :**

1. **Authorization Filters** - Vérifient l'autorisation
2. **Resource Filters** - Avant model binding
3. **Action Filters** - Avant/après l'exécution de l'action
4. **Exception Filters** - Gèrent les exceptions
5. **Result Filters** - Avant/après l'exécution du résultat

**Exemple - Action Filter :**

```csharp
public class LogActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"Avant: {context.ActionDescriptor.DisplayName}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"Après: {context.ActionDescriptor.DisplayName}");
    }
}

// Utilisation
[LogActionFilter]
public IActionResult Index()
{
    return View();
}

// Ou global
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogActionFilter>();
});
```

**Filters intégrés courants :**
- `[Authorize]` - Authorization Filter
- `[ValidateAntiForgeryToken]` - Action Filter
- `[ResponseCache]` - Result Filter

---

### Q15. Comment implémenter l'authentification JWT dans ASP.NET Core ?

**Réponse attendue :**

**JWT (JSON Web Token)** = standard pour sécuriser les APIs stateless.

**Configuration :**

```csharp
// 1. Installation
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

// 2. Configuration dans Program.cs
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your-issuer",
            ValidAudience = "your-audience",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("your-secret-key-min-32-chars"))
        };
    });

// 3. Activation
app.UseAuthentication();
app.UseAuthorization();

// 4. Génération du token
public string GenerateToken(User user)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, "Admin")
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret-key"));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "your-issuer",
        audience: "your-audience",
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
}

// 5. Protection des endpoints
[Authorize] // Nécessite un token valide
public IActionResult SecureEndpoint()
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    return Ok($"User ID: {userId}");
}

[Authorize(Roles = "Admin")] // Nécessite role Admin
public IActionResult AdminOnly()
{
    return Ok("Admin access");
}
```

---

### Q16. Qu'est-ce que CORS et comment le configurer dans ASP.NET Core ?

**Réponse attendue :**

**CORS (Cross-Origin Resource Sharing)** = mécanisme permettant à une application web d'accéder à des ressources d'un domaine différent.

**Problème :** Par défaut, les navigateurs bloquent les requêtes cross-origin pour sécurité.

**Configuration :**

```csharp
// 1. Configuration dans Program.cs
builder.Services.AddCors(options =>
{
    // Policy permissive (développement)
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

    // Policy restrictive (production)
    options.AddPolicy("AllowSpecific", policy =>
    {
        policy.WithOrigins("https://example.com", "https://app.example.com")
              .WithMethods("GET", "POST")
              .WithHeaders("Content-Type", "Authorization")
              .AllowCredentials(); // Cookies/Auth headers
    });
});

// 2. Activation globale
app.UseCors("AllowSpecific"); // Avant UseAuthorization

// 3. Ou par endpoint
app.MapGet("/api/data", () => "Data")
   .RequireCors("AllowAll");

// 4. Ou par contrôleur
[EnableCors("AllowSpecific")]
public class ProductsController : ControllerBase { }
```

**⚠️ Sécurité :** Ne jamais utiliser `AllowAnyOrigin()` en production

---

### Q17. Comment implémenter le caching dans ASP.NET Core ?

**Réponse attendue :**

**3 types de caching :**

**1. Response Caching (HTTP Cache) :**

```csharp
// Configuration
builder.Services.AddResponseCaching();
app.UseResponseCaching();

// Utilisation
[ResponseCache(Duration = 60)] // Cache 60 secondes
public IActionResult Index()
{
    return View();
}
```

**2. In-Memory Caching :**

```csharp
// Configuration
builder.Services.AddMemoryCache();

// Utilisation
public class ProductService
{
    private readonly IMemoryCache _cache;

    public ProductService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<Product> GetProduct(int id)
    {
        return await _cache.GetOrCreateAsync($"product_{id}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return await _repository.GetByIdAsync(id);
        });
    }
}
```

**3. Distributed Cache (Redis) :**

```csharp
// Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

// Utilisation
public class CacheService
{
    private readonly IDistributedCache _cache;

    public async Task<string> GetOrSetAsync(string key, Func<Task<string>> factory)
    {
        var cached = await _cache.GetStringAsync(key);
        if (cached != null) return cached;

        var value = await factory();
        await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        });

        return value;
    }
}
```

---

### Q18. Qu'est-ce que Blazor et quelles sont les différences entre Blazor Server et Blazor WebAssembly ?

**Réponse attendue :**

**Blazor** = framework pour construire des applications web interactives avec **C# au lieu de JavaScript**.

**Comparaison :**

| Aspect | Blazor Server | Blazor WebAssembly |
|--------|---------------|---------------------|
| **Exécution** | Côté serveur (SignalR) | Côté client (navigateur) |
| **Latence** | Dépend du réseau | Locale (rapide) |
| **Taille téléchargement** | Petite (HTML/CSS) | Grande (~2-3 MB .NET runtime) |
| **Offline** | ❌ Nécessite connexion | ✅ Fonctionne offline (PWA) |
| **Débogage** | ✅ Facile | Plus complexe |
| **Scalabilité** | Limite (connexions SignalR) | ✅ Excellente (client-side) |
| **Sécurité** | Code côté serveur (sécurisé) | Code téléchargé (visible) |

**Exemple Blazor Server :**

```csharp
// Counter.razor
@page "/counter"

<h1>Counter</h1>
<p>Current count: @currentCount</p>
<button @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++; // Exécuté sur le serveur
    }
}
```

**Quand utiliser :**
- **Blazor Server** : Applications internes, dashboards, faible latence réseau
- **Blazor WASM** : Applications publiques, PWA, offline-first

---

## 📚 Ressources complémentaires

- [Documentation officielle ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [.NET 9 What's New](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview)
- [ASP.NET Core Performance Best Practices](https://learn.microsoft.com/en-us/aspnet/core/performance/performance-best-practices)
- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)

---

**Bon courage pour vos entretiens ! 💪**
