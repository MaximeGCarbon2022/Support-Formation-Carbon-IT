# Montée en compétence auto-rythmée - C#

## 📚 Module 0 : Setup & Conventions

### Objectif

**Préparer votre environnement de développement .NET et apprendre les conventions de nommage professionnelles AVANT d'écrire votre première ligne de code.**

Ce module est un **prérequis obligatoire** pour tous les autres modules. Il garantit que :

- Votre environnement est correctement configuré
- Vous connaissez les standards professionnels .NET
- Vous êtes prêt à coder proprement dès le Module 1

---

## 🛠️ Installation et Configuration

### 1. Installation .NET 9 SDK

Le **.NET SDK** est l'outil de base pour développer en C#. Il inclut le compilateur, les bibliothèques, et l'outil CLI.

#### **Windows :**

1. **Téléchargez le .NET 9 SDK :**

   - [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
   - Choisissez : ".NET 9.0 SDK" (PAS le Runtime seul)

2. **Suivez le guide d'installation :**

   - [Guide Microsoft - Install .NET on Windows](https://learn.microsoft.com/en-us/dotnet/core/install/windows)
   - Installer avec les options par défaut

3. **Vérifiez l'installation :**

```bash
dotnet --version
# Devrait afficher : 9.0.x ou supérieur
```

**Si la commande ne fonctionne pas :**

- Redémarrez votre terminal/IDE
- Vérifiez que le PATH Windows inclut le SDK .NET
- Réinstallez si nécessaire

#### **Commandes .NET CLI essentielles**

```bash
# Créer un nouveau projet console
dotnet new console -n MonProjet

# Compiler le projet
dotnet build

# Exécuter le projet
dotnet run

# Lancer les tests
dotnet test

# Restaurer les dépendances NuGet
dotnet restore

# Nettoyer les fichiers de build
dotnet clean
```

**📖 Documentation complète :** [.NET CLI Overview](https://learn.microsoft.com/en-us/dotnet/core/tools/)

---

### 2. Configuration IDE

Vous avez le choix entre **Visual Studio 2022** ou **Visual Studio Code**.

#### **Option A : Visual Studio 2022 (Recommandé pour débutants)**

**Avantages :**

- Environnement complet "tout-en-un"
- Débogueur puissant intégré
- Meilleure expérience pour C# (.NET)
- IntelliSense avancé

**Installation :**

1. Téléchargez [Visual Studio 2022 Community](https://visualstudio.microsoft.com/downloads/) (gratuit)
2. Lors de l'installation, sélectionnez la workload : **".NET desktop development"**
3. Lancez VS 2022 et créez un projet test

#### **Option B : Visual Studio Code (Plus léger)**

**Avantages :**

- Léger et rapide
- Multi-plateforme
- Extensible

**Installation :**

1. Téléchargez [Visual Studio Code](https://code.visualstudio.com/)
2. Installez l'extension **"C# Dev Kit"** (Microsoft)
3. Redémarrez VS Code

**Extensions recommandées :**

- C# Dev Kit (essentiel)
- NuGet Package Manager
- GitLens (optionnel)

---

### 3. Configuration UTF-8 (IMPORTANT)

⚠️ **PROBLÈME COURANT :** Sans cette configuration, les symboles spéciaux (€, é, à, etc.) ne s'affichent pas correctement dans la console.

**Solution :** Ajoutez cette ligne **au début de CHAQUE `Program.cs`** :

```csharp
Console.OutputEncoding = System.Text.Encoding.UTF8;
```

**Exemple complet :**

```csharp
using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;  // ← TOUJOURS en premier

Console.WriteLine("Prix : 25.50€");      // ✅ Affiche correctement le €
Console.WriteLine("Créé par François");  // ✅ Affiche correctement le ç
```

Sans cette ligne :

```
Prix : 25.50Ç  ❌
Cr?? par Fran?ois  ❌
```

---

## 📐 Conventions de nommage .NET (FONDAMENTAL)

**C'est la BASE du code professionnel .NET.** En entreprise, ne pas respecter ces conventions = code rejeté en code review.

### Pourquoi c'est important ?

✅ **Lisibilité** : Code facile à lire et comprendre
✅ **Cohérence** : Tous les développeurs .NET suivent les mêmes règles
✅ **Professionnalisme** : Attendu en entreprise et en mission
✅ **IDE** : Visual Studio/Rider appliquent ces conventions automatiquement

---

### **1. Classes, Méthodes, Propriétés : PascalCase**

Chaque mot commence par une majuscule, pas d'underscore.

```csharp
// ✅ CORRECT
public class EventCalculator { }
public class UserService { }
public void CalculateFillRate() { }
public void ProcessPayment() { }
public string EventName { get; set; }
public int AttendeeCount { get; set; }

// ❌ INCORRECT
public class eventCalculator { }        // Pas de minuscule au début
public class Event_Calculator { }       // Pas d'underscore
public void calculate_fill_rate() { }   // snake_case (Python/Ruby)
public void calculateFillRate() { }     // camelCase (JavaScript)
```

---

### **2. Variables locales et paramètres : camelCase**

Première lettre en minuscule, mots suivants en majuscule.

```csharp
// ✅ CORRECT
int attendeeCount = 50;
string eventName = "Tech Meetup";
decimal ticketPrice = 25.99m;

public void CalculateRevenue(int attendees, decimal price)
{
    var totalRevenue = attendees * price;
}

// ❌ INCORRECT
int AttendeeCount = 50;           // PascalCase (réservé aux propriétés)
int attendee_count = 50;          // snake_case
int Attendee_Count = 50;          // Mix (horrible)
```

---

### **3. Champs privés : \_camelCase (underscore prefix)**

Les champs privés commencent par `_` suivi de camelCase.

```csharp
public class EventService
{
    // ✅ CORRECT - Champs privés
    private int _maxCapacity;
    private readonly string _eventName;
    private readonly ILogger _logger;

    // ❌ INCORRECT
    private int maxCapacity;          // Manque l'underscore
    private int MaxCapacity;          // PascalCase
    private int m_maxCapacity;        // Notation hongroise (obsolète)
}
```

**💡 Pourquoi l'underscore ?**

- Différencie visuellement champs privés vs variables locales
- Convention Microsoft officielle
- Évite les conflits de noms avec paramètres

**Exemple complet :**

```csharp
public class Calculator
{
    private readonly int _maxValue;    // Champ privé

    public Calculator(int maxValue)    // Paramètre
    {
        _maxValue = maxValue;          // Assignation claire
    }

    public int Calculate(int input)    // Paramètre
    {
        var result = input * 2;        // Variable locale
        return Math.Min(result, _maxValue);
    }
}
```

---

### **4. Constantes : PascalCase**

Les constantes utilisent PascalCase (PAS SCREAMING_CASE).

```csharp
// ✅ CORRECT - .NET Style
public const int MaxAttendees = 100;
public const decimal VatRate = 0.20m;
private const string DefaultEventName = "Untitled Event";

// ❌ INCORRECT - Style C/Java (obsolète en C#)
public const int MAX_ATTENDEES = 100;        // SCREAMING_CASE
public const decimal VAT_RATE = 0.20m;
```

---

### **5. Interfaces : IPascalCase (préfixe I)**

Les interfaces commencent toujours par `I` majuscule.

```csharp
// ✅ CORRECT
public interface IEventService { }
public interface ICalculator { }
public interface IRepository<T> { }

// ❌ INCORRECT
public interface EventService { }       // Manque le I
public interface iEventService { }      // i minuscule
public interface InterfaceEventService { }  // Verbeux
```

---

### **6. Namespaces : PascalCase**

```csharp
// ✅ CORRECT
namespace EventManagement.Services { }
namespace Company.Product.Module { }

// ❌ INCORRECT
namespace eventManagement.services { }
namespace Event_Management { }
```

---

### **7. Résumé visuel des conventions**

| Type                | Convention  | Exemple                     |
| ------------------- | ----------- | --------------------------- |
| **Classe**          | PascalCase  | `EventCalculator`           |
| **Méthode**         | PascalCase  | `CalculateRevenue()`        |
| **Propriété**       | PascalCase  | `EventName { get; set; }`   |
| **Variable locale** | camelCase   | `attendeeCount`             |
| **Paramètre**       | camelCase   | `void Process(int count)`   |
| **Champ privé**     | \_camelCase | `private int _maxCapacity;` |
| **Constante**       | PascalCase  | `const int MaxValue = 100;` |
| **Interface**       | IPascalCase | `IEventService`             |
| **Namespace**       | PascalCase  | `Company.Product`           |

---

### **8. L'IDE vous aide !**

**Visual Studio / Rider détecte les violations :**

```csharp
public void calculate_revenue()  // ⚠️ Soulignement vert : "Use PascalCase"
{
    int Total_Price = 100;       // ⚠️ Soulignement vert : "Use camelCase"
}
```

**💡 Conseil :** Suivez TOUJOURS les suggestions de l'IDE pour les conventions de nommage.

---

**📖 Référence officielle :** [C# Coding Conventions - Microsoft](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

---

## 📦 Introduction à NuGet

**NuGet** est le gestionnaire de packages pour .NET. Il permet d'installer des bibliothèques tierces dans vos projets.

### Pourquoi NuGet ?

Au lieu de réinventer la roue, utilisez des bibliothèques existantes :

- **xUnit** : Tests unitaires
- **Newtonsoft.Json** : Manipulation JSON
- **Dapper** : Accès aux données
- **Serilog** : Logging avancé

### Commandes essentielles

```bash
# Rechercher un package
dotnet search xunit

# Installer un package dans le projet actuel
dotnet add package xunit

# Installer une version spécifique
dotnet add package xunit --version 2.9.3

# Lister les packages installés
dotnet list package

# Mettre à jour un package
dotnet add package xunit  # Installe la dernière version

# Supprimer un package
dotnet remove package xunit
```

### Où sont enregistrés les packages ?

Dans le fichier `.csproj` de votre projet :

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="xunit" Version="2.9.3" />
    <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
  </ItemGroup>
</Project>
```

### Où trouver des packages ?

- **Site officiel :** [https://www.nuget.org/](https://www.nuget.org/)
- **Recherche :** Tapez ce que vous cherchez (ex: "json", "http", "testing")
- **Popularité :** Regardez le nombre de téléchargements (millions = fiable)
- **Maintenance :** Vérifiez la date de dernière mise à jour

**📖 Documentation :** [Introduction to NuGet](https://learn.microsoft.com/en-us/nuget/what-is-nuget)

---

## ✅ Checklist de validation

Avant de passer au Module 1, vérifiez que vous avez tout configuré :

### Installation

- [ ] .NET 9 SDK installé (`dotnet --version` fonctionne)
- [ ] Visual Studio 2022 ou VS Code avec C# Dev Kit installé
- [ ] Vous savez créer un projet : `dotnet new console -n Test`
- [ ] Vous savez compiler : `dotnet build`
- [ ] Vous savez exécuter : `dotnet run`

### Configuration

- [ ] UTF-8 configuré (`Console.OutputEncoding = Encoding.UTF8;`)
- [ ] Vous voyez le symbole € correctement dans la console
- [ ] Vous comprenez comment installer un package NuGet

### Conventions de nommage

- [ ] Je connais PascalCase (classes, méthodes, propriétés)
- [ ] Je connais camelCase (variables, paramètres)
- [ ] Je connais \_camelCase (champs privés)
- [ ] Je sais que les interfaces commencent par `I`
- [ ] Je sais que les constantes utilisent PascalCase (PAS SCREAMING_CASE)

**💡 Si tout est coché :** Vous êtes prêt pour le Module 1 ! 🚀

---

## 📖 Ressources complémentaires

### Documentation officielle

- [Get started with .NET](https://learn.microsoft.com/en-us/dotnet/core/get-started)
- [.NET CLI Overview](https://learn.microsoft.com/en-us/dotnet/core/tools/)
- [C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

### Tutoriels

- [.NET Interactive Tutorials](https://dotnet.microsoft.com/learn/dotnet/in-browser-tutorial/1)
- [C# for Beginners - Microsoft Learn](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/)

---

## 🚀 Prochaine étape

**Module 0 terminé ? Passez au [Module 1 - Fondamentaux C#](../1-Module-Fondamentaux/1-module-fondamentaux.md) !**

Vous allez maintenant apprendre la syntaxe C# et créer votre première application : **EventCalculator** 💪
