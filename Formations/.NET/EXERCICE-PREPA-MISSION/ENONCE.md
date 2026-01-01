# 🎮 Gaming Hub - Plateforme de Tournois Esport

## 🎯 Contexte du projet

Vous êtes développeur .NET dans une startup qui lance **Gaming Hub**, une plateforme de gestion de tournois esport. L'équipe produit a défini les fonctionnalités essentielles et vous devez créer l'API backend en suivant les **best practices professionnelles**.

Cet exercice vous met en situation réelle de mission : architecture propre, tests, logging, et déploiement. Vous devez être **autonome** et prendre des décisions techniques justifiées.

---

## 📋 Cahier des charges fonctionnel

### User Stories

#### 🎮 Gestion des tournois

**US-1 : Créer un tournoi**

- En tant qu'organisateur, je veux créer un tournoi (nom, jeu, date, places max, description)
- Statuts possibles : Draft, Open, InProgress, Completed, Cancelled
- Un tournoi en Draft n'est pas visible publiquement

**US-2 : Publier un tournoi**

- En tant qu'organisateur, je veux publier un tournoi (passer de Draft à Open)
- Une fois publié, les inscriptions sont ouvertes

**US-3 : Consulter les tournois**

- En tant que joueur, je veux voir tous les tournois ouverts (statut Open)
- Je peux filtrer par jeu, date, statut
- Je vois le nombre de places disponibles

#### 👥 Gestion des équipes

**US-4 : Créer une équipe**

- En tant que joueur, je peux créer une équipe (nom, tag, logo URL)
- Une équipe a un capitaine (celui qui la crée)
- Une équipe peut avoir 1 à 5 joueurs

**US-5 : Inviter des joueurs**

- En tant que capitaine, je peux inviter d'autres joueurs dans mon équipe
- Les joueurs reçoivent une invitation (Pending, Accepted, Declined)

#### 📝 Inscriptions aux tournois

**US-6 : S'inscrire à un tournoi**

- En tant que capitaine, je peux inscrire mon équipe à un tournoi
- Contraintes métier :
  - Le tournoi doit être Open
  - Il doit rester des places
  - Mon équipe doit avoir au moins 3 joueurs
  - Mon équipe ne peut pas déjà être inscrite

**US-7 : Désinscrire une équipe**

