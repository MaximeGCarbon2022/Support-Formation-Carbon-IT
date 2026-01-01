# 🎯 GamingHub.Infrastructure

**Couche Infrastructure** - Accès aux données et implémentations techniques

## 📋 À implémenter ici

### Data (Data/)

- `ApplicationDbContext.cs` - DbContext EF Core
- Configurations EF Core (Configurations/)
  - `TournamentConfiguration.cs`
  - `TeamConfiguration.cs`
  - `UserConfiguration.cs`

### Repositories (Repositories/)

- `TournamentRepository.cs` - Implémente `ITournamentRepository`
- `TeamRepository.cs` - Implémente `ITeamRepository`
- `UserRepository.cs` - Implémente `IUserRepository`

## ⚠️ Règles importantes

- ✅ **Implémente les interfaces de Application**
- ✅ **Dépend de Application et Domain**
- ✅ **Utilise EF Core** pour la persistance
- ✅ **Configurations Fluent API** (pas d'attributs dans Domain)

## 📝 Exemple de DbContext

```csharp
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tournament> Tournaments => Set<Tournament>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
```

## 📝 Exemple de configuration EF Core

```csharp
public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        builder.ToTable("Tournaments");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Status)
            .HasConversion<string>();

        // Relations
        builder.HasMany(t => t.Registrations)
            .WithOne()
            .HasForeignKey("TournamentId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

## 📝 Exemple de repository

```csharp
public class TournamentRepository : ITournamentRepository
{
    private readonly ApplicationDbContext _context;

    public TournamentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Tournament?> GetByIdAsync(Guid id)
    {
        return await _context.Tournaments
            .Include(t => t.Registrations)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(Tournament tournament)
    {
        await _context.Tournaments.AddAsync(tournament);
        await _context.SaveChangesAsync();
    }
}
```

## 🗄️ Migrations

```bash
# Créer une migration
dotnet ef migrations add InitialCreate --project . --startup-project ../GamingHub.API

# Appliquer les migrations
dotnet ef database update --project . --startup-project ../GamingHub.API
```
