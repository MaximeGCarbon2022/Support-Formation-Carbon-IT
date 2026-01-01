# 🗃️ Entity Framework Core - MECA

## 🎯 Objectif de la formation

**Devenir autonome avec Entity Framework Core** pour créer des applications qui parlent à la base de données simplement et efficacement.

## 📊 Prérequis

- Bases de C#
- Notions SQL de base
- .NET installé

---

## 🚀 Pourquoi EF Core ?

**Sans EF Core :**

```csharp
// Code SQL brut - complexe et répétitif
string sql = "SELECT * FROM Products WHERE CategoryId = @categoryId";
using var connection = new SqlConnection(connectionString);
using var command = new SqlCommand(sql, connection);
command.Parameters.AddWithValue("@categoryId", categoryId);
// ... 15 lignes pour récupérer des données
```

**Avec EF Core :**

```csharp
// Simple et lisible
var products = await context.Products
    .Where(p => p.CategoryId == categoryId)
    .ToListAsync();
```

---

## 🗺️ Parcours pratique

### 🏁 **Module 1 : Mes premières entités**

#### 📚 **Ce qu'on va apprendre**

- Créer des classes qui deviennent des tables
- Faire le lien entre C# et la base de données
- Comprendre les relations (1 produit → 1 catégorie)

#### 💡 **Code de base**

**Mes premières entités :**

```csharp
public class Product
{
    public int Id { get; set; }          // Clé primaire automatique
    public string Name { get; set; }     // VARCHAR en base
    public decimal Price { get; set; }   // DECIMAL en base
    public int CategoryId { get; set; }  // Clé étrangère
    public Category Category { get; set; } // Navigation
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; } // 1 catégorie → plusieurs produits
}
```

**Mon contexte (la connexion) :**

```csharp
public class ShopContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=MyShop;Trusted_Connection=true;");
    }
}
```

**Créer la base de données :**

```bash
# Dans le terminal
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Créer la migration
dotnet ef migrations add InitialCreate

# Créer la base de données
dotnet ef database update
```

#### 🛠️ **Exercice pratique M1 : Mon premier CRUD**

**Mission :** Créez une application qui gère des produits et catégories

**Ce que vous devez faire :**

- Créer 2-3 entités simples de votre choix
- Les connecter avec des relations basiques
- Faire du CRUD (Create, Read, Update, Delete)
- Tester que ça marche !

**Exemples d'entités simples :**

- Livre → Auteur
- Commande → Client
- Étudiant → Cours
- Article → Catégorie

**Validation :** Montrer que vous savez créer, lire, modifier et supprimer des données

---

### 🔧 **Module 2 : Relations et navigation**

#### 📚 **Ce qu'on va apprendre**

- Différents types de relations (1-1, 1-n, n-n)
- Comment naviguer entre les entités
- Charger les données liées

#### 💡 **Types de relations**

**Un-à-plusieurs (1-n) :**

```csharp
public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<Post> Posts { get; set; } // Un blog a plusieurs posts
}

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }  // Clé étrangère
    public Blog Blog { get; set; }   // Navigation vers le blog
}
```

**Plusieurs-à-plusieurs (n-n) :**

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Course> Courses { get; set; } // Un étudiant a plusieurs cours
}

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; } // Un cours a plusieurs étudiants
}
```

**Charger les données liées :**

```csharp
// Sans les données liées
var blogs = await context.Blogs.ToListAsync();
// blogs[0].Posts sera null !

// Avec les données liées (Include)
var blogsWithPosts = await context.Blogs
    .Include(b => b.Posts)  // Charge aussi les posts
    .ToListAsync();
// blogsWithPosts[0].Posts aura les posts !
```

#### 🛠️ **Exercice pratique M2 : Gérer les relations**

**Mission :** Ajoutez des relations à votre application du Module 1

**Défis à résoudre :**

- Comment ajouter une relation plusieurs-à-plusieurs ?
- Comment récupérer des données avec leurs relations ?
- Comment éviter les erreurs de références nulles ?

**Validation :** Application avec relations qui fonctionnent et données liées récupérées

---

### 🔍 **Module 3 : Recherche et filtrage**

#### 📚 **Ce qu'on va apprendre**

- Faire des recherches avec Where
- Trier avec OrderBy
- Paginer les résultats
- Compter et grouper

#### 💡 **Requêtes courantes**

**Recherche et filtrage :**

```csharp
// Recherche simple
var expensiveProducts = await context.Products
    .Where(p => p.Price > 100)
    .ToListAsync();

// Recherche par texte
var techProducts = await context.Products
    .Where(p => p.Name.Contains("tech"))
    .ToListAsync();

// Recherche avec relations
var blogsWithManyPosts = await context.Blogs
    .Where(b => b.Posts.Count > 5)
    .Include(b => b.Posts)
    .ToListAsync();
```

**Tri et pagination :**

```csharp
// Tri simple
var sortedProducts = await context.Products
    .OrderBy(p => p.Price)          // Du moins cher au plus cher
    .ThenBy(p => p.Name)            // Puis par nom
    .ToListAsync();

// Pagination
var page2Products = await context.Products
    .Skip(10)                       // Ignorer les 10 premiers
    .Take(10)                       // Prendre les 10 suivants
    .ToListAsync();
```

**Statistiques simples :**

```csharp
// Compter
var productCount = await context.Products.CountAsync();

