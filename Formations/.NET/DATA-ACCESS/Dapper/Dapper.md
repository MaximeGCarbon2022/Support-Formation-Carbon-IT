# ⚡ Dapper - MECA

## 🎯 Objectif de la formation

Maîtriser Dapper pour accéder aux données rapidement quand vous voulez **contrôler votre SQL** et avoir de **meilleures performances**.

## 📋 Prérequis

- Bases de C#
- SQL de base (SELECT, INSERT, UPDATE, DELETE)
- .NET installé

---

## 🤔 Pourquoi Dapper ?

**EF Core :**

```csharp
// EF Core génère le SQL pour vous
var products = await context.Products
    .Where(p => p.Price > 100)
    .ToListAsync();
// Mais parfois le SQL généré n'est pas optimal...
```

**Dapper :**

```csharp
// Vous écrivez le SQL, Dapper fait le mapping
var products = await connection.QueryAsync<Product>(
    "SELECT * FROM Products WHERE Price > @price",
    new { price = 100 });
// Contrôle total + performance maximale !
```

**⚖️ Quand utiliser Dapper ?**

- Requêtes complexes avec plusieurs jointures
- Performance critique (rapports, analytics)
- Stored procedures existantes
- SQL spécifique à votre base de données
- Quand EF Core génère du SQL lent

---

## 🗺️ Parcours pratique

### 🏁 **Mission 1 : Tes premières requêtes Dapper**

#### 📚 **Ce qu'on va apprendre**

- Installer et configurer Dapper
- Faire des requêtes simples
- Mapper le SQL vers des objets C#

#### 🛠️ **Setup de base**

**Installation :**

```bash
dotnet add package Dapper
dotnet add package Microsoft.Data.SqlClient
# OU pour SQLite
dotnet add package Microsoft.Data.Sqlite
```

**Ma première classe :**

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
```

**Ma première requête :**

```csharp
using Dapper;
using Microsoft.Data.SqlClient;

public class ProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Récupérer tous les produits
    public async Task<List<Product>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);

        var sql = "SELECT Id, Name, Price, CategoryId FROM Products";
        var products = await connection.QueryAsync<Product>(sql);

        return products.ToList();
    }

    // Récupérer un produit par ID
    public async Task<Product> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);

        var sql = "SELECT * FROM Products WHERE Id = @Id";
        var product = await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });

        return product;
    }
}
```

**Utilisation :**

```csharp
// Dans votre application
var repository = new ProductRepository("Server=.;Database=MyShop;Trusted_Connection=true;");

// Récupérer tous les produits
var allProducts = await repository.GetAllAsync();

// Récupérer un produit spécifique
var product = await repository.GetByIdAsync(1);
```

#### 🎯 **Défi Mission 1 : CRUD avec Dapper**

**Mission :** Créez un repository complet avec Dapper

**Ce que vous devez faire :**

- Créer une classe simple (Product, Customer, etc.)
- Implémenter les 4 opérations : Create, Read, Update, Delete
- Tester que tout marche !

**Template de départ :**

```csharp
public class ProductRepository
{
    // TODO : GetAllAsync()
    // TODO : GetByIdAsync(int id)
    // TODO : CreateAsync(Product product)
    // TODO : UpdateAsync(Product product)
    // TODO : DeleteAsync(int id)
}
```

**✅ Validation :** Repository fonctionnel avec les 5 méthodes qui marchent

---

### 🔒 **Mission 2 : Paramètres et sécurité**

#### 📚 **Ce qu'on va apprendre**

- Utiliser des paramètres sécurisés
- Éviter les injections SQL
- Gérer différents types de paramètres

#### 🛡️ **Paramètres sécurisés**

**❌ JAMAIS faire ça (injection SQL) :**

```csharp
// DANGER ! Ne jamais concaténer comme ça
var sql = $"SELECT * FROM Products WHERE Name = '{productName}'";
// Un hacker peut injecter : '; DROP TABLE Products; --
```

**✅ Toujours utiliser des paramètres :**

```csharp
// Sécurisé avec @paramètres
var sql = "SELECT * FROM Products WHERE Name = @Name";
var products = await connection.QueryAsync<Product>(sql, new { Name = productName });
```

**🔧 Différents types de paramètres :**

```csharp
public class ProductService
{
    // Paramètre simple
    public async Task<List<Product>> GetByNameAsync(string name)
    {
        using var connection = new SqlConnection(_connectionString);

        var sql = "SELECT * FROM Products WHERE Name LIKE @Name";
        return (await connection.QueryAsync<Product>(sql, new { Name = $"%{name}%" })).ToList();
    }

