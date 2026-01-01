# Montée en compétence auto-rythmée - C#

## 📚 Module 2 : Programmation Orientée Objet

**🔗 Prérequis :** Module 1 - Fondamentaux C# ✅

### Objectifs

- Concevoir et implémenter des classes robustes
- Maîtriser l'encapsulation et l'héritage
- Comprendre le polymorphisme et les interfaces
- Appliquer les principes SOLID de base

---

### Contenu théorique

- **Classes et objets** : constructeurs, destructeurs, membres statiques
- **Encapsulation** : properties, access modifiers (private, public, protected, internal)
- **Héritage** : base classes, override, virtual, sealed
- **Polymorphisme** : méthodes virtuelles, classes abstraites
- **Interfaces** : contrats, implémentation multiple
- **Principes SOLID** : Single Responsibility, Open/Closed

### Exemple rapide

```csharp
// Classe de base avec encapsulation
public class Person
{
    private string _name;
    public string Name
    {
        get => _name;
        set => _name = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Name cannot be empty");
    }

    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // Méthode virtuelle : peut être redéfinie dans les classes filles
    public virtual void Introduce()
    {
        Console.WriteLine($"Je suis {Name}, {Age} ans");
    }
}

// Héritage et polymorphisme
public class Employee : Person
{
    public decimal Salary { get; set; }

    public Employee(string name, int age, decimal salary) : base(name, age)
    {
        Salary = salary;
    }

    // Redéfinition de la méthode de la classe parent
    public override void Introduce()
    {
        base.Introduce(); // Appel du comportement parent
        Console.WriteLine($"Mon salaire est {Salary:C}");
    }
}

// Interface : contrat que doivent respecter les classes
public interface IPayable
{
    decimal CalculateSalary(); // Calcul du salaire
}
```

---

### 📖 Ressources théoriques

