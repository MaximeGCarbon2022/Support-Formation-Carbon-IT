# Montée en compétence auto-rythmée - Auth0

## 📚 Module 2 : Auth0 - Production Ready

### Objectif

**Utiliser Auth0 pour gérer l'authentification en production comme dans les entreprises.**

Ce module vous apprend à intégrer Auth0, la solution industry-standard, pour sécuriser vos APIs professionnellement. Vous verrez aussi les concepts avancés : refresh tokens, SSO, et gestion des tenants.

### Prérequis

- Module 1 : JWT - Les fondamentaux ✅
- Comprendre JWT, claims, [Authorize]

---

## 🎯 Pourquoi Auth0 en entreprise ?

### Le problème avec l'authentification "faite maison"

**Module 1 vous a appris les bases, mais en production :**

- ❌ Gérer le stockage sécurisé des mots de passe (BCrypt, salting, etc.)
- ❌ Implémenter la rotation des tokens
- ❌ Gérer la récupération de mot de passe
- ❌ Implémenter 2FA (Two-Factor Authentication)
- ❌ Maintenir la conformité RGPD/OWASP
- ❌ Gérer SSO (Single Sign-On) entre plusieurs apps
- ❌ Refresh tokens sécurisés
- ❌ Monitoring et alertes de sécurité
- ❌ Risques de failles de sécurité

### Avec Auth0

**Tout est géré pour vous :**

- ✅ Conformité RGPD/OWASP out-of-the-box
- ✅ 2FA inclus (SMS, Email, Authenticator apps)
- ✅ SSO facile à configurer
- ✅ Refresh tokens automatiques avec rotation
- ✅ Passwordless login (magic links, biométrie)
- ✅ Social login (Google, Microsoft, GitHub)
- ✅ Dashboard d'administration des utilisateurs
- ✅ Monitoring et logs d'authentification
- ✅ Concentrez-vous sur votre métier

### Quand utiliser Auth0 ?

**Auth0 (RECOMMANDÉ) :**

- Applications professionnelles en production
- Besoin de SSO entre plusieurs applications (.NET, React, Angular)
- Conformité et sécurité critiques
- Équipe sans expertise sécurité approfondie
- Gain de temps = gain d'argent

**Authentification custom (Module 1) :**

