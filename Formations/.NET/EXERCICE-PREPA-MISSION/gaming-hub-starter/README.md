# 🎮 Gaming Hub - Starter Template

Projet de démarrage pour l'exercice Gaming Hub. La structure Clean Architecture est déjà en place avec tous les packages NuGet installés.

## 🚀 Démarrage rapide (5 minutes)

### 1. Cloner le projet

```bash
git clone <url-du-repo>
cd gaming-hub-starter
```

### 2. Démarrer les services Docker (SQL Server + Seq)

```bash
docker-compose up -d
```

**Services disponibles :**
- SQL Server : `localhost:1433` (User: `sa`, Password: `GamingHub123!`)
- Seq (Logs) : [http://localhost:5341](http://localhost:5341)

### 3. Restaurer les packages NuGet

```bash
dotnet restore
```

### 4. Lancer l'API

```bash
dotnet run --project src/GamingHub.API
```

**API disponible sur :** [https://localhost:7001/swagger](https://localhost:7001/swagger)

---

## 🎯 Comment procéder ? (Approche couche par couche)

**⚠️ IMPORTANT : Travaillez couche par couche dans cet ordre :**

### Étape 1️⃣ : Domain Layer (GamingHub.Domain)

**Pourquoi commencer ici ?** Le Domain est pur (pas de dépendances), c'est le cœur métier.

**À faire :**
1. Créer les **Enums** : `TournamentStatus.cs`, `UserRole.cs`
2. Créer les **Exceptions** : `DomainException.cs`, `TournamentFullException.cs`
3. Créer les **Entités** : `Tournament.cs`, `Team.cs`, `User.cs`
4. Implémenter la **logique métier** dans les entités (méthodes `RegisterTeam()`, `Publish()`, etc.)

**📖 Voir :** [src/GamingHub.Domain/README.md](src/GamingHub.Domain/README.md)

---

### Étape 2️⃣ : Application Layer (GamingHub.Application)

**Pourquoi maintenant ?** Application orchestre le Domain et définit les contrats (interfaces).

**À faire :**
1. Créer les **Interfaces** : `ITournamentRepository.cs`, `ITeamRepository.cs`
2. Créer les **DTOs** : `CreateTournamentDto.cs`, `TournamentDto.cs`
3. Créer les **Validators** : `CreateTournamentDtoValidator.cs` (FluentValidation)
4. Créer les **Services** : `TournamentService.cs`, `TeamService.cs`

**📖 Voir :** [src/GamingHub.Application/README.md](src/GamingHub.Application/README.md)

---

### Étape 3️⃣ : Infrastructure Layer (GamingHub.Infrastructure)

**Pourquoi après ?** Infrastructure implémente les interfaces définies dans Application.

**À faire :**
1. Créer le **DbContext** : `ApplicationDbContext.cs`
2. Créer les **Configurations EF Core** : `TournamentConfiguration.cs`, etc.
3. Implémenter les **Repositories** : `TournamentRepository.cs`
4. Créer et appliquer les **Migrations** : `dotnet ef migrations add InitialCreate`

**📖 Voir :** [src/GamingHub.Infrastructure/README.md](src/GamingHub.Infrastructure/README.md)

---

### Étape 4️⃣ : API Layer (GamingHub.API)

**Pourquoi en dernier ?** L'API utilise tout ce qui a été créé avant.

**À faire :**
1. Configurer **JWT Authentication** dans `Program.cs`
2. Configurer **FluentValidation** et **DbContext** (DI)
3. Créer les **Controllers** : `AuthController.cs`, `TournamentsController.cs`
4. Créer le **Middleware d'erreurs** : `GlobalExceptionHandler.cs`

**📖 Voir :** [src/GamingHub.API/README.md](src/GamingHub.API/README.md)

---

### Étape 5️⃣ : Tests (GamingHub.UnitTests & IntegrationTests)

**Pourquoi à la fin ?** Une fois que tout fonctionne, on sécurise avec des tests.

**À faire :**
1. **Tests unitaires** : Validators, Services, Domain logic
2. **Tests d'intégration** : Endpoints API (WebApplicationFactory)
3. Vérifier le **coverage** : ≥80%

---

## 📁 Structure du projet

```
gaming-hub-starter/
├── src/
│   ├── GamingHub.API/              ✅ Packages installés (Swagger, Serilog, JWT)
│   ├── GamingHub.Application/      ✅ Packages installés (FluentValidation)
│   ├── GamingHub.Domain/           ✅ Aucun package (Domain pur)
│   └── GamingHub.Infrastructure/   ✅ Packages installés (EF Core)
├── tests/
│   ├── GamingHub.UnitTests/        ✅ Packages installés (xUnit, Moq, FluentAssertions)
│   └── GamingHub.IntegrationTests/ ✅ Packages installés (WebApplicationFactory)
├── docker-compose.yml              ✅ SQL Server + Seq préconfigurés
└── GamingHub.sln                   ✅ Solution avec toutes les références
```

**✅ Ce qui est déjà fait :**
- Structure Clean Architecture (4 couches + tests)
- Packages NuGet installés
- Références entre projets configurées
- Program.cs minimal avec Swagger + Serilog + Health Checks
- Docker Compose pour SQL Server + Seq
- appsettings.json avec connection string

**❌ Ce qu'il vous reste à faire (l'exercice) :**
- Entités Domain (Tournament, Team, User, etc.)
- Services Application
- DbContext + Configurations EF Core
- Repositories Infrastructure
- Controllers API
- Validators FluentValidation
- Middleware d'erreurs
- Tests unitaires et intégration

---

## 🎯 Votre mission

Consultez l'énoncé complet : **[exercice-prepa-mission.md](../exercice-prepa-mission.md)**

**Fonctionnalités à implémenter (MVP) :**
- ✅ Authentification JWT (Register/Login)
- ✅ Gestion des tournois (Create, Publish, List)
- ✅ Gestion des équipes (Create, Add members)
- ✅ Inscriptions aux tournois
- ✅ Tests unitaires + intégration (≥80% coverage)
- ✅ Logging avec Serilog + Seq

---

## 🛠️ Commandes utiles

### Gérer Docker

```bash
# Démarrer les services
docker-compose up -d

# Voir les logs
docker-compose logs -f

# Arrêter les services
docker-compose down

# Tout supprimer (y compris les données)
docker-compose down -v
```

### Base de données

```bash
# Créer une migration
dotnet ef migrations add InitialCreate --project src/GamingHub.Infrastructure --startup-project src/GamingHub.API

# Appliquer les migrations
dotnet ef database update --project src/GamingHub.Infrastructure --startup-project src/GamingHub.API

# Supprimer la base de données
dotnet ef database drop --project src/GamingHub.Infrastructure --startup-project src/GamingHub.API
```

### Tests

```bash
# Lancer tous les tests
dotnet test

# Tests avec coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Tests unitaires uniquement
dotnet test tests/GamingHub.UnitTests

# Tests d'intégration uniquement
dotnet test tests/GamingHub.IntegrationTests
```

### Build & Run

```bash
# Build
dotnet build

# Run l'API
dotnet run --project src/GamingHub.API

# Watch mode (redémarre à chaque changement)
dotnet watch --project src/GamingHub.API
```

---

## 📦 Packages NuGet installés

### API Layer
- Microsoft.AspNetCore.Authentication.JwtBearer (9.0.0)
- Swashbuckle.AspNetCore (7.2.0)
- Serilog.AspNetCore (8.0.3)
- Serilog.Sinks.Seq (8.0.0)
- Serilog.Sinks.Console (6.0.0)
- BCrypt.Net-Next (4.0.3)

### Application Layer
- FluentValidation (11.11.0)
- FluentValidation.DependencyInjectionExtensions (11.11.0)

### Infrastructure Layer
- Microsoft.EntityFrameworkCore.SqlServer (9.0.0)
- Microsoft.EntityFrameworkCore.Tools (9.0.0)
- Microsoft.EntityFrameworkCore.Design (9.0.0)

### Tests
- xunit (2.9.2)
- xunit.runner.visualstudio (2.8.2)
- Microsoft.NET.Test.Sdk (17.12.0)
- FluentAssertions (7.0.0)
- Moq (4.20.72)
- Microsoft.AspNetCore.Mvc.Testing (9.0.0)
- coverlet.collector (6.0.2)

---

## 🔧 Configuration

### Connection String (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=GamingHubDb;User Id=sa;Password=GamingHub123!;TrustServerCertificate=True;"
  }
}
```

### JWT Configuration (appsettings.json)

À configurer dans votre code :

```json
{
  "Jwt": {
    "Key": "VotreCléSecrèteTrèsLongueEtSécurisée123!",
    "Issuer": "GamingHubAPI",
    "Audience": "GamingHubClient",
    "ExpiresInMinutes": 60
  }
}
```

---

## ❓ FAQ

**Q: Pourquoi Docker pour SQL Server ?**
R: Fonctionne sur Windows/Mac/Linux, facile à reset, isolation complète.

**Q: Puis-je utiliser LocalDB au lieu de Docker ?**
R: Oui, changez la connection string vers `Server=(localdb)\\mssqllocaldb;Database=GamingHubDb;Trusted_Connection=true;`

**Q: Seq ne démarre pas ?**
R: Vérifiez que Docker Desktop est lancé et que le port 5341 n'est pas déjà utilisé.

**Q: Comment reset complètement la base de données ?**
R: `docker-compose down -v` puis `docker-compose up -d`

**Q: Les packages sont-ils vraiment installés ?**
R: Oui ! Faites `dotnet restore` pour les télécharger depuis NuGet.

---

## 🎓 Ressources

- [Énoncé complet](../exercice-prepa-mission.md)
- [Clean Architecture - Microsoft](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)
- [FluentValidation Docs](https://docs.fluentvalidation.net/)
- [EF Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [Serilog Docs](https://serilog.net/)

---

**🎮 Bon développement ! Vous avez toutes les cartes en main pour réussir l'exercice.**
