# 🎮 Gaming Hub - Exercice de Préparation Mission

## 📋 Vue d'ensemble

**Objectif :** Créer une API .NET professionnelle de gestion de tournois esport avec toutes les best practices attendues en mission.

Cet exercice vous met en situation réelle : vous êtes développeur dans une startup qui lance **Gaming Hub**, une plateforme de tournois esport. Vous devez créer l'API backend en **autonomie complète** en suivant les standards de l'industrie.

---

## 🚀 Démarrage rapide

### Option 1 : Utiliser le starter (recommandé) ⚡

**Gain de temps : ~1h** - Structure et packages déjà installés

1. **Copier le dossier starter**

   ```bash
   # Copier gaming-hub-starter vers votre espace de travail
   cp -r gaming-hub-starter mon-gaming-hub
   cd mon-gaming-hub
   ```

2. **Ouvrir la solution**

   - Double-cliquez sur `GamingHub.sln`
   - Ou ouvrez avec VS Code / Rider

3. **Lire l'énoncé**

   - 📖 [ENONCE.md](ENONCE.md) - Cahier des charges complet
   - 📖 [gaming-hub-starter/README.md](gaming-hub-starter/README.md) - Guide d'utilisation du starter

4. **Commencer à coder !**
   - Suivre l'approche **couche par couche** (voir README du starter)
   - Commencer par le Domain Layer

**➡️ Voir : [gaming-hub-starter/](gaming-hub-starter/)** pour la structure complète

---

### Option 2 : Partir de zéro

Si vous préférez tout créer manuellement :

1. **Lire l'énoncé complet** : [ENONCE.md](ENONCE.md)
2. **Créer la structure Clean Architecture** (4 projets + tests)
3. **Installer les packages NuGet** (voir section ci-dessous)
4. **Suivre les User Stories** de l'énoncé

---

## 🎯 Ce que vous allez apprendre

### Compétences techniques

- ✅ **Clean Architecture** - Séparation Domain/Application/Infrastructure/API
- ✅ **Entity Framework Core 9** - Modélisation, migrations, configurations
- ✅ **FluentValidation** - Validation robuste des inputs utilisateur
- ✅ **Tests** - Unitaires + Intégration (≥80% coverage)
- ✅ **Logging** - Serilog + Seq pour structured logging
- ✅ **Gestion d'erreurs** - Middleware global exception handler
- ✅ **Authentification JWT** - Sécurisation des endpoints
- ✅ **Documentation API** - Swagger professionnel

### Compétences professionnelles

En mission, on vous demandera de :

- Rejoindre un projet avec une architecture existante → **Clean Architecture**
- Ajouter des fonctionnalités sans casser l'existant → **Tests + Architecture propre**
- Valider les données utilisateur → **FluentValidation**
- Déboguer en production → **Logging structuré**
- Livrer du code de qualité → **Tests + Coverage ≥80%**

**Cet exercice simule exactement ces conditions.**

---

## 🚀 Le projet Gaming Hub

### Contexte

Gaming Hub est une plateforme qui permet de :

- **Créer et gérer des tournois** esport (League of Legends, CS2, Valorant, etc.)
- **Former des équipes** de joueurs (3-5 membres)
- **S'inscrire aux tournois** avec son équipe
- **Gérer les résultats** et voir les classements

### Fonctionnalités attendues (MVP)

**✅ Authentification**

- Register / Login avec JWT
- Rôles : Player, Organizer, Admin

**✅ Gestion des tournois**

- Créer un tournoi (Organizer)
- Publier un tournoi (Draft → Open)
- Lister les tournois (avec filtres)
- Voir les détails d'un tournoi

**✅ Gestion des équipes**

- Créer une équipe
- Ajouter des membres (max 5)
- Voir mes équipes

**✅ Inscriptions**

- Inscrire mon équipe à un tournoi
- Désinscrire mon équipe
- Voir les équipes inscrites

**✅ Qualité**

- Tests unitaires + intégration
- Logging avec Serilog + Seq
- Validation avec FluentValidation
- Documentation Swagger

### Fonctionnalités bonus (optionnel)

- 🎁 Invitations d'équipe (Pending/Accepted/Declined)
- 🎁 Matchs & résultats
- 🎁 Classements de tournois
- 🎁 Dapper pour queries de lecture
- 🎁 Déploiement Azure

---

## 📦 Packages NuGet (si vous partez de zéro)

**Si vous utilisez le starter, les packages sont déjà installés !**

