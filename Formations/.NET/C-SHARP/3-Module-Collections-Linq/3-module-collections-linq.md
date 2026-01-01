# Montée en compétence auto-rythmée - C#

## 📚 Module 3 : Collections et LINQ

**🔗 Prérequis :** Module 1 - Fondamentaux ✅ + Module 2 - POO ✅

### Objectifs

- Choisir la bonne collection selon le contexte
- Maîtriser les opérations LINQ essentielles
- Manipuler efficacement les données
- Comprendre les performances des différentes collections

---

### Contenu théorique

- **Collections** : List<T>, Dictionary<TKey,TValue>, HashSet<T>, Queue<T>, Stack<T>
- **Comparaisons** : IComparable, IComparer, Equals, GetHashCode
- **LINQ méthodes** : Where, Select, GroupBy, OrderBy, Join, First/FirstOrDefault, Any, All
- **LINQ syntaxe** : query syntax vs method syntax
- **Performance** : quand utiliser quelle collection, complexité algorithmique

### Exemple rapide

```csharp
// Collections de base
var numbers = new List<int> { 1, 2, 3, 4, 5 };
var ages = new Dictionary<string, int>
{
    { "Jean", 25 },
    { "Marie", 30 },
    { "Paul", 22 }
};
var uniqueIds = new HashSet<int> { 1, 2, 3, 2, 1 }; // Garde seulement 1, 2, 3

// LINQ - Method Syntax
var adults = ages.Where(person => person.Value >= 18)
                .Select(person => person.Key)
                .OrderBy(name => name)
                .ToList(); // ["Jean", "Marie", "Paul"]

// LINQ - Query Syntax
var adultNames = (from person in ages
                  where person.Value >= 18
                  orderby person.Key
                  select person.Key).ToList();

// Opérations avancées
var evenNumbers = numbers.Where(n => n % 2 == 0).ToList(); // [2, 4]
var hasAdults = ages.Any(person => person.Value >= 18); // true
var allAdults = ages.All(person => person.Value >= 18); // true
```

---

### 📖 Ressources théoriques