// Moyenne
var averagePrice = await context.Products.AverageAsync(p => p.Price);

// Grouper par catégorie
var productsByCategory = await context.Products
    .GroupBy(p => p.Category.Name)
    .Select(g => new
    {
        CategoryName = g.Key,
        ProductCount = g.Count(),
        AveragePrice = g.Average(p => p.Price)
    })
    .ToListAsync();
```

#### 🛠️ **Exercice pratique M3 : Moteur de recherche simple**

**Mission :** Ajoutez des fonctionnalités de recherche à votre application

**Fonctionnalités à créer :**

- Recherche par mot-clé
- Filtres (prix, catégorie, etc.)
- Tri (prix, nom, date)
- Pagination (10 résultats par page)

**Validation :** Interface de recherche fonctionnelle avec plusieurs critères

---

### ⚡ **Module 4 : Performance de base**

#### 📚 **Ce qu'on va apprendre**

- Éviter les pièges de performance courants
- Optimiser les requêtes simples
- Comprendre quand EF Core est lent

#### 💡 **Bonnes pratiques simples**

**AsNoTracking pour la lecture seule :**

```csharp
// Pour afficher des données (pas de modification)
var products = await context.Products
    .AsNoTracking()                    // Plus rapide !
    .Where(p => p.IsActive)
    .ToListAsync();
```

**Select pour récupérer moins de données :**

```csharp
// Au lieu de récupérer tout l'objet
var products = await context.Products.ToListAsync(); // Lourd

// Récupérer seulement ce qu'on affiche
var productNames = await context.Products
    .Select(p => new { p.Id, p.Name, p.Price }) // Léger
    .ToListAsync();
```

**Éviter les requêtes dans les boucles :**

```csharp
// ❌ Mauvais - requête à chaque tour de boucle
foreach(var categoryId in categoryIds)
{
    var products = await context.Products
        .Where(p => p.CategoryId == categoryId)
        .ToListAsync();
}

// ✅ Bon - une seule requête
var products = await context.Products
    .Where(p => categoryIds.Contains(p.CategoryId))
    .ToListAsync();
```

#### 🛠️ **Exercice pratique M4 : Optimisation simple**

**Mission :** Trouvez et corrigez les problèmes de performance dans votre code

**Points à vérifier :**

- Utilisez-vous AsNoTracking quand c'est possible ?
- Récupérez-vous trop de données ?
- Y a-t-il des requêtes dans des boucles ?
- Les Include sont-ils nécessaires ?

**Validation :** Code optimisé avec temps de réponse mesurés

---

### 🧪 **Module 5 : Tests de base**

#### 📚 **Ce qu'on va apprendre**

- Tester du code qui utilise EF Core
- Base de données en mémoire pour les tests
- Tests simples et efficaces

#### 💡 **Tests avec InMemory**

**Configuration de test :**

```csharp
[TestClass]
public class ProductServiceTests
{
    private DbContextOptions<ShopContext> GetInMemoryDbOptions()
    {
        return new DbContextOptionsBuilder<ShopContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [TestMethod]
    public async Task GetExpensiveProducts_ReturnsCorrectProducts()
    {
        // Arrange - Préparer
        var options = GetInMemoryDbOptions();
        await using var context = new ShopContext(options);

        context.Products.AddRange(
            new Product { Name = "Cheap", Price = 10 },
            new Product { Name = "Expensive", Price = 200 }
        );
        await context.SaveChangesAsync();

        var service = new ProductService(context);

        // Act - Exécuter
        var expensiveProducts = await service.GetExpensiveProductsAsync(100);

        // Assert - Vérifier
        Assert.AreEqual(1, expensiveProducts.Count);
        Assert.AreEqual("Expensive", expensiveProducts[0].Name);
    }
}
```

#### 🛠️ **Exercice pratique M5 : Mes premiers tests**

**Mission :** Écrivez des tests pour vos fonctions principales

**Tests à créer :**

- Test de création d'entité
- Test de recherche
- Test de modification
- Test avec relations

**Validation :** Suite de tests qui passent et couvrent les cas principaux

---

## 📊 Projet final simple

### 🎯 **Application de gestion (au choix)**

**Idées d'applications :**

- Gestion de bibliothèque (livres, auteurs, emprunts)
- Blog simple (articles, catégories, commentaires)
- Boutique en ligne basique (produits, commandes, clients)
- Gestionnaire de tâches (projets, tâches, utilisateurs)

**Fonctionnalités minimum :**

- 3-4 entités avec relations
- CRUD complet
- Recherche et filtrage
- Quelques tests
- Interface simple (console ou web)

**Évaluation :**

- ✅ Code propre et lisible
- ✅ Relations qui fonctionnent
- ✅ Pas d'erreurs courantes
- ✅ Quelques optimisations
- ✅ Tests de base

---

## 💪 Ressources utiles

### 📚 **Documentation officielle :**

- [EF Core pour débutants](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app)
- [Tutoriels EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/)

### 🎓 **Tutos Microsoft Learn :**

- [Persist data with Entity Framework Core](https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/)

### 🛠️ **Outils pratiques :**

- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (gratuit)
- [SQLite Browser](https://sqlitebrowser.org/) (pour débuter)

**🎯 L'objectif : être à l'aise avec EF Core sans se perdre dans la complexité !**
