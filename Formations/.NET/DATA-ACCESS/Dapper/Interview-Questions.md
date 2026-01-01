# 🎤 Questions techniques pour entretiens/missions

## Q1 : Quelle est la différence entre Dapper et Entity Framework Core ?

**Réponse attendue :**

- **Dapper** : Micro-ORM, vous écrivez le SQL, mapping automatique uniquement
  - Avantages : Performance maximale, contrôle total du SQL
  - Inconvénients : Plus de code à écrire, pas de migrations automatiques

- **EF Core** : ORM complet, génère le SQL pour vous
  - Avantages : Développement rapide, migrations automatiques, change tracking
  - Inconvénients : SQL généré parfois non optimal, courbe d'apprentissage

**Réponse courte :** "Dapper pour la performance et le contrôle, EF Core pour la productivité"

---

## Q2 : Pourquoi utiliser Dapper plutôt qu'ADO.NET pur ?

**Réponse attendue :**

**ADO.NET pur :**
```csharp
// Beaucoup de code boilerplate
var command = new SqlCommand(sql, connection);
var reader = command.ExecuteReader();
var products = new List<Product>();
while (reader.Read())
{
    products.Add(new Product
    {
        Id = reader.GetInt32(0),
        Name = reader.GetString(1),
        Price = reader.GetDecimal(2)
    });
}
```

**Dapper :**
```csharp
// Mapping automatique, moins de code
var products = await connection.QueryAsync<Product>(sql);
```

**Réponse courte :** "Dapper = performance d'ADO.NET + productivité du mapping automatique"

---

## Q3 : Comment évitez-vous les injections SQL avec Dapper ?

**Réponse attendue :**

```csharp
// ❌ Mauvais - injection SQL
var sql = $"SELECT * FROM Users WHERE Username = '{username}'";

// ✅ Bon - paramètres sécurisés
var sql = "SELECT * FROM Users WHERE Username = @Username";
var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
```

**Réponse courte :** "Toujours utiliser des paramètres avec @, jamais de concaténation de strings"

---

## Q4 : Comment gérer les transactions avec Dapper ?

**Réponse attendue :**

```csharp
using var connection = new SqlConnection(_connectionString);
await connection.OpenAsync();
using var transaction = await connection.BeginTransactionAsync();

try
{
    await connection.ExecuteAsync(sql1, param1, transaction);
    await connection.ExecuteAsync(sql2, param2, transaction);
    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync();
    throw;
}
```

**Réponse courte :** "BeginTransaction, passer la transaction à chaque requête, Commit/Rollback"

---

## Q5 : Qu'est-ce que le problème N+1 et comment l'éviter ?

**Réponse attendue :**

**Problème :**
```csharp
// 1 requête pour les produits + N requêtes pour les catégories
var products = await GetAllProductsAsync(); // 1 requête
foreach (var product in products)
{
    product.Category = await GetCategoryAsync(product.CategoryId); // N requêtes
}
```

**Solution :**
```csharp
// 1 seule requête avec JOIN
var sql = @"
    SELECT p.*, c.Id, c.Name
    FROM Products p
    INNER JOIN Categories c ON p.CategoryId = c.Id";

var products = await connection.QueryAsync<Product, Category, Product>(
    sql, (product, category) => { product.Category = category; return product; });
```

**Réponse courte :** "Éviter les requêtes en boucle, utiliser des JOIN pour tout récupérer en une fois"

---

## Q6 : Comment faire du multi-mapping avec Dapper ?

**Réponse attendue :**

```csharp
var sql = @"
    SELECT p.Id, p.Name, p.Price, c.Id, c.Name
    FROM Products p
    INNER JOIN Categories c ON p.CategoryId = c.Id";

var products = await connection.QueryAsync<Product, Category, Product>(
    sql,
    (product, category) =>
    {
        product.Category = category;
        return product;
    },
    splitOn: "Id"); // Dapper split ici pour séparer Product et Category
```