    // Plusieurs paramètres
    public async Task<List<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        using var connection = new SqlConnection(_connectionString);

        var sql = @"
            SELECT * FROM Products
            WHERE Price >= @MinPrice AND Price <= @MaxPrice
            ORDER BY Price";

        return (await connection.QueryAsync<Product>(sql, new { MinPrice = minPrice, MaxPrice = maxPrice })).ToList();
    }

    // Objet comme paramètre
    public async Task<int> CreateAsync(Product product)
    {
        using var connection = new SqlConnection(_connectionString);

        var sql = @"
            INSERT INTO Products (Name, Price, CategoryId)
            VALUES (@Name, @Price, @CategoryId);
            SELECT CAST(SCOPE_IDENTITY() as int)"; // Récupère l'ID généré

        var id = await connection.QuerySingleAsync<int>(sql, product);
        return id;
    }
}
```

**🔄 Paramètres optionnels et recherche dynamique :**

```csharp
public async Task<List<Product>> SearchAsync(string name = null, decimal? minPrice = null, int? categoryId = null)
{
    using var connection = new SqlConnection(_connectionString);

    var conditions = new List<string>();
    var parameters = new DynamicParameters();

    // Construction dynamique de la requête
    if (!string.IsNullOrEmpty(name))
    {
        conditions.Add("Name LIKE @Name");
        parameters.Add("Name", $"%{name}%");
    }

    if (minPrice.HasValue)
    {
        conditions.Add("Price >= @MinPrice");
        parameters.Add("MinPrice", minPrice.Value);
    }

    if (categoryId.HasValue)
    {
        conditions.Add("CategoryId = @CategoryId");
        parameters.Add("CategoryId", categoryId.Value);
    }

    var sql = "SELECT * FROM Products";
    if (conditions.Any())
    {
        sql += " WHERE " + string.Join(" AND ", conditions);
    }

    return (await connection.QueryAsync<Product>(sql, parameters)).ToList();
}
```

#### 📦 **Paramètres avec Stored Procedures**

**En mission, vous allez souvent trouver des Stored Procedures déjà créées. Voici comment les appeler avec Dapper :**

**SP simple avec paramètres IN :**

```csharp
// SQL : La SP existe déjà dans la base
/*
CREATE PROCEDURE sp_GetProductsByCategory
    @CategoryId INT
AS
BEGIN
    SELECT * FROM Products WHERE CategoryId = @CategoryId
END
*/

public async Task<List<Product>> GetProductsByCategorySpAsync(int categoryId)
{
    using var connection = new SqlConnection(_connectionString);

    // Même principe que les requêtes normales, juste commandType différent
    return (await connection.QueryAsync<Product>(
        "sp_GetProductsByCategory",
        new { CategoryId = categoryId },
        commandType: CommandType.StoredProcedure)).ToList();
}
```

**SP avec paramètre OUTPUT (récupérer une valeur) :**

```csharp
// SP qui retourne l'ID créé via OUTPUT
/*
CREATE PROCEDURE sp_CreateProduct
    @Name NVARCHAR(100),
    @Price DECIMAL(10,2),
    @CategoryId INT,
    @NewId INT OUTPUT
AS
BEGIN
    INSERT INTO Products (Name, Price, CategoryId)
    VALUES (@Name, @Price, @CategoryId)
    SET @NewId = SCOPE_IDENTITY()
END
*/

