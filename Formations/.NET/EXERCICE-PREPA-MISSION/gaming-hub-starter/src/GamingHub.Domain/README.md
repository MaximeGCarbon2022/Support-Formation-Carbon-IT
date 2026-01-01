# 🎯 GamingHub.Domain

**Couche Domain** - Logique métier pure

## 📋 À implémenter ici

### Entités (Entities/)

- `Tournament.cs` - Tournoi esport
- `Team.cs` - Équipe de joueurs
- `User.cs` - Utilisateur (joueur/organisateur/admin)
- `TeamMember.cs` - Membre d'une équipe
- `TournamentRegistration.cs` - Inscription d'une équipe à un tournoi

### Enums (Enums/)

- `TournamentStatus.cs` - Draft, Open, InProgress, Completed, Cancelled
- `UserRole.cs` - Player, Organizer, Admin

### Exceptions (Exceptions/)

- `DomainException.cs` - Exception métier de base
- `TournamentFullException.cs` - Tournoi complet
- `InvalidTeamSizeException.cs` - Équipe trop petite/grande

## ⚠️ Règles importantes

- ❌ **Aucune dépendance externe** - Le Domain doit rester pur
- ❌ **Pas de dépendance vers EF Core, ASP.NET, etc.**
- ✅ **Uniquement des types .NET de base** (string, DateTime, Guid, etc.)
- ✅ **Logique métier dans les entités** (pas dans les controllers)

## 📝 Exemples de règles métier

```csharp
// Dans Tournament.cs
public void RegisterTeam(Team team)
{
    if (Status != TournamentStatus.Open)
        throw new DomainException("Tournament is not open for registration");

    if (IsFull())
        throw new TournamentFullException($"Tournament {Name} is full");

    if (!team.HasMinimumPlayers(3))
        throw new InvalidTeamSizeException("Team must have at least 3 players");
}
```
