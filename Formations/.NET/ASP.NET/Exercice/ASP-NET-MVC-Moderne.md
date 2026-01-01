# 🚀 ASP.NET - Montée en compétence

## 🎯 Objectif de la formation

**Devenir opérationnel sur les technologies ASP.NET modernes** à travers des exercices pratiques progressifs qui simulent des situations réelles d'entreprise.

## 📊 Prérequis

- Connaissances de base en C#
- Notions de développement web
- Visual Studio 2022 ou VS Code installé

---

## 🗺️ Parcours pratique progressif

### 🎯 **Contexte de l'exercice fil rouge**

Tout au long de cette formation, vous allez construire **TodoAPI**, une API REST moderne pour gérer des tâches qui évoluera progressivement :

**📌 Vision globale du projet :**

- **Module 1** : Découverte .NET 9 et premiers pas
- **Module 2** : TodoAPI MVC avec Controllers (CRUD, stockage mémoire)
- **Module 3** : Architecture en couches (Services, Repository pattern)
- **Module 4** : Sécurisation avec JWT et authentification
- **Module 5** : Interface Blazor pour consommer l'API
- **Module 6** : Tests complets (Unit + Integration)

**🎁 Résultat final :** Une API REST complète, sécurisée, testée et documentée, avec une interface Blazor fonctionnelle.

---

### 📋 **Cahier des charges - TodoAPI**

#### **Entités principales**

**Todo** (Tâche)
- `Id` (int) : Identifiant unique
- `Title` (string, requis, max 100 caractères) : Titre de la tâche
- `Description` (string, optionnel, max 500 caractères) : Description détaillée
- `Status` (enum) : `Todo`, `InProgress`, `Done`
- `CreatedAt` (DateTime) : Date de création automatique
- `DueDate` (DateTime?, optionnel) : Date limite
- `Priority` (enum) : `Low`, `Normal`, `High`
- `UserId` (int, requis à partir du Module 4) : Propriétaire de la tâche

**User** (Utilisateur - à partir du Module 4)
- `Id` (int) : Identifiant unique
- `Username` (string, requis, unique) : Nom d'utilisateur
- `Email` (string, requis, unique) : Email
- `PasswordHash` (string, requis) : Mot de passe hashé
- `Role` (enum) : `User`, `Admin`
- `CreatedAt` (DateTime) : Date d'inscription

---

#### **Fonctionnalités métier**

**CRUD de base (Module 2)**
- ✅ Créer une tâche
- ✅ Lister toutes les tâches
- ✅ Récupérer une tâche par ID
- ✅ Modifier une tâche
- ✅ Supprimer une tâche

**Filtrage et recherche (Module 3)**
- ✅ Filtrer par statut (`/todos?status=InProgress`)
- ✅ Filtrer par priorité (`/todos?priority=High`)
- ✅ Rechercher par titre (`/todos?search=meeting`)
- ✅ Tri par date d'échéance, priorité, etc.

**Authentification (Module 4)**
- ✅ Inscription utilisateur (`POST /auth/register`)
- ✅ Connexion avec JWT (`POST /auth/login`)
- ✅ Endpoints protégés par authentification

**Authorization (Module 4)**
- ✅ Un utilisateur ne peut voir que **ses propres tâches**
- ✅ Un utilisateur ne peut modifier/supprimer que **ses propres tâches**
- ✅ Un admin peut voir **toutes les tâches**

**Interface Blazor (Module 5)**
- ✅ Page de liste des tâches
- ✅ Formulaire d'ajout de tâche
- ✅ Modification inline
- ✅ Suppression avec confirmation
- ✅ Filtrage par statut/priorité

**Tests (Module 6)**
- ✅ Tests unitaires des services
- ✅ Tests d'intégration des endpoints API
- ✅ Tests E2E Blazor (optionnel)

---

#### **Contraintes techniques**

**Architecture**
- **Module 2** : Controllers MVC basiques (logique dans le controller, stockage en mémoire)
- **Module 3+** : Architecture en couches professionnelle :
  - Controllers → Services → Repositories
  - Respect du pattern Repository pour l'accès aux données
  - Injection de dépendances pour tous les services
- **Note importante** : Chaque module améliore progressivement l'architecture pour respecter les principes SOLID.

**Base de données**
- SQL Server ou SQLite (au choix)
- Entity Framework Core pour l'ORM
- Code First avec migrations

**API REST**
- Endpoints RESTful respectant les conventions HTTP
- Codes de statut appropriés (200, 201, 400, 401, 404, etc.)
- Documentation OpenAPI/Swagger générée automatiquement

**Validation**
- Validation des modèles avec Data Annotations
- Messages d'erreur clairs et explicites
- Gestion des erreurs centralisée

**Sécurité**
- Mots de passe hashés avec BCrypt ou Identity
- JWT avec expiration (15 minutes + refresh token optionnel)
- Protection contre l'injection SQL (via EF Core)
- Validation des inputs