public async Task<int> CreateProductWithSpAsync(Product product)
{
    using var connection = new SqlConnection(_connectionString);

    var parameters = new DynamicParameters();
    parameters.Add("Name", product.Name);
    parameters.Add("Price", product.Price);
    parameters.Add("CategoryId", product.CategoryId);
    parameters.Add("NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

    await connection.ExecuteAsync(
        "sp_CreateProduct",
        parameters,
        commandType: CommandType.StoredProcedure);

    // Récupérer la valeur OUTPUT après l'exécution
    return parameters.Get<int>("NewId");
}
```

**💡 Pourquoi c'est utile en mission ?**

- Beaucoup d'entreprises ont déjà des SP en prod
- Vous ne pouvez pas toujours les modifier (contraintes métier)
- Dapper vous permet de les utiliser facilement
- Même logique de sécurité : les paramètres sont protégés automatiquement

#### 🎯 **Défi Mission 2 : Recherche sécurisée + SP**

**Mission :** Ajoutez une fonction de recherche flexible à votre repository

**Partie 1 - Recherche dynamique :**

- Recherche par nom (contient le texte)
- Filtrage par fourchette de prix
- Filtrage par catégorie
- Tous les filtres optionnels

**Partie 2 - Stored Procedure (optionnel) :**

- Créer une SP simple (ex: GetProductsByPriceRange)
- L'appeler depuis votre repository avec Dapper

**✅ Validation :**

- Fonction de recherche qui combine plusieurs critères sans failles de sécurité
- (Bonus) Au moins une SP fonctionnelle appelée avec Dapper

---

### 🔗 **Mission 3 : Jointures et relations**

#### 📚 **Ce qu'on va apprendre**

- Faire des jointures avec Dapper
- Mapper des objets complexes
- Gérer les relations un-à-plusieurs

#### 🔧 **Jointures simples**

**Objets avec relation :**

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

    public Category Category { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

**Jointure simple :**

```csharp
public async Task<List<Product>> GetProductsWithCategoryAsync()
{
    using var connection = new SqlConnection(_connectionString);

    var sql = @"
        SELECT
            p.Id, p.Name, p.Price, p.CategoryId,
            c.Id, c.Name
        FROM Products p
        INNER JOIN Categories c ON p.CategoryId = c.Id";

    var products = await connection.QueryAsync<Product, Category, Product>(
        sql,
        (product, category) =>
        {
            product.Category = category;
            return product;
        },
        splitOn: "Id"); // Dapper split sur la 2ème colonne "Id"

    return products.ToList();
}
```

**📊 Relation un-à-plusieurs :**

```csharp
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; } = new();
}

public async Task<List<Category>> GetCategoriesWithProductsAsync()
{
    using var connection = new SqlConnection(_connectionString);

    var sql = @"
        SELECT
            c.Id, c.Name,
            p.Id, p.Name, p.Price, p.CategoryId
        FROM Categories c
        LEFT JOIN Products p ON c.Id = p.CategoryId
        ORDER BY c.Id";

    var categoryDict = new Dictionary<int, Category>();

    await connection.QueryAsync<Category, Product, Category>(
        sql,
        (category, product) =>
        {
            if (!categoryDict.TryGetValue(category.Id, out var existingCategory))
            {
                existingCategory = category;
                categoryDict.Add(category.Id, existingCategory);
            }

            if (product != null)
            {
                existingCategory.Products.Add(product);
            }

            return existingCategory;
        },
        splitOn: "Id");

    return categoryDict.Values.ToList();
}
```

**💡 Méthode plus simple :**

```csharp
// Approche plus simple : 2 requêtes séparées
public async Task<List<Category>> GetCategoriesWithProductsSimpleAsync()
{
    using var connection = new SqlConnection(_connectionString);

    // 1. Récupérer les catégories
    var categories = (await connection.QueryAsync<Category>(
        "SELECT Id, Name FROM Categories")).ToList();

    // 2. Récupérer les produits pour chaque catégorie
    foreach (var category in categories)
    {
        category.Products = (await connection.QueryAsync<Product>(
            "SELECT * FROM Products WHERE CategoryId = @CategoryId",
            new { CategoryId = category.Id })).ToList();
    }

    return categories;
}
```

#### 🎯 **Défi Mission 3 : Données liées**

**Mission :** Récupérez des données avec leurs relations

**Ce que vous devez faire :**

- Créer une deuxième entité liée à la première
- Faire une jointure pour récupérer les deux ensemble
- Gérer une relation un-à-plusieurs

**✅ Validation :** Requêtes qui retournent des objets avec leurs données liées

---

### ⚡ **Mission 4 : Performance et bonnes pratiques**

#### 📚 **Ce qu'on va apprendre**

- Optimiser les requêtes Dapper
- Éviter les pièges de performance
- Comparer avec EF Core

#### 🚀 **Optimisations simples**

**Requêtes en lot (éviter N+1) :**

```csharp
// ❌ Mauvais : requête dans une boucle (N+1 problème)
public async Task<List<Product>> GetProductsWithCategoryBadAsync()
{
    var products = await GetAllAsync();

    foreach (var product in products)
    {
        // UNE requête par produit = lent !
        product.Category = await GetCategoryByIdAsync(product.CategoryId);
    }

    return products;
}