- [Object-Oriented Programming (C#) - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop)
- [Classes and Objects - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes)
- [Inheritance in C# - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance)

### 🎥 Vidéos recommandées

- [Object-Oriented Programming - Microsoft Learn](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/object-oriented-programming-oop-csharp-for-beginners)
- [OOP with derived or abstract classes, overrides - Microsoft Learn](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/oop-with-derived-or-abstract-classes-overrides-ienumerable-csharp-for-beginners)

---

## 🎯 Projet fil rouge : TechEvent Manager - Phase 2

### 📋 Contexte

**Rappel Module 1 :** Vous avez créé une calculatrice console pour analyser la viabilité des événements.

**Phase 2 :** Il est temps de structurer le domaine métier avec des classes ! L'ESN veut gérer différents types d'événements (workshops, conférences, meetups), chacun avec ses propres règles de tarification et de gestion.

**Objectif :** Créer un modèle objet robuste qui représente le domaine des événements techniques.

---

### 💻 Exercice pratique : Modèle objet TechEvent Manager

**Objectif :** Concevoir une architecture orientée objet complète avec héritage, interfaces et encapsulation.

#### 📐 Diagramme de classes cible

```
                    ┌─────────────┐
                    │   <<abstract>>   │
                    │     Event        │
                    ├─────────────┤
                    │ +Id          │
                    │ +Title       │
                    │ +Date        │
                    │ +Capacity    │
                    │ -attendees   │
                    ├─────────────┤
                    │ +Register()  │
                    │ +IsFull()    │
                    │ +GetPrice()* │
                    └──────┬──────┘
                           │
         ┌─────────────────┼─────────────────┐
         │                 │                 │
    ┌────▼────┐      ┌────▼────┐      ┌────▼────┐
    │ Workshop│      │Conference│     │ Meetup  │
    ├─────────┤      ├─────────┤      ├─────────┤
    │ +Level  │      │ +Topics │      │ +IsFree │
    └─────────┘      └─────────┘      └─────────┘

    ┌──────────────┐
    │ <<interface>>│
    │  IBookable   │
    ├──────────────┤
    │ +Register()  │
    │ +Cancel()    │
    │ +IsFull()    │
    └──────────────┘
```

#### 🏗️ Architecture à implémenter :

```csharp
// Interface pour les événements réservables
public interface IBookable
{
    bool Register(Participant participant);
    bool Cancel(Participant participant);
    bool IsFull();
    int GetAvailableSeats();
}

// Classe abstraite de base pour tous les événements
public abstract class Event : IBookable
{
    // Propriétés publiques
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public int Capacity { get; set; }
    public string Location { get; set; }

    // Encapsulation : liste privée des participants
    private List<Participant> _attendees;

    // Propriété calculée (read-only)
    public int AttendeesCount => _attendees.Count;

    // Constructeur
    protected Event(int id, string title, DateTime date, int capacity, string location)
    {
        // TODO: Initialiser toutes les propriétés et _attendees
    }

    // Méthode abstraite : chaque type d'événement a son propre prix
    public abstract decimal GetPrice();

    // Méthode virtuelle : comportement par défaut pour l'inscription
    public virtual bool Register(Participant participant)
    {
        // TODO: Vérifier si complet, si déjà inscrit (même email), puis ajouter
        // Cette méthode sera redéfinie dans Meetup pour autoriser le surbooking
    }

    public virtual bool Cancel(Participant participant)
    {
        // TODO: Retirer le participant de la liste
    }

    public bool IsFull()
    {
        // TODO: Comparer le nombre d'inscrits avec la capacité
    }

    public int GetAvailableSeats()
    {
        // TODO: Calculer les places restantes
    }

    public List<Participant> GetAttendees()
    {
        // TODO: Retourner une copie de la liste pour préserver l'encapsulation
    }

    public virtual void DisplayInfo()
    {
        // TODO (Optionnel): Afficher les informations de l'événement
    }
}

// Classes dérivées : types d'événements spécifiques

public class Workshop : Event
{
    public string Level { get; set; } // Beginner, Intermediate, Advanced
    public int Duration { get; set; } // En heures

    public Workshop(int id, string title, DateTime date, int capacity, string location, string level, int duration)
        : base(id, title, date, capacity, location)
    {
        // TODO: Initialiser Level et Duration
    }

    public override decimal GetPrice()
    {
        // TODO: 25€/h * Duration, avec réduction/majoration selon Level
        // Beginner: -20%, Intermediate: tarif normal, Advanced: +50%
    }

    public override void DisplayInfo()
    {
        // TODO (Optionnel): Afficher les infos + Level et Duration
    }
}

public class Conference : Event
{
    public List<string> Topics { get; set; }
    public bool IncludesLunch { get; set; }

    public Conference(int id, string title, DateTime date, int capacity, string location, bool includesLunch)
        : base(id, title, date, capacity, location)
    {
        // TODO: Initialiser Topics et IncludesLunch
    }

    public override decimal GetPrice()
    {
        // TODO: 150€ de base, +30€ si IncludesLunch
    }

    public override void DisplayInfo()
    {
        // TODO (Optionnel): Afficher les infos + Topics et IncludesLunch
    }
}

public class Meetup : Event
{
    public bool IsFree => true; // Les meetups sont toujours gratuits

    public Meetup(int id, string title, DateTime date, int capacity, string location)
        : base(id, title, date, capacity, location)
    {
        // Pas de propriétés supplémentaires
    }

    public override decimal GetPrice()
    {
        // TODO: Retourner 0m (gratuit)
    }

    public override bool Register(Participant participant)
    {
        // TODO: Autoriser le surbooking jusqu'à 110% de la capacité
    }

    public override void DisplayInfo()
    {
        // TODO (Optionnel): Afficher les infos + mention "Free event"
    }
}

// Classe Participant (fournie)
public class Participant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Company { get; set; }

    public Participant(int id, string name, string email, string company = "")
    {
        Id = id;
        Name = name;
        Email = email;
        Company = company;
    }
}

// Gestionnaire d'événements
public class EventManager
{
    private List<Event> _events = new List<Event>();

    public void AddEvent(Event evt)
    {
        // TODO: Vérifier si un événement avec le même Id existe, sinon ajouter
        // Lever InvalidOperationException si doublon
    }

    public bool RemoveEvent(int eventId)
    {
        // TODO: Trouver et supprimer l'événement par ID
    }

    public Event GetEventById(int id)
    {
        // TODO: Rechercher l'événement par ID
    }

    public List<Event> GetAllEvents()
    {
        // TODO: Retourner une copie de la liste
    }

    public List<Event> GetUpcomingEvents()
    {
        // TODO: Filtrer les événements futurs et trier par date
    }

    public decimal GetTotalRevenue()
    {
        // TODO: Calculer le revenu total (prix * participants pour chaque événement)
    }

    public int GetTotalAttendees()
    {
        // TODO: Sommer tous les participants
    }

    public void DisplayAllEvents()
    {
        // TODO (Optionnel): Afficher tous les événements
    }
}
```

#### 📋 Fonctionnalités à implémenter

**F1 - Gestion des événements**

- Créer différents types d'événements (Workshop, Conference, Meetup)
- Ajouter un événement au système → `EventManager.AddEvent()`
- Supprimer un événement par ID → `EventManager.RemoveEvent()`
- Récupérer tous les événements → `EventManager.GetAllEvents()`
- Filtrer les événements à venir → `EventManager.GetUpcomingEvents()`

**F2 - Gestion des inscriptions (IBookable)**

- Inscrire un participant → `event.Register(Participant)`
- Annuler une inscription → `event.Cancel(Participant)`
- Vérifier si complet → `event.IsFull()`
- Compter les places disponibles → `event.GetAvailableSeats()`
- Empêcher les doublons (même email)

**F3 - Calcul des prix (Polymorphisme)**

- **Workshop** : Prix basé sur niveau + durée
  - Beginner: 20€/h, Intermediate: 25€/h, Advanced: 37.50€/h
- **Conference** : Prix fixe 150€ (+30€ si lunch inclus)
- **Meetup** : Gratuit (0€)

**F4 - Règles métier spécifiques**

- **Workshop** : Capacité stricte (pas de surbooking)
- **Conference** : Validation stricte de capacité
- **Meetup** : Surbooking autorisé (+10% de la capacité)

**F5 - Statistiques et rapports**

- Revenu total estimé → `EventManager.GetTotalRevenue()`
- Nombre total de participants → `EventManager.GetTotalAttendees()`
- Affichage des infos par type d'événement → `DisplayInfo()` polymorphe

#### ⚠️ Points d'attention

**Encapsulation :**

- ✅ Liste `_attendees` privée → accès uniquement via méthodes
- ✅ Propriété calculée `AttendeesCount` (read-only)
- ✅ `GetAttendees()` retourne une copie (pas la liste originale)

**Héritage :**

- ✅ Classe abstraite `Event` → code commun partagé
- ✅ Classes dérivées (`Workshop`, `Conference`, `Meetup`) → spécialisations
- ✅ Constructeur `protected` dans `Event` → appelé via `base()`

**Polymorphisme :**

- ✅ `GetPrice()` abstraite → chaque type calcule différemment
- ✅ `DisplayInfo()` virtuelle → comportement par défaut + redéfinition
- ✅ `Register()` virtuelle → surbooking uniquement pour Meetup

**Interface :**

- ✅ `IBookable` → contrat pour les inscriptions
- ✅ Implémentée dans `Event` → tous les types réservables

**SOLID Principles :**

- ✅ **SRP** : Event gère événements, Participant gère participants, EventManager gère collection
- ✅ **OCP** : Nouveau type d'événement = nouvelle classe (pas de modification d'Event)
- ✅ **LSP** : Workshop/Conference/Meetup utilisables partout où Event est attendu
- ✅ **ISP** : IBookable simple et focused
- ✅ **DIP** : EventManager dépend de l'abstraction Event, pas des types concrets

---

### ✅ Critères d'acceptance

**Technique :**
- [ ] Code compile sans erreur ni warning
- [ ] Classe abstraite `Event` avec méthode `GetPrice()` abstraite
- [ ] Interface `IBookable` implémentée correctement
- [ ] Les 3 types d'événements héritent de `Event`
- [ ] Encapsulation : `_attendees` privée, pas d'accès direct
- [ ] Propriété calculée `AttendeesCount` (read-only)
- [ ] Validation : pas de doublons (même email)

**Fonctionnel :**
- [ ] Workshop : prix selon niveau et durée (formules correctes)
- [ ] Conference : prix 150€ + 30€ si lunch
- [ ] Meetup : gratuit + surbooking 110%
- [ ] Inscription refuse si événement complet
- [ ] Annulation fonctionne correctement
- [ ] EventManager gère plusieurs types d'événements

**Tests unitaires :**
- [ ] Au minimum 6 tests essentiels qui passent
- [ ] Coverage ≥ 70% sur les classes métier
- [ ] Tests de polymorphisme (GetPrice retourne valeurs différentes)
- [ ] Tests d'encapsulation (Register/Cancel fonctionnent correctement)

**Livrables :**
```
2-Module-POO/
├── TechEventManager/
│   ├── Models/
│   │   ├── Event.cs
│   │   ├── Workshop.cs
│   │   ├── Conference.cs
│   │   ├── Meetup.cs
│   │   ├── Participant.cs
│   │   └── IBookable.cs
│   ├── Services/
│   │   └── EventManager.cs
│   └── TechEventManager.csproj
└── TechEventManager.Tests/
    ├── EventTests.cs
    ├── WorkshopTests.cs
    └── TechEventManager.Tests.csproj
```

---

### 🧪 Tests unitaires

**Objectif :** Valider encapsulation, héritage, polymorphisme et interfaces.

**Format** : 2 exemples fournis + 4 à implémenter vous-même

---

### 📝 Exemples fournis (2 tests)

Voici 2 exemples complets de tests de polymorphisme :

```csharp
[Theory]
[InlineData("Beginner", 4, 80)]      // 4h * 25€ * 0.8 = 80€
[InlineData("Intermediate", 4, 100)] // 4h * 25€ = 100€
[InlineData("Advanced", 4, 150)]     // 4h * 25€ * 1.5 = 150€
public void Workshop_GetPrice_CalculatesCorrectly(string level, int duration, decimal expected)
{
    // Arrange & Act
    var workshop = new Workshop(1, "C# Basics", DateTime.Now, 20, "Paris", level, duration);
    var result = workshop.GetPrice();

    // Assert
    Assert.Equal(expected, result);
}

[Theory]
[InlineData(true, 180)]  // 150 + 30 lunch
[InlineData(false, 150)] // 150 base
public void Conference_GetPrice_IncludesLunch(bool includesLunch, decimal expected)
{
    // Arrange & Act
    var conf = new Conference(1, ".NET Conf", DateTime.Now, 100, "Lyon", includesLunch);
    var result = conf.GetPrice();

    // Assert
    Assert.Equal(expected, result);
}
```

---

### ✍️ Tests à implémenter vous-même (4 tests)

En vous inspirant des exemples de polymorphisme ci-dessus, implémentez ces 4 tests :

**Test 3 : Event.Register() - Inscription réussie**
```csharp
[Fact]
public void Event_Register_AddsParticipant()
{
    // TODO: Tester qu'un participant s'inscrit correctement et incrémente AttendeesCount
}
```

**Test 4 : Meetup.GetPrice() - Toujours gratuit**
```csharp
[Fact]
public void Meetup_GetPrice_AlwaysFree()
{
    // TODO: Vérifier que GetPrice() retourne 0m pour un Meetup
}
```

**Test 5 : Event.Cancel() - Annulation réussie**
```csharp
[Fact]
public void Event_Cancel_RemovesParticipant()
{
    // TODO: Tester l'annulation d'inscription et la décrémentation d'AttendeesCount
}
```

**Test 6 : Polymorphisme - Liste mixte d'événements**
```csharp
[Fact]
public void EventList_MixedTypes_PolymorphismWorks()
{
    // TODO: Créer une List<Event> mixte et vérifier que GetPrice() renvoie les bonnes valeurs
}
```

---

### 🎓 Bonnes pratiques POO appliquées

#### **Encapsulation - Protéger l'état interne**

```csharp
// ❌ MAUVAIS : Liste publique, pas de contrôle
public class Event
{
    public List<Participant> Attendees = new List<Participant>(); // Dangereux !
}
// Problème : n'importe qui peut faire Attendees.Clear() ou ajouter sans validation

// ✅ BON : Liste privée, accès contrôlé
public class Event
{
    private List<Participant> _attendees = new List<Participant>();

    public int AttendeesCount => _attendees.Count; // Read-only

    public bool Register(Participant p)
    {
        if (IsFull()) return false;
        _attendees.Add(p);
        return true;
    }

    public List<Participant> GetAttendees()
    {
        return new List<Participant>(_attendees); // Copie, pas l'original
    }
}
```

#### **Héritage - Réutiliser sans dupliquer**

```csharp
// ❌ MAUVAIS : Duplication du code commun
public class Workshop
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    private List<Participant> _attendees = new List<Participant>();
    // ... tout dupliqué dans Conference et Meetup
}

// ✅ BON : Code commun dans la classe de base
public abstract class Event
{
    // Propriétés et méthodes communes
    public int Id { get; set; }
    // ...
    protected Event(...) { } // Constructeur commun
}

public class Workshop : Event
{
    // Uniquement ce qui est spécifique au Workshop
    public string Level { get; set; }
    public Workshop(...) : base(...) { } // Appel du parent
}
```

#### **Polymorphisme - Traiter uniformément**

```csharp
// ❌ MAUVAIS : if/switch sur le type
decimal CalculateTotalRevenue(List<Event> events)
{
    decimal total = 0;
    foreach (var evt in events)
    {
        if (evt is Workshop w)
            total += w.Duration * 25m;
        else if (evt is Conference c)
            total += 150m;
        else if (evt is Meetup)
            total += 0m;
    }
    return total;
}

// ✅ BON : Méthode polymorphe
decimal CalculateTotalRevenue(List<Event> events)
{
    return events.Sum(evt => evt.GetPrice() * evt.AttendeesCount);
    // Appelle automatiquement la bonne implémentation
}
```

#### **Interface vs Classe abstraite**

**Utilisez une interface quand :**
- Contrat de comportement (IBookable, IPayable, IComparable)
- Implémentation multiple nécessaire
- Pas d'état commun à partager

**Utilisez une classe abstraite quand :**
- Code commun à réutiliser
- État partagé (fields, properties)
- Héritage unique suffit

```csharp
// Interface : contrat pur
public interface IBookable
{
    bool Register(Participant p);
    bool IsFull();
}

// Classe abstraite : code + état partagé
public abstract class Event : IBookable
{
    private List<Participant> _attendees; // État partagé
    public abstract decimal GetPrice();   // Comportement variable

    public virtual bool Register(Participant p) // Implémentation par défaut
    {
        // Code réutilisable par tous les enfants
    }
}
```

---

### 💡 Indices progressifs

<details>
<summary>🔍 Indice 1 : Démarrer avec Event abstrait</summary>

```csharp
public abstract class Event : IBookable
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public int Capacity { get; set; }
    public string Location { get; set; }

    private List<Participant> _attendees = new List<Participant>();

    public int AttendeesCount => _attendees.Count;

    protected Event(int id, string title, DateTime date, int capacity, string location)
    {
        Id = id;
        Title = title;
        Date = date;
        Capacity = capacity;
        Location = location;
    }

    // À implémenter : GetPrice(), Register(), Cancel(), IsFull(), GetAvailableSeats()
}
```
</details>

<details>
<summary>🔍 Indice 2 : Implémenter Workshop avec GetPrice()</summary>

```csharp
public class Workshop : Event
{
    public string Level { get; set; }
    public int Duration { get; set; }

    public Workshop(int id, string title, DateTime date, int capacity,
                    string location, string level, int duration)
        : base(id, title, date, capacity, location)
    {
        Level = level;
        Duration = duration;
    }

    public override decimal GetPrice()
    {
        decimal basePrice = Duration * 25m;

        return Level switch
        {
            "Beginner" => basePrice * 0.8m,
            "Intermediate" => basePrice,
            "Advanced" => basePrice * 1.5m,
            _ => basePrice
        };
    }
}
```
</details>

<details>
<summary>🔍 Indice 3 : Meetup avec surbooking</summary>

```csharp
public class Meetup : Event
{
    public Meetup(int id, string title, DateTime date, int capacity, string location)
        : base(id, title, date, capacity, location)
    {
    }

    public override decimal GetPrice() => 0m; // Gratuit

    // Redéfinition pour autoriser 110% de capacité
    public override bool Register(Participant participant)
    {
        int maxCapacity = (int)(Capacity * 1.1m);

        if (AttendeesCount >= maxCapacity)
            return false;

        return base.Register(participant); // Appel du comportement parent
    }
}
```
</details>

<details>
<summary>🔍 Indice 4 : Tester le polymorphisme</summary>

```csharp
[Fact]
public void PolymorphismTest()
{
    // Liste polymorphe : différents types, même interface
    List<Event> events = new List<Event>
    {
        new Workshop(1, "W1", DateTime.Now, 20, "Paris", "Beginner", 4),
        new Conference(2, "C1", DateTime.Now, 50, "Lyon", true),
        new Meetup(3, "M1", DateTime.Now, 30, "Remote")
    };

    // Traitement uniforme
    foreach (var evt in events)
    {
        evt.DisplayInfo(); // Polymorphisme : appelle la bonne version
        Console.WriteLine($"Prix : {evt.GetPrice()}€");
    }
}
```
</details>

### 📖 Ressources pour les tests

- [Unit testing best practices - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
- [Testing Object-Oriented Code](https://xunit.net/docs/getting-started/v3/getting-started)

---

### 💪 Exercices bonus (optionnel)

**Extensions du système :**

- Ajouter type `OnlineWorkshop` : hérite de Workshop + propriété URL Zoom
- Implémenter `Speaker` : Name, Bio, Topics, association avec Event
- Créer `Venue` : Name, Address, MaxCapacity, Facilities
- Ajouter validation : Date ne peut pas être passée, Title non vide
- Property `IsUpcoming` calculée : Date > DateTime.Now

---

### 🎯 Questions d'entretien type

**1. Différence entre classe abstraite et interface ?**

<details>
<summary>Voir la réponse</summary>

Classe abstraite = code partagé + état (fields). Interface = contrat pur.

Event est abstraite (état `_attendees` partagé), IBookable est interface (contrat).

```csharp
// Classe abstraite : code + état partagés
public abstract class Event
{
    protected List<Participant> _attendees; // État partagé
    public virtual bool Register() { }      // Implémentation partagée
}

// Interface : contrat pur (pas d'état)
public interface IBookable
{
    bool Register(Participant p);  // Signature uniquement
    bool Cancel(string email);
}
```
</details>

**2. Pourquoi `GetPrice()` est abstraite dans Event ?**

<details>
<summary>Voir la réponse</summary>

Chaque type d'événement calcule son prix différemment (Workshop selon niveau, Conference fixe, Meetup gratuit). Impossible d'avoir une implémentation par défaut commune.

```csharp
public abstract class Event
{
    public abstract decimal GetPrice(); // Pas d'implémentation par défaut
}

public class Workshop : Event
{
    public override decimal GetPrice() => Level == "Advanced" ? 500 : 200;
}

public class Meetup : Event
{
    public override decimal GetPrice() => 0; // Gratuit
}
```
</details>

**3. Expliquer l'encapsulation avec `_attendees`**

<details>
<summary>Voir la réponse</summary>

Liste privée, accès uniquement via `Register()`, `Cancel()`, `GetAttendees()`. Empêche modification directe (pas de `attendees.Clear()`). `GetAttendees()` retourne copie pour préserver encapsulation.

```csharp
public abstract class Event
{
    private List<Participant> _attendees = new(); // Privé = protégé

    public virtual bool Register(Participant p)
    {
        if (IsFull()) return false;
        _attendees.Add(p); // Accès contrôlé
        return true;
    }

    public List<Participant> GetAttendees()
    {
        return new List<Participant>(_attendees); // Copie défensive
    }
}

// ❌ Impossible de faire :
// event._attendees.Clear(); // Erreur compilation
```
</details>

**4. Qu'est-ce que le polymorphisme dans votre code ?**

<details>
<summary>Voir la réponse</summary>

`List<Event>` peut contenir Workshop, Conference, Meetup. `foreach(var e in events) e.GetPrice()` appelle automatiquement la bonne méthode selon le type réel.

```csharp
List<Event> events = new()
{
    new Workshop("C#", 100, "Advanced"),  // GetPrice() → 500
    new Conference("DevConf", 200, 150),  // GetPrice() → 150
    new Meetup("Tech Meetup", 50)         // GetPrice() → 0
};

foreach (var e in events)
{
    Console.WriteLine(e.GetPrice()); // Polymorphisme !
}
// Affiche : 500, 150, 0
```
</details>

**5. Principe Open/Closed appliqué ?**

<details>
<summary>Voir la réponse</summary>

Ajouter `OnlineWorkshop` = créer nouvelle classe qui hérite. Pas besoin de modifier `Event`, `EventManager`, ni `GetTotalRevenue()`. Code fermé à modification, ouvert à extension.

```csharp
// ✅ Extension : Ajouter un nouveau type
public class OnlineWorkshop : Workshop
{
    public string Platform { get; set; }

    public override decimal GetPrice() => base.GetPrice() * 0.8m; // -20%
}

// ✅ Aucune modification du code existant
var manager = new EventManager();
manager.AddEvent(new OnlineWorkshop(...)); // Fonctionne !
manager.GetTotalRevenue(); // Fonctionne !
```
</details>

**6. Pourquoi `Register()` est virtual, pas abstract ?**

<details>
<summary>Voir la réponse</summary>

Implémentation par défaut convient pour Workshop/Conference. Seul Meetup redéfinit (surbooking). `virtual` permet réutilisation + spécialisation optionnelle.

```csharp
public abstract class Event
{
    // virtual = implémentation par défaut réutilisable
    public virtual bool Register(Participant p)
    {
        if (IsFull()) return false; // Comportement standard
        _attendees.Add(p);
        return true;
    }
}

public class Meetup : Event
{
    // override optionnel pour comportement spécial
    public override bool Register(Participant p)
    {
        if (_attendees.Count >= MaxCapacity * 1.2m) return false; // Surbooking
        _attendees.Add(p);
        return true;
    }
}

public class Workshop : Event
{
    // Pas de override = utilise implémentation par défaut
}
```
</details>

---

### ✅ Validation du module

**Checklist apprenant :**

- [ ] Exercice fil rouge : TechEvent Manager Phase 2 fonctionne
- [ ] Classes : Event abstraite + 3 types concrets implémentés
- [ ] Interface IBookable implémentée correctement
- [ ] Encapsulation : `_attendees` privée, accès contrôlé
- [ ] Polymorphisme : Liste mixte d'événements traités uniformément
- [ ] Tests : 6 tests essentiels passent, coverage ≥ 70%
- [ ] SOLID : Je peux expliquer les 5 principes appliqués
- [ ] Questions entretien : Je réponds aux 6 questions techniques

---

**⬅️ [Module 1 - Fondamentaux](../1-Module-Fondamentaux/1-module-fondamentaux.md) | [Retour au MECA](../README.md) | [Module 3 - Collections et LINQ](../3-Module-Collections-Linq/3-module-collections-linq.md) ➡️**

---
