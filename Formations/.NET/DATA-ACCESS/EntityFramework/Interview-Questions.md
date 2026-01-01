# 🎤 Questions techniques pour entretiens/missions

## Q1 : Qu'est-ce qu'Entity Framework Core et pourquoi l'utiliser ?

**Réponse attendue :**

- **EF Core** : ORM (Object-Relational Mapper) qui mappe les objets C# vers des tables SQL
  - Avantages : Développement rapide, LINQ, migrations automatiques, change tracking
  - Inconvénients : Courbe d'apprentissage, peut générer du SQL non optimal

**Réponse courte :** "EF Core transforme vos classes C# en tables SQL et génère les requêtes automatiquement"

---

## Q2 : Quelle est la différence entre DbContext et DbSet ?

**Réponse attendue :**

```csharp
public class ShopContext : DbContext  // Le contexte = la connexion à la DB
{
    public DbSet<Product> Products { get; set; }  // DbSet = une table
    public DbSet<Category> Categories { get; set; }
}
```

- **DbContext** : Représente une session avec la base de données, gère les connexions et transactions
- **DbSet** : Représente une collection (table) d'entités, permet de faire des requêtes LINQ

**Réponse courte :** "DbContext = la connexion, DbSet = une table"

---

## Q3 : Comment fonctionnent les migrations EF Core ?

**Réponse attendue :**

```bash
# Créer une migration (snapshot des changements)
dotnet ef migrations add AddProductTable

# Appliquer les migrations (créer/modifier la DB)
dotnet ef database update

# Supprimer la dernière migration
dotnet ef migrations remove
```

**Explication :** Les migrations créent des fichiers C# qui décrivent les changements de schéma. EF Core peut alors créer ou mettre à jour la base de données automatiquement.

**Réponse courte :** "Les migrations génèrent et appliquent automatiquement les changements de schéma de base de données"

---

## Q4 : Quelle est la différence entre Include et ThenInclude ?

**Réponse attendue :**

```csharp
// Include : charger la relation directe
var blogs = await context.Blogs
    .Include(b => b.Posts)  // Charger les posts du blog
    .ToListAsync();

// ThenInclude : charger une relation imbriquée
var blogs = await context.Blogs
    .Include(b => b.Posts)
        .ThenInclude(p => p.Comments)  // Charger les commentaires des posts
    .ToListAsync();
```

**Réponse courte :** "Include pour les relations directes, ThenInclude pour les relations imbriquées"

---

## Q5 : Qu'est-ce que le lazy loading et pourquoi l'éviter ?

**Réponse attendue :**

**Lazy Loading :**
```csharp
// Avec lazy loading activé
var blog = await context.Blogs.FirstAsync();
var posts = blog.Posts;  // Requête SQL automatique en arrière-plan
```

**Problème :** Peut générer des centaines de requêtes SQL sans qu'on s'en rende compte (problème N+1)

**Solution : Eager Loading avec Include**
```csharp
var blog = await context.Blogs
    .Include(b => b.Posts)  // Tout charger en une seule requête
    .FirstAsync();
```

**Réponse courte :** "Lazy loading charge les données à la demande, mais peut créer des problèmes de performance (N+1)"

---

## Q6 : Quelle est la différence entre Add, Attach, et Update ?

**Réponse attendue :**

```csharp
// Add : Nouvelle entité à insérer
var product = new Product { Name = "New Product", Price = 99 };
context.Products.Add(product);
await context.SaveChangesAsync();  // INSERT

// Attach : Entité existante non trackée
var product = new Product { Id = 5, Name = "Updated", Price = 150 };
context.Products.Attach(product);  // EF Core commence à tracker
context.Entry(product).State = EntityState.Modified;
await context.SaveChangesAsync();  // UPDATE

// Update : Marque toute l'entité comme modifiée
var product = new Product { Id = 5, Name = "Updated", Price = 150 };
context.Products.Update(product);
await context.SaveChangesAsync();  // UPDATE tous les champs
```

**Réponse courte :** "Add = INSERT, Attach = tracker une entité, Update = UPDATE tous les champs"

---

## Q7 : Comment éviter le problème N+1 avec EF Core ?

**Réponse attendue :**

