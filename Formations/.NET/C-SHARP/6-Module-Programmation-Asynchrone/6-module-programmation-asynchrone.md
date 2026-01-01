# Montée en compétence auto-rythmée - C#

## 📚 Module 6 : Programmation Asynchrone

**🔗 Prérequis :** Module 1-5 ✅ (Fondamentaux, POO, Collections/LINQ, Gestion d'Erreurs, C# Moderne)

### Objectifs

- Comprendre async/await et Task de base
- Savoir faire des opérations parallèles simples
- Éviter les pièges de base (deadlocks, exceptions)
- Identifier quand utiliser l'asynchrone

---

### Contenu théorique

- **Task et Task<T>** : création et attente
- **Async/await** : syntaxe de base et propagation
- **Task.Delay()** : simulation d'opérations asynchrones
- **Task.WhenAll** : parallélisation simple
- **Exception handling** : try/catch avec async
- **Deadlocks** : comment les éviter

### Exemple rapide

```csharp
// Simulation d'une opération asynchrone
public class FileProcessor
{
    public async Task<string> ProcessFileAsync(string filename)
    {
        Console.WriteLine($"Starting to process {filename}...");

        await Task.Delay(2000); // 2 secondes

        Console.WriteLine($"Finished processing {filename}");
        return $"Processed content of {filename}";
    }

    // Traitement parallèle de plusieurs fichiers
    public async Task<List<string>> ProcessMultipleFilesAsync(List<string> filenames)
    {
        Console.WriteLine($"Processing {filenames.Count} files in parallel...");

        // Créer toutes les tâches
        var tasks = filenames.Select(name => ProcessFileAsync(name));

        // Attendre que toutes se terminent
        var results = await Task.WhenAll(tasks);

        return results.ToList();
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        var processor = new FileProcessor();

        // Traitement séquentiel
        var result1 = await processor.ProcessFileAsync("file1.txt");
        Console.WriteLine(result1);

        // Traitement parallèle
        var files = new List<string> { "file2.txt", "file3.txt", "file4.txt" };
        var results = await processor.ProcessMultipleFilesAsync(files);

        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }
}
```

---

### 📖 Ressources théoriques

- [Asynchronous programming - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/)
- [Task-based asynchronous pattern - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap)

---

## 🎯 Projet fil rouge : TechEvent Manager - Phase 6

### 📋 Contexte

**Phases précédentes :**

- Phase 1-5 : Système d'événements moderne et robuste

**Phase 6 :** Les opérations externes (emails, API, notifications) bloquent le thread. Il faut **rendre le système asynchrone** pour améliorer performances et réactivité.

**Objectif :** Transformer TechEvent Manager en système async avec envoi d'emails, API externes, opérations parallèles.

---

### 💻 Exercice pratique : TechEvent Manager Asynchrone

**Objectif :** Ajouter async/await au système d'événements pour gérer opérations I/O.

#### Code de base à améliorer (version synchrone) :

```csharp
namespace TechEventManager;

public class EmailService
{
    public void SendConfirmationEmail(Participant participant, Event evt)
    {
        Console.WriteLine($"Sending email to {participant.Email}...");
        Thread.Sleep(2000); // Simule appel SMTP
        Console.WriteLine($"Email sent to {participant.Email}");
    }
}

public class ExternalEventApi
{
    public EventDetails FetchEventDetails(int eventId)
    {
        Console.WriteLine($"Fetching details for event {eventId}...");
        Thread.Sleep(1500); // Simule appel API
        return new EventDetails { Id = eventId, ExternalRating = 4.5 };
    }
}

public class EventService
{
    private readonly EventSearchEngine _searchEngine;
    private readonly EmailService _emailService;

    public bool RegisterParticipant(int eventId, Participant participant)
    {
        var evt = _searchEngine.FindEventById(eventId);
        if (evt == null) throw new ArgumentException("Event not found");

        var success = evt.Register(participant);

        if (success)
        {
            // Bloque le thread pendant 2 secondes !
            _emailService.SendConfirmationEmail(participant, evt);
        }

        return success;
    }

    public List<bool> RegisterMultipleParticipants(int eventId, List<Participant> participants)
    {
        var results = new List<bool>();

        // Séquentiel - très lent si beaucoup de participants
        foreach (var participant in participants)
        {
            var result = RegisterParticipant(eventId, participant);
            results.Add(result);
        }

        return results;
    }
}

public record EventDetails(int Id, double ExternalRating);
```

---

## 🎯 Missions à Implémenter

### **Mission 1 : Conversion Async des Services Externes**

**À implémenter :**

```csharp
public class EmailService
{
    // TODO: Créer SendConfirmationEmailAsync(Participant, Event)
    // Valider paramètres, logger, simuler avec Task.Delay(2000), logger fin
}

public class ExternalEventApi
{
    // TODO: Créer FetchEventDetailsAsync(int eventId) → Task<EventDetails>
    // Valider eventId, logger, simuler avec Task.Delay(1500), retourner EventDetails
}
```

**Indice :** Thread.Sleep → await Task.Delay, void → Task, T → Task<T>

---

### **Mission 2 : Registration Asynchrone avec Email**

**À implémenter :**

```csharp
public class EventService
{
    private readonly EventSearchEngine _searchEngine;
    private readonly EmailService _emailService;

    // TODO: Créer RegisterParticipantAsync(int eventId, Participant) → Task<bool>
    // Trouver événement, inscrire, si succès envoyer email async, retourner résultat
}
```

**Indice :** Propager async jusqu'à l'appelant (Main async).

---

### **Mission 3 : Inscriptions Parallèles**

**À implémenter :**

```csharp
public class EventService
{
    // TODO: Créer RegisterMultipleParticipantsSequentialAsync (version séquentielle)
    // Utiliser foreach avec await dans la boucle → lent mais simple

    // TODO: Créer RegisterMultipleParticipantsAsync (version parallèle)
    // Créer toutes les tasks, puis await Task.WhenAll(tasks) → rapide
}
```

**Indice :** Task.WhenAll exécute toutes les tâches en parallèle.

---

### **Mission 4 : Gestion d'Erreurs Simple**

**À implémenter :**

```csharp
public class EmailService
{
    // TODO: Créer SendBulkEmailsAsync(List<Participant>, Event)
    // foreach avec try/catch pour continuer même si un email échoue
    // Compter sent et failed, logger résumé final
}
```

**Indice :** L'objectif est que si un email échoue, les autres continuent à s'envoyer.

---

### **Mission 5 : Programme de Démonstration**

**À implémenter :**

```csharp
// Demo console - Mettre tout ensemble
public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("=== TechEvent Manager Async Demo ===\n");

        // Initialiser les services
        var emailService = new EmailService();
        var api = new ExternalEventApi();
        var searchEngine = new EventSearchEngine();
        var eventService = new EventService(searchEngine, emailService);

        // TODO: Demo 1 - Inscription simple avec email
        // Créer workshop, participant, puis await RegisterParticipantAsync

        // TODO: Demo 2 - Comparer séquentiel vs parallèle
        // Utiliser Stopwatch pour comparer Sequential vs Parallel avec 3+ participants

        // TODO: Demo 3 - Envoi bulk avec gestion d'erreurs
        // Créer liste avec 1 email contenant "error", tester SendBulkEmailsAsync
    }
}
```

**Indice :** Utiliser `var sw = Stopwatch.StartNew();` puis `sw.Stop();` et `sw.ElapsedMilliseconds`.

---

## 🧪 Tests unitaires

**Objectif :** Apprendre à tester les méthodes asynchrones avec async/await.

**Format** : 2 exemples fournis + 4 à implémenter vous-même

---

### 📝 Exemples fournis (2 tests)

Voici 2 exemples complets pour vous montrer comment tester async/await :

```csharp
[Fact]
public async Task SendConfirmationEmailAsync_WithValidInputs_CompletesSuccessfully()
{
    // Arrange
    var emailService = new EmailService();
    var participant = new Participant("John", "john@test.com");
    var evt = new Workshop(1, "Test", DateTime.Now.AddDays(1), 20, "Paris", "Beginner", 2);

    // Act
    await emailService.SendConfirmationEmailAsync(participant, evt);

    // Assert
    // Pas d'exception = succès
    Assert.True(true);
}

[Fact]
public async Task SendConfirmationEmailAsync_NullParticipant_ThrowsArgumentNullException()
{
    // Arrange
    var emailService = new EmailService();
    var evt = new Workshop(1, "Test", DateTime.Now.AddDays(1), 20, "Paris", "Beginner", 2);

    // Act & Assert
    await Assert.ThrowsAsync<ArgumentNullException>(() =>
        emailService.SendConfirmationEmailAsync(null, evt));
}
```

---

### ✍️ Tests à implémenter vous-même (4 tests)

En vous inspirant des exemples, implémentez ces 4 tests pour valider async/await :

**Test 3 : SendBulkEmailsAsync - Gestion d'erreurs**
```csharp
[Fact]
public async Task SendBulkEmailsAsync_WithErrors_ContinuesProcessing()
{
    // TODO: Implémenter ce test
    // Créer un EmailService et un événement
    // Créer une liste avec 3 participants
    // Appeler SendBulkEmailsAsync (qui peut avoir des erreurs)
    // Vérifier qu'aucune exception n'est levée (gestion d'erreurs avec try/catch)
}
```

**Test 4 : RegisterMultipleParticipantsAsync - Exécution parallèle**
```csharp
[Fact]
public async Task RegisterMultipleParticipantsAsync_ParallelExecution_Works()
{
    // TODO: Implémenter ce test
    // Créer EventService avec plusieurs participants
    // Mesurer le temps d'exécution de RegisterMultipleParticipantsAsync
    // Vérifier que tous les participants sont inscrits correctement
    // Bonus: Comparer avec version séquentielle pour vérifier gain de performance
}
```

**Test 5 : GetEventStatisticsAsync - Combinaison de résultats**
```csharp
[Fact]
public async Task GetEventStatisticsAsync_CombinesResults_Correctly()
{
    // TODO: Implémenter ce test
    // Créer un événement avec plusieurs participants
    // Appeler GetEventStatisticsAsync qui combine plusieurs opérations async
    // Vérifier que les statistiques sont correctes (nombre d'inscrits, taux de remplissage)
}
```

**Test 6 : RegisterParticipantAsync - Délai d'attente**
```csharp
[Fact]
public async Task RegisterParticipantAsync_CompletesWithinTimeout()
{
    // TODO: Implémenter ce test
    // Créer un EventService
    // Mesurer le temps d'exécution avec Stopwatch
    // Vérifier que l'opération se termine en moins de 3 secondes
    // (l'envoi d'email simule 2 secondes)
}
```

**Indice** : Pour tester async, utilisez `async Task` comme type de retour et `await Assert.ThrowsAsync<>()` pour les exceptions.

---

### 🎓 Bonnes pratiques

#### **Async all the way**

```csharp
// ❌ MAUVAIS : Bloquer avec .Result
public void RegisterUser(int eventId, Participant p)
{
    var result = RegisterParticipantAsync(eventId, p).Result; // Deadlock risk!
}

// ✅ BON : Propager async
public async Task RegisterUserAsync(int eventId, Participant p)
{
    await RegisterParticipantAsync(eventId, p);
}
```

#### **Task.WhenAll pour parallélisme**

```csharp
// ❌ MAUVAIS : Await dans boucle = séquentiel
foreach (var p in participants)
{
    await SendEmailAsync(p); // Un par un
}

// ✅ BON : Task.WhenAll = parallèle
var tasks = participants.Select(p => SendEmailAsync(p));
await Task.WhenAll(tasks);
```

---

### 🎯 Questions d'entretien

**1. Différence entre Task.Wait() et await ?**

<details>
<summary>Voir la réponse</summary>

**`await`** libère le thread pendant l'attente (non-bloquant). **`Wait()`** bloque le thread (risque de deadlock). **Toujours préférer await**.

```csharp
// ❌ MAUVAIS : Wait() bloque le thread
public void RegisterUser(int eventId, Participant p)
{
    var task = RegisterParticipantAsync(eventId, p);
    task.Wait(); // Bloque le thread ! Risque deadlock en UI/ASP.NET
}

// ✅ BON : await libère le thread
public async Task RegisterUserAsync(int eventId, Participant p)
{
    await RegisterParticipantAsync(eventId, p); // Thread libre pendant l'attente
}

// Deadlock classique avec Wait()
public void DeadlockExample()
{
    // Dans un contexte UI ou ASP.NET
    var result = GetDataAsync().Result; // Deadlock !
    // GetDataAsync attend le contexte UI qui est bloqué par Result
}

// Solution
public async Task NoDeadlockExample()
{
    var result = await GetDataAsync(); // Pas de deadlock
}
```
</details>

**2. Comment faire des opérations en parallèle ?**

<details>
<summary>Voir la réponse</summary>

Créer toutes les tâches **sans await** : `var tasks = items.Select(x => DoAsync(x));` puis **`await Task.WhenAll(tasks);`** pour les exécuter en parallèle.

```csharp
// ❌ MAUVAIS : Séquentiel (une par une)
foreach (var participant in participants)
{
    await SendEmailAsync(participant); // Attend chacune
    // Total: 3 participants × 2s = 6 secondes
}

// ✅ BON : Parallèle avec Task.WhenAll
var tasks = participants.Select(p => SendEmailAsync(p));
await Task.WhenAll(tasks);
// Total: 2 secondes (toutes en même temps)

// Exemple complet
public async Task<List<EventDetails>> FetchMultipleEventsAsync(List<int> eventIds)
{
    // Créer toutes les tâches SANS await
    var tasks = eventIds.Select(id => FetchEventDetailsAsync(id));

    // Attendre que toutes se terminent
    var results = await Task.WhenAll(tasks);

    return results.ToList();
}

// Avec gestion d'erreurs
try
{
    await Task.WhenAll(tasks);
}
catch (Exception ex)
{
    // Première exception seulement
    // Toutes les exceptions dans aggregateException.InnerExceptions
}
```
</details>

**3. Que se passe-t-il si exception dans méthode async ?**

<details>
<summary>Voir la réponse</summary>

L'exception est **stockée dans la Task**, puis **relancée lors du await**. Avec `Task.WhenAll`, la **première exception est relancée** mais toutes sont capturées dans `task.Exception`.

```csharp
// Exception dans méthode async
public async Task<Event> GetEventAsync(int id)
{
    await Task.Delay(100);
    throw new EventNotFoundException(id); // Exception stockée dans Task
}

// Gestion de l'exception
try
{
    var evt = await GetEventAsync(123); // Exception relancée ici
}
catch (EventNotFoundException ex)
{
    Console.WriteLine($"Event not found: {ex.EventId}");
}

// Avec Task.WhenAll
var tasks = new[]
{
    SendEmailAsync(p1), // Réussit
    SendEmailAsync(p2), // Lance InvalidEmailException
    SendEmailAsync(p3)  // Lance TimeoutException
};

try
{
    await Task.WhenAll(tasks);
}
catch (Exception ex)
{
    // ex = InvalidEmailException (première exception)

    // Accéder à TOUTES les exceptions
    var aggregateTask = Task.WhenAll(tasks);
    try { await aggregateTask; }
    catch
    {
        foreach (var innerEx in aggregateTask.Exception.InnerExceptions)
        {
            Console.WriteLine($"Error: {innerEx.Message}");
        }
    }
}
```
</details>

**4. Pourquoi éviter .Result ou .Wait() ?**

<details>
<summary>Voir la réponse</summary>

**`.Result` et `.Wait()` bloquent le thread**, créent un **risque de deadlock** (surtout en UI/ASP.NET), et causent une **perte de stack trace**. **Utiliser async all the way**.

```csharp
// ❌ MAUVAIS : Utilisation de .Result
public Event GetEvent(int id)
{
    // Bloque le thread, risque deadlock
    return GetEventAsync(id).Result;
}

// ❌ MAUVAIS : Utilisation de .Wait()
public void RegisterUser(Participant p)
{
    var task = RegisterParticipantAsync(p);
    task.Wait(); // Bloque le thread
}

// ✅ BON : Async all the way
public async Task<Event> GetEventAsync(int id)
{
    return await FetchEventFromDatabaseAsync(id);
}

public async Task RegisterUserAsync(Participant p)
{
    await RegisterParticipantAsync(p);
}

// Problème de deadlock en ASP.NET
// Controller ASP.NET Core
public IActionResult GetData()
{
    // ❌ Deadlock !
    var data = GetDataAsync().Result;
    return Ok(data);
}

// ✅ Solution
public async Task<IActionResult> GetDataAsync()
{
    var data = await GetDataAsync();
    return Ok(data);
}
```
</details>

**5. Quand utiliser Task.Delay vs Thread.Sleep ?**

<details>
<summary>Voir la réponse</summary>

**`Task.Delay`** est async et **libère le thread**. **`Thread.Sleep`** est synchrone et **bloque le thread**. **Toujours utiliser `Task.Delay` dans du code async**.

```csharp
// ❌ MAUVAIS : Thread.Sleep bloque le thread
public async Task SendEmailAsync(Participant p)
{
    Console.WriteLine("Sending email...");
    Thread.Sleep(2000); // ❌ Bloque le thread pendant 2 secondes !
    Console.WriteLine("Email sent");
}

// ✅ BON : Task.Delay libère le thread
public async Task SendEmailAsync(Participant p)
{
    Console.WriteLine("Sending email...");
    await Task.Delay(2000); // ✅ Thread libre pendant 2 secondes
    Console.WriteLine("Email sent");
}

// Exemple concret de différence
public async Task ProcessMultipleFilesAsync()
{
    var tasks = new[]
    {
        ProcessWithThreadSleep(), // Bloque 3 threads
        ProcessWithThreadSleep(),
        ProcessWithThreadSleep()
    };

    await Task.WhenAll(tasks); // 3 secondes, 3 threads bloqués
}

public async Task ProcessWithThreadSleep()
{
    Thread.Sleep(3000); // Thread bloqué
}

// vs

public async Task ProcessMultipleFilesOptimizedAsync()
{
    var tasks = new[]
    {
        ProcessWithTaskDelay(), // Libère les threads
        ProcessWithTaskDelay(),
        ProcessWithTaskDelay()
    };

    await Task.WhenAll(tasks); // 3 secondes, 0 threads bloqués
}

public async Task ProcessWithTaskDelay()
{
    await Task.Delay(3000); // Thread libre
}
```
</details>

---

### ✅ Validation du module

**Checklist TechEvent Manager - Phase 6 :**

- [ ] **Mission 1** : EmailService.SendConfirmationEmailAsync() implémentée avec await Task.Delay(2000)
- [ ] **Mission 1** : ExternalEventApi.FetchEventDetailsAsync() implémentée avec await Task.Delay(1500)
- [ ] **Mission 2** : EventService.RegisterParticipantAsync() qui envoie un email async
- [ ] **Mission 3** : RegisterMultipleParticipantsSequentialAsync() avec foreach + await
- [ ] **Mission 3** : RegisterMultipleParticipantsAsync() avec Task.WhenAll
- [ ] **Mission 4** : SendBulkEmailsAsync() avec try/catch pour gérer les erreurs
- [ ] **Mission 5** : Programme Main avec 3 démos fonctionnelles
- [ ] Je constate que la version parallèle est plus rapide (Stopwatch)
- [ ] Aucun .Result ou .Wait() dans mon code
- [ ] Je réponds correctement aux 5 questions d'entretien

---

**⬅️ [Module 5 - C# Moderne](../5-Module-Csharp-Moderne/5-module-Csharp-moderne.md) | [Retour au MECA](../README.md) | [Module 7 - Test avancé](../Module-Test-Advanced/7-module-test-advanced.md) ➡️**

---