- Apprentissage académique (vous l'avez fait ✅)
- Contraintes très spécifiques impossibles avec Auth0
- Systèmes legacy (mais migrer vers Auth0 est souvent mieux)

---

## 🔑 Concepts Auth0

### Tenant

**Tenant** : Votre "instance" Auth0 (ex: `dev-xyz.eu.auth0.com`)

- Isolé des autres tenants
- Contient vos users, apps, APIs
- Généralement : 1 tenant dev + 1 tenant prod

### Application vs API dans Auth0

**Application :** Frontend qui demande l'authentification

- SPA (React, Angular)
- Mobile App (iOS, Android)
- Web App (.NET MVC)

**API :** Backend qui valide les tokens

- .NET Web API (ce que nous faisons)
- Node.js API
- Python API

**Notre cas :** On créera une API Auth0 pour notre backend .NET.

### OIDC (OpenID Connect)

**OIDC** : Protocole d'authentification construit sur OAuth 2.0

**OAuth 2.0 :** Autorisation (permissions)
**OIDC :** OAuth 2.0 + Identité utilisateur (authentification)

**Flow OIDC simplifié :**

```
1. User clique "Se connecter" dans le frontend
2. Redirection vers Auth0 (login page)
3. User entre email/password (ou Google, etc.)
4. Auth0 génère un ID Token (JWT)
5. Frontend reçoit le token
6. Frontend envoie le token à l'API .NET
7. API .NET valide le token avec Auth0
8. User est authentifié
```

### Access Token vs ID Token vs Refresh Token

**ID Token (OIDC) :**

- Prouve l'identité de l'utilisateur
- Contient : sub (user ID), email, name
- Destiné au frontend

**Access Token (OAuth 2.0) :**

- Permet d'accéder aux ressources (API)
- Contient : permissions, scopes
- Courte durée (1h)
- Destiné au backend

**Refresh Token :**

- Permet d'obtenir un nouvel Access Token
- Longue durée (7-30 jours)
- Stocké sécurisé (HttpOnly cookie, secure storage)
- Révocable

**Notre API .NET valide l'Access Token.**

### SSO (Single Sign-On)

**Le problème :**

- App1 (.NET) : connexion avec email/password
- App2 (React) : re-connexion avec email/password
- App3 (Angular) : encore re-connexion...

**Avec SSO Auth0 :**

- Connexion une seule fois à Auth0
- Accès automatique à toutes vos apps
- Déconnexion globale possible
- Exemple : Google Workspace (Gmail, Drive, Calendar)

**Auth0 gère le SSO entre vos applications .NET, React, Angular, mobile, etc.**

---

## 📖 Ressources théoriques

### Documentation Auth0

- [Auth0 Documentation](https://auth0.com/docs) - Guide complet
- [Auth0 ASP.NET Core API](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi) - Démarrage rapide
- [Auth0 APIs](https://auth0.com/docs/secure/tokens/access-tokens) - Sécuriser une API

### Concepts OIDC et OAuth

- [OIDC Explained](https://openid.net/connect/) - Protocole OpenID Connect
- [OAuth 2.0](https://oauth.net/2/) - Standard d'autorisation
- [Access Token vs ID Token](https://auth0.com/blog/id-token-access-token-what-is-the-difference/)
- [Refresh Tokens](https://auth0.com/blog/refresh-tokens-what-are-they-and-when-to-use-them/)

### Vidéos

- [Auth0 YouTube Channel](https://www.youtube.com/@auth0) - Tutoriels vidéo
- [What is OAuth 2.0?](https://www.youtube.com/watch?v=996OiexHze0) - Expliqué simplement

---

## 💻 Exercice pratique : Migration vers Auth0

### Vue d'ensemble

**Vous allez migrer le projet TaskAPI du Module 1 vers Auth0.**

**Changements :**

- ❌ Supprimer AuthService (register/login custom)
- ❌ Supprimer BCrypt, génération JWT manuelle
- ✅ Configurer Auth0 pour valider les tokens
- ✅ Utiliser les users Auth0 au lieu de la table User locale
- ✅ Tester avec des tokens Auth0

**Avantage :** Moins de code, plus de sécurité !

---

### Setup Auth0

#### 1. Créer un compte Auth0 gratuit

1. Allez sur [https://auth0.com/](https://auth0.com/)
2. Créez un compte gratuit
3. Choisissez une région (EU pour RGPD si Europe)

**Résultat :** Vous obtenez un tenant (ex: `dev-xyz.eu.auth0.com`)

#### 2. Créer une API dans Auth0

1. Dashboard Auth0 → **Applications** → **APIs**
2. Cliquer "Create API"
3. Remplir :
   - **Name** : TaskAPI
   - **Identifier** : `https://taskapi` (URL fictive, juste un ID unique)
   - **Signing Algorithm** : RS256 (recommandé)
4. Cliquer "Create"

**Notez ces informations :**

- **Domain** : `dev-xyz.eu.auth0.com`
- **API Identifier (Audience)** : `https://taskapi`

**📖 Guide détaillé :** [Auth0 Create API](https://auth0.com/docs/get-started/auth0-overview/create-apis)

#### 3. Créer un test user

1. Dashboard Auth0 → **User Management** → **Users**
2. Cliquer "Create User"
3. Email : `user@example.com`, Password : `Password123!`
4. Créer un deuxième user pour l'admin : `admin@example.com`

#### 4. Ajouter les rôles (optionnel mais recommandé)

**Option A : Utiliser app_metadata (simple)**

1. Allez dans le user `admin@example.com`
2. Onglet "Metadata"
3. App Metadata :

```json
{
  "role": "Admin"
}
```

4. Pour le user normal, mettre `"role": "User"`

**Option B : Utiliser les Roles Auth0 (professionnel)**

1. Dashboard → **User Management** → **Roles**
2. Créer les rôles "Admin" et "User"
3. Assigner les rôles aux users
4. Configurer une Action/Rule pour ajouter le rôle au token

**Pour ce module, utilisez l'Option A (plus simple).**

---

### Modification du projet TaskAPI

#### 📁 appsettings.json

Remplacez la section JWT par Auth0 :

```json
{
  "Auth0": {
    "Domain": "dev-xyz.eu.auth0.com",
    "Audience": "https://taskapi"
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

**⚠️ Remplacez `dev-xyz` par votre domain Auth0.**

#### 📁 Program.cs

Remplacez la configuration JWT par Auth0 :

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuration Auth0
var domain = builder.Configuration["Auth0:Domain"];
var audience = builder.Configuration["Auth0:Audience"];

// Base de données InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskDB"));

// ❌ Supprimer AuthService (plus besoin)
// builder.Services.AddScoped<IAuthService, AuthService>();

// Configuration JWT Bearer avec Auth0
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{domain}/";
        options.Audience = audience;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            NameClaimType = "sub", // Auth0 utilise "sub" pour l'ID user
            RoleClaimType = "https://taskapi/roles" // Custom claim pour les rôles
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

#### 📁 Models/User.cs

Modifiez pour stocker l'ID Auth0 :

```csharp
namespace TaskAPI.Models;

public class User
{
    public int Id { get; set; }
    public string Auth0UserId { get; set; } = string.Empty; // "auth0|123456"
    public string Email { get; set; } = string.Empty;
    public List<TaskItem> Tasks { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

**Note :** Le rôle est maintenant dans le token Auth0, pas en base.

#### 📁 Extensions/ClaimsPrincipalExtensions.cs

Mise à jour pour Auth0 :

```csharp
using System.Security.Claims;

namespace TaskAPI.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetAuth0UserId(this ClaimsPrincipal user)
    {
        // Auth0 met l'ID dans le claim "sub"
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value
            ?? throw new UnauthorizedAccessException("User ID not found in token");
    }

    public static string GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Email)?.Value
            ?? user.FindFirst("email")?.Value
            ?? string.Empty;
    }

    public static string GetRole(this ClaimsPrincipal user)
    {
        // Auth0 custom claim
        return user.FindFirst("https://taskapi/roles")?.Value ?? "User";
    }
}
```

#### 📁 Controllers/TasksController.cs

Mise à jour pour Auth0 :

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
[Authorize]
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
        var auth0UserId = User.GetAuth0UserId();

        // Trouver ou créer le user en base
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Auth0UserId == auth0UserId);
        if (user == null)
        {
            user = new User
            {
                Auth0UserId = auth0UserId,
                Email = User.GetEmail()
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        var tasks = await _context.Tasks
            .Where(t => t.UserId == user.Id)
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

    // TODO: Adapter les autres endpoints (GET by id, POST, PUT, DELETE)
    // Utiliser User.GetAuth0UserId() au lieu de User.GetUserId()
    // Trouver le user en base avec Auth0UserId
}

public class CreateTaskRequest
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
}
```

#### ❌ Supprimer AuthController.cs

**Plus besoin** : Auth0 gère le register/login pour vous !

Les users s'inscrivent via l'interface Auth0, ou vous les créez dans le Dashboard.

---

### Obtenir un token Auth0 pour tester

#### Méthode 1 : Auth0 Test Token (rapide)

1. Dashboard Auth0 → **Applications** → **APIs** → **TaskAPI**
2. Onglet "Test"
3. Copier le token de test

**⚠️ Ce token expire rapidement.**

#### Méthode 2 : Postman avec OAuth 2.0 (recommandé)

1. Créer une "Application" dans Auth0 :
   - Dashboard → **Applications** → **Applications** → "Create Application"
   - Name : "TaskAPI Postman"
   - Type : "Machine to Machine"
   - Autoriser l'accès à l'API "TaskAPI"
2. Notez : **Client ID** et **Client Secret**
3. Dans Postman :
   - Authorization → Type : OAuth 2.0
   - Token URL : `https://dev-xyz.eu.auth0.com/oauth/token`
   - Client ID : `votre_client_id`
   - Client Secret : `votre_client_secret`
   - Audience : `https://taskapi`
   - Grant Type : Client Credentials
4. Cliquer "Get New Access Token"

**📖 Guide :** [Testing with Auth0](https://auth0.com/docs/get-started/auth0-overview/test-your-application)

#### Méthode 3 : Créer un frontend simple (avancé)

Pour tester le flow complet OIDC, créez une SPA React/Angular qui redirige vers Auth0.

**📖 Guide :** [Auth0 SPA Quickstart](https://auth0.com/docs/quickstart/spa)

---

### Tests et validation

#### Tests avec Postman/Swagger

**1. Obtenir un token Auth0** (voir méthodes ci-dessus)

**2. Tester endpoint protégé :**

```
GET https://localhost:7xxx/api/tasks
Authorization: Bearer VOTRE_TOKEN_AUTH0
→ 200 OK + liste de tâches
```

**3. Sans token :**

```
GET https://localhost:7xxx/api/tasks
→ 401 Unauthorized
```

**4. Token invalide/expiré :**

```
GET https://localhost:7xxx/api/tasks
Authorization: Bearer token_invalide
→ 401 Unauthorized
```

#### Ajouter un rôle au token (pour tester Admin)

**Créer une Action Auth0** (remplace les Rules) :

1. Dashboard → **Actions** → **Flows** → **Login**
2. Créer une Custom Action "Add Roles to Token"
3. Code :

```javascript
exports.onExecutePostLogin = async (event, api) => {
  const namespace = 'https://taskapi';
  const role = event.user.app_metadata?.role || 'User';

  api.accessToken.setCustomClaim(`${namespace}/roles`, role);
};
```

4. Ajouter l'Action au flow Login (drag & drop)
5. Cliquer "Apply"

**Test :**

- User avec `app_metadata.role = "Admin"` → token contient le claim `https://taskapi/roles: "Admin"`
- Endpoint `[Authorize(Roles = "Admin")]` fonctionne !

**📖 Guide :** [Auth0 Actions](https://auth0.com/docs/customize/actions)

#### Checklist de validation

- [ ] Endpoint protégé sans token → 401
- [ ] Endpoint protégé avec token Auth0 valide → 200
- [ ] User voit uniquement ses tâches
- [ ] Admin peut accéder aux endpoints admin (après ajout du rôle)
- [ ] Token expiré → 401

---

## 🔄 Refresh Tokens avec Auth0

### Pourquoi les refresh tokens ?

**Problème :**

- Access Token expire après 1h
- User doit se re-connecter toutes les heures → mauvaise UX

**Solution :**

- Refresh Token (longue durée : 7-30 jours)
- Frontend utilise le refresh token pour obtenir un nouvel access token
- User reste connecté sans re-login

### Configuration Auth0

**1. Activer Refresh Tokens :**

- Dashboard → **Applications** → Votre app frontend
- Onglet "Settings"
- Scroll "Advanced Settings" → "Grant Types"
- Cocher "Refresh Token"

**2. Activer Refresh Token Rotation (sécurité) :**

- Dashboard → **Applications** → Votre app
- Onglet "Settings"
- Scroll "Refresh Token Rotation"
- Activer "Rotation"
- Activer "Reuse Interval" : 0

**Rotation :** Chaque fois qu'un refresh token est utilisé, Auth0 génère un nouveau refresh token. L'ancien est révoqué. Sécurité ++

### Flow Refresh Token

**Géré côté frontend (React, Angular, mobile) :**

```
1. Login → Recevoir Access Token + Refresh Token
2. Stocker Refresh Token sécurisé (HttpOnly cookie, SecureStorage)
3. Access Token expire (1h)
4. Frontend envoie Refresh Token à Auth0
5. Auth0 retourne un nouvel Access Token + nouveau Refresh Token
6. Continuer sans re-login
```

**Votre API .NET :** Rien à faire ! Elle valide juste l'Access Token.

**📖 Guide :** [Auth0 Refresh Tokens](https://auth0.com/docs/secure/tokens/refresh-tokens)

---

## 🌐 SSO (Single Sign-On)

### Activer le SSO entre plusieurs apps

**Scénario :** Vous avez 3 apps (TaskAPI .NET, Dashboard React, Mobile App)

**Sans SSO :**

- Login dans TaskAPI
- Login dans Dashboard
- Login dans Mobile App

**Avec SSO Auth0 :**

- Login une seule fois
- Accès automatique aux 3 apps
- Déconnexion globale possible

### Configuration

**1. Toutes vos apps utilisent le même tenant Auth0**

- TaskAPI : `dev-xyz.eu.auth0.com`
- Dashboard React : `dev-xyz.eu.auth0.com`
- Mobile App : `dev-xyz.eu.auth0.com`

**2. Auth0 gère le SSO automatiquement avec les cookies**

**3. Tester :**

- Connectez-vous à l'app 1 → Auth0 crée une session
- Ouvrez l'app 2 → Redirection Auth0 → Reconnexion automatique !

**📖 Guide :** [Auth0 SSO](https://auth0.com/docs/authenticate/single-sign-on)

---

## 🎯 Questions d'entretien

### NÉCESSAIRE

**1. Quelle est la différence entre un tenant Auth0 et une API Auth0 ?**

<details>
<summary>Voir la réponse</summary>

**Tenant** : Votre instance Auth0 (ex: `dev-xyz.eu.auth0.com`)

- Isolé des autres clients Auth0
- Contient vos users, apps, APIs
- Généralement : 1 tenant dev + 1 tenant prod

**API** : Configuration pour votre backend .NET

- Audience (identifier) : `https://taskapi`
- Signing algorithm : RS256
- Utilisée pour valider les tokens

</details>

**2. Quelle est la différence entre Access Token et ID Token ?**

<details>
<summary>Voir la réponse</summary>

**ID Token (OIDC) :**

- Prouve l'identité de l'utilisateur
- Contient : sub, email, name
- Destiné au frontend
- Format : JWT

**Access Token (OAuth 2.0) :**

- Permet d'accéder aux ressources (API)
- Contient : permissions, scopes
- Destiné au backend (API .NET)
- Format : JWT (avec Auth0)

**Notre API .NET valide l'Access Token.**

</details>

**3. Pourquoi utiliser Auth0 plutôt que l'implémentation du Module 1 ?**

<details>
<summary>Voir la réponse</summary>

**Module 1 (apprentissage) :**

- Comprendre les mécanismes JWT ✅
- Simple pour débuter
- Nécessite de gérer : BCrypt, refresh tokens, 2FA, récupération password, conformité

**Auth0 (production) :**

- Sécurité professionnelle (OWASP, RGPD)
- 2FA, SSO, passwordless inclus
- Refresh tokens gérés automatiquement
- Monitoring, logs, alertes
- Gain de temps énorme
- Moins de risques de failles

**Conclusion :** Module 1 pour apprendre, Auth0 pour la production.

</details>

**4. Comment Auth0 valide-t-il le token JWT ?**

<details>
<summary>Voir la réponse</summary>

**Signature asymétrique (RS256) :**

1. Auth0 signe le token avec sa **clé privée**
2. Auth0 expose sa **clé publique** sur `https://dev-xyz.eu.auth0.com/.well-known/jwks.json`
3. Votre API .NET télécharge la clé publique (automatique)
4. Votre API vérifie la signature du token avec la clé publique
5. Si valide → Token authentique, pas modifié

**Avantage :** Pas besoin de partager une clé secrète entre Auth0 et votre API.

</details>

**5. Qu'est-ce qu'un refresh token et à quoi sert-il ?**

<details>
<summary>Voir la réponse</summary>

**Refresh Token :**

- Token de longue durée (7-30 jours)
- Permet d'obtenir un nouvel Access Token sans re-login
- Stocké sécurisé (HttpOnly cookie, SecureStorage mobile)
- Révocable (logout, sécurité compromise)

**Flow :**

1. Login → Access Token (1h) + Refresh Token (7j)
2. Access Token expire
3. Frontend envoie Refresh Token à Auth0
4. Auth0 retourne nouvel Access Token
5. User reste connecté

**Avec Refresh Token Rotation :** Nouveau refresh token à chaque utilisation (sécurité ++).

</details>

---

### BASIQUE

**6. Qu'est-ce que le SSO et comment Auth0 le gère-t-il ?**

<details>
<summary>Voir la réponse</summary>

**SSO (Single Sign-On) :** Se connecter une fois pour accéder à plusieurs applications.

**Avec Auth0 :**

- Toutes vos apps utilisent le même tenant Auth0
- Connexion à l'app 1 → Auth0 crée une session (cookie)
- Connexion à l'app 2 → Auth0 détecte la session existante → Reconnexion automatique
- Déconnexion globale possible

**Exemple :** Google Workspace (Gmail, Drive, Calendar).

</details>

**7. Comment ajouter un rôle personnalisé au token Auth0 ?**

<details>
<summary>Voir la réponse</summary>

**Utiliser une Action Auth0 (Login flow) :**

```javascript
exports.onExecutePostLogin = async (event, api) => {
  const namespace = 'https://taskapi';
  const role = event.user.app_metadata?.role || 'User';

  api.accessToken.setCustomClaim(`${namespace}/roles`, role);
};
```

**Résultat :** Le token contient `https://taskapi/roles: "Admin"`

**Lecture en .NET :**

```csharp
var role = User.FindFirst("https://taskapi/roles")?.Value;
```

**⚠️ Namespace obligatoire** pour les custom claims (format URL).

</details>

**8. Quelle est la différence entre HS256 et RS256 ?**

<details>
<summary>Voir la réponse</summary>

**HS256 (Symmetric - HMAC-SHA256) :**

- Une seule clé secrète
- Même clé pour signer ET vérifier
- Simple, rapide
- Nécessite de partager la clé entre émetteur et validateur
- Utilisé dans le Module 1

**RS256 (Asymmetric - RSA-SHA256) :**

- Paire de clés : privée + publique
- Clé privée pour signer (Auth0)
- Clé publique pour vérifier (votre API .NET)
- Plus sécurisé (clé publique peut être partagée)
- Utilisé par Auth0, Azure AD

**Quand utiliser quoi ?**

- **HS256** : API simple, single backend, apprentissage
- **RS256** : Microservices, plusieurs backends, Auth0, production

</details>

**9. Comment récupérer l'ID utilisateur Auth0 dans un controller ?**

<details>
<summary>Voir la réponse</summary>

```csharp
// Via extension method
var auth0UserId = User.GetAuth0UserId();

// Via claims directement
var auth0UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
    ?? User.FindFirst("sub")?.Value;

// Exemple de valeur : "auth0|123456789"
```

**Auth0 met l'ID dans le claim "sub" (subject).**

</details>

**10. Comment lier un user Auth0 à un user en base de données locale ?**

<details>
<summary>Voir la réponse</summary>

**Approche recommandée :**

```csharp
[HttpGet]
public async Task<IActionResult> GetMyTasks()
{
    var auth0UserId = User.GetAuth0UserId();

    // Trouver ou créer le user en base
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Auth0UserId == auth0UserId);
    if (user == null)
    {
        user = new User
        {
            Auth0UserId = auth0UserId,
            Email = User.GetEmail()
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    // Utiliser user.Id pour filtrer les tâches
    var tasks = await _context.Tasks.Where(t => t.UserId == user.Id).ToListAsync();

    return Ok(tasks);
}
```

**Modèle User :**

```csharp
public class User
{
    public int Id { get; set; }
    public string Auth0UserId { get; set; } // "auth0|123456"
    public string Email { get; set; }
    public List<TaskItem> Tasks { get; set; }
}
```

</details>

---

### INTERMÉDIAIRE

**11. Comment fonctionne le Refresh Token Rotation ?**

<details>
<summary>Voir la réponse</summary>

**Sans Rotation (dangereux) :**

- Refresh Token A valable 30 jours
- Si volé → Attaquant peut générer des access tokens pendant 30 jours

**Avec Rotation (sécurisé) :**

1. Login → Refresh Token A (RTa)
2. Refresh → Nouveau Access Token + Nouveau Refresh Token B (RTb)
3. RTa est révoqué (ne peut plus être utilisé)
4. Si RTa est réutilisé → Auth0 détecte une attaque → Révocation de tous les RT du user

**Configuration Auth0 :**

- Dashboard → Application → Settings → Refresh Token Rotation
- Activer "Rotation"
- Reuse Interval : 0 (aucune réutilisation autorisée)

**Sécurité ++**

</details>

**12. Qu'est-ce qu'OIDC et quelle est la différence avec OAuth 2.0 ?**

<details>
<summary>Voir la réponse</summary>

**OAuth 2.0 (2012) :**

- Protocole d'**autorisation**
- "Est-ce que l'app X peut accéder à mes photos Google ?"
- Délivre un Access Token
- Ne dit rien sur l'identité de l'utilisateur

**OIDC (OpenID Connect - 2014) :**

- Protocole d'**authentification** basé sur OAuth 2.0
- OAuth 2.0 + Identité utilisateur
- Délivre un ID Token (JWT) + Access Token
- "Qui est cet utilisateur ?"

**En résumé :**

- **OAuth 2.0** : Autorisation ("Que peut-il faire ?")
- **OIDC** : Authentification ("Qui est-il ?") + Autorisation

**Auth0 implémente OIDC.**

</details>

**13. Comment implémenter le logout avec Auth0 ?**

<details>
<summary>Voir la réponse</summary>

**Logout local (frontend) :**

- Supprimer le token du localStorage/sessionStorage
- Rediriger vers la page de login

**Logout Auth0 (global) :**

- Rediriger vers l'endpoint de logout Auth0 :

```
GET https://dev-xyz.eu.auth0.com/v2/logout?
  client_id=YOUR_CLIENT_ID&
  returnTo=https://yourapp.com/logged-out
```

- Auth0 supprime la session SSO
- Toutes les apps utilisant le même tenant sont déconnectées

**Côté API .NET :** Rien à faire. Le token expire après 1h de toute façon.

**Optionnel :** Blacklist des tokens révoqués (complexe, rarement nécessaire).

</details>

**14. Comment configurer Auth0 pour plusieurs environnements (dev, staging, prod) ?**

<details>
<summary>Voir la réponse</summary>

**Approche recommandée : 1 tenant par environnement**

**Development :**

- Tenant : `dev-xyz.eu.auth0.com`
- API : `https://taskapi-dev`
- Test users

**Staging :**

- Tenant : `staging-xyz.eu.auth0.com`
- API : `https://taskapi-staging`
- Données de pré-production

**Production :**

- Tenant : `prod-xyz.eu.auth0.com`
- API : `https://taskapi`
- Vrais users

**Configuration dans appsettings.json :**

```json
{
  "Auth0": {
    "Domain": "dev-xyz.eu.auth0.com", // Changer selon l'environnement
    "Audience": "https://taskapi-dev"
  }
}
```

**Ou variables d'environnement (meilleur) :**

```bash
export AUTH0_DOMAIN="dev-xyz.eu.auth0.com"
export AUTH0_AUDIENCE="https://taskapi-dev"
```

</details>

**15. Quelles sont les bonnes pratiques de sécurité avec Auth0 ?**

<details>
<summary>Voir la réponse</summary>

**Tokens :**

- ✅ Access Token courte durée (1h max)
- ✅ Refresh Token Rotation activé
- ✅ HTTPS uniquement (jamais HTTP)
- ✅ Stocker refresh token sécurisé (HttpOnly cookie, SecureStorage mobile)

**Configuration :**

- ✅ RS256 (asymétrique) pour les APIs
- ✅ Valider Issuer, Audience, Lifetime
- ✅ Utiliser les variables d'environnement (pas de secrets dans le code)

**Auth0 Dashboard :**

- ✅ Activer 2FA pour les admins Auth0
- ✅ Monitoring et logs des authentifications
- ✅ Configurer les alertes (tentatives de login suspectes)
- ✅ 1 tenant par environnement (dev/staging/prod)

**Users :**

- ✅ Forcer des mots de passe forts
- ✅ Activer 2FA (optionnel ou obligatoire)
- ✅ Bloquer les comptes après X tentatives échouées

</details>

---

## 💪 Exercices bonus

### Exercice 1 : Implémenter le Social Login

**Objectif :** Permettre la connexion via Google

1. Dashboard Auth0 → **Authentication** → **Social**
2. Activer Google
3. Configurer OAuth (Client ID/Secret depuis Google Cloud Console)
4. Tester la connexion Google dans votre frontend

### Exercice 2 : Ajouter des permissions granulaires

**Au lieu de seulement rôles, utiliser des permissions :**

- `read:tasks` - Lire les tâches
- `write:tasks` - Créer/modifier des tâches
- `delete:tasks` - Supprimer des tâches

**Configuration :**

1. Auth0 Dashboard → API → Permissions
2. Ajouter les permissions
3. Dans une Action, ajouter les permissions au token :

```javascript
api.accessToken.setCustomClaim('permissions', event.authorization.permissions);
```

4. En .NET, utiliser `[Authorize(Policy = "read:tasks")]`

### Exercice 3 : Implémenter un frontend React avec Auth0

**Créer une SPA React qui :**

- Se connecte via Auth0
- Affiche les tâches de l'user
- Envoie le token à l'API .NET

**📖 Guide :** [Auth0 React SDK](https://auth0.com/docs/quickstart/spa/react)

### Exercice 4 : Monitoring des authentifications

**Explorer le Dashboard Auth0 :**

- **Logs** : Voir toutes les tentatives de login (success/failed)
- **Anomaly Detection** : Auth0 détecte les attaques (brute-force, credential stuffing)
- **Alerts** : Configurer des notifications

---

## 📚 Ressources complémentaires

### Documentation officielle

- [Auth0 .NET SDK](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi)
- [Auth0 Actions](https://auth0.com/docs/customize/actions)
- [Auth0 Refresh Tokens](https://auth0.com/docs/secure/tokens/refresh-tokens)

### Tutoriels vidéo

- [Auth0 YouTube Channel](https://www.youtube.com/@auth0)
- [Auth0 ASP.NET Core Tutorial](https://www.youtube.com/results?search_query=auth0+asp.net+core)

### Outils

- [JWT.io](https://jwt.io/) - Décoder vos JWT Auth0
- [Auth0 Dashboard](https://manage.auth0.com/) - Gérer vos users et config

---

## ✅ Points clés à retenir

**Auth0 vs Custom :**

- ✅ Module 1 (custom) pour **apprendre** les mécanismes
- ✅ Auth0 pour la **production** (sécurité, gain de temps)

**Concepts Auth0 :**

- ✅ **Tenant** : Votre instance Auth0 (dev-xyz.eu.auth0.com)
- ✅ **API** : Configuration pour votre backend .NET
- ✅ **Access Token** : Validation par votre API
- ✅ **Refresh Token** : Obtenir un nouvel access token
- ✅ **SSO** : Connexion unique pour toutes vos apps

**Configuration .NET :**

- ✅ Authority : `https://{domain}/`
- ✅ Audience : Votre API identifier
- ✅ RS256 (asymétrique) : Clé publique/privée
- ✅ Custom claims avec namespace : `https://taskapi/roles`

**Sécurité :**

- ✅ HTTPS uniquement
- ✅ Refresh Token Rotation
- ✅ Access Token courte durée (1h)
- ✅ Monitoring et alertes
- ✅ 2FA pour les admins

**🎯 Vous êtes maintenant prêt à utiliser Auth0 en production !**
