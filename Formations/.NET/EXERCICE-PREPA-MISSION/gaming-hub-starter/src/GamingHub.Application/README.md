# 🎯 GamingHub.Application

**Couche Application** - Services métier et cas d'usage

## 📋 À implémenter ici

### Services (Services/)

- `ITournamentService.cs` + `TournamentService.cs`
- `ITeamService.cs` + `TeamService.cs`
- `IAuthService.cs` + `AuthService.cs`

### DTOs (DTOs/)

- `CreateTournamentDto.cs`, `TournamentDto.cs`
- `CreateTeamDto.cs`, `TeamDto.cs`
- `RegisterDto.cs`, `LoginDto.cs`, `AuthResponseDto.cs`

### Validators (Validators/)

- `CreateTournamentDtoValidator.cs` - FluentValidation
- `CreateTeamDtoValidator.cs`
- `RegisterDtoValidator.cs`
- `LoginDtoValidator.cs`

### Interfaces (Interfaces/)

- `ITournamentRepository.cs` - Contrat repository
- `ITeamRepository.cs`
- `IUserRepository.cs`

## ⚠️ Règles importantes

- ✅ **Dépend uniquement de Domain**
- ❌ **Ne connaît PAS Infrastructure**
- ✅ **Utilise FluentValidation** pour valider les DTOs
- ✅ **Services contiennent la logique applicative** (orchestration)

## 📝 Exemple de service

```csharp
public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;
    private readonly ILogger<TournamentService> _logger;

    public TournamentService(
        ITournamentRepository tournamentRepository,
        ILogger<TournamentService> logger)
    {
        _tournamentRepository = tournamentRepository;
        _logger = logger;
    }

    public async Task<TournamentDto> CreateAsync(CreateTournamentDto dto)
    {
        _logger.LogInformation("Creating tournament {Name}", dto.Name);

        // Créer l'entité Domain
        var tournament = new Tournament(dto.Name, dto.Game, dto.Description, ...);

        // Persister via repository
        await _tournamentRepository.AddAsync(tournament);

        // Retourner DTO
        return MapToDto(tournament);
    }
}
```

## 📝 Exemple de validator

```csharp
public class CreateTournamentDtoValidator : AbstractValidator<CreateTournamentDto>
{
    public CreateTournamentDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200);

        RuleFor(x => x.StartDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Start date must be in the future");

        RuleFor(x => x.MaxTeams)
            .GreaterThan(0)
            .LessThanOrEqualTo(64);
    }
}
```
