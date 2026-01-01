# Montée en compétence auto-rythmée - C#

## 📚 Module 5 : C# Moderne (C# 8-11-12)

**🔗 Prérequis :** Module 1-4 ✅ (Fondamentaux, POO, Collections/LINQ, Gestion d'Erreurs)

### Objectifs

- Utiliser les fonctionnalités essentielles du C# moderne
- Écrire du code plus concis et sûr
- Se préparer au C# des projets actuels (2024-2025)
- Maîtriser 4-5 patterns modernes clés

---

### Contenu théorique

- **C# 8** : Nullable Reference Types, Switch Expressions, Pattern Matching
- **C# 9** : Records, Init-only Properties
- **C# 10** : File-scoped Namespaces, Global Using
- **C# 11** : Raw String Literals, Required Members
- **C# 12** : Primary Constructors, Collection Expressions

### Exemple rapide

```csharp
// C# 10 - File-scoped namespace
namespace ModernCSharp.Examples;

// C# 9 - Records avec C# 11 - Required members
public record Customer
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public int Age { get; init; }

    // C# 8 - Pattern matching
    public string GetAgeCategory() => Age switch
    {
        < 18 => "Minor",
        >= 18 and < 65 => "Adult",
        >= 65 => "Senior"
    };
}

// C# 12 - Primary constructor
public class OrderService(ILogger<OrderService> logger)
{
    // C# 8 - Nullable reference types
    public Order? FindOrder(string? orderId)
    {
        // C# 8 - Switch expression
        return orderId switch
        {
            null or "" => null,
            _ => new Order { Id = orderId }
        };
    }
}

// C# 12 - Collection expressions
public class ModernCollections
{
    private readonly List<string> _statuses = ["pending", "processing", "completed"];
    private readonly Dictionary<string, int> _priorities = [];

    // C# 11 - Raw string literals (pratique pour JSON, SQL, etc.)
    public string GetJsonTemplate() => """
        {
            "name": "Event",
            "status": "pending",
            "data": {
                "capacity": 100
            }
        }
        """;
}
```

---

### 📖 Ressources théoriques

- [What's new in C# 12 - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12)
- [What's new in C# 11 - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11)
- [What's new in C# 10 - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10)
- [What's new in C# 9 - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9)
- [What's new in C# 8 - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8)

---

## 🎯 Projet fil rouge : TechEvent Manager - Phase 5

### 📋 Contexte

**Phases précédentes :**
- Phase 1-4 : Système d'événements fonctionnel avec validation et gestion d'erreurs

**Phase 5 :** Le code fonctionne mais utilise encore de l'ancien C#. Il faut **moderniser** avec C# 8-12 pour plus de sûreté, concision et maintenabilité.

**Objectif :** Moderniser TechEvent Manager avec nullable reference types, records, pattern matching, collection expressions.

---

### 💻 Exercice pratique : Modernisation du TechEvent Manager

**Objectif :** Appliquer progressivement les fonctionnalités C# modernes au système d'événements.

#### Code de base à moderniser (Phases 2-4) :

```csharp
using System;
using System.Collections.Generic;

namespace TechEventManager
{
    public abstract class Event : IBookable
    {
        private List<Participant> _attendees;

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public int AttendeesCount => _attendees.Count;

        protected Event(int id, string title, DateTime date, int capacity, string location)
        {
            if (id <= 0)
                throw new ArgumentException("Event ID must be positive", nameof(id));

            Id = id;
            Title = title;
            Date = date;
            Capacity = capacity;
            Location = location;
            _attendees = new List<Participant>();
        }

        public abstract decimal GetPrice();

        public virtual bool Register(Participant participant)
        {
            if (participant == null)
                throw new ArgumentNullException(nameof(participant));

            if (IsFull())
                throw new EventFullException(Id, Capacity);

            _attendees.Add(participant);
            return true;
        }

        public bool IsFull() => _attendees.Count >= Capacity;

        public List<Participant> GetAttendees() => _attendees;
    }

    public class Workshop : Event
    {
        public string Level { get; set; }
        public int Duration { get; set; }

        public Workshop(int id, string title, DateTime date, int capacity, string location, string level, int duration)
            : base(id, title, date, capacity, location)
        {
            Level = level;
            Duration = duration;
        }

        public override decimal GetPrice()
        {
            decimal basePrice = Duration * 25m;

            if (Level == "Beginner")
                return basePrice * 0.8m;
            else if (Level == "Intermediate")
                return basePrice;
            else if (Level == "Advanced")
                return basePrice * 1.5m;
            else
                return basePrice;
        }
    }

    public class EventSearchEngine
    {
        private List<Event> _events = new List<Event>();

        public void AddEvent(Event evt)
        {
            if (evt == null)
                throw new ArgumentNullException(nameof(evt));

            _events.Add(evt);
        }

        public Event FindEventById(int id)
        {
            foreach (var evt in _events)
            {
                if (evt.Id == id)
                    return evt;
            }
            return null;
        }

        public string GetEventScale()
        {
            if (_events.Count == 0)
                return "No events";
            else if (_events.Count <= 5)
                return "Small catalog";
            else if (_events.Count <= 20)
                return "Medium catalog";
            else
                return "Large catalog";
        }
    }

    public class Participant
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Participant(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
```

---

## 🎯 Missions de Modernisation

### **Mission 1 : Configuration C# Moderne**

**À implémenter :**

1. **Créer GlobalUsings.cs**
   - Déclarer `global using System;`, `System.Collections.Generic`, `System.Linq`
   - Supprimer les usings redondants dans Event.cs

2. **Activer Nullable Reference Types**
   - **Configuration .csproj** : Ajouter `<Nullable>enable</Nullable>` dans PropertyGroup
   ```xml
   <Project Sdk="Microsoft.NET.Sdk">
     <PropertyGroup>
       <TargetFramework>net9.0</TargetFramework>
       <Nullable>enable</Nullable>
     </PropertyGroup>
   </Project>
   ```
   - Corriger les warnings : `Event? FindEventById(int id)`, `Participant? participant`
   - Marquer Title, Location comme non-nullable (string au lieu de string?)

3. **File-scoped Namespace**
   - Convertir `namespace TechEventManager { }` → `namespace TechEventManager;`

**Indice :** FindEventById peut retourner null, donc `Event?`.

---

### **Mission 2 : Collection Expressions et Primary Constructors**

**À implémenter :**

1. **Collection Expressions dans EventSearchEngine**
   - Remplacer `new List<Event>()` par `[]`
   - Ajouter `Dictionary<int, Event> _eventsById = [];`

2. **Primary Constructor pour EventSearchEngine**
   - Convertir en `public class EventSearchEngine(ILogger<EventSearchEngine>? logger = null)`
   - Logger les ajouts, recherches

3. **Init-only Properties dans Event**
   - Convertir Id, Title, Date, Capacity, Location en `{ get; init; }`
   - Adapter constructeur avec object initializer si pertinent

**Indice :** `_events = []` est équivalent à `new List<Event>()`.

---

### **Mission 3 : Pattern Matching et Switch Expressions**

**À implémenter :**

1. **Moderniser GetEventScale()**
   - Convertir les if/else en switch expression :
   ```csharp
   public string GetEventScale() => _events.Count switch
   {
       0 => "No events",
       <= 5 => "Small catalog",
       <= 20 => "Medium catalog",
       _ => "Large catalog"
   };
   ```

2. **Moderniser Workshop.GetPrice()**
   - Convertir en switch expression sur Level :
   ```csharp
   public override decimal GetPrice() => Level switch
   {
       "Beginner" => Duration * 25m * 0.8m,
       "Intermediate" => Duration * 25m,
       "Advanced" => Duration * 25m * 1.5m,
       _ => Duration * 25m
   };
   ```

3. **Validation avec Pattern Matching dans AddEvent()**
   - Utiliser `is not null`, pattern matching pour validation
   - Gérer événements passés, capacité invalide

4. **Catégoriser événements par remplissage**
   - Ajouter méthode `GetFillCategory()` dans Event
   - Switch expression : < 30% = "Low", 30-70% = "Medium", > 70% = "High"

**Indice :** Pattern matching avec `and`, `or`, `not` : `>= 18 and < 65`.

---

### **Mission 4 : Records et Types Modernes**

**À implémenter :**

1. **Créer record EventInfo**
   ```csharp
   public record EventInfo(int Id, string Title, DateTime Date, string Location, int Capacity, int AttendeesCount);
   ```
   - Utiliser dans exports, rapports
   - Méthode `ToEventInfo()` dans Event

2. **Convertir Participant en record**
   ```csharp
   public record Participant(string Name, string Email);
   ```
   - Exploiter equality par valeur (utile pour détecter doublons)

3. **Créer record EventSearchResult**
   ```csharp
   public record EventSearchResult(Event Event, double FillRate, decimal Price);
   ```
   - Pour retours de recherche enrichis

**Indice :** Records = DTOs, value objects. Event reste une classe (héritage, comportement).

---

### **Mission 5 : Optimisations Modernes**

**À implémenter :**

1. **Optimiser FindEventById() avec Dictionary**
   ```csharp
   public Event? FindEventById(int id) => _eventsById.GetValueOrDefault(id);
   ```

2. **Intégration Logging structuré**
   - Logger ajouts : `logger?.LogInformation("Event {EventId} '{Title}' added", id, title)`
   - Logger recherches, erreurs

3. **Extension : Meetup avec primary constructor**
   ```csharp
   public class Meetup(int id, string title, DateTime date, int capacity, string location, string topic)
       : Event(id, title, date, capacity, location)
   {
       public string Topic { get; init; } = topic;
   }
   ```

**Indice :** `?.` évite NullReferenceException sur logger optionnel.

---

## 🧪 Tests unitaires

**Objectif :** Valider les fonctionnalités C# modernes (switch expressions, records, nullable reference types).

**Format** : 2 exemples fournis + 4 à implémenter vous-même

---

### 📝 Exemples fournis (2 tests)

Voici 2 exemples complets pour vous montrer comment tester les fonctionnalités modernes :

```csharp
[Theory]
[InlineData(0, "No events")]
[InlineData(3, "Small catalog")]
[InlineData(15, "Medium catalog")]
[InlineData(50, "Large catalog")]
public void GetEventScale_DifferentCounts_ReturnsCorrectScale(int eventCount, string expected)
{
    // Arrange
    var engine = new EventSearchEngine();
    for (int i = 0; i < eventCount; i++)
        engine.AddEvent(new Workshop(i, $"Test{i}", DateTime.Now.AddDays(1), 20, "Paris", "Beginner", 2));

    // Act
    var result = engine.GetEventScale();

    // Assert
    Assert.Equal(expected, result);
}

[Fact]
public void ParticipantRecord_EqualityByValue_Works()
{
    // Arrange
    var p1 = new Participant("John", "john@test.com");
    var p2 = new Participant("John", "john@test.com");

    // Act & Assert
    Assert.Equal(p1, p2); // Records : equality par valeur
}
```

---

### ✍️ Tests à implémenter vous-même (4 tests)

En vous inspirant des exemples, implémentez ces 4 tests pour valider C# moderne :

**Test 3 : FindEventById - Nullable reference types**
```csharp
[Fact]
public void FindEventById_NonExistent_ReturnsNull()
{
    // TODO: Implémenter ce test
    // Créer un EventSearchEngine vide
    // Chercher un événement avec ID 999
    // Vérifier que result == null (Event? retourne null)
}
```

**Test 4 : GetPrice - Switch expression**
```csharp
[Theory]
[InlineData("Beginner", 2, 40)]
[InlineData("Intermediate", 2, 50)]
[InlineData("Advanced", 2, 75)]
public void GetPrice_DifferentLevels_ReturnsCorrectPrice(string level, int duration, decimal expected)
{
    // TODO: Implémenter ce test
    // Créer un Workshop avec le level et duration fournis
    // Appeler GetPrice()
    // Vérifier que le prix correspond à expected
}
```

**Test 5 : AddEvent - Pattern matching**
```csharp
[Fact]
public void AddEvent_NullEvent_ThrowsArgumentNullException()
{
    // TODO: Implémenter ce test
    // Créer un EventSearchEngine
    // Appeler AddEvent(null)
    // Vérifier que ArgumentNullException est levée
    // (Validation avec 'is not null' pattern matching)
}
```

**Test 6 : Collection expressions**
```csharp
[Fact]
public void CollectionExpression_Initialization_Works()
{
    // TODO: Implémenter ce test
    // Créer un EventSearchEngine (qui utilise [] pour initialiser _events)
    // Vérifier que le moteur est créé sans erreur
    // Ajouter un événement et vérifier qu'il est bien ajouté
}
```

**Indice** : Les tests vérifient que vos switch expressions, records et nullable reference types fonctionnent correctement.

---

### 🎓 Bonnes pratiques

#### **Nullable Reference Types**

```csharp
// ❌ MAUVAIS : Peut retourner null mais non annoté
public Event FindEventById(int id)
{
    return _events.FirstOrDefault(e => e.Id == id); // Warning!
}

// ✅ BON : Signature explicite avec ?
public Event? FindEventById(int id)
{
    return _events.FirstOrDefault(e => e.Id == id);
}
```

#### **Records vs Classes**

```csharp
// ✅ BON : Record pour DTO/value object
public record EventInfo(int Id, string Title, DateTime Date);

// ✅ BON : Classe pour entité avec comportement
public class Event { /* méthodes métier, héritage */ }

// ❌ MAUVAIS : Record pour entité mutable avec héritage complexe
public record Event { /* ... */ }
```

#### **Switch Expressions**

```csharp
// ❌ MAUVAIS : If/else verbeux
public string GetLevel()
{
    if (price < 30) return "Low";
    else if (price < 70) return "Medium";
    else return "High";
}

// ✅ BON : Switch expression concis
public string GetLevel() => price switch
{
    < 30 => "Low",
    < 70 => "Medium",
    _ => "High"
};
```

---

### 🎯 Questions d'entretien

**1. Expliquez les Nullable Reference Types**

<details>
<summary>Voir la réponse</summary>

Les **Nullable Reference Types** (C# 8) permettent la **détection à la compilation des NullReferenceException**. `Event?` indique qu'une variable peut être null, `Event` garantit qu'elle ne peut pas être null. Migration progressive avec `#nullable enable`.

```csharp
// Activer dans .csproj
// <Nullable>enable</Nullable>

// ❌ Sans nullable reference types
public Event FindEvent(int id)
{
    return _events.FirstOrDefault(e => e.Id == id); // Peut retourner null, pas d'avertissement
}

// ✅ Avec nullable reference types
public Event? FindEvent(int id) // ? indique peut être null
{
    return _events.FirstOrDefault(e => e.Id == id);
}

// Utilisation sécurisée
var evt = FindEvent(123);
if (evt != null) // Compilateur vérifie le null check
{
    Console.WriteLine(evt.Title); // OK
}

// Variables non-nullables
public class Event
{
    public string Title { get; init; } // Ne peut pas être null
    public string? Description { get; init; } // Peut être null

    public Event(string title)
    {
        Title = title; // OK
        // Title = null; // ❌ Erreur de compilation
    }
}
```
</details>

**2. Différence entre class et record ?**

<details>
<summary>Voir la réponse</summary>

**Records** : equality par **valeur**, immutabilité par défaut, idéaux pour **DTOs**. **Classes** : equality par **référence**, pour entités avec comportement/héritage complexe.

```csharp
// Record : equality par valeur, immutable
public record EventInfo(int Id, string Title, DateTime Date);

var e1 = new EventInfo(1, "Angular", DateTime.Now);
var e2 = new EventInfo(1, "Angular", DateTime.Now);
Console.WriteLine(e1 == e2); // ✅ true (equality par valeur)

// Immutabilité avec 'with' expression
var e3 = e1 with { Title = "React" };

// Class : equality par référence
public class Event
{
    public int Id { get; set; }
    public string Title { get; set; }
}

var evt1 = new Event { Id = 1, Title = "Angular" };
var evt2 = new Event { Id = 1, Title = "Angular" };
Console.WriteLine(evt1 == evt2); // ❌ false (références différentes)

// Quand utiliser quoi ?
// ✅ Record : DTO, value object, données immuables
public record ParticipantDto(string Name, string Email);

// ✅ Class : entité avec comportement, héritage complexe
public class Event
{
    public virtual decimal GetPrice() { /* ... */ }
    public void Register(Participant p) { /* ... */ }
}
```
</details>

**3. Quand utiliser switch expressions ?**

<details>
<summary>Voir la réponse</summary>

Utiliser les **switch expressions** pour un **mapping de valeurs exhaustif**, plus concis que if/else. Éviter si la logique est complexe ou s'il y a des side effects.

```csharp
// ❌ If/else verbeux
public string GetAgeCategory(int age)
{
    if (age < 18)
        return "Minor";
    else if (age >= 18 && age < 65)
        return "Adult";
    else
        return "Senior";
}

// ✅ Switch expression concis
public string GetAgeCategory(int age) => age switch
{
    < 18 => "Minor",
    >= 18 and < 65 => "Adult",
    >= 65 => "Senior"
};

// Pattern matching avec propriétés
public decimal GetPrice(Workshop workshop) => workshop switch
{
    { Level: "Beginner", Duration: var d } => d * 25m * 0.8m,
    { Level: "Intermediate", Duration: var d } => d * 25m,
    { Level: "Advanced", Duration: var d } => d * 25m * 1.5m,
    _ => 50m
};

// ❌ Éviter si logique complexe ou side effects
public string Process(Event evt) => evt switch
{
    Workshop w => DoComplexProcessing(w), // ❌ Logique complexe
    Conference c => { Logger.Info("Processing"); return c.Title; } // ❌ Side effect
};

// ✅ Préférer if/else pour logique complexe
public string Process(Event evt)
{
    if (evt is Workshop w)
    {
        // Logique complexe claire
        return DoComplexProcessing(w);
    }
    // ...
}
```
</details>

**4. Avantages des primary constructors ?**

<details>
<summary>Voir la réponse</summary>

Les **primary constructors** (C# 12) réduisent le boilerplate, simplifient l'injection de dépendances, et les paramètres sont utilisables directement dans la classe.

```csharp
// ❌ Ancien : boilerplate pour injection de dépendances
public class EventService
{
    private readonly ILogger<EventService> _logger;
    private readonly EmailService _emailService;

    public EventService(ILogger<EventService> logger, EmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public void Register(Participant p)
    {
        _logger.LogInfo("Registering...");
        _emailService.Send(p.Email);
    }
}

// ✅ Primary constructor : concis
public class EventService(ILogger<EventService> logger, EmailService emailService)
{
    public void Register(Participant p)
    {
        logger.LogInfo("Registering..."); // Utilise directement le paramètre
        emailService.Send(p.Email);
    }
}

// Combinaison avec héritage
public class Workshop(int id, string title, DateTime date, int capacity, string location, string level)
    : Event(id, title, date, capacity, location)
{
    public string Level { get; init; } = level;
}

// Utilisation dans records (déjà standard)
public record EventInfo(int Id, string Title, DateTime Date);
```
</details>

**5. Collection expressions `[]` vs `new List<>()` ?**

<details>
<summary>Voir la réponse</summary>

Les **collection expressions** `[]` (C# 12) sont plus concises que `new List<>()`, avec les **mêmes performances**. Elles permettent l'initialisation inline : `int[] nums = [1, 2, 3];`.

```csharp
// ❌ Ancien : verbeux
var numbers = new List<int>();
var names = new List<string> { "John", "Jane" };
var events = new Dictionary<int, Event>();

// ✅ Collection expressions : concis
var numbers = [];
var names = ["John", "Jane"];
var events = new Dictionary<int, Event>(); // Dictionary pas encore supporté avec []

// Arrays
int[] nums = [1, 2, 3, 4, 5]; // Au lieu de new int[] { 1, 2, 3, 4, 5 }

// Avec type inference
List<int> ages = [25, 30, 35];

// Spread operator (C# 12)
int[] first = [1, 2, 3];
int[] second = [4, 5, 6];
int[] combined = [..first, ..second]; // [1, 2, 3, 4, 5, 6]

// Utilisation dans les champs
public class EventSearchEngine
{
    private readonly List<Event> _events = []; // Au lieu de new List<Event>()
    private readonly List<string> _statuses = ["pending", "active", "completed"];
}
```
</details>

---

### ✅ Validation du module

**Checklist TechEvent Manager - Phase 5 :**
- [ ] `#nullable enable` activé, Event? pour retours nullables
- [ ] File-scoped namespace : `namespace TechEventManager;`
- [ ] Collection expressions : `[]` pour List/Dictionary
- [ ] Switch expressions dans GetEventScale() et GetPrice()
- [ ] Primary constructor pour EventSearchEngine avec logger
- [ ] Init-only properties pour Id, Title, Date, Capacity, Location
- [ ] 3 records créés (EventInfo, Participant, EventSearchResult)
- [ ] Tests : 6 tests essentiels passent, coverage ≥ 70%
- [ ] Comportement préservé après modernisation (pas de régression)
- [ ] Je réponds aux 5 questions d'entretien

---

**⬅️ [Module 4 - Gestion d'Erreurs](../4-Module-Gestion-Erreurs/4-module-gestion-erreurs.md) | [Retour au MECA](../README.md) | [Module 6 - Programmation Asynchrone](../6-Module-Programmation-Asynchrone/6-module-programmation-asynchrone.md) ➡️**