- [LINQ in C# - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/linq/linq-in-csharp)
- [Introduction to LINQ Queries - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/introduction-to-linq-queries)
- [Collections and Data Structures - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/standard/collections/)
- [Generic Collections - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/standard/collections/commonly-used-collection-types)

### 🎥 Vidéos recommandées

- [List<T> and Collections of Data - Microsoft Learn](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/listt-and-collections-of-data-csharp-for-beginners)
- [Language Integrated Query (LINQ) - Microsoft Learn](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/language-integrated-query-linq-and-ienumerable-csharp-for-beginners)
- [LINQ Query Expressions - Microsoft Learn](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/linq-query-expressions-from-where-orderby-and-select-csharp-for-beginners)
- [LINQ Method Syntax vs Query - Microsoft Learn](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/linq-method-syntax-vs-query-csharp-for-beginners)

### 📚 Tutoriels complets

- [LINQ Tutorial - Dot Net Tutorials](https://dotnettutorials.net/course/linq/)
- [How To Use LINQ in C# - FreeCodeCamp](https://www.freecodecamp.org/news/how-to-use-linq/)

---

## 🎯 Projet fil rouge : TechEvent Manager - Phase 3

### 📋 Contexte

**Rappel Phases 1 & 2 :**
- Phase 1 : Calculatrice console (Module 1)
- Phase 2 : Modèle objet POO (Module 2)

**Phase 3 :** L'ESN a maintenant des dizaines d'événements passés et à venir. Il faut implémenter un système de recherche et d'analyse puissant avec **Collections** et **LINQ**.

**Objectif :** Créer un moteur de recherche et d'analyse pour filtrer, trier et analyser les événements.

---

### 💻 Exercice pratique : Recherche & Analyse d'événements

**Objectif :** Maîtriser Collections et LINQ pour des opérations de recherche sur les événements.

```csharp
// Utiliser les classes Event, Workshop, Conference, Meetup, Participant du Module 2

public class EventSearchEngine
{
    // Collections principales
    private List<Event> _events;                  // Liste principale
    private Dictionary<int, Event> _eventById;    // Lookup rapide par ID

    public EventSearchEngine()
    {
        _events = new List<Event>();
        _eventById = new Dictionary<int, Event>();
    }

    // Ajouter un événement (méthode fournie - focus sur LINQ après)
    public void AddEvent(Event evt)
    {
        if (evt == null)
            throw new ArgumentNullException(nameof(evt));

        if (_eventById.ContainsKey(evt.Id))
            throw new ArgumentException($"Event with ID {evt.Id} already exists");

        _events.Add(evt);
        _eventById[evt.Id] = evt;
    }

    // Trouver un événement par ID (Dictionary - O(1))
    public Event FindEventById(int id)
    {
        // TODO: Utiliser TryGetValue du Dictionary pour lookup rapide
    }

    // F1 - Événements à venir, triés par date
    public List<Event> GetUpcomingEvents()
    {
        // TODO: Filtrer Date > DateTime.Now et trier par Date
    }

    // F2 - Événements disponibles (pas complets)
    public List<Event> GetAvailableEvents()
    {
        // TODO: Filtrer les événements où !IsFull()
    }

    // F3 - Top N événements les plus populaires (par nombre d'inscrits)
    public List<Event> GetMostPopularEvents(int count = 10)
    {
        // TODO: Trier par AttendeesCount (décroissant) et prendre les N premiers
    }

    // F4 - Recherche textuelle (titre contient mot-clé)
    public List<Event> SearchByKeyword(string keyword)
    {
        // TODO: Rechercher avec Contains (insensible à la casse)
    }

    // F5 - Recherche par critères multiples
    public List<Event> SearchEvents(
        string city = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? maxPrice = null)
    {
        // TODO: Appliquer des filtres Where cumulatifs selon les paramètres non-null
    }
}
```

---

---

### 🚀 Fonctionnalités BONUS (Optionnel)

Une fois les 5 fonctionnalités de base maîtrisées, vous pouvez explorer ces opérations avancées :

<details>
<summary>Statistiques avec GroupBy</summary>

```csharp
// BONUS : Événements par ville
public Dictionary<string, int> GetEventCountByCity()
{
    // Grouper par Location et compter
    return _events.GroupBy(e => e.Location)
                  .ToDictionary(g => g.Key, g => g.Count());
}

// BONUS : Taux de remplissage moyen par type
public Dictionary<string, double> GetAverageFillRateByType()
{
    return _events.GroupBy(e => e.GetType().Name)
                  .ToDictionary(
                      g => g.Key,
                      g => g.Average(e => (double)e.AttendeesCount / e.Capacity * 100)
                  );
}
```

</details>

---

### ✅ Critères d'acceptance

**Technique :**
- [ ] Dictionary utilisé pour lookup par ID (FindEventById)
- [ ] 5 méthodes LINQ implémentées (F1-F5)
- [ ] Utilisation correcte de Where, OrderBy, OrderByDescending, Take
- [ ] Gestion des paramètres optionnels (nullable)

**Fonctionnel :**
- [ ] GetUpcomingEvents retourne seulement événements futurs, triés
- [ ] GetAvailableEvents filtre les événements non complets
- [ ] GetMostPopularEvents retourne top N dans le bon ordre
- [ ] SearchByKeyword trouve les événements avec mot-clé (insensible à la casse)
- [ ] SearchEvents combine correctement les filtres multiples

**Tests :**
- [ ] Au minimum 6 tests essentiels qui passent
- [ ] Coverage ≥ 70% sur EventSearchEngine
- [ ] Tests avec listes vides, null, edge cases

---

### 🧪 Tests unitaires

**Format** : 2 exemples fournis + 4 à implémenter vous-même

---

### 📝 Exemples fournis (2 tests)

Voici 2 exemples complets pour vous montrer comment tester LINQ et Dictionary :

```csharp
[Fact]
public void FindEventById_ExistingEvent_ReturnsEvent()
{
    // Arrange
    var engine = new EventSearchEngine();
    var workshop = new Workshop(1, "Test", DateTime.Now.AddDays(5), 20, "Paris", "Beginner", 2);
    engine.AddEvent(workshop);

    // Act
    var result = engine.FindEventById(1);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Test", result.Title);
}

[Fact]
public void GetUpcomingEvents_ReturnsFutureEventsOnly()
{
    // Arrange
    var engine = new EventSearchEngine();
    engine.AddEvent(new Workshop(1, "Past", DateTime.Now.AddDays(-5), 20, "Paris", "Beginner", 2));
    engine.AddEvent(new Workshop(2, "Future", DateTime.Now.AddDays(5), 20, "Lyon", "Beginner", 2));

    // Act
    var result = engine.GetUpcomingEvents();

    // Assert
    Assert.Single(result);
    Assert.Equal("Future", result[0].Title);
}
```

---

### ✍️ Tests à implémenter vous-même (4 tests)

En vous inspirant des exemples, implémentez ces 4 tests pour valider LINQ :

**Test 3 : FindEventById - Événement introuvable**
```csharp
[Fact]
public void FindEventById_NonExistingEvent_ReturnsNull()
{
    // TODO: Implémenter ce test
    // Créer un EventSearchEngine vide
    // Chercher un événement avec ID 999
    // Vérifier que result == null
}
```

**Test 4 : GetAvailableEvents - Filtre événements non complets**
```csharp
[Fact]
public void GetAvailableEvents_ReturnsOnlyNonFullEvents()
{
    // TODO: Implémenter ce test
    // Créer 2 workshops : un disponible (capacité 20), un complet (capacité 1)
    // Inscrire 1 participant au workshop complet
    // Appeler GetAvailableEvents()
    // Vérifier qu'il retourne seulement le workshop disponible
}
```

**Test 5 : GetMostPopularEvents - Tri par nombre d'inscrits**
```csharp
[Fact]
public void GetMostPopularEvents_ReturnsInDescendingOrder()
{
    // TODO: Implémenter ce test
    // Créer 2 événements
    // Inscrire 2 participants au premier, 1 au second
    // Appeler GetMostPopularEvents(10)
    // Vérifier que le plus populaire est en premier dans la liste
}
```

**Test 6 : SearchByKeyword - Recherche insensible à la casse**
```csharp
[Fact]
public void SearchByKeyword_CaseInsensitive_FindsEvents()
{
    // TODO: Implémenter ce test
    // Créer un événement avec titre "Angular Workshop"
    // Chercher avec mot-clé "ANGULAR" (majuscules)
    // Vérifier que l'événement est trouvé (case insensitive)
}
```

---

### 🎓 Bonnes pratiques LINQ

#### **Filtrer tôt (Where d'abord)**

```csharp
// ❌ MAU VAIS : Tout charger en mémoire puis filtrer
var events = _events.ToList()
    .Where(e => e.Date > DateTime.Now)
    .Where(e => e.Location == "Paris");

// ✅ BON : Filtrer avant ToList
var events = _events
    .Where(e => e.Date > DateTime.Now && e.Location == "Paris")
    .ToList();
```

#### **Éviter les N+1**

```csharp
// ❌ MAUVAIS : Requête dans une boucle
foreach (var evt in events)
{
    var attendeeCount = _events.Where(e => e.Id == evt.Id).First().AttendeesCount;
}

// ✅ BON : Une seule requête
var eventData = events.Select(e => new { e.Id, e.AttendeesCount }).ToList();
```

#### **GroupBy efficace**

```csharp
// ✅ BON : GroupBy retourne IGrouping<TKey, TElement>
var byCity = _events
    .GroupBy(e => e.Location)
    .Select(g => new
    {
        City = g.Key,
        Count = g.Count(),
        TotalRevenue = g.Sum(e => e.GetPrice() * e.AttendeesCount)
    })
    .ToList();
```

---

### 💡 Indices

<details>
<summary>🔍 Indice 1 : Search multicritères</summary>

```csharp
public List<Event> SearchEvents(string city = null, DateTime? startDate = null, ...)
{
    IEnumerable<Event> query = _events;

    if (city != null)
        query = query.Where(e => e.Location == city);

    if (startDate.HasValue)
        query = query.Where(e => e.Date >= startDate.Value);

    // ... autres filtres

    return query.ToList();
}
```
</details>

<details>
<summary>🔍 Indice 2 : GroupBy par mois</summary>

```csharp
public Dictionary<string, decimal> GetRevenueByMonth()
{
    return _events
        .GroupBy(e => new { e.Date.Year, e.Date.Month })
        .ToDictionary(
            g => $"{g.Key.Month:D2}/{g.Key.Year}",
            g => g.Sum(e => e.GetPrice() * e.AttendeesCount)
        );
}
```
</details>

<details>
<summary>🔍 Indice 3 : Participant dans événements</summary>

```csharp
public List<Event> GetEventsByParticipant(string email)
{
    return _events
        .Where(e => e.GetAttendees().Any(p => p.Email == email))
        .ToList();
}
```
</details>

---

### 🎯 Questions d'entretien

**1. List vs Dictionary vs HashSet ?**

<details>
<summary>Voir la réponse</summary>

**List** est optimisée pour un accès par index et maintient l'ordre d'insertion. **Dictionary** offre un lookup O(1) par clé unique. **HashSet** garantit l'unicité des éléments et permet des opérations ensemblistes rapides (union, intersection).

```csharp
// List : accès par index et ordre
var numbers = new List<int> { 1, 2, 3 };
var second = numbers[1]; // 2

// Dictionary : lookup rapide par clé
var ages = new Dictionary<string, int>
{
    { "Jean", 25 },
    { "Marie", 30 }
};
var jeanAge = ages["Jean"]; // O(1)

// HashSet : uniques et opérations ensemblistes
var set1 = new HashSet<int> { 1, 2, 3 };
var set2 = new HashSet<int> { 2, 3, 4 };
set1.UnionWith(set2); // { 1, 2, 3, 4 }
```
</details>

**2. First() vs FirstOrDefault() ?**

<details>
<summary>Voir la réponse</summary>

**First()** lève une exception `InvalidOperationException` si la séquence est vide. **FirstOrDefault()** retourne `null` (pour les types référence) ou la valeur par défaut (pour les types valeur) si la séquence est vide. En production, toujours préférer `*OrDefault` pour éviter les exceptions inattendues.

```csharp
var events = new List<Event>();

// ❌ First() : lève une exception si vide
var first = events.First(); // InvalidOperationException!

// ✅ FirstOrDefault() : retourne null si vide
var firstOrDefault = events.FirstOrDefault(); // null

// Utilisation sécurisée
if (firstOrDefault != null)
{
    Console.WriteLine(firstOrDefault.Title);
}
```
</details>

**3. Exécution différée LINQ ?**

<details>
<summary>Voir la réponse</summary>

**L'exécution différée** (deferred execution) signifie que la requête LINQ n'est pas exécutée lors de sa définition, mais seulement lors de l'énumération (avec `ToList()`, `Count()`, `foreach`, etc.). Cela permet de composer des requêtes et d'optimiser les performances.

```csharp
var events = new List<Event> { /* ... */ };

// La requête n'est PAS exécutée ici
var query = events.Where(e => e.Date > DateTime.Now);

// La requête s'exécute ici
var upcomingEvents = query.ToList(); // Exécution

// Ou ici
foreach (var evt in query) // Exécution
{
    Console.WriteLine(evt.Title);
}

// Composition possible
query = query.OrderBy(e => e.Date); // Toujours pas exécutée
var count = query.Count(); // Exécution
```
</details>

**4. Optimiser LINQ lent ?**

<details>
<summary>Voir la réponse</summary>

Pour optimiser une requête LINQ lente : **filtrer tôt** avec `Where` en premier, **éviter les `ToList()` intermédiaires**, **utiliser des index** (Dictionary pour lookup O(1)), et **éviter les N+1 queries**.

```csharp
// ❌ MAUVAIS : ToList() inutile, filtrage tardif
var result = events.ToList()
    .OrderBy(e => e.Date)
    .Where(e => e.Location == "Paris")
    .ToList();

// ✅ BON : Filtrer d'abord, pas de ToList() intermédiaire
var result = events
    .Where(e => e.Location == "Paris")
    .OrderBy(e => e.Date)
    .ToList();

// ✅ BON : Dictionary pour lookup rapide (éviter N+1)
var eventById = events.ToDictionary(e => e.Id);
var event1 = eventById[1]; // O(1) au lieu de O(n)
```
</details>

**5. GroupBy expliqué ?**

<details>
<summary>Voir la réponse</summary>

**GroupBy** retourne un `IEnumerable<IGrouping<TKey, TElement>>` où chaque groupe a une propriété `.Key` (la clé de regroupement) et est énumérable (contient tous les éléments du groupe).

```csharp
var events = new List<Event>
{
    new Workshop(1, "Angular", DateTime.Now, 20, "Paris", "Beginner", 2),
    new Workshop(2, "React", DateTime.Now, 30, "Lyon", "Advanced", 3),
    new Workshop(3, "Vue", DateTime.Now, 25, "Paris", "Intermediate", 2)
};

// Grouper par ville
var byCity = events.GroupBy(e => e.Location);

foreach (var group in byCity)
{
    Console.WriteLine($"Ville: {group.Key}"); // Paris, Lyon

    foreach (var evt in group) // Énumérer les événements du groupe
    {
        Console.WriteLine($"  - {evt.Title}");
    }
}

// Avec projection
var eventCountByCity = events
    .GroupBy(e => e.Location)
    .ToDictionary(g => g.Key, g => g.Count());
// { "Paris": 2, "Lyon": 1 }
```
</details>

---

### ✅ Validation

**Checklist obligatoire :**
- [ ] EventSearchEngine : 5 fonctionnalités essentielles implémentées (F1-F5)
- [ ] Dictionary utilisé pour FindEventById (lookup O(1))
- [ ] Tests : 6 tests essentiels passent, coverage ≥ 70%
- [ ] LINQ maîtrisé : Where, OrderBy, OrderByDescending, Take
- [ ] Je peux expliquer List vs Dictionary vs HashSet
- [ ] Je peux expliquer First() vs FirstOrDefault()
- [ ] Je réponds aux 5 questions d'entretien

**Checklist BONUS (optionnel) :**
- [ ] J'ai exploré GroupBy avec les statistiques bonus
- [ ] J'ai implémenté GetEventCountByCity ou GetAverageFillRateByType

---

**⬅️ [Module 2 - POO](../2-Module-POO/2-module-POO.md) | [Retour au MECA](../README.md) | [Module 4 - Gestion d'Erreurs](../4-Module-Gestion-Erreurs/4-module-gestion-erreurs.md) ➡️**

---
