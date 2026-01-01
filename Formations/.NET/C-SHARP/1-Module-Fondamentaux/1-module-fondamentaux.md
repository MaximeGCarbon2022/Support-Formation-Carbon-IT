# Montée en compétence auto-rythmée - C#

## 📚 Module 1 : Fondamentaux C#

### Objectifs

- Maîtriser la syntaxe de base de C#
- Comprendre les types de données et leur utilisation
- Savoir utiliser `var`, les collections de base, et l'interpolation de chaînes
- Savoir structurer son code avec des méthodes

---

## ⚠️ Prérequis

**Avant de commencer ce module, vous DEVEZ avoir terminé le [Module 0 - Setup & Conventions](../0-Module-Setup/0-module-setup.md).**

Le Module 0 vous apprend :

- Installation .NET 9 SDK
- Configuration UTF-8 (symbole € qui s'affiche correctement)
- **Conventions de nommage .NET** (PascalCase, camelCase, \_camelCase, etc.)
- NuGet packages

**Sans le Module 0, vous risquerez d'écrire du code non conforme aux standards professionnels .NET.**

---

## 📝 Rappel rapide : Configuration UTF-8

Ajoutez **au début de votre `Program.cs`** :

```csharp
Console.OutputEncoding = System.Text.Encoding.UTF8;
```

Cela garantit l'affichage correct du symbole `€` dans vos calculs de revenus.

---

## 🛠️ Créer votre projet EventCalculator

```bash
# Créer le projet console
dotnet new console -n EventCalculator
cd EventCalculator

# Ouvrir dans VS Code (optionnel)
code .
```

**N'oubliez pas** d'ajouter `Console.OutputEncoding = Encoding.UTF8;` au début de votre `Program.cs` !

---

### Contenu théorique

- **Types de données** : int, string, bool, decimal, DateTime
- **Inférence de type avec `var`** : quand et comment l'utiliser
- **Variables et constantes** : déclaration, initialisation, portée
- **Collections de base** : arrays et `List<T>` (introduction)
- **String interpolation** : formatage et manipulation de chaînes
- **Structures de contrôle** : if/else, switch expressions (C# 8+), boucles
- **Méthodes** : paramètres, valeurs de retour, overloading

### Exemples détaillés

#### 1. Types de données et `var` keyword

```csharp
// Typage explicite (traditionnel)
int attendeeCount = 50;
string eventName = "Tech Meetup";
decimal ticketPrice = 25.99m;
DateTime eventDate = DateTime.Now;

// ✅ Typage implicite avec var (MODERNE - recommandé par l'IDE)
var count = 50;                    // Type inféré : int
var name = "Tech Meetup";          // Type inféré : string
var price = 25.99m;                // Type inféré : decimal
var date = DateTime.Now;           // Type inféré : DateTime

// ⚠️ IMPORTANT : 'var' n'est PAS 'dynamic' !
// Le type est déterminé à la compilation, pas à l'exécution
var number = 10;
// number = "text";                 // ❌ ERREUR de compilation !

// 💡 Quand utiliser var ?
// - Quand le type est évident : var list = new List<string>();  ✅
// - Quand le type est complexe : var result = students.Where(...).Select(...);  ✅
// - À ÉVITER si ça réduit la lisibilité : var x = GetSomething();  ❌ (type pas clair)
```

#### 2. Collections de base (introduction)

```csharp
// Tableau (array) - taille fixe
int[] scores = new int[3];         // Tableau de 3 entiers
scores[0] = 85;
scores[1] = 92;
scores[2] = 78;

// Ou initialisation directe
int[] numbers = { 10, 20, 30, 40 };

// List<T> - taille dynamique (PLUS UTILISÉ)
var attendees = new List<string>();
attendees.Add("Alice");
attendees.Add("Bob");
attendees.Add("Charlie");

// Initialisation directe de List
var eventNames = new List<string> { "Workshop C#", "Meetup .NET", "Conference Azure" };

// Parcourir une collection
foreach (var name in attendees)
{
    Console.WriteLine($"Participant: {name}");
}

Console.WriteLine($"Total participants: {attendees.Count}");
```

**💡 Note :** Les collections seront approfondies au Module 3. Pour l'instant, sachez juste utiliser `List<T>` de base.

#### 3. String interpolation (ESSENTIEL)

```csharp
var firstName = "John";
var lastName = "Doe";
var age = 28;
var salary = 45000.50m;

// ✅ MODERNE : String interpolation (recommandé)
var message = $"Hello {firstName} {lastName}, you are {age} years old";
Console.WriteLine(message);
// Output: Hello John Doe, you are 28 years old

// Formatage dans l'interpolation
Console.WriteLine($"Salary: {salary:C}");           // C = Currency → 45 000,50 €
Console.WriteLine($"Salary: {salary:F2}");          // F2 = 2 decimales → 45000.50
Console.WriteLine($"Age in 5 years: {age + 5}");    // Expressions possibles

// ❌ ANCIENNE MÉTHODE : Concatenation (à éviter)
var oldWay = "Hello " + firstName + " " + lastName;  // Moins lisible

// ❌ ANCIENNE MÉTHODE : String.Format (verbeux)
var oldWay2 = String.Format("Hello {0} {1}", firstName, lastName);
```

#### 4. Structures de contrôle et méthodes

```csharp
// Structure de contrôle
int attendeeCount = 45;
if (attendeeCount >= 50)
{
    Console.WriteLine("Event is full");
}
else
{
    Console.WriteLine($"Still {50 - attendeeCount} spots available");
}

// Méthode simple avec conventions de nommage
public static int CalculateRevenue(int attendees, decimal ticketPrice)
{
    return (int)(attendees * ticketPrice);
}

// Méthode avec var dans le corps
public static double CalculateFillRate(int attendees, int capacity)
{
    var fillRate = (double)attendees / capacity * 100;
    return fillRate;
}
```

---

### 📖 Ressources théoriques

- [Write your first code using C# - Microsoft Learn](https://learn.microsoft.com/en-us/training/paths/get-started-c-sharp-part-1/)
- [Interactive C# Tutorials](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/)
- [C# Guide - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/)

### 🎥 Vidéos recommandées

- [C# for Beginners - Microsoft Learn](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/)
- [C# for Beginners - The Basics of Strings](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/the-basics-of-strings-csharp-for-beginners)
- [C# for Beginners - Numbers, Integers, and Math](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/numbers-integers-and-math-csharp-for-beginners)
- [C# for Beginners - Branching, Ifs, and Conditional Logic](https://learn.microsoft.com/en-us/shows/csharp-for-beginners/branching-ifs-and-conditional-logic-csharp-for-beginners)

---

## 🎯 Projet fil rouge : TechEvent Manager

### 📋 Contexte

Vous rejoignez une ESN qui organise régulièrement des événements techniques (meetups, workshops, conférences) pour ses consultants en intercontrat et ses clients. Actuellement, tout est géré sur Excel avec des formules qui cassent régulièrement.

**Votre mission :** Créer progressivement une solution .NET complète pour automatiser la gestion de ces événements.

**Phase 1 (Module 1) :** Outils de calcul en console pour aider à la prise de décision.

---

### 💻 Exercice pratique : Calculatrice d'événements

**Objectif :** Créer une application console qui calcule les indicateurs clés pour décider si un événement est viable.

#### 📋 Fonctionnalités requises

**F1 - Calculer le taux de remplissage**

- Entrées : nombre d'inscrits, capacité maximale de la salle
- Sortie : pourcentage de remplissage
- Exemple : 45 inscrits / 50 places = 90%
- Affichage : `"Taux de remplissage : 90.0%"`

**F2 - Calculer le revenu estimé**

- Entrées : nombre d'inscrits, prix du ticket
- Sortie : revenu total
- Exemple : 45 inscrits × 25€ = 1125€
- Affichage : `"Revenu estimé : 1125.00€"`

**F3 - Déterminer la rentabilité**

- Entrées : revenu estimé, coûts fixes (salle + traiteur + intervenants)
- Sortie : bénéfice ou perte
- Affichage colorée :
  - Si bénéfice : `"✅ Rentable : +500.00€"` (vert)
  - Si perte : `"❌ Perte : -200.00€"` (rouge)

**F4 - Calculer le seuil de rentabilité**

- Entrées : coûts fixes, prix du ticket
- Sortie : nombre minimum d'inscrits pour être rentable
- Exemple : 800€ de coûts / 25€ par ticket = 32 participants minimum
- Affichage : `"Seuil de rentabilité : 32 participants"`

**F5 - Menu interactif**

- Afficher un menu clair avec les 4 calculs disponibles
- Permettre de refaire des calculs sans relancer le programme (boucle)
- Option "0" pour quitter proprement
- Validation des entrées utilisateur (pas de crash)

#### 🏗️ Structure de code suggérée

```csharp
class EventCalculator
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== TechEvent Manager - Calculatrice v1.0 ===\n");

        bool isRunning = true;
        while (isRunning)
        {
            DisplayMenu();
            int choice = GetMenuChoice();

            switch (choice)
            {
                case 1:
                    CalculateFillRateMenu();
                    break;
                case 2:
                    CalculateRevenueMenu();
                    break;
                case 3:
                    DetermineProfitabilityMenu();
                    break;
                case 4:
                    CalculateBreakEvenMenu();
                    break;
                case 0:
                    isRunning = false;
                    Console.WriteLine("Au revoir !");
                    break;
                default:
                    Console.WriteLine("❌ Choix invalide");
                    break;
            }

            if (isRunning)
            {
                Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    // Affichage
    static void DisplayMenu() { /* ... */ }
    static int GetMenuChoice() { /* ... */ }
    static double GetPositiveNumber(string prompt) { /* ... */ }

    // Calculs métier (à tester unitairement)
    static double CalculateFillRate(int attendees, int capacity) { /* ... */ }
    static double CalculateRevenue(int attendees, double ticketPrice) { /* ... */ }
    static double DetermineProfitability(double revenue, double costs) { /* ... */ }
    static int CalculateBreakEven(double costs, double ticketPrice) { /* ... */ }

    // Orchestration (appels depuis le menu)
    static void CalculateFillRateMenu() { /* ... */ }
    static void CalculateRevenueMenu() { /* ... */ }
    static void DetermineProfitabilityMenu() { /* ... */ }
    static void CalculateBreakEvenMenu() { /* ... */ }
}
```

#### ⚠️ Points d'attention

**Validation des entrées :**

- Utilisez `int.TryParse()` et `double.TryParse()`
- Redemandez la saisie si invalide (pas de crash)
- Vérifiez que les nombres sont positifs

**Gestion des erreurs :**

- Division par zéro : capacité = 0 ou prix = 0
- Levez une `DivideByZeroException` dans les méthodes de calcul
- Capturez l'exception dans le menu et affichez un message clair

**Qualité du code :**

- Séparez la logique métier (calculs) de l'UI (menu, affichage)
- Une méthode = une responsabilité
- Nommage en anglais et explicite
- Commentaires sur les méthodes complexes

#### 🎓 Bonnes pratiques à appliquer

**Principe SRP (Single Responsibility Principle)**

Chaque méthode doit avoir **une seule raison de changer**.

```csharp
// ❌ MAUVAIS : Une méthode fait calcul + affichage
static void CalculateFillRate()
{
    Console.Write("Inscrits : ");
    int attendees = int.Parse(Console.ReadLine());
    // ... calcul ...
    Console.WriteLine($"Résultat : {result}%");
}

// ✅ BON : Séparer calcul (testable) et affichage (UI)
static double CalculateFillRate(int attendees, int capacity)
{
    return (double)attendees / capacity * 100; // Pure logique
}

static void CalculateFillRateMenu()
{
    // Gère uniquement l'interaction utilisateur
    int attendees = GetPositiveInteger("Inscrits : ");
    int capacity = GetPositiveInteger("Capacité : ");
    double result = CalculateFillRate(attendees, capacity);
    Console.WriteLine($"Résultat : {result:F1}%");
}
```

**Principe DRY (Don't Repeat Yourself)**

Ne dupliquez jamais du code. Si vous copiez-collez, créez une méthode réutilisable.

```csharp
// ❌ MAUVAIS : Duplication de la validation
static void Method1()
{
    Console.Write("Prix : ");
    string input = Console.ReadLine();
    if (!double.TryParse(input, out double price) || price < 0)
    {
        Console.WriteLine("Erreur");
        return;
    }
}

static void Method2()
{
    Console.Write("Coût : ");
    string input = Console.ReadLine();
    if (!double.TryParse(input, out double cost) || cost < 0)
    {
        Console.WriteLine("Erreur");
        return;
    }
}

// ✅ BON : Une seule méthode réutilisable
static double GetPositiveNumber(string prompt)
{
    // Logique centralisée, une seule fois
    // Utilisée par Method1 ET Method2
}
```

---

### 🎓 Bonnes pratiques avancées (Bonus optionnel)

Une fois les principes SRP et DRY maîtrisés, vous pouvez explorer ces concepts :

<details>
<summary>Principe KISS (Keep It Simple, Stupid)</summary>

Privilégiez la simplicité. Un code simple est plus facile à lire, tester et maintenir.

```csharp
// ❌ COMPLEXE : Ternaires imbriqués
string message = fillRate > 90 ? "Complet" :
                 fillRate > 50 ? "Moyen" :
                 fillRate > 0 ? "Faible" : "Vide";

// ✅ SIMPLE : if/else clairement lisible
string message;
if (fillRate > 90)
    message = "Complet";
else if (fillRate > 50)
    message = "Moyen";
else if (fillRate > 0)
    message = "Faible";
else
    message = "Vide";
```

</details>

<details>
<summary>Principe YAGNI (You Aren't Gonna Need It)</summary>

N'implémentez que ce qui est demandé. Pas de sur-ingénierie.

```csharp
// ❌ SUR-INGÉNIERIE : Fonctionnalités "au cas où"
class EventCalculator
{
    private List<Calculation> history;      // Pas demandé
    private ILogger logger;                 // Pas demandé
    private IConfigurationService config;   // Pas demandé
}

// ✅ JUSTE CE QU'IL FAUT : Répondre au besoin actuel
class EventCalculator
{
    static void Main() { /* Menu simple */ }
    static double CalculateFillRate(int a, int c) { /* Calcul */ }
}
```

</details>

**Bonus (optionnel) :**

- Affichage avec couleurs (`Console.ForegroundColor` pour succès/erreur)
- Formater les montants avec symbole € (string interpolation)
- Ajouter une confirmation avant de quitter ("Êtes-vous sûr ?")

---

### ✅ Critères d'acceptance

**Technique :**

- [ ] Le code compile sans erreur ni warning
- [ ] Utilisation de `double` pour les calculs avec décimales
- [ ] Utilisation de `int` pour les nombres de participants
- [ ] Validation robuste des entrées avec `TryParse()`
- [ ] Gestion explicite de la division par zéro
- [ ] Au moins 8 méthodes distinctes (séparation calcul/UI)
- [ ] Code formatté et indenté correctement

**Fonctionnel :**

- [ ] Les 4 calculs retournent les valeurs mathématiquement correctes
- [ ] Le menu s'affiche clairement et est compréhensible
- [ ] La boucle permet de refaire plusieurs calculs sans relancer
- [ ] L'option "0" permet de quitter proprement
- [ ] Les messages d'erreur sont clairs et explicites
- [ ] L'application ne crash JAMAIS (même avec des entrées absurdes)

**Tests unitaires :**

- [ ] Projet de tests créé avec xUnit
- [ ] Au minimum 6 tests essentiels qui passent (vert)
- [ ] Code coverage > 70% sur les méthodes de calcul
- [ ] Pattern AAA respecté (Arrange, Act, Assert)

**Livrables attendus :**

```
1-Module-Fondamentaux/
├── EventCalculator/
│   ├── EventCalculator.csproj
│   └── Program.cs
└── EventCalculator.Tests/
    ├── EventCalculator.Tests.csproj
    └── EventCalculatorTests.cs
```

---

### 🧪 Tests unitaires

**Objectif :** Apprendre les bases des tests avec xUnit dès le premier module.

#### 📦 Setup du projet de tests

```bash
# Créer le projet de tests
dotnet new xunit -n EventCalculator.Tests

# Ajouter les packages nécessaires
cd EventCalculator.Tests
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package Microsoft.NET.Test.Sdk

# ⚠️ IMPORTANT : Ajouter les 2 packages Coverlet pour le code coverage
dotnet add package coverlet.collector
dotnet add package coverlet.msbuild

# Référencer le projet principal
dotnet add reference ../EventCalculator/EventCalculator.csproj
```

**💡 Note :** `coverlet.collector` ET `coverlet.msbuild` sont tous les deux nécessaires pour générer correctement les rapports de couverture de code.

#### ✅ Tests à implémenter (6 au total)

**Format** : 2 exemples fournis + 4 à implémenter vous-même

---

### 📝 Exemples fournis (2 tests)

Voici 2 exemples complets pour vous montrer comment écrire les tests :

```csharp
using Xunit;

namespace EventCalculator.Tests;

public class EventCalculatorTests
{
    [Fact]
    public void CalculateFillRate_NormalCase_ReturnsCorrectPercentage()
    {
        // Arrange
        int attendees = 45;
        int capacity = 50;
        double expected = 90.0;

        // Act
        double result = EventCalculator.CalculateFillRate(attendees, capacity);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculateFillRate_ZeroCapacity_ThrowsException()
    {
        // Arrange
        int attendees = 10;
        int capacity = 0;

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() =>
            EventCalculator.CalculateFillRate(attendees, capacity));
    }

}
```

---

### ✍️ Tests à implémenter vous-même (4 tests)

En vous inspirant des 2 exemples ci-dessus, implémentez ces 4 tests :

**Test 3 : CalculateRevenue - Cas nominal**

```csharp
[Fact]
public void CalculateRevenue_NormalCase_ReturnsCorrectAmount()
{
    // TODO: Implémenter ce test
    // Arrange : attendees = 45, ticketPrice = 25.0, expected = 1125.0
    // Act : Appeler CalculateRevenue
    // Assert : Vérifier que result == expected
}
```

**Test 4 : DetermineProfitability - Bénéfice**

```csharp
[Fact]
public void DetermineProfitability_Profit_ReturnsPositive()
{
    // TODO: Implémenter ce test
    // Arrange : revenue = 1125.0, costs = 800.0, expected = 325.0
    // Act : Appeler DetermineProfitability
    // Assert : Vérifier result == expected (bénéfice positif)
}
```

**Test 5 : DetermineProfitability - Perte**

```csharp
[Fact]
public void DetermineProfitability_Loss_ReturnsNegative()
{
    // TODO: Implémenter ce test
    // Arrange : revenue = 500.0, costs = 800.0, expected = -300.0
    // Act : Appeler DetermineProfitability
    // Assert : Vérifier result == expected (perte négative)
}
```

**Test 6 : CalculateBreakEven - Seuil de rentabilité**

```csharp
[Fact]
public void CalculateBreakEven_NormalCase_ReturnsMinimumAttendees()
{
    // TODO: Implémenter ce test
    // Arrange : costs = 800.0, ticketPrice = 25.0, expected = 32
    // Act : Appeler CalculateBreakEven
    // Assert : Vérifier result == expected
}
```

**Indice** : Utilisez le pattern AAA (Arrange, Act, Assert) comme dans les exemples fournis.

---

### 🚀 Pour aller plus loin (Bonus optionnel)

Une fois les 6 tests de base maîtrisés, vous pouvez explorer les tests avancés avec `[Theory]` et `[InlineData]` :

<details>
<summary>Voir les tests avancés avec Theory</summary>

```csharp
// BONUS : Tests paramétrés pour tester plusieurs cas en une fois
[Theory]
[InlineData(45, 50, 90.0)]    // Cas normal
[InlineData(0, 100, 0.0)]     // Salle vide
[InlineData(50, 50, 100.0)]   // Salle pleine
public void CalculateFillRate_VariousCases_ReturnsCorrectPercentage(
    int attendees, int capacity, double expected)
{
    // Act
    var result = EventCalculator.CalculateFillRate(attendees, capacity);

    // Assert
    Assert.Equal(expected, result);
}
```

**💡 Avantage des Theory :** Un seul test qui vérifie plusieurs cas automatiquement !

</details>

#### 🎯 Exécuter les tests

```bash
# Lancer tous les tests
dotnet test

# Lancer avec détails
dotnet test --verbosity detailed

# Générer un rapport de coverage (nécessite coverlet)
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Analyse dans le fichier coverage.opencover.xml généré
```

**💡 Objectif :** Visez 70%+ de coverage sur vos méthodes de calcul.

### 📖 Ressources pour les tests

- [Unit testing C# with xUnit - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-csharp-with-xunit)
- [Getting Started with xUnit.net v3](https://xunit.net/docs/getting-started/v3/getting-started)
- [xUnit Assertions Reference](https://xunit.net/docs/assertions)

---

## 📦 Packages NuGet requis

Ce module utilise les packages suivants (commandes NuGet détaillées dans le [Module 0](../0-Module-Setup/0-module-setup.md#-introduction-à-nuget)) :

```bash
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package coverlet.collector
dotnet add package coverlet.msbuild
```

---

### 💪 Exercices complémentaires

Une fois votre calculatrice terminée, renforcez vos acquis avec ces exercices :

#### Exercices Codewars (niveau 8 kyu - débutant) :

- [Codewars C# Kata](https://www.codewars.com/kata/search/csharp)
- [Beginner Exercises Collection](https://www.codewars.com/collections/beginners)
- [C# Practice Problems](https://www.codewars.com/collections/c-number-practice)

#### Suggestions d'exercices spécifiques :

1. **"Even or Odd"** - Déterminer si un nombre est pair ou impair
2. **"Sum of positive"** - Additionner uniquement les nombres positifs d'un tableau
3. **"String repeat"** - Répéter une chaîne N fois
4. **"Convert boolean to string"** - Convertir true/false en "Yes"/"No"

---

### 🎯 Questions d'entretien type

Préparez-vous aux entretiens en maîtrisant ces questions fondamentales :

#### Questions techniques :

1. **Quelle est la différence entre `var` et un type explicite ?**

<details>
<summary>Voir la réponse</summary>

`var` permet l'inférence de type au moment de la compilation, mais le type reste statique. Utile pour la lisibilité mais à éviter si le type n'est pas évident.

```csharp
var nombre = 10;        // int (évident)
var texte = "Hello";    // string (évident)
var resultat = GetData(); // Type pas clair, mieux vaut être explicite
```
</details>

2. **Que se passe-t-il si on divise par zéro avec des entiers ?**

<details>
<summary>Voir la réponse</summary>

Lève une `DivideByZeroException`. Avec des floats, retourne `Infinity` ou `NaN`.

```csharp
int a = 10 / 0;        // Exception !
double b = 10.0 / 0.0; // Infinity
double c = 0.0 / 0.0;  // NaN
```
</details>

3. **Expliquer la différence entre `==` et `.Equals()` ?**

<details>
<summary>Voir la réponse</summary>

`==` compare les références (sauf pour les types valeur), `.Equals()` compare les valeurs. Comportement peut être redéfini.

```csharp
string a = "hello";
string b = "hello";
a == b;        // true (optimisation string)
a.Equals(b);   // true

object x = a;
object y = b;
x == y;        // false (références différentes)
x.Equals(y);   // true (valeurs identiques)
```
</details>

4. **Différence entre types valeur et types référence ?**

<details>
<summary>Voir la réponse</summary>

Types valeur (int, bool, struct) stockés sur la stack, types référence (string, class, array) sur la heap avec référence sur la stack.

```csharp
int x = 10;      // Stack
int y = x;       // Copie de la valeur
y = 20;          // x reste 10

string a = "hello"; // Heap (référence sur stack)
string b = a;       // Copie de la référence
b = "world";        // a reste "hello" (strings immutables)
```
</details>

#### Questions pratiques :

- Comment gérez-vous la validation des entrées utilisateur ?
- Pourquoi séparer la logique métier de l'interface utilisateur ?
- Comment testez-vous une méthode qui peut lever une exception ?

---

### ✅ Validation du module

**Avant de passer au Module 2, assurez-vous de maîtriser :**

#### 📝 Checklist apprenant

**Obligatoire :**

- [ ] Ma calculatrice d'événements fonctionne parfaitement (4 calculs + menu)
- [ ] J'ai écrit et fait passer les 6 tests unitaires essentiels
- [ ] Code coverage ≥ 70% sur les méthodes de calcul
- [ ] Je comprends et applique SRP (séparation calcul/UI)
- [ ] Je comprends et applique DRY (pas de code dupliqué)
- [ ] Je gère proprement la division par zéro avec try/catch
- [ ] Je peux expliquer la différence entre var et types explicites

**Bonus optionnel :**

- [ ] J'ai exploré les tests avec [Theory] et [InlineData]
- [ ] J'ai compris les principes KISS et YAGNI
- [ ] J'ai résolu 3+ kata Codewars niveau 8 kyu

---

**⬅️ [Retour au MECA](../README.md) | [Module 2 - POO](../2-Module-POO/2-module-POO.md) ➡️**

---
