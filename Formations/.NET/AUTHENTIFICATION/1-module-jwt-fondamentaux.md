# Montée en compétence auto-rythmée - Authentification JWT

## 📚 Module 1 : JWT - Les fondamentaux

### Objectif

**Comprendre comment fonctionne l'authentification JWT en l'implémentant soi-même.**

Ce module vous apprend les bases de l'authentification JWT en créant votre propre système. Vous comprendrez les mécanismes internes avant d'utiliser des solutions professionnelles comme Auth0 (Module 2).

### Prérequis

- Module C# Fondamentaux ✅
- Bases d'ASP.NET Core
- Comprendre ce qu'est une API REST

---

## 🔑 Concepts fondamentaux

### Authentification vs Autorisation

**Authentification :** "Qui êtes-vous ?"

- Je suis jean.dupont@email.com
- Prouvé par mot de passe, token, biométrie

**Autorisation :** "Que pouvez-vous faire ?"

- Jean peut voir SES données
- Jean ne peut pas voir les données de Paul
- Admin peut voir toutes les données

### Qu'est-ce qu'un JWT ?

**JWT (JSON Web Token)** : Token sécurisé contenant des informations (claims) sur l'utilisateur.

**Structure d'un JWT :**

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

**Décomposé en 3 parties :**

1. **Header** : Algorithme de signature (HS256, RS256)
2. **Payload** : Claims (sub, name, email, roles, exp)
3. **Signature** : Garantit que le token n'a pas été modifié