### 🔹 API Layer

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 9.0.0
dotnet add package Swashbuckle.AspNetCore --version 7.2.0
dotnet add package Serilog.AspNetCore --version 8.0.3
dotnet add package Serilog.Sinks.Seq --version 8.0.0
dotnet add package Serilog.Sinks.Console --version 6.0.0
dotnet add package BCrypt.Net-Next --version 4.0.3
```

### 🔹 Application Layer

```bash
dotnet add package FluentValidation --version 11.11.0
dotnet add package FluentValidation.DependencyInjectionExtensions --version 11.11.0
```

### 🔹 Infrastructure Layer

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0
```

### 🔹 Tests

```bash
dotnet add package xunit --version 2.9.2
dotnet add package xunit.runner.visualstudio --version 2.8.2
dotnet add package Microsoft.NET.Test.Sdk --version 17.12.0
dotnet add package FluentAssertions --version 7.0.0
dotnet add package Moq --version 4.20.72
dotnet add package Microsoft.AspNetCore.Mvc.Testing --version 9.0.0
dotnet add package coverlet.collector --version 6.0.2
```

---

## 🛠️ Prérequis

**Obligatoires :**

- ✅ .NET 9 SDK ([Télécharger](https://dotnet.microsoft.com/download))
- ✅ Visual Studio 2022 / Rider / VS Code
- ✅ Docker Desktop (pour SQL Server + Seq)
- ✅ Git

**Vérification :**

```bash
dotnet --version  # Doit afficher 9.x.x
git --version
docker --version
```

---

## 📖 Documentation

- **📄 [ENONCE.md](ENONCE.md)** - Cahier des charges complet (User Stories, modèle de données, critères d'évaluation)
- **📂 [gaming-hub-starter/](gaming-hub-starter/)** - Projet de démarrage avec structure et packages
- **📖 [gaming-hub-starter/README.md](gaming-hub-starter/README.md)** - Guide d'utilisation du starter (approche couche par couche)

---

## ✅ Critères d'évaluation

Votre projet sera évalué sur :

### Architecture

- ✅ Clean Architecture respectée
- ✅ Règles de dépendances correctes
- ✅ Séparation des responsabilités claire

### Code Quality

- ✅ Code lisible et maintenable
- ✅ Nommage cohérent
- ✅ Validation des inputs (FluentValidation)
- ✅ Gestion d'erreurs appropriée

### Domain Logic

- ✅ Logique métier dans le Domain (pas dans controllers)
- ✅ Exceptions métier custom
- ✅ Règles métier implémentées (places dispo, min 3 joueurs, etc.)

### Tests

- ✅ Tests unitaires (≥80% coverage)
- ✅ Tests d'intégration (endpoints principaux)

### Logging & Documentation

- ✅ Serilog + Seq configuré
- ✅ Structured logging
- ✅ Swagger documenté

---

## 📚 Ressources complémentaires

### Documentation officielle

- [Clean Architecture - Microsoft](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)
- [FluentValidation Docs](https://docs.fluentvalidation.net/)
- [EF Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [Serilog Documentation](https://serilog.net/)
- [JWT Authentication in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/)

### Tutoriels vidéo

- [Clean Architecture with ASP.NET Core - Jason Taylor](https://www.youtube.com/watch?v=dK4Yb6-LxAk)

### Outils utiles

- [Seq](https://datalust.co/seq) - Visualisation de logs
- [Postman](https://www.postman.com/) - Tester l'API
- [JWT.io](https://jwt.io/) - Décoder les JWT

---

## 💬 FAQ

**Q: Dois-je vraiment faire toutes les fonctionnalités ?**
R: Le MVP (authentification, tournois, équipes, inscriptions) est obligatoire. Les fonctionnalités bonus sont optionnelles.

**Q: Puis-je utiliser le starter ?**
R: Oui ! C'est même recommandé. Le starter vous fait gagner ~1h de setup et vous permet de vous concentrer sur le code métier.

**Q: Je suis bloqué, que faire ?**
R:

1. Relire l'énoncé,
2. Consulter les README dans chaque projet du starter (exemples de code),
3. Consulter la doc des packages.

**Q: Puis-je utiliser cet exercice pour le présenter au client ?**
R: Absolument ! N'hésitez pas à le déployez-le sur Azure

---

## 🎓 Après cet exercice

**Vous serez capable de :**

- ✅ Créer une API .NET production-ready en autonomie
- ✅ Appliquer Clean Architecture sur un vrai projet
- ✅ Écrire du code testé et maintenable
- ✅ Déboguer efficacement avec des logs structurés
- ✅ Déployer une application sur Azure (bonus)

**Prochaine étape :**

- Ajouter un frontend (React, Angular, Blazor)

---

**🎮 Prêt à commencer ? Ouvrez [ENONCE.md](ENONCE.md) et lancez-vous !**