**Tests**
- Couverture de code minimale : 70%
- Tests des scénarios critiques (auth, CRUD, autorisation)
- Tests d'intégration avec base en mémoire (InMemory)

---

#### **Évolution progressive par module**

| Module | Ce qui est ajouté |
|--------|-------------------|
| **Module 1** | Setup .NET 9, premier projet API |
| **Module 2** | Controllers MVC, CRUD basique, stockage en mémoire (`List<Todo>`) |
| **Module 3** | Services injectés, Repository pattern, filtrage, architecture en couches |
| **Module 4** | Entity Framework, SQL Server, JWT, authentification, User, autorisation |
| **Module 5** | Interface Blazor Server ou WASM consommant l'API |
| **Module 6** | Tests unitaires, tests d'intégration, qualité du code |

**💡 Approche pédagogique :** Chaque module enrichit le projet existant plutôt que de repartir de zéro, simulant ainsi l'évolution réelle d'un projet en entreprise.

---

### 🏁 **Module 1 : Découverte de l'écosystème .NET 9**

#### 📚 **Théorie**

- Visionner : [What's new in .NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview) (Microsoft Learn)
- Lire : [Documentation officielle .NET 9](https://learn.microsoft.com/en-us/dotnet/)
- Installation : [Télécharger .NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

#### 🛠️ **Exercice pratique :**

**Projet : "Mon premier projet .NET 9"**

```bash
# Installation et setup
dotnet --version  # Vérifier .NET 9
dotnet new webapi -n MonPremierAPI
cd MonPremierAPI
dotnet run
```

**Tâches à réaliser :**

- [ ] Créer un nouveau projet Web API avec .NET 9
- [ ] Explorer la structure du projet généré
- [ ] Modifier le contrôler WeatherForecast pour retourner des données personnalisées
- [ ] Tester l'API avec Swagger UI
- [ ] Documenter les différences observées vs versions précédentes

**📋 Livrable :** Capture d'écran de Swagger + fichier de documentation des observations

---

### ⚡ **Module 2 : Minimal APIs modernes**

#### 📚 **Théorie**

- Documentation : [Minimal APIs overview](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/overview?view=aspnetcore-9.0)
- Tutorial : [Create a minimal API with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-9.0)
- Référence : [Minimal APIs quick reference](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-9.0)
- GitHub : [MinimalApiPlayground](https://github.com/DamianEdwards/MinimalApiPlayground)

#### 🛠️ **Exercice pratique : Todo API de base**

**Projet : "TodoAPI avec Minimal APIs"**

**Tâches à réaliser :**

- [ ] Créer un projet vide et configurer une Minimal API
- [ ] Implémenter les endpoints CRUD pour des tâches :
  - `GET /todos` - Liste toutes les tâches
  - `GET /todos/{id}` - Récupère une tâche
  - `POST /todos` - Crée une tâche
  - `PUT /todos/{id}` - Met à jour une tâche
  - `DELETE /todos/{id}` - Supprime une tâche
- [ ] Utiliser un stockage en mémoire (List<Todo>)
- [ ] Ajouter validation des modèles
- [ ] Générer la documentation OpenAPI automatiquement

---

### 🔧 **Module 3 : Injection de dépendances avancée**

#### 📚 **Théorie**

- [Dependency injection in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-9.0)
- [What's new in .NET 9 libraries](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/libraries)

#### 🛠️ **Exercice pratique : Refactoring avec DI**

**Projet : "Refactoring TodoAPI avec architecture en couches"**

**Tâches à réaliser :**

- [ ] Créer une interface `ITodoRepository`
- [ ] Implémenter `InMemoryTodoRepository` et `SqlTodoRepository` (avec Entity Framework)
- [ ] Créer un service `ITodoService` avec logique métier
- [ ] Configurer l'injection de dépendances dans `Program.cs`
- [ ] Implémenter un système de switch entre repositories via configuration
- [ ] Ajouter un service de notification (interface + implémentation email fictive)

**Bonus :**

- [ ] Utiliser les nouveaux feature switches de .NET 9
- [ ] Implémenter le pattern Decorator pour le cache

---

### 🔐 **Module 4 : Sécurité moderne**

#### 📚 **Théorie**

- [ASP.NET Core security overview](https://learn.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-9.0)
- [Overview of ASP.NET Core authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-9.0)
- [What's new in ASP.NET Core 9.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-9.0?view=aspnetcore-9.0) (sécurité)
- [Safe storage of app secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0)

#### 🛠️ **Exercice pratique : Sécurisation complète**

**Projet : "TodoAPI sécurisée"**

**Tâches à réaliser :**

- [ ] Implémenter l'authentification JWT
- [ ] Créer un endpoint `/auth/login` qui retourne un token
- [ ] Sécuriser les endpoints Todo avec `[Authorize]`
- [ ] Implémenter des rôles (Admin, User)
- [ ] Créer des policies personnalisées (ex: propriétaire de la tâche)
- [ ] Configurer la protection des données avec rotation automatique des clés
- [ ] Implémenter la gestion des secrets de développement

**Scénarios à tester :**

- [ ] Utilisateur non authentifié → 401
- [ ] Utilisateur authentifié mais sans droits → 403
- [ ] Admin peut tout voir/modifier
- [ ] User ne peut voir que ses tâches

---

### 🎨 **Module 5 : Blazor Server + WebAssembly**

#### 📚 **Théorie**

- [ASP.NET Core Blazor overview](https://learn.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-9.0)
- [Blazor hosting models](https://learn.microsoft.com/en-us/aspnet/core/blazor/hosting-models?view=aspnetcore-9.0)
- [Tutorial: Build your first Blazor app](https://dotnet.microsoft.com/en-us/learn/aspnet/blazor-tutorial/intro)
- [ASP.NET Core Blazor fundamentals](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/?view=aspnetcore-9.0)

#### 🛠️ **Exercice pratique : Interface Blazor Server**

**Projet : "TodoApp - Interface utilisateur"**

**Tâches à réaliser :**

- [ ] Créer un projet Blazor Server
- [ ] Consommer votre TodoAPI via HttpClient
- [ ] Créer des composants :
  - `TodoList.razor` - Affichage liste
  - `TodoForm.razor` - Création/édition
  - `TodoItem.razor` - Élément individuel
- [ ] Implémenter la validation côté client
- [ ] Ajouter des notifications utilisateur (succès/erreur)

---

### 🧪 **Module 6 : Tests complets**

#### 📚 **Théorie**

- Tests unitaires, intégration, end-to-end
- Nouveaux outils testing .NET 9

#### 🛠️ **Exercice pratique M6 : Couverture de tests**

**Projet : "Suite de tests complète"**

**Tâches à réaliser :**

- [ ] Tests unitaires des services et repositories (xUnit)
- [ ] Tests d'intégration de l'API (WebApplicationFactory)
- [ ] Tests des composants Blazor
- [ ] Configuration CI/CD basique avec GitHub Actions
- [ ] Rapport de couverture de code

**Scénarios de test obligatoires :**

- [ ] CRUD complet avec différents utilisateurs
- [ ] Gestion des erreurs et cas limites
- [ ] Performance sous charge
- [ ] Sécurité (tentatives d'accès non autorisé)

**📋 Livrable :** Suite de tests avec > 80% de couverture + pipeline CI/CD

---

## ✅ **Checklist de préparation mission**

Avant de vous lancer en mission ASP.NET, assurez-vous de maîtriser :

### **Fondamentaux**

- [ ] Créer une API REST avec Minimal APIs ou Controllers
- [ ] Configurer Dependency Injection (Scoped, Singleton, Transient)
- [ ] Implémenter l'authentification JWT
- [ ] Gérer les erreurs globalement
- [ ] Valider les modèles avec Data Annotations

### **Architecture**

- [ ] Séparer la logique métier dans des services
- [ ] Utiliser EF Core avec une couche service
- [ ] Organiser le code (Controllers, Services, Models, DTOs)
- [ ] Appliquer les bonnes pratiques SOLID

### **Sécurité**

- [ ] Sécuriser les endpoints avec `[Authorize]`
- [ ] Gérer les CORS correctement
- [ ] Protéger contre les injections SQL
- [ ] Configurer HTTPS et cookies sécurisés

### **Tests et qualité**

- [ ] Écrire des tests unitaires (services)
- [ ] Écrire des tests d'intégration (API)
- [ ] Documenter l'API avec Swagger/OpenAPI
- [ ] Gérer les migrations EF Core

### **Déploiement**

- [ ] Déployer sur un environnement Cloud (Azure ?)

---

## 💪 Ressources et support

### 📚 **Documentation officielle :**

- [Microsoft Learn - .NET](https://learn.microsoft.com/en-us/dotnet/)
- [ASP.NET Core documentation](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-9.0)
- [Blazor documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-9.0)

### 🎓 **Parcours d'apprentissage Microsoft Learn :**

- [Build web apps with Blazor](https://learn.microsoft.com/en-us/training/paths/build-web-apps-with-blazor/)
- [Build a web API with minimal API, ASP.NET Core, and .NET](https://learn.microsoft.com/en-us/training/modules/build-web-api-minimal-api/)
- [Create a web API with ASP.NET Core controllers](https://learn.microsoft.com/en-us/training/modules/build-web-api-aspnet-core/)

### 🚀 **Tutoriels pratiques officiels :**

- [Tutorial: Create a minimal API with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-9.0)
- [Tutorial: Create a controller-based web API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0)
- [ASP.NET Core Blazor tutorials](https://learn.microsoft.com/en-us/aspnet/core/blazor/tutorials/?view=aspnetcore-9.0)

### 🛠️ **Outils et téléchargements :**

- [Télécharger .NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [.NET Learning center](https://dotnet.microsoft.com/en-us/learn)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

**🔥 Prêt à devenir un expert ASP.NET 2025 ? Let's code! 🚀**