// ✅ Bon : une seule requête avec jointure
public async Task<List<Product>> GetProductsWithCategoryGoodAsync()
{
    using var connection = new SqlConnection(_connectionString);

    var sql = @"
        SELECT p.*, c.Id, c.Name
        FROM Products p
        INNER JOIN Categories c ON p.CategoryId = c.Id";

    // Une seule requête pour tout !
    return (await connection.QueryAsync<Product, Category, Product>(
        sql,
        (product, category) => { product.Category = category; return product; },
        splitOn: "Id")).ToList();
}
```

**📄 Pagination efficace :**

```csharp
public async Task<List<Product>> GetProductsPaginatedAsync(int page, int pageSize)
{
    using var connection = new SqlConnection(_connectionString);

    var offset = (page - 1) * pageSize;

    var sql = @"
        SELECT * FROM Products
        ORDER BY Id
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY";

    return (await connection.QueryAsync<Product>(sql, new { Offset = offset, PageSize = pageSize })).ToList();
}

// Bonus : compter le total pour la pagination
public async Task<int> GetProductsTotalCountAsync()
{
    using var connection = new SqlConnection(_connectionString);

    return await connection.QuerySingleAsync<int>("SELECT COUNT(*) FROM Products");
}
```

**🔄 Gestion des transactions :**

```csharp
public async Task<bool> CreateOrderWithItemsAsync(Order order, List<OrderItem> items)
{
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    using var transaction = await connection.BeginTransactionAsync();

    try
    {
        // 1. Créer la commande
        var orderId = await connection.QuerySingleAsync<int>(@"
            INSERT INTO Orders (CustomerId, OrderDate, Total)
            OUTPUT INSERTED.Id
            VALUES (@CustomerId, @OrderDate, @Total)",
            order, transaction);

        // 2. Ajouter les items
        foreach (var item in items)
        {
            item.OrderId = orderId;
            await connection.ExecuteAsync(@"
                INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price)
                VALUES (@OrderId, @ProductId, @Quantity, @Price)",
                item, transaction);
        }

        await transaction.CommitAsync();
        return true;
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
}
```

#### 🎯 **Défi Mission 4 : Optimisations**

**Mission :** Optimisez vos requêtes pour de meilleures performances

**Ce que vous devez faire :**

- Éliminer les requêtes N+1 dans votre code
- Ajouter la pagination à votre recherche
- Créer une opération avec transaction
- Mesurer les temps d'exécution

**✅ Validation :** Code optimisé avec mesures de performance documentées

---

### 🧪 **Mission 5 : Tests simples**

#### 📚 **Ce qu'on va apprendre**

- Tester du code Dapper
- Mocker les connexions
- Tests avec base de données de test

#### 🔬 **Tests de base**

**Approche simple avec vraie DB de test :**

```csharp
[TestClass]
public class ProductRepositoryTests
{
    private string _testConnectionString = "Server=.;Database=MyShopTest;Trusted_Connection=true;";
    private ProductRepository _repository;