**Point clé :** Le `splitOn` indique où Dapper doit "couper" pour créer le second objet.

---

## Q7 : Le projet utilise déjà EF Core, pourquoi ajouter Dapper ?

**Réponse pragmatique :**

"On peut combiner les deux ! EF Core pour le CRUD standard, Dapper pour les requêtes complexes ou critiques en performance."

**Exemple :**
```csharp
public class ProductService
{
    private readonly AppDbContext _context; // EF Core
    private readonly IDbConnection _connection; // Dapper

    // CRUD simple avec EF Core
    public async Task<Product> GetByIdAsync(int id)
        => await _context.Products.FindAsync(id);

    // Rapport complexe avec Dapper
    public async Task<List<SalesReport>> GetMonthlySalesAsync()
        => (await _connection.QueryAsync<SalesReport>(
            "sp_GetMonthlySales",
            commandType: CommandType.StoredProcedure)).ToList();
}
```

---

## Q8 : Comment testez-vous du code utilisant Dapper ?

**Réponse attendue :**

**Approche 1 : Base de test réelle (recommandé)**
```csharp
[TestInitialize]
public async Task Setup()
{
    // Utiliser une vraie DB de test (SQLite en mémoire par exemple)
    _connection = new SqliteConnection("Data Source=:memory:");
    await _connection.OpenAsync();
    await _connection.ExecuteAsync("CREATE TABLE Products (...)");
}
```

**Approche 2 : Abstraction avec interface**
```csharp
public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
}

// Tests avec mock du repository
var mockRepo = new Mock<IProductRepository>();
```

**Réponse courte :** "Préférer une vraie DB de test, ou abstraire avec des interfaces pour mocker"

---

## Q9 : La requête Dapper est lente, comment diagnostiquer ?

**Réponse pratique :**

1. **Activer le profiling SQL**
```csharp
// Utiliser un outil comme MiniProfiler
var stopwatch = Stopwatch.StartNew();
var products = await connection.QueryAsync<Product>(sql);
stopwatch.Stop();
_logger.LogInformation("Query took {Ms}ms", stopwatch.ElapsedMilliseconds);
```

2. **Vérifier le SQL généré**
   - Copier le SQL avec les paramètres dans SSMS/Azure Data Studio
   - Vérifier le plan d'exécution
   - Ajouter des index si nécessaire

3. **Problèmes fréquents :**
   - Pas d'index sur les colonnes du WHERE/JOIN
   - SELECT * au lieu de colonnes spécifiques
   - Problème N+1

---

## Q10 : Comment gérer les connexions avec Dapper ?

**Réponse attendue :**

**✅ Bon : using + pool automatique**
```csharp
public async Task<List<Product>> GetAllAsync()
{
    using var connection = new SqlConnection(_connectionString);
    // Pas besoin d'OpenAsync, Dapper le fait automatiquement
    return (await connection.QueryAsync<Product>(sql)).ToList();
}
// Connection retournée au pool automatiquement
```

**❌ Mauvais : réutiliser la même connection**
```csharp
private SqlConnection _connection; // NON ! Problème de threading

public async Task<List<Product>> GetAllAsync()
{
    return (await _connection.QueryAsync<Product>(sql)).ToList();
}
```

**Réponse courte :** "Toujours utiliser `using` pour les connexions, le pool ADO.NET gère la réutilisation"

---

## 📋 Checklist de préparation

Avant un entretien/mission sur Dapper, assurez-vous de pouvoir :

- [ ] Expliquer la différence Dapper vs EF Core
- [ ] Écrire une requête avec paramètres sécurisés
- [ ] Faire un multi-mapping avec JOIN
- [ ] Expliquer le problème N+1
- [ ] Gérer une transaction
- [ ] Appeler une stored procedure
- [ ] Expliquer comment tester du code Dapper
- [ ] Diagnostiquer une requête lente
