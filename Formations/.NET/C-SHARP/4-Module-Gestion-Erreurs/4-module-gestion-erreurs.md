# Montée en compétence auto-rythmée - C#

## 📚 Module 4 : Gestion d'Erreurs

**🔗 Prérequis :** Module 1 - Fondamentaux ✅ + Module 2 - POO ✅ + Module 3 - Collections/LINQ ✅

### Objectifs

- Maîtriser try/catch/finally et les bonnes pratiques
- Créer des exceptions personnalisées appropriées
- Adopter une approche défensive (defensive programming)
- Intégrer un logging basique efficace

---

### Contenu théorique

- **Try/Catch/Finally** : syntaxe, bonnes pratiques, quand utiliser
- **Types d'exceptions** : système vs métier, hiérarchie d'exceptions
- **Exceptions personnalisées** : création, héritage, données contextuelles
- **Defensive programming** : validation des paramètres, guard clauses
- **Logging** : intégration avec ILogger, niveaux de log appropriés

### Exemple rapide

```csharp
// Exception personnalisée simple
public class InvalidOrderException : Exception
{
    public int OrderId { get; }

    public InvalidOrderException(int orderId)
        : base($"Order with ID {orderId} is not valid")
    {
        OrderId = orderId;
    }

    public InvalidOrderException(int orderId, Exception innerException)
        : base($"Order with ID {orderId} is not valid", innerException)
    {
        OrderId = orderId;
    }
}

// Classe utilitaire pour logger (simple)
public static class Logger
{
    public static void Info(string message) => Console.WriteLine($"[INFO] {message}");
    public static void Warning(string message) => Console.WriteLine($"[WARNING] {message}");
    public static void Error(string message) => Console.WriteLine($"[ERROR] {message}");
}

// Service avec gestion d'erreurs et logging
public class OrderService
{
    public decimal CalculateTotal(int orderId, List<OrderItem> items)
    {
        // Guard clauses - validation défensive
        if (orderId <= 0)
        {
            Logger.Warning($"Invalid order ID: {orderId}");
            throw new ArgumentException("Order ID must be positive", nameof(orderId));
        }

        if (items == null || !items.Any())
        {
            Logger.Warning($"Empty items list for order {orderId}");
            throw new ArgumentException("Order must have at least one item", nameof(items));
        }

        try
        {
            Logger.Info($"Calculating total for order {orderId} with {items.Count} items");

            decimal total = 0;
            foreach (var item in items)
            {
                if (item.Quantity <= 0 || item.Price < 0)
                {
                    Logger.Error($"Invalid item in order {orderId}: Quantity={item.Quantity}, Price={item.Price}");
                    throw new InvalidOrderException(orderId);
                }

                total += item.Quantity * item.Price;
            }

            Logger.Info($"Order {orderId} total calculated: {total:C}");
            return total;
        }
        catch (OverflowException ex)
        {
            Logger.Error($"Overflow error calculating order {orderId}: {ex.Message}");
            throw new InvalidOrderException(orderId, ex);
        }
        catch (Exception ex)
        {
            Logger.Error($"Unexpected error calculating order {orderId}: {ex.Message}");
            throw; // Rethrow pour les erreurs inattendues
        }
        finally
        {
            // Bloc finally : s'exécute TOUJOURS, peu importe si exception ou return
            Logger.Info($"Order {orderId} processing completed");
        }
    }
}
```

**Exemple concret avec finally pour libérer des ressources :**

```csharp
public class FileProcessor
{
    public string ReadData(string filePath)
    {
        StreamReader reader = null;

        try
        {
            Logger.Info($"Opening file: {filePath}");
            reader = new StreamReader(filePath);

            string content = reader.ReadToEnd();
            Logger.Info($"File read successfully: {content.Length} characters");

            return content;
        }
        catch (FileNotFoundException ex)
        {
            Logger.Error($"File not found: {filePath}");
            throw;
        }
        catch (IOException ex)
        {
            Logger.Error($"IO error reading file: {ex.Message}");
            throw;
        }
        finally
        {
            // TOUJOURS exécuté : fermeture du fichier même si exception
            if (reader != null)
            {
                reader.Close();
                Logger.Info("File closed successfully");
            }
        }
    }
}

// ⚡ Alternative moderne avec 'using' (équivalent à try-finally automatique)
public string ReadDataModern(string filePath)
{
    using (var reader = new StreamReader(filePath))
    {
        return reader.ReadToEnd();
    } // reader.Dispose() appelé automatiquement ici
}
```