**Visualiser un JWT :** [https://jwt.io/](https://jwt.io/)

### Comment ça marche ?

```
1. User se connecte avec email/password
2. API vérifie les credentials
3. API génère un JWT contenant l'ID user, email, rôle
4. User stocke le token
5. User envoie le token dans chaque requête (Authorization: Bearer <token>)
6. API valide le token et autorise l'accès
```

### Claims (Revendications)

Les claims sont des informations stockées dans le JWT :

```csharp
new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // ID utilisateur
new Claim(ClaimTypes.Email, user.Email)                  // Email
new Claim(ClaimTypes.Role, user.Role)                    // Rôle (Admin, User)
```

---

## 📖 Ressources théoriques

### Documentation

- [Qu'est-ce qu'un JWT ?](https://jwt.io/introduction) - Introduction simple
- [ASP.NET Core Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/)
- [JWT Bearer Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authentication)

### Sécurité

- [BCrypt](https://en.wikipedia.org/wiki/Bcrypt) - Pourquoi hasher les mots de passe
- [OWASP API Security](https://owasp.org/www-project-api-security/) - Bonnes pratiques

---

## 💻 Exercice pratique : TaskAPI avec JWT

### Vue d'ensemble du projet

**Vous allez créer une API de gestion de tâches sécurisée avec JWT.**

**Fonctionnalités :**

- Register : Créer un compte utilisateur
- Login : Se connecter et obtenir un JWT
- Endpoints protégés avec [Authorize]
- Rôles : User, Admin
- Chaque utilisateur ne voit que ses tâches

**Technologies :**

- .NET 9 Web API
- JWT Bearer Authentication
- BCrypt pour hasher les mots de passe
- Entity Framework Core (InMemory)

---

### Setup du projet

```bash
# Créer le projet
dotnet new webapi -n TaskAPI
cd TaskAPI

# Packages nécessaires
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package BCrypt.Net-Next
dotnet add package System.IdentityModel.Tokens.Jwt
```

---

### Structure du projet

```
TaskAPI/
├── Models/
│   ├── User.cs
│   └── TaskItem.cs
├── Data/
│   └── AppDbContext.cs
├── Services/
│   └── AuthService.cs
├── Controllers/
│   ├── AuthController.cs
│   ├── TasksController.cs
│   └── AdminController.cs
├── Extensions/
│   └── ClaimsPrincipalExtensions.cs
├── Program.cs
└── appsettings.json
```

---

### Implémentation

#### 📁 Models/User.cs

```csharp
namespace TaskAPI.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "User"; // User, Admin
    public List<TaskItem> Tasks { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

#### 📁 Models/TaskItem.cs

```csharp
namespace TaskAPI.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

#### 📁 Data/AppDbContext.cs

```csharp
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;

namespace TaskAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);
    }
}
```

#### 📁 Services/AuthService.cs

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskAPI.Data;
using TaskAPI.Models;

namespace TaskAPI.Services;

public interface IAuthService
{
    Task<User> RegisterAsync(string email, string password);
    Task<string> LoginAsync(string email, string password);
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<User> RegisterAsync(string email, string password)
    {
        // Vérifier si l'utilisateur existe déjà
        if (await _context.Users.AnyAsync(u => u.Email == email))
        {
            throw new InvalidOperationException("Cet email est déjà utilisé");
        }

        var user = new User
        {
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = "User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Email ou mot de passe incorrect");
        }

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var key = _configuration["Jwt:Key"]!;
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

#### 📁 Extensions/ClaimsPrincipalExtensions.cs

```csharp
using System.Security.Claims;

namespace TaskAPI.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdClaim ?? "0");
    }

    public static string GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    }

    public static string GetRole(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Role)?.Value ?? "User";
    }
}
```

#### 📁 appsettings.json

```json
{
  "Jwt": {
    "Key": "VotreCleSecreteTresTresLongueMinimum32Caracteres!",
    "Issuer": "TaskAPI",
    "Audience": "TaskAPI"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**⚠️ IMPORTANT :** En production, ne JAMAIS mettre la clé JWT dans appsettings.json. Utilisez les variables d'environnement ou Azure Key Vault.

#### 📁 Program.cs

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskAPI.Data;
using TaskAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration JWT depuis appsettings.json
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// Base de données InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskDB"));

// Service d'authentification
builder.Services.AddScoped<IAuthService, AuthService>();

// Configuration JWT Bearer Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ORDRE IMPORTANT : Authentication puis Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
```

#### 📁 Controllers/AuthController.cs

```csharp
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskAPI.Services;

namespace TaskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = await _authService.RegisterAsync(request.Email, request.Password);
            return Ok(new { Message = "Compte créé avec succès", UserId = user.Id });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);
            return Ok(new
            {
                Token = token,
                Type = "Bearer",
                ExpiresIn = 3600
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
    }
}

public class RegisterRequest
{
    [Required(ErrorMessage = "L'email est obligatoire")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le mot de passe est obligatoire")]
    [MinLength(6, ErrorMessage = "Le mot de passe doit faire au moins 6 caractères")]
    public string Password { get; set; } = string.Empty;
}

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
```

#### 📁 Controllers/TasksController.cs

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TaskAPI.Data;
using TaskAPI.Extensions;
using TaskAPI.Models;

namespace TaskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Tous les endpoints nécessitent un token
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyTasks()
    {
        var userId = User.GetUserId();

        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId)
            .Select(t => new
            {
                t.Id,
                t.Title,
                t.Description,
                t.IsCompleted,
                t.CreatedAt
            })
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var userId = User.GetUserId();

        var task = await _context.Tasks
            .Where(t => t.Id == id && t.UserId == userId)
            .FirstOrDefaultAsync();

        if (task == null)
        {
            return NotFound(new { Error = "Tâche non trouvée" });
        }

        return Ok(task);
    }

    // TODO: Implémenter POST /api/tasks - Créer une nouvelle tâche
    // Utiliser CreateTaskRequest avec validation
    // Assigner automatiquement UserId du user connecté
    // Retourner 201 Created avec la tâche créée

    // TODO: Implémenter PUT /api/tasks/{id} - Modifier MA tâche
    // Vérifier que la tâche existe et appartient au user
    // Utiliser UpdateTaskRequest avec validation
    // Retourner 404 si pas trouvée ou pas la sienne

    // TODO: Implémenter DELETE /api/tasks/{id} - Supprimer MA tâche
    // Vérifier que la tâche existe et appartient au user
    // Supprimer de la base
    // Retourner 404 si pas trouvée
}

public class CreateTaskRequest
{
    [Required(ErrorMessage = "Le titre est obligatoire")]
    [MaxLength(100, ErrorMessage = "Le titre ne peut pas dépasser 100 caractères")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    public string Description { get; set; } = string.Empty;
}

public class UpdateTaskRequest
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}
```

#### 📁 Controllers/AdminController.cs

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Data;

namespace TaskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // Réservé aux admins
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    // TODO: Implémenter GET /api/admin/tasks - Voir TOUTES les tâches
    // Inclure les infos du propriétaire (User.Email)
    // Retourner TaskId, Title, IsCompleted, OwnerEmail, CreatedAt

    // TODO: Implémenter GET /api/admin/users - Voir tous les utilisateurs
    // Retourner Id, Email, Role, TaskCount

    // TODO: Implémenter PUT /api/admin/users/{id}/role - Changer le rôle
    // Valider que le rôle est "User" ou "Admin"
    // Retourner 404 si user introuvable
}
```

---

### Tests et validation

#### Tests avec Swagger

**1. Register un nouveau user :**

```json
POST /api/auth/register
{
  "email": "user@example.com",
  "password": "password123"
}
→ 200 OK
```

**2. Login :**

```json
POST /api/auth/login
{
  "email": "user@example.com",
  "password": "password123"
}
→ 200 OK + Token JWT
```

**3. Copier le token dans Swagger :**

- Cliquer sur "Authorize" en haut
- Entrer : `Bearer VOTRE_TOKEN`
- Cliquer "Authorize"

**4. Tester endpoint protégé :**

```
GET /api/tasks
→ 200 OK (avec token)
→ 401 Unauthorized (sans token)
```

#### Créer un admin manuellement

Pour tester les endpoints admin, créez un admin en base :

1. Lancez l'app et mettez un breakpoint après `var app = builder.Build();`
2. Dans la console immediate :

```csharp
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Users.Add(new User
    {
        Email = "admin@example.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
        Role = "Admin"
    });
    context.SaveChanges();
}
```

3. Connectez-vous avec admin@example.com / admin123

#### Checklist de validation

- [ ] Register avec email invalide → 400
- [ ] Register avec email déjà utilisé → 400
- [ ] Login avec mauvais password → 401
- [ ] Login réussi → token retourné
- [ ] Endpoint protégé sans token → 401
- [ ] Endpoint protégé avec token → 200
- [ ] User voit uniquement ses tâches
- [ ] Admin peut accéder aux endpoints admin
- [ ] User ne peut PAS accéder aux endpoints admin → 403

---

## ✅ Tests unitaires

### Configuration des tests

```bash
dotnet new xunit -n TaskAPI.Tests
cd TaskAPI.Tests
dotnet add package Microsoft.AspNetCore.Mvc.Testing
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add reference ../TaskAPI/TaskAPI.csproj
```

### Exemple de tests

```csharp
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TaskAPI.Tests;

public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_ValidUser_ReturnsOk()
    {
        // Arrange
        var request = new { Email = "test@example.com", Password = "password123" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_InvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var request = new { Email = "wrong@example.com", Password = "wrongpassword" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    // TODO: Ajouter tests pour :
    // - Register avec email déjà utilisé
    // - Login réussi retourne un token
    // - Endpoint protégé sans token retourne 401
    // - Endpoint protégé avec token retourne 200
}
```

---

## 🎯 Questions d'entretien

### NÉCESSAIRE

**1. Quelle est la différence entre authentification et autorisation ?**

<details>
<summary>Voir la réponse</summary>

- **Authentification** : Vérifier l'identité de l'utilisateur ("Qui êtes-vous ?")
  - Exemple : Login avec email/password, vérification du JWT
- **Autorisation** : Vérifier les permissions de l'utilisateur ("Que pouvez-vous faire ?")
  - Exemple : [Authorize(Roles = "Admin")]
</details>

**2. Qu'est-ce qu'un JWT et comment est-il structuré ?**

<details>
<summary>Voir la réponse</summary>

**JWT (JSON Web Token)** : Token sécurisé contenant des claims (informations) sur l'utilisateur.

**Structure :** `Header.Payload.Signature`

- **Header** : Algorithme de signature (HS256)
- **Payload** : Claims (sub, email, role, exp)
- **Signature** : HMAC(header + payload + secret key)

**Utilité :**

- Authentifier les requêtes API
- Transporter des informations (email, rôle, ID)
- Vérifiable sans base de données (signature cryptographique)
</details>

**3. Pourquoi hasher les mots de passe avec BCrypt ?**

<details>
<summary>Voir la réponse</summary>

**BCrypt** : Algorithme de hashing sécurisé pour les mots de passe

**Pourquoi ?**

- Les mots de passe ne sont JAMAIS stockés en clair
- BCrypt ajoute un "salt" aléatoire pour chaque hash
- Même password = hash différent à chaque fois
- Lent volontairement (ralentit les attaques brute-force)

**Vérification :**

```csharp
// Hashing
var hash = BCrypt.Net.BCrypt.HashPassword("password123");

// Vérification
bool isValid = BCrypt.Net.BCrypt.Verify("password123", hash); // true
```

</details>

**4. Comment protéger un endpoint dans ASP.NET Core ?**

<details>
<summary>Voir la réponse</summary>

```csharp
[Authorize] // Nécessite un token valide
public IActionResult GetData() { }

[Authorize(Roles = "Admin")] // Nécessite rôle Admin
public IActionResult AdminOnly() { }

[AllowAnonymous] // Accessible sans token (même si controller a [Authorize])
public IActionResult Public() { }
```

</details>

**5. Comment récupérer l'ID de l'utilisateur connecté ?**

<details>
<summary>Voir la réponse</summary>

```csharp
// Via extension method (recommandé)
var userId = User.GetUserId();

// Via claims directement
var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
var userId = int.Parse(userIdClaim ?? "0");

// Email
var email = User.FindFirst(ClaimTypes.Email)?.Value;

// Rôle
var role = User.FindFirst(ClaimTypes.Role)?.Value;
```

</details>

---

### BASIQUE

**6. Comment implémenter "Je ne vois que mes données" ?**

<details>
<summary>Voir la réponse</summary>

```csharp
[HttpGet]
public async Task<IActionResult> GetMyTasks()
{
    var userId = User.GetUserId();

    // Filtrer par userId TOUJOURS
    var tasks = await _context.Tasks
        .Where(t => t.UserId == userId) // Sécurité critique
        .ToListAsync();

    return Ok(tasks);
}
```

**Principe :** TOUJOURS filtrer par `userId` dans les requêtes pour éviter qu'un user accède aux données d'un autre.

</details>

**7. Quelle est la différence entre [Authorize] et [Authorize(Roles = "Admin")] ?**

<details>
<summary>Voir la réponse</summary>

**[Authorize]** : N'importe quel utilisateur connecté (token valide)

**[Authorize(Roles = "Admin")]** : Token valide ET rôle "Admin"

**Multiple rôles :** `[Authorize(Roles = "Admin,Manager")]` → Admin OU Manager

</details>

**8. Où doit-on appeler UseAuthentication() et UseAuthorization() ?**

<details>
<summary>Voir la réponse</summary>

**Ordre CRITIQUE dans Program.cs :**

```csharp
app.UseAuthentication(); // 1. Vérifier le token et charger les claims
app.UseAuthorization();  // 2. Vérifier les permissions (rôles, policies)
app.MapControllers();    // 3. Router les requêtes
```

**Si inversé ou oublié :** Les endpoints protégés retournent toujours 401.

</details>

**9. Comment gérer le cas où un user tente d'accéder aux données d'un autre user ?**

<details>
<summary>Voir la réponse</summary>

**Retourner 404 (pas 403) pour ne pas révéler l'existence de la ressource :**

```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetTask(int id)
{
    var userId = User.GetUserId();

    var task = await _context.Tasks
        .Where(t => t.Id == id && t.UserId == userId)
        .FirstOrDefaultAsync();

    if (task == null)
    {
        return NotFound(); // 404 (ne pas dire si elle existe ou si pas accès)
    }

    return Ok(task);
}
```

**Pourquoi 404 ?** Éviter de révéler qu'une ressource existe.

</details>

**10. Quels sont les codes HTTP appropriés pour l'authentification/autorisation ?**

<details>
<summary>Voir la réponse</summary>

**401 Unauthorized** : Pas de token ou token invalide

- "Vous devez être connecté"

**403 Forbidden** : Token valide mais pas les permissions

- "Vous n'avez pas le droit d'accéder à cette ressource"

**404 Not Found** : Ressource inexistante OU pas accès (préférable pour sécurité)

- "Ressource non trouvée" (sans révéler si elle existe)

</details>

---

### INTERMÉDIAIRE

**11. Comment fonctionne la validation du JWT ?**

<details>
<summary>Voir la réponse</summary>

**Étapes de validation dans ASP.NET Core :**

1. Extraire le token du header `Authorization: Bearer <token>`
2. Décoder le JWT (Header, Payload, Signature)
3. Vérifier la signature avec la clé secrète (IssuerSigningKey)
4. Vérifier l'expiration (`exp` claim)
5. Vérifier l'Issuer (émetteur du token)
6. Vérifier l'Audience (destinataire du token)
7. Extraire les claims (sub, email, role) → User.Claims

**Configuration :**

```csharp
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = true,
        ValidIssuer = "TaskAPI",
        ValidateAudience = true,
        ValidAudience = "TaskAPI",
        ValidateLifetime = true
    };
});
```

</details>

**12. Pourquoi ne jamais stocker la clé JWT dans le code source ?**

<details>
<summary>Voir la réponse</summary>

**Problème :**

- Si la clé est dans le code source → visible sur Git
- Quelqu'un peut générer des tokens valides
- Compromettre toute la sécurité de l'app

**Solution :**

- Variables d'environnement
- Azure Key Vault
- AWS Secrets Manager
- appsettings.json pour dev uniquement (pas commité en production)

```csharp
// ❌ JAMAIS
var key = "MaCleSecrete123";

// ✅ BIEN
var key = builder.Configuration["Jwt:Key"];
```

</details>

**13. Quelle est la différence entre SymmetricSecurityKey et AsymmetricSecurityKey ?**

<details>
<summary>Voir la réponse</summary>

**SymmetricSecurityKey (une seule clé) :**

- Même clé pour signer ET vérifier
- HMAC-SHA256 (HS256)
- Simple, rapide
- Nécessite de partager la clé secrète

**AsymmetricSecurityKey (paire de clés) :**

- Clé privée pour signer
- Clé publique pour vérifier
- RSA-SHA256 (RS256)
- Plus sécurisé (clé publique peut être partagée)
- Utilisé par Auth0, Azure AD

**Quand utiliser quoi ?**

- **Symmetric** : API simple, single backend
- **Asymmetric** : Microservices, plusieurs backends, Auth0

</details>

**14. Comment implémenter un système de refresh token ?**

<details>
<summary>Voir la réponse</summary>

**Concept :**

- **Access Token** : Courte durée (1h)
- **Refresh Token** : Longue durée (7 jours), stocké sécurisé

**Flow :**

1. Login → Retourner Access Token + Refresh Token
2. Access Token expire → Utiliser Refresh Token
3. API génère un nouvel Access Token
4. Continuer sans re-login

**Stockage Refresh Token :**

- Base de données (table RefreshTokens)
- Lié à l'utilisateur
- Expiration configurable
- Révocable (logout)

**⚠️ Note :** Refresh tokens sont complexes. Le Module 2 (Auth0) les gère automatiquement.

</details>

**15. Comment tester les endpoints protégés en tests d'intégration ?**

<details>
<summary>Voir la réponse</summary>

**Option 1 : Générer un vrai token dans les tests**

```csharp
private async Task<string> GetValidTokenAsync()
{
    var registerRequest = new { Email = "test@example.com", Password = "password123" };
    await _client.PostAsJsonAsync("/api/auth/register", registerRequest);

    var loginRequest = new { Email = "test@example.com", Password = "password123" };
    var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);
    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

    return result.Token;
}

[Fact]
public async Task GetTasks_WithToken_ReturnsOk()
{
    var token = await GetValidTokenAsync();
    _client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", token);

    var response = await _client.GetAsync("/api/tasks");

    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
}
```

**Option 2 : Mocker l'authentification**

Plus complexe, voir documentation Microsoft.

</details>

---

## 💪 Exercices bonus

### Exercice 1 : Implémenter les TODOs

Complétez les méthodes POST, PUT, DELETE dans TasksController et AdminController.

### Exercice 2 : Ajouter une validation forte du mot de passe

**Règles :**

- Au moins 8 caractères
- Au moins 1 majuscule
- Au moins 1 chiffre
- Au moins 1 caractère spécial

### Exercice 3 : Endpoint "Mes statistiques"

**Endpoint :** `GET /api/tasks/stats`

**Retourner :**

```json
{
  "totalTasks": 15,
  "completedTasks": 8,
  "pendingTasks": 7,
  "completionRate": 53.33
}
```

### Exercice 4 : Filtrage et tri des tâches

**Endpoint :** `GET /api/tasks?completed=true&sortBy=createdAt&order=desc`

---

## 📚 Ressources complémentaires

### Documentation officielle

- [ASP.NET Core Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/)
- [JWT Bearer Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authentication)
- [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net)

### Outils

- [JWT.io](https://jwt.io/) - Décoder et debugger vos JWT
- [Postman](https://www.postman.com/) - Tester vos APIs

---

## ✅ Points clés à retenir

**Sécurité :**

- ✅ JAMAIS stocker les mots de passe en clair → BCrypt
- ✅ JAMAIS commiter la clé JWT dans Git
- ✅ Toujours filtrer par userId (WHERE userId = currentUserId)
- ✅ Retourner 404 au lieu de 403 pour masquer l'existence des ressources
- ✅ Valider les données d'entrée (DataAnnotations)

**JWT :**

- ✅ Structure : Header.Payload.Signature
- ✅ Claims = informations dans le token (sub, email, role)
- ✅ Expiration configurable (1h recommandé pour access token)
- ✅ Configuration dans appsettings.json (dev) ou variables env (prod)

**ASP.NET Core :**

- ✅ Ordre IMPORTANT : UseAuthentication() puis UseAuthorization()
- ✅ [Authorize] sur controllers/actions à protéger
- ✅ Extensions methods pour récupérer userId/email
- ✅ Tests avec tokens valides et invalides

**🎯 Vous comprenez maintenant comment fonctionne JWT !**

**➡️ Prochaine étape : Module 2 - Auth0 pour la production**