- En tant que capitaine, je peux désinscrire mon équipe (si le tournoi n'a pas commencé)

#### 🏆 Résultats & Classements

**US-8 : Enregistrer un résultat**

- En tant qu'organisateur, je peux enregistrer le résultat d'un match
- Résultat : TeamA vs TeamB, score, winner

**US-9 : Voir le classement**

- En tant que joueur, je veux voir le classement final d'un tournoi
- Classement trié par position (1er, 2ème, 3ème, etc.)

#### 👤 Authentification & Rôles

**US-10 : S'inscrire / Se connecter**

- En tant que visiteur, je peux créer un compte (email, password, username)
- Je peux me connecter et recevoir un JWT token

**US-11 : Autorisations par rôle**

- **Joueur** : Créer équipe, s'inscrire aux tournois, voir tournois
- **Organisateur** : Créer tournois, gérer résultats, publier/annuler tournois
- **Admin** : Tous les droits

---

## 🏗️ Exigences techniques

### Architecture

Vous devez implémenter une **Clean Architecture** (ou Architecture Hexagonale) avec 4 couches :

```
GamingHub/
├── src/
│   ├── GamingHub.API/              # Controllers, Middleware, Program.cs
│   ├── GamingHub.Application/      # Services métier, DTOs, Interfaces
│   ├── GamingHub.Domain/           # Entités, Value Objects, Exceptions
│   └── GamingHub.Infrastructure/   # EF Core, Repositories, Auth
└── tests/
    ├── GamingHub.UnitTests/
    └── GamingHub.IntegrationTests/
```

**Règles de dépendances (IMPORTANT) :**

- **Domain** : Aucune dépendance externe (logique métier pure)
- **Application** : Dépend uniquement de Domain
- **Infrastructure** : Implémente les interfaces de Application, dépend de Application et Domain
- **API** : Dépend de Application et Infrastructure, configure l'injection de dépendances

### Technologies imposées

**Frameworks & Libraries :**

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core 9 (SQL Server)
- FluentValidation (validation des inputs)
- Serilog + Seq (logging structuré)
- xUnit + FluentAssertions + Moq (tests)

**Authentification :**

- JWT Bearer tokens (manuel ou Auth0, à vous de choisir)
- Hash des mots de passe avec BCrypt

**Optionnel (bonus) :**

- Dapper pour certaines queries de lecture
- Application Insights (si déploiement Azure)
- Testcontainers pour tests d'intégration

### Features techniques obligatoires

#### ✅ Validation

- Toutes les entrées utilisateur doivent être validées avec **FluentValidation**
- Exemples : email valide, username min 3 caractères, date tournoi dans le futur, etc.

#### ✅ Gestion d'erreurs

- Middleware global d'exception handling
- Retourner des status HTTP appropriés (400, 401, 403, 404, 500)
- Format d'erreur standardisé JSON :

```json
{
  "error": "Tournament is full",
  "details": "This tournament has reached maximum capacity"
}
```

#### ✅ Logging

- Structured logging avec Serilog
- Logs envoyés à Seq (local via Docker)
- Logger tous les appels API importants (création tournoi, inscriptions, erreurs)

#### ✅ Tests

- **Tests unitaires** : Services métier, validators, logique domain (≥80% coverage)
- **Tests d'intégration** : Endpoints API avec WebApplicationFactory

#### ✅ Documentation API

- Swagger configuré avec descriptions claires
- Exemples de requêtes/réponses
- Documentation des codes d'erreur

---

## 🎯 Modèle de données suggéré

Voici une suggestion de modèle de données. Vous êtes libre d'adapter selon vos choix d'architecture.

### Entités principales

**Tournament**

- Id (Guid)
- Name (string)
- Game (string) - ex: "League of Legends", "Counter-Strike 2"
- Description (string)
- StartDate (DateTime)
- MaxTeams (int)
- Status (enum: Draft, Open, InProgress, Completed, Cancelled)
- OrganizerId (Guid) - relation avec User
- CreatedAt, UpdatedAt

**Team**

- Id (Guid)
- Name (string)
- Tag (string) - ex: "FNC", "G2"
- LogoUrl (string)
- CaptainId (Guid) - relation avec User
- CreatedAt

**User**

- Id (Guid)
- Username (string)
- Email (string)
- PasswordHash (string)
- Role (enum: Player, Organizer, Admin)
- CreatedAt

**TeamMember** (relation many-to-many Team ↔ User)

- TeamId (Guid)
- UserId (Guid)
- JoinedAt (DateTime)

**TournamentRegistration** (relation many-to-many Tournament ↔ Team)

- TournamentId (Guid)
- TeamId (Guid)
- RegisteredAt (DateTime)
- Status (enum: Registered, Cancelled)

**Match** (optionnel si US-8/US-9 implémentées)

- Id (Guid)
- TournamentId (Guid)
- TeamAId (Guid)
- TeamBId (Guid)
- ScoreTeamA (int)
- ScoreTeamB (int)
- WinnerId (Guid)
- PlayedAt (DateTime)

**TournamentResult** (optionnel)

- TournamentId (Guid)
- TeamId (Guid)
- Position (int) - 1er, 2ème, 3ème, etc.

### Exemples de règles métier (Domain)

Ces règles doivent être dans vos **entités Domain**, pas dans les controllers :

**Tournament.cs**

```csharp
public void Publish()
{
    if (Status != TournamentStatus.Draft)
        throw new DomainException("Only draft tournaments can be published");

    if (StartDate <= DateTime.UtcNow)
        throw new DomainException("Cannot publish tournament with past date");

    Status = TournamentStatus.Open;
}

public bool IsFull() => Registrations.Count >= MaxTeams;

public void RegisterTeam(Team team)
{
    if (Status != TournamentStatus.Open)
        throw new DomainException("Tournament is not open for registration");

    if (IsFull())
        throw new DomainException("Tournament is full");

    if (Registrations.Any(r => r.TeamId == team.Id))
        throw new DomainException("Team already registered");

    // Logique d'inscription...
}
```

**Team.cs**

```csharp
public void AddMember(User user)
{
    if (Members.Count >= 5)
        throw new DomainException("Team is full (max 5 players)");

    if (Members.Any(m => m.UserId == user.Id))
        throw new DomainException("User already in team");

    // Logique d'ajout...
}

public bool HasMinimumPlayers() => Members.Count >= 3;
```

---

## 🚀 Livrables attendus

### Fonctionnalités minimales (MVP)

Pour réussir cet exercice, vous devez livrer **au minimum** :

**✅ Authentification**

- Register (POST /api/auth/register)
- Login (POST /api/auth/login) → retourne JWT
- Protection des endpoints avec `[Authorize]`

**✅ Tournois**

- Créer un tournoi (POST /api/tournaments) - Organisateur seulement
- Publier un tournoi (POST /api/tournaments/{id}/publish) - Organisateur
- Lister les tournois (GET /api/tournaments) - filtres optionnels
- Voir détails d'un tournoi (GET /api/tournaments/{id})

**✅ Équipes**

- Créer une équipe (POST /api/teams)
- Voir mes équipes (GET /api/teams/my-teams)
- Ajouter un membre (POST /api/teams/{id}/members)

**✅ Inscriptions**

- Inscrire mon équipe à un tournoi (POST /api/tournaments/{id}/register)
- Désinscrire mon équipe (DELETE /api/tournaments/{id}/register)
- Voir les équipes inscrites (GET /api/tournaments/{id}/teams)

**✅ Tests**

- Tests unitaires (validators, services, domain)
- Tests d'intégration (au moins 3 endpoints complets)

**✅ Logging**

- Serilog + Seq configuré
- Logs structurés visibles dans Seq

### Fonctionnalités bonus (optionnel)

Si vous finissez le MVP, vous pouvez implémenter :

**🎁 Invitations d'équipe**

- Système d'invitations (Pending, Accepted, Declined)
- POST /api/teams/{id}/invitations
- GET /api/teams/invitations/my-invitations
- PUT /api/teams/invitations/{id}/accept

**🎁 Matchs & Résultats**

- Enregistrer résultats de matchs
- Générer classement final
- GET /api/tournaments/{id}/results

**🎁 Dapper pour queries**

- Utiliser Dapper pour certaines requêtes de lecture (ex: liste des tournois)
- Comparer les perfs avec EF Core

**🎁 Déploiement Azure**

- Azure App Service + Azure SQL Database
- Application Insights
- CI/CD avec GitHub Actions

---

## 📦 Setup & Dépendances

### Prérequis

- .NET 9 SDK
- Visual Studio 2022 / Rider / VS Code
- SQL Server (LocalDB ou Docker)
- Docker Desktop (pour Seq)
- Git

### Packages NuGet recommandés

**API Layer**

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 9.0.0
dotnet add package Swashbuckle.AspNetCore --version 7.2.0
dotnet add package Serilog.AspNetCore --version 8.0.3
dotnet add package Serilog.Sinks.Seq --version 8.0.0
dotnet add package Serilog.Sinks.Console --version 6.0.0
dotnet add package BCrypt.Net-Next --version 4.0.3
```

**Application Layer**

```bash
dotnet add package FluentValidation --version 11.11.0
dotnet add package FluentValidation.DependencyInjectionExtensions --version 11.11.0
```

**Infrastructure Layer**

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0

# Optionnel : Dapper
dotnet add package Dapper --version 2.1.35
```

**Tests**

```bash
dotnet add package xunit --version 2.9.2
dotnet add package xunit.runner.visualstudio --version 2.8.2
dotnet add package Microsoft.NET.Test.Sdk --version 17.12.0
dotnet add package FluentAssertions --version 7.0.0
dotnet add package Moq --version 4.20.72
dotnet add package Microsoft.AspNetCore.Mvc.Testing --version 9.0.0
dotnet add package Testcontainers.MsSql --version 4.3.0 # Optionnel
dotnet add package coverlet.collector --version 6.0.2
```

### Installer Seq (Logging)

```bash
docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest
```

Accès : `http://localhost:5341`

---

## ✅ Critères d'évaluation

Votre projet sera évalué sur les critères suivants :

### Architecture

- ✅ Clean Architecture respectée (Domain, Application, Infrastructure, API)
- ✅ Règles de dépendances correctes (Domain indépendant, Application → Domain, etc.)
- ✅ Séparation des responsabilités claire

### Code Quality

- ✅ Code lisible et maintenable
- ✅ Nommage cohérent (conventions C#)
- ✅ Pas de code dupliqué
- ✅ Gestion d'erreurs appropriée
- ✅ Validation des inputs avec FluentValidation

### Domain Logic

- ✅ Logique métier dans le Domain (pas dans les controllers)
- ✅ Exceptions métier custom (DomainException)
- ✅ Règles métier correctement implémentées (ex: vérifier places dispo, min 3 joueurs, etc.)

### Tests

- ✅ Tests unitaires avec coverage ≥ 80%
- ✅ Tests d'intégration sur les endpoints principaux
- ✅ Tests des validators

### Logging & Monitoring

- ✅ Serilog + Seq configuré
- ✅ Structured logging (pas de string.Format)
- ✅ Logs pertinents (création, erreurs, actions importantes)

### Documentation

- ✅ README.md clair avec instructions de setup
- ✅ Swagger bien documenté
- ✅ Commentaires sur la logique complexe

---

## 💡 Conseils & Best Practices

### 🎯 Conseils d'implémentation

**Commencez simple :**

1. Créez la structure de projets (API, Application, Domain, Infrastructure, Tests)
2. Implémentez l'authentification (Register/Login)
3. Créez vos entités Domain (Tournament, Team, User)
4. Implémentez EF Core (DbContext, Configurations, Migrations)
5. Créez les services Application
6. Créez les controllers API
7. Ajoutez les tests
8. Configurez le logging

**Ne pas over-engineer :**

- Pas besoin de CQRS/MediatR pour cet exercice
- Services simples dans Application suffisent
- Repository pattern optionnel (EF Core peut être utilisé directement)

**Gestion d'erreurs :**

- Utilisez des exceptions métier (`DomainException`) dans Domain
- Utilisez un middleware global pour catcher et formater les erreurs
- Retournez les bons status codes HTTP

**Validation :**

- FluentValidation pour les DTOs d'entrée
- Logique métier pour les règles complexes (dans Domain)

### 🚫 Erreurs à éviter

❌ Logique métier dans les controllers
❌ Dépendances circulaires entre projets
❌ Passwords en clair (toujours hasher avec BCrypt)
❌ Pas de validation des inputs
❌ Exceptions non gérées
❌ Logs avec `Console.WriteLine` au lieu de Serilog
❌ Tests qui modifient la vraie base de données

### 📚 Ressources utiles

- [Clean Architecture - Microsoft](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)
- [FluentValidation Docs](https://docs.fluentvalidation.net/)
- [Serilog Best Practices](https://github.com/serilog/serilog/wiki/Getting-Started)
- [EF Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [JWT Authentication in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/)

---

## 🎯 Pour aller plus loin

Une fois le projet terminé, vous pouvez l'améliorer avec :

**Features avancées :**

- Pagination sur les listes (tournois, équipes)
- Système de notifications (email quand tournoi commence)
- Upload de logo d'équipe (Azure Blob Storage)
- Brackets de tournois (arbres d'élimination)
- Statistiques (nombre de tournois gagnés par équipe)

**Architecture avancée :**

- CQRS avec MediatR
- Domain Events
- Outbox Pattern (pour fiabilité)
- Redis pour caching

**DevOps :**

- Docker Compose (API + SQL Server + Seq)
- Terraform pour infra Azure
- Monitoring avancé (Prometheus, Grafana)

---

**🎮 Bon développement ! Vous avez toutes les cartes en main pour créer une API production-ready.**