---

### 📖 Ressources théoriques

- [Exception Handling in C# - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/)
- [Creating and Throwing Exceptions - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions)
- [Logging in .NET - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging)
- [Best Practices for Exception Handling - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions)

### 🎥 Vidéos recommandées

- [Exception Handling in C# - Microsoft Learn](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/exception-handling-csharp-for-beginners)

---

## 🎯 Projet fil rouge : TechEvent Manager - Phase 4

### 📋 Contexte

**Phases précédentes :**
- Phase 1-3 : Base fonctionnelle du système d'événements

**Phase 4 :** Le système plante régulièrement avec des données invalides. Il faut **sécuriser** toutes les opérations avec validation, exceptions custom et logging.

**Objectif :** Rendre le système robuste avec gestion d'erreurs professionnelle.

---

### 💻 Exercice pratique : Validation & Gestion d'erreurs robuste

**Objectif :** Ajouter validation défensive, exceptions personnalisées et logging au TechEvent Manager.

#### 🛡️ Architecture à implémenter :

```csharp
// Classe utilitaire pour logger (simple)
public static class Logger
{
    public static void Info(string message) => Console.WriteLine($"[INFO] {message}");
    public static void Warning(string message) => Console.WriteLine($"[WARNING] {message}");
    public static void Error(string message) => Console.WriteLine($"[ERROR] {message}");
}

// Exceptions personnalisées métier
public class EventFullException : Exception
{
    public int EventId { get; }
    public int Capacity { get; }

    public EventFullException(int eventId, int capacity)
        : base($"Event {eventId} is full (capacity: {capacity})")
    {
        EventId = eventId;
        Capacity = capacity;
    }
}

public class InvalidRegistrationException : Exception
{
    public string Reason { get; }

    public InvalidRegistrationException(string reason)
        : base($"Registration failed: {reason}")
    {
        Reason = reason;
    }

    public InvalidRegistrationException(string reason, Exception innerException)
        : base($"Registration failed: {reason}", innerException)
    {
        Reason = reason;
    }
}

// Event avec validation défensive
public abstract class Event : IBookable
{
    private List<Participant> _attendees;

    public int Id { get; }
    public string Title { get; }
    public DateTime Date { get; }
    public int Capacity { get; }
    public string Location { get; }

    protected Event(int id, string title, DateTime date, int capacity, string location)
    {
        // TODO: Guard clauses - Valider tous les paramètres
        // - id > 0
        // - title non vide
        // - date pas dans le passé (tolerance 1h)
        // - capacity > 0
        // - location non vide
        //
        // Lever ArgumentException ou InvalidEventDateException si invalide

        Id = id;
        Title = title;
        Date = date;
        Capacity = capacity;
        Location = location;
        _attendees = new List<Participant>();
    }

    public virtual bool Register(Participant participant)
    {
        // TODO: Mission 1 - Ajouter validation défensive
        // 1. Valider participant != null (lever ArgumentNullException)
        // 2. Valider participant.Email non vide avec string.IsNullOrWhiteSpace
        // 3. Logger avec Logger.Warning si validation échoue

        // TODO: Mission 2 - Vérifications métier avec try/catch
        try
        {
            // TODO: Vérifier si événement complet avec la méthode IsFull()
            //    - Si complet: logger un warning et lever EventFullException

            // TODO: Vérifier si email déjà inscrit dans _attendees (utiliser LINQ Any)
            //    - Si déjà inscrit: logger et lever InvalidRegistrationException

            // TODO: Ajouter le participant à la liste _attendees

            // TODO: Logger le succès de l'inscription avec Logger.Info

            return true;
        }
        catch (EventFullException)
        {
            throw; // Rethrow les exceptions métier
        }
        catch (InvalidRegistrationException)
        {
            throw; // Rethrow les exceptions métier
        }
        catch (Exception ex)
        {
            // TODO: Logger l'erreur système et wrapper dans InvalidRegistrationException
            throw new InvalidRegistrationException("System error during registration", ex);
        }
    }

    public bool IsFull() => _attendees.Count >= Capacity;

    public abstract decimal GetPrice();
}

// Service d'événements avec gestion d'erreurs
public class EventService
{
    private readonly EventSearchEngine _searchEngine;

    public EventService(EventSearchEngine searchEngine)
    {
        // TODO: Mission 3 - Validation défensive du paramètre
        // Vérifier que searchEngine n'est pas null
        // Utiliser l'opérateur ?? pour lever ArgumentNullException si null
        // Assigner à _searchEngine
    }

    public bool RegisterParticipant(int eventId, Participant participant)
    {
        // TODO: Mission 4 - Gestion d'erreurs avec try/catch
        //
        // Étapes:
        // 1. Logger le début de l'opération avec Logger.Info
        // 2. Dans un try:
        //    - Trouver l'événement avec _searchEngine.FindEventById
        //    - Si événement non trouvé (null), logger warning et lever ArgumentException
        //    - Appeler evt.Register(participant) et retourner le résultat
        // 3. Catch EventFullException : logger warning puis rethrow (throw;)
        // 4. Catch InvalidRegistrationException : logger warning puis rethrow
        // 5. Catch Exception générique : logger error puis rethrow
    }

    // BONUS : Méthode défensive qui ne crash jamais
    public List<Event> GetSafeEventList()
    {
        try
        {
            return _searchEngine.GetAllEvents();
        }
        catch (Exception ex)
        {
            Logger.Error($"Error fetching event list: {ex.Message}");
            return new List<Event>(); // Defensive: retourner liste vide plutôt que crash
        }
    }
}
```

---

### ✅ Critères d'acceptance

**Exceptions personnalisées :**
- [ ] EventFullException avec EventId, Capacity implémentée
- [ ] InvalidRegistrationException avec Reason implémentée
- [ ] Héritage correct d'Exception
- [ ] Constructeurs avec inner exception

**Validation défensive :**
- [ ] Guard clauses dans Event constructor (5 validations)
- [ ] Validation dans Register() (null, email vide, complet, doublons)
- [ ] ArgumentNullException dans EventService constructor

**Logging avec classe Logger :**
- [ ] Logger.Info pour les opérations réussies
- [ ] Logger.Warning pour les erreurs utilisateur (event full, doublons, not found)
- [ ] Logger.Error pour les erreurs système
- [ ] Messages clairs avec contexte (IDs, emails, etc.)

**Try/Catch :**
- [ ] Rethrow exceptions métier avec throw; (pas throw ex)
- [ ] Catch spécifique avant catch générique
- [ ] Pas de catch vide
- [ ] Messages d'erreur contextuels

---

### 🎓 Bonnes pratiques

#### **throw vs throw ex**

```csharp
// ❌ MAUVAIS : Perd la stack trace originale
catch (Exception ex)
{
    throw ex; // Reset la stack trace !
}

// ✅ BON : Préserve la stack trace
catch (EventFullException)
{
    throw; // Rethrow sans modification
}

// ✅ BON : Wrapper avec InnerException
catch (Exception ex)
{
    throw new InvalidRegistrationException("System error", ex);
}
```

#### **Guard clauses**

```csharp
// ❌ MAUVAIS : Imbrication profonde
public void Register(Participant p)
{
    if (p != null)
    {
        if (!string.IsNullOrEmpty(p.Email))
        {
            if (!IsFull())
            {
                _attendees.Add(p);
            }
        }
    }
}

// ✅ BON : Guard clauses, fail-fast
public void Register(Participant p)
{
    if (p == null)
        throw new ArgumentNullException(nameof(p));

    if (string.IsNullOrWhiteSpace(p.Email))
        throw new InvalidRegistrationException("Email required");

    if (IsFull())
        throw new EventFullException(Id, Capacity);

    _attendees.Add(p);
}
```

#### **Exceptions pour contrôle de flux**

```csharp
// ❌ MAUVAIS : Exception pour contrôle normal
try
{
    var evt = GetEvent(id);
    // ...
}
catch (EventNotFoundException)
{
    return null; // Cas normal traité par exception
}

// ✅ BON : TryGet pattern
if (!TryGetEvent(id, out var evt))
{
    return null; // Cas normal, pas d'exception
}
```

---

### 🧪 Tests unitaires

**Objectif :** Apprendre à tester les exceptions et les validations.

**Format** : 2 exemples fournis + 4 à implémenter vous-même

---

### 📝 Exemples fournis (2 tests)

Voici 2 exemples complets pour vous montrer comment tester les exceptions :

```csharp
[Fact]
public void Constructor_InvalidId_ThrowsArgumentException()
{
    // Arrange & Act & Assert
    var exception = Assert.Throws<ArgumentException>(() =>
        new Workshop(-1, "Test", DateTime.Now.AddDays(1), 20, "Paris", "Beginner", 2, null));

    Assert.Equal("id", exception.ParamName);
}

[Fact]
public void Register_FullEvent_ThrowsEventFullException()
{
    // Arrange
    var workshop = new Workshop(1, "Test", DateTime.Now.AddDays(1), 2, "Paris", "Beginner", 2, null);
    workshop.Register(new Participant("John", "john@test.com"));
    workshop.Register(new Participant("Jane", "jane@test.com"));

    // Act & Assert
    var exception = Assert.Throws<EventFullException>(() =>
        workshop.Register(new Participant("Bob", "bob@test.com")));

    Assert.Equal(1, exception.EventId);
    Assert.Equal(2, exception.Capacity);
}
```

---

### ✍️ Tests à implémenter vous-même (4 tests)

En vous inspirant des exemples, implémentez ces 4 tests pour valider la gestion d'erreurs :

**Test 3 : Constructor - Date passée**
```csharp
[Fact]
public void Constructor_PastDate_ThrowsInvalidEventDateException()
{
    // TODO: Implémenter ce test
    // Arrange : Créer une date passée avec DateTime.Now.AddDays(-5)
    // Act & Assert : Utiliser Assert.Throws<InvalidEventDateException>
    // Vérifier que exception.InvalidDate == pastDate
}
```

**Test 4 : Register - Participant null**
```csharp
[Fact]
public void Register_NullParticipant_ThrowsArgumentNullException()
{
    // TODO: Implémenter ce test
    // Créer un Workshop valide
    // Appeler Register(null)
    // Vérifier que ArgumentNullException est levée
}
```

**Test 5 : Register - Email en double**
```csharp
[Fact]
public void Register_DuplicateEmail_ThrowsInvalidRegistrationException()
{
    // TODO: Implémenter ce test
    // Créer un Workshop, inscrire "john@test.com"
    // Essayer d'inscrire un autre participant avec le même email
    // Vérifier que InvalidRegistrationException est levée
    // Vérifier que le message contient "already registered"
}
```

**Test 6 : Constructor - Titre invalide**
```csharp
[Fact]
public void Constructor_InvalidTitle_ThrowsArgumentException()
{
    // TODO: Implémenter ce test
    // Essayer de créer un Workshop avec titre null ou vide
    // Vérifier que ArgumentException est levée
}
```

**Indice** : Utilisez `Assert.Throws<TException>()` pour vérifier qu'une exception est bien levée.

---

### 💡 Indices

<details>
<summary>🔍 Indice 1 : Constructeur avec guard clauses</summary>

```csharp
protected Event(int id, string title, DateTime date, int capacity, string location, ILogger logger = null)
{
    // 1. Valider chaque paramètre avant affectation
    // 2. Lancer exceptions appropriées (ArgumentException, InvalidEventDateException)
    // 3. Affecter aux propriétés seulement si tout valide

    if (id <= 0)
        throw new ArgumentException("Event ID must be positive", nameof(id));

    // TODO: Autres validations...
}
```
</details>

<details>
<summary>🔍 Indice 2 : Register avec validation métier</summary>

```csharp
public virtual bool Register(Participant participant)
{
    // 1. Validation paramètres (null, email)
    // 2. Try/catch pour exceptions métier
    // 3. Vérifier IsFull() → EventFullException
    // 4. Vérifier doublons → InvalidRegistrationException
    // 5. Logger Info en cas de succès

    if (participant == null)
        throw new ArgumentNullException(nameof(participant));

    if (IsFull())
        throw new EventFullException(Id, Capacity);

    // TODO: Autres vérifications...
}
```
</details>

<details>
<summary>🔍 Indice 3 : EventService avec gestion d'erreurs</summary>

```csharp
public bool RegisterParticipant(int eventId, Participant participant)
{
    _logger.LogInformation("Registering {Email} to event {EventId}", participant?.Email, eventId);

    try
    {
        var evt = _searchEngine.GetEventById(eventId);
        // TODO: Vérifier si null
        return evt.Register(participant);
    }
    catch (EventFullException ex)
    {
        _logger.LogWarning(ex, "Event {EventId} is full", eventId);
        throw; // Rethrow exceptions métier
    }
    // TODO: Autres catches...
}
```
</details>

---

### 🎯 Questions d'entretien type

Maîtrisez ces concepts clés pour vos entretiens :

**1. Quand créer une exception personnalisée ?**

<details>
<summary>Voir la réponse</summary>

Créer une exception personnalisée quand :
- L'erreur est **spécifique au domaine métier** (ex: `EventFullException`, `InvalidRegistrationException`)
- Elle contient des **données contextuelles importantes** (ex: `EventId`, `Capacity`)
- Elle nécessite un **traitement particulier** par l'appelant

```csharp
// Exception personnalisée avec contexte métier
public class EventFullException : Exception
{
    public int EventId { get; }
    public int Capacity { get; }

    public EventFullException(int eventId, int capacity)
        : base($"Event {eventId} is full (capacity: {capacity})")
    {
        EventId = eventId;
        Capacity = capacity;
    }
}

// Utilisation
try
{
    RegisterParticipant(eventId, participant);
}
catch (EventFullException ex)
{
    // Traitement spécifique avec accès au contexte
    Logger.Warning($"Cannot register: event {ex.EventId} at full capacity ({ex.Capacity})");
}
```
</details>

**2. Différence entre throw et throw ex ?**

<details>
<summary>Voir la réponse</summary>

**`throw`** préserve la stack trace originale de l'exception. **`throw ex`** remplace la stack trace, perdant ainsi les informations de débogage originales. **Toujours utiliser `throw` pour rethrow**.

```csharp
try
{
    SomeMethod();
}
catch (Exception ex)
{
    // ❌ MAUVAIS : Perd la stack trace originale
    throw ex;
}

try
{
    SomeMethod();
}
catch (EventFullException)
{
    // ✅ BON : Préserve la stack trace
    throw;
}

try
{
    SomeMethod();
}
catch (Exception ex)
{
    // ✅ BON : Wrapper avec InnerException
    throw new InvalidRegistrationException("System error", ex);
}
```
</details>

**3. Que faire dans un bloc finally ?**

<details>
<summary>Voir la réponse</summary>

Le bloc `finally` doit servir au **nettoyage des ressources** (fermeture de fichiers, connexions) et contient du code qui doit **s'exécuter quoi qu'il arrive** (exception ou non). **Éviter les exceptions dans finally**.

```csharp
StreamReader reader = null;
try
{
    reader = new StreamReader("file.txt");
    var content = reader.ReadToEnd();
    return content;
}
catch (FileNotFoundException ex)
{
    Logger.Error($"File not found: {ex.Message}");
    throw;
}
finally
{
    // S'exécute TOUJOURS, même avec return ou exception
    if (reader != null)
    {
        reader.Close();
        Logger.Info("File closed");
    }
}

// ✅ Alternative moderne avec using (équivalent à try-finally)
using (var reader = new StreamReader("file.txt"))
{
    return reader.ReadToEnd();
} // Dispose() appelé automatiquement
```
</details>

**4. Quand utiliser ArgumentException vs InvalidOperationException ?**

<details>
<summary>Voir la réponse</summary>

**ArgumentException** : pour les **paramètres invalides** passés à une méthode.
**InvalidOperationException** : quand l'objet est dans un **état incorrect** pour l'opération demandée.

```csharp
public class Event
{
    private List<Participant> _attendees = new();
    public int Capacity { get; }
    public bool IsCancelled { get; private set; }

    // ArgumentException : paramètre invalide
    public void Register(Participant participant)
    {
        if (participant == null)
            throw new ArgumentNullException(nameof(participant));

        if (string.IsNullOrWhiteSpace(participant.Email))
            throw new ArgumentException("Email required", nameof(participant));

        // InvalidOperationException : état incorrect
        if (IsCancelled)
            throw new InvalidOperationException("Cannot register to cancelled event");

        if (_attendees.Count >= Capacity)
            throw new InvalidOperationException("Event is full");

        _attendees.Add(participant);
    }
}
```
</details>

**5. Comment éviter les exceptions pour le contrôle de flux ?**

<details>
<summary>Voir la réponse</summary>

Les exceptions doivent gérer des **situations exceptionnelles**, pas le flux normal. Utiliser des **alternatives** : TryParse, vérifications préalables (if), méthodes qui retournent bool + out parameter, pattern matching.

```csharp
// ❌ MAUVAIS : Exception pour contrôle normal
try
{
    var evt = GetEvent(id);
    // traitement
}
catch (EventNotFoundException)
{
    return null; // Cas normal traité par exception
}

// ✅ BON : TryGet pattern
public bool TryGetEvent(int id, out Event? evt)
{
    evt = _events.FirstOrDefault(e => e.Id == id);
    return evt != null;
}

if (TryGetEvent(id, out var evt))
{
    // traitement
}

// ✅ BON : Vérification préalable
if (int.TryParse(input, out var number))
{
    // utiliser number
}

// ✅ BON : Pattern matching
var result = input switch
{
    null or "" => "Empty",
    _ => "Valid"
};
```
</details>

**6. Qu'est-ce que le defensive programming ?**

<details>
<summary>Voir la réponse</summary>

Le **defensive programming** consiste à **valider systématiquement les entrées**, utiliser des **guard clauses**, adopter une approche **fail-fast**, **ne jamais assumer** que les données sont valides, et **vérifier les préconditions**.

```csharp
public class Event
{
    // Guard clauses : fail-fast
    public Event(int id, string title, DateTime date, int capacity)
    {
        // Valider AVANT d'affecter
        if (id <= 0)
            throw new ArgumentException("ID must be positive", nameof(id));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title required", nameof(title));

        if (date < DateTime.Now)
            throw new ArgumentException("Date must be in future", nameof(date));

        if (capacity <= 0)
            throw new ArgumentException("Capacity must be positive", nameof(capacity));

        Id = id;
        Title = title;
        Date = date;
        Capacity = capacity;
    }

    // Validation défensive dans les méthodes
    public bool Register(Participant participant)
    {
        // Guard clauses
        if (participant == null)
            throw new ArgumentNullException(nameof(participant));

        if (string.IsNullOrWhiteSpace(participant.Email))
            throw new ArgumentException("Email required");

        // Préconditions métier
        if (IsFull())
            throw new EventFullException(Id, Capacity);

        // Code métier seulement si tout est valide
        _attendees.Add(participant);
        return true;
    }
}
```
</details>

---

### ✅ Validation du module

**Checklist TechEvent Manager - Phase 4 :**
- [ ] 2 exceptions personnalisées implémentées (EventFullException, InvalidRegistrationException)
- [ ] Guard clauses dans constructeur Event (5 validations)
- [ ] Register() avec validation complète (null, email, doublons, full)
- [ ] EventService avec try/catch et logging approprié
- [ ] Classe Logger simple utilisée (Info, Warning, Error)
- [ ] Tests : 6 tests essentiels passent, coverage ≥ 70%
- [ ] Aucun catch vide, toujours `throw;` pour rethrow
- [ ] Je réponds aux 6 questions d'entretien

---

**⬅️ [Module 3 - Collections et LINQ](../3-Module-Collections-Linq/3-module-collections-linq.md) | [Retour au MECA](../README.md) | [Module 5 - C# Moderne](../5-Module-Csharp-Moderne/5-module-Csharp-moderne.md) ➡️**

---