**❌ Problème N+1 :**
```csharp
var categories = await context.Categories.ToListAsync(); // 1 requête
foreach (var category in categories)
{
    var products = category.Products.ToList(); // N requêtes !
}
```

**✅ Solution 1 : Include**
```csharp
var categories = await context.Categories
    .Include(c => c.Products)  // 1 seule requête avec JOIN
    .ToListAsync();
```

**✅ Solution 2 : Select**
```csharp
var data = await context.Categories
    .Select(c => new
    {
        Category = c,
        Products = c.Products
    })
    .ToListAsync();
```

**Réponse courte :** "Utiliser Include ou Select pour charger toutes les données en une seule requête"

---

## Q8 : Qu'est-ce qu'AsNoTracking et quand l'utiliser ?

**Réponse attendue :**

```csharp
// Sans AsNoTracking (par défaut)
var products = await context.Products.ToListAsync();
// EF Core track les changements → plus lent

// Avec AsNoTracking
var products = await context.Products
    .AsNoTracking()  // Plus rapide !
    .ToListAsync();
// EF Core ne track pas → idéal pour la lecture seule
```

**Quand utiliser :**
- ✅ Affichage de données (read-only)
- ✅ APIs GET qui retournent des données
- ✅ Rapports et exports
- ❌ Quand vous devez modifier les données après

**Réponse courte :** "AsNoTracking désactive le change tracking pour améliorer les performances en lecture seule"

---

## Q9 : Comment gérer les transactions avec EF Core ?

**Réponse attendue :**

```csharp
using var transaction = await context.Database.BeginTransactionAsync();

try
{
    // Opération 1
    var order = new Order { CustomerId = 1, Total = 150 };
    context.Orders.Add(order);
    await context.SaveChangesAsync();

    // Opération 2
    var payment = new Payment { OrderId = order.Id, Amount = 150 };
    context.Payments.Add(payment);
    await context.SaveChangesAsync();

    // Tout a réussi
    await transaction.CommitAsync();
}
catch
{
    // Erreur : annuler tout
    await transaction.RollbackAsync();
    throw;
}
```

**Réponse courte :** "BeginTransaction, opérations + SaveChanges, puis Commit ou Rollback"

---

## Q10 : Quelle est la différence entre Find et FirstOrDefault ?

**Réponse attendue :**

```csharp
// Find : cherche par clé primaire, check d'abord le cache
var product = await context.Products.FindAsync(5);
// Avantage : Si l'entité est déjà trackée, pas de requête SQL

// FirstOrDefault : toujours fait une requête SQL
var product = await context.Products
    .FirstOrDefaultAsync(p => p.Id == 5);
// Avantage : Permet des conditions complexes
```

**Quand utiliser quoi :**
- `Find` : Recherche simple par ID, peut éviter une requête SQL
- `FirstOrDefault` : Recherche avec conditions complexes ou Include

**⚠️ Attention avec les soft deletes :**
`FindAsync` n'applique PAS les QueryFilters ! Si vous utilisez des soft deletes, préférez `FirstOrDefaultAsync`.

```csharp
// Avec soft delete + QueryFilter
var product = await context.Products.FindAsync(5);
// ⚠️ Peut retourner un produit supprimé (IsDeleted = true) depuis le cache !

var product = await context.Products.FirstOrDefaultAsync(p => p.Id == 5);
// ✅ Respecte le QueryFilter, ne retourne pas les supprimés
```

**Réponse courte :** "Find cherche par ID et utilise le cache, FirstOrDefault fait toujours une requête SQL"

---

## Q11 : Comment configurer une relation many-to-many en EF Core ?

**Réponse attendue :**

**Approche moderne (EF Core 5+) :**
```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Course> Courses { get; set; }
}

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; }
}

// EF Core crée automatiquement la table de jointure !
```

**Approche explicite (avec table de jointure) :**
```csharp
public class StudentCourse
{
    public int StudentId { get; set; }
    public Student Student { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }

    public DateTime EnrolledDate { get; set; } // Données supplémentaires
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<StudentCourse>()
        .HasKey(sc => new { sc.StudentId, sc.CourseId });
}
```

**Réponse courte :** "EF Core 5+ gère automatiquement les many-to-many, sinon créer une entité de jointure"

---

## Q12 : Comment optimiser une requête EF Core lente ?

**Réponse pratique :**

