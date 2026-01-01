# 🗃️ Parcours Data Access .NET - MECA

## 🎯 Objectif du parcours

**Maîtriser l'accès aux données en .NET** avec les deux approches principales du marché : Entity Framework Core (ORM complet) et Dapper (micro-ORM performant). Apprenez à choisir la bonne technologie selon votre contexte projet.

---

## 📋 Contenu du parcours

Ce parcours contient **2 MECA distincts** pour couvrir l'ensemble des besoins d'accès aux données :

### 🗃️ **[MECA Entity Framework Core](./EntityFramework/EntityFramework.md)**

_L'ORM complet pour un développement rapide_

**Ce que vous apprendrez :**

- Créer des entités et relations automatiquement
- Gérer les migrations de base de données
- Faire des requêtes LINQ expressives
- Optimiser les performances EF Core
- Tester avec InMemory database
- **Préparer vos entretiens techniques avec 15 questions/réponses**

**Parfait pour :**

- ✅ Développement rapide d'applications
- ✅ CRUD standard et relations complexes
- ✅ Équipes moins expérimentées en SQL
- ✅ Projets où la productivité prime
- ✅ Applications web classiques

### ⚡ **[MECA Dapper](./Dapper/Dapper.md)**

_Le micro-ORM pour performance et contrôle_

**Ce que vous apprendrez :**

- Écrire du SQL optimisé manuellement
- Mapper automatiquement vers des objets C#
- Gérer les paramètres sécurisés
- Faire des jointures complexes
- Optimiser les requêtes critiques
- **Préparer vos entretiens techniques avec 10/15 questions/réponses**

**Parfait pour :**

- ✅ Performance critique (reporting, analytics)
- ✅ Contrôle total du SQL généré
- ✅ Requêtes complexes avec jointures multiples
- ✅ Bases de données legacy
- ✅ Stored procedures existantes

---

## 🤔 Comment choisir ?

### **Commencez par EF Core si :**

- Vous débutez avec l'accès aux données en .NET
- Votre application a des besoins CRUD standards
- Vous voulez être productif rapidement
- Votre équipe est moins à l'aise avec SQL complexe

### **Passez à Dapper quand :**

- EF Core génère du SQL lent pour vos besoins
- Vous avez des requêtes très spécifiques
- La performance est critique
- Vous voulez utiliser des stored procedures

### **Utilisez les deux ensemble :**

- EF Core pour le CRUD quotidien
- Dapper pour les requêtes de reporting/analytics
- C'est une approche courante en entreprise !

---

## 🛠️ Setup technique

### **Prérequis**

- .NET 9 installé
- Visual Studio 2022 ou VS Code
- SQL Server Express (ou SQLite pour débuter)
- Bases de C# et SQL

### **Packages à installer**

```bash
# Pour Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Pour Dapper
dotnet add package Dapper
dotnet add package Microsoft.Data.SqlClient

# Pour les tests
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package MSTest.TestAdapter
dotnet add package MSTest.TestFramework
```

---

## 📚 Ressources complémentaires

### **Documentation officielle**

- [Entity Framework Core documentation](https://learn.microsoft.com/en-us/ef/core/)
- [Dapper GitHub repository](https://github.com/DapperLib/Dapper)

### **Comparaisons et guides**

- [EF Core vs Dapper performance](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core)
- [Choosing data access technology](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/work-with-data-in-asp-net-core-apps)

### **Outils recommandés**

- [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- [DB Browser for SQLite](https://sqlitebrowser.org/)
- [EF Core Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools)

---

## 🎯 Objectifs d'apprentissage

À la fin de ce parcours, vous saurez :

### **Compétences techniques**

- ✅ Modéliser des données avec EF Core
- ✅ Écrire des requêtes SQL optimisées avec Dapper
- ✅ Choisir la bonne approche selon le contexte
- ✅ Optimiser les performances d'accès aux données
- ✅ Tester vos couches d'accès aux données

### **Compétences métier**

- ✅ Analyser les besoins de performance
- ✅ Architecturer une couche de données mixte
- ✅ Maintenir du code d'accès aux données
- ✅ Déboguer les problèmes de requêtes

---

## 🚀 C'est parti !

**Prêt à devenir autonome sur l'accès aux données en .NET ?**

1. 📖 Commencez par le [MECA Entity Framework Core](./EntityFramework/EntityFramework.md)
2. ⚡ Continuez avec le [MECA Dapper](./Dapper/Dapper.md)

**Bon dev ! 💪**