    [TestInitialize]
    public async Task Setup()
    {
        _repository = new ProductRepository(_testConnectionString);

        // Nettoyer et préparer la base de test
        using var connection = new SqlConnection(_testConnectionString);
        await connection.ExecuteAsync("DELETE FROM Products");
        await connection.ExecuteAsync("DELETE FROM Categories");

        // Ajouter des données de test
        await connection.ExecuteAsync("INSERT INTO Categories (Id, Name) VALUES (1, 'Test Category')");
    }

    [TestMethod]
    public async Task CreateAsync_ShouldCreateProduct()
    {
        // Arrange
        var product = new Product { Name = "Test Product", Price = 99.99m, CategoryId = 1 };

        // Act
        var id = await _repository.CreateAsync(product);

        // Assert
        Assert.IsTrue(id > 0);

        var createdProduct = await _repository.GetByIdAsync(id);
        Assert.IsNotNull(createdProduct);
        Assert.AreEqual("Test Product", createdProduct.Name);
    }

    [TestMethod]
    public async Task GetByNameAsync_ShouldReturnMatchingProducts()
    {
        // Arrange
        await _repository.CreateAsync(new Product { Name = "iPhone 14", Price = 999, CategoryId = 1 });
        await _repository.CreateAsync(new Product { Name = "Samsung Galaxy", Price = 899, CategoryId = 1 });

        // Act
        var iphones = await _repository.GetByNameAsync("iPhone");

        // Assert
        Assert.AreEqual(1, iphones.Count);
        Assert.AreEqual("iPhone 14", iphones[0].Name);
    }
}
```

**🔍 Test d'intégration simple :**

```csharp
[TestMethod]
public async Task SearchAsync_ShouldCombineFilters()
{
    // Arrange - Créer des produits de test
    await _repository.CreateAsync(new Product { Name = "Expensive iPhone", Price = 1200, CategoryId = 1 });
    await _repository.CreateAsync(new Product { Name = "Cheap Android", Price = 200, CategoryId = 1 });

    // Act - Rechercher avec plusieurs filtres
    var results = await _repository.SearchAsync(name: "iPhone", minPrice: 1000);

    // Assert
    Assert.AreEqual(1, results.Count);
    Assert.AreEqual("Expensive iPhone", results[0].Name);
}
```

#### 🎯 **Défi Mission 5 : Tests de base**

**Mission :** Écrivez des tests pour vos repositories

**Tests à créer :**

- Test de création
- Test de recherche
- Test de mise à jour
- Test avec paramètres multiples

**✅ Validation :** Suite de tests qui passent et couvrent vos méthodes principales

---

## 🏆 Projet final simple

### 💻 **Mini-application avec Dapper**

**💡 Idées d'applications :**

- Gestionnaire de contacts (contacts, groupes)
- Catalogue de films (films, genres, acteurs)
- Suivi de dépenses (dépenses, catégories)
- Gestionnaire de recettes (recettes, ingrédients)

**🎯 Fonctionnalités minimum :**

- 2-3 tables avec relations
- CRUD complet avec Dapper
- Recherche avec filtres multiples
- Quelques jointures
- Tests de base
- Interface simple (console ou API)

**📊 Ce qui sera évalué :**

- Requêtes SQL propres et sécurisées
- Pas d'injection SQL
- Jointures qui fonctionnent
- Performance correcte (pas de N+1)
- Tests qui passent

---

## 📚 Ressources utiles

### 📖 **Documentation et tutos :**

- [Dapper GitHub](https://github.com/DapperLib/Dapper) (exemples officiels)
- [Dapper tutorial](https://dappertutorial.net/step-by-step-tutorial) (très bien fait)

### ⚖️ **Quand utiliser Dapper vs EF Core :**

**🔥 Utilisez Dapper quand :**

- Vous voulez contrôler votre SQL
- Performance maximale nécessaire
- Requêtes complexes avec beaucoup de jointures
- Stored procedures existantes
- Base de données legacy avec noms bizarres

**🌟 Utilisez EF Core quand :**

- Développement rapide
- Application simple/moyenne
- Équipe junior en SQL
- Migrations automatiques voulues
- CRUD standard

**🎯 L'objectif : maîtriser Dapper pour les cas où vous en avez vraiment besoin !**