**1. Vérifier le SQL généré**
```csharp
// Activer le logging
optionsBuilder
    .UseSqlServer(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information);

// Voir le SQL dans les logs
var products = await context.Products.Where(p => p.Price > 100).ToListAsync();
```

**2. Optimisations courantes**
```csharp
// ❌ Mauvais : récupérer toutes les colonnes
var products = await context.Products.ToListAsync();

// ✅ Bon : projection (Select) pour récupérer uniquement ce qui est nécessaire
var products = await context.Products
    .Select(p => new { p.Id, p.Name, p.Price })
    .ToListAsync();

// ✅ Bon : AsNoTracking si lecture seule
var products = await context.Products
    .AsNoTracking()
    .ToListAsync();

// ✅ Bon : Pagination
var products = await context.Products
    .Skip(20)
    .Take(10)
    .ToListAsync();
```

**3. Éviter les problèmes fréquents**
- Pas d'Include inutiles
- Pas de requêtes en boucle
- Utiliser des index sur les colonnes filtrées

**Réponse courte :** "Logger le SQL, utiliser Select/AsNoTracking/Pagination, éviter les Include inutiles"

---

## Q13 : Quelle est la différence entre SaveChanges et SaveChangesAsync ?

**Réponse attendue :**

```csharp
// SaveChanges : synchrone (bloque le thread)
context.Products.Add(product);
context.SaveChanges();  // Bloque jusqu'à ce que la DB réponde

// SaveChangesAsync : asynchrone (libère le thread)
context.Products.Add(product);
await context.SaveChangesAsync();  // Le thread peut faire autre chose
```

**Quand utiliser quoi :**
- `SaveChangesAsync` : Toujours dans les applications web/API (meilleure scalabilité)
- `SaveChanges` : Applications console simples ou scripts

**Réponse courte :** "SaveChangesAsync est asynchrone et préférable pour les apps web (meilleure performance)"

---

## Q14 : Comment gérer les soft deletes avec EF Core ?

**Réponse attendue :**

**Approche 1 : Propriété IsDeleted**
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }  // Soft delete
}

// Global query filter
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Product>()
        .HasQueryFilter(p => !p.IsDeleted);  // Filtrer automatiquement
}

// Utilisation
product.IsDeleted = true;
await context.SaveChangesAsync();  // UPDATE, pas DELETE

// Ignorer le filtre si besoin
var allProducts = await context.Products
    .IgnoreQueryFilters()  // Inclure les supprimés
    .ToListAsync();
```

**Réponse courte :** "Ajouter une propriété IsDeleted + QueryFilter global pour filtrer automatiquement"

---

## Q15 : Comment tester du code utilisant EF Core ?

**Réponse attendue :**

**Approche 1 : InMemory Database**
```csharp
var options = new DbContextOptionsBuilder<ShopContext>()
    .UseInMemoryDatabase(databaseName: "TestDb")
    .Options;

await using var context = new ShopContext(options);

// Ajouter des données de test
context.Products.Add(new Product { Name = "Test", Price = 100 });
await context.SaveChangesAsync();

// Tester
var service = new ProductService(context);
var result = await service.GetExpensiveProductsAsync(50);

Assert.AreEqual(1, result.Count);
```

**Approche 2 : SQLite en mémoire (plus réaliste)**
```csharp
var connection = new SqliteConnection("DataSource=:memory:");
connection.Open();

var options = new DbContextOptionsBuilder<ShopContext>()
    .UseSqlite(connection)
    .Options;

await using var context = new ShopContext(options);
await context.Database.EnsureCreatedAsync();
```

**Réponse courte :** "InMemory pour les tests rapides, SQLite en mémoire pour plus de réalisme"

---

## 📋 Checklist de préparation

Avant un entretien/mission sur EF Core, assurez-vous de pouvoir :

- [ ] Expliquer la différence DbContext vs DbSet
- [ ] Créer et appliquer des migrations
- [ ] Utiliser Include et ThenInclude correctement
- [ ] Expliquer et éviter le problème N+1
- [ ] Utiliser AsNoTracking quand c'est approprié
- [ ] Gérer les transactions
- [ ] Configurer des relations (1-n, n-n)
- [ ] Optimiser une requête lente
- [ ] Tester du code avec EF Core
- [ ] Expliquer Find vs FirstOrDefault
