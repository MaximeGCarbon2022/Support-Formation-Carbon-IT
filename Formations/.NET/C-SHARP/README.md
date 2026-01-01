# Montée en compétence auto-rythmée - C#

## 📋 Vue d'ensemble du parcours

Cette formation est structurée en **1 module de setup + 6 modules progressifs**, chacun combinant théorie, pratique et tests pour une montée en compétence complète.

**Projet fil rouge :** Tous les modules construisent progressivement le **TechEvent Manager**, une application de gestion d'événements techniques (workshops, conférences, meetups).

---

## 📚 Liste des modules

### Module 0 : Setup & Conventions (PRÉREQUIS OBLIGATOIRE)

**Objectifs :** Installer .NET 9, configurer l'environnement, apprendre les conventions de nommage professionnelles
**Prérequis :** Aucun

⚠️ **À FAIRE EN PREMIER** : Ce module prépare votre environnement et vous enseigne les standards professionnels .NET. Sans lui, vous risquez d'écrire du code non conforme aux conventions d'entreprise.

### Module 1 : Fondamentaux C#

**Objectifs :** Maîtriser la syntaxe de base, types de données, structures de contrôle
**Projet :** TechEvent Manager - Phase 1 (EventCalculator - Calculs métier)
**Prérequis :** Module 0 ✅

### Module 2 : Programmation Orientée Objet

**Objectifs :** Classes, encapsulation, héritage, polymorphisme, interfaces
**Projet :** TechEvent Manager - Phase 2 (Modèle objet avec Event, Workshop, Conference, Meetup)
**Prérequis :** Module 1 ✅

### Module 3 : Collections et LINQ

**Objectifs :** Maîtriser les collections (List, Dictionary), opérations LINQ essentielles
**Projet :** TechEvent Manager - Phase 3 (EventSearchEngine - Recherche et filtres LINQ)
**Prérequis :** Modules 1-2 ✅

### Module 4 : Gestion d'Erreurs

**Objectifs :** Try/catch, exceptions personnalisées, defensive programming
**Projet :** TechEvent Manager - Phase 4 (Gestion d'erreurs robuste et validation)
**Prérequis :** Modules 1-3 ✅

### Module 5 : C# Moderne (C# 8-11-12)

**Objectifs :** Nullable reference types, pattern matching, records, raw string literals, switch expressions
**Projet :** TechEvent Manager - Phase 5 (Modernisation du code avec C# 8-11-12)
**Prérequis :** Modules 1-4 ✅

### Module 6 : Programmation Asynchrone

**Objectifs :** async/await, Task, Task.WhenAll, gestion d'erreurs async
**Projet :** TechEvent Manager - Phase 6 (EmailService async, envois parallèles)
**Prérequis :** Modules 1-5 ✅

---

## 🏗️ Structure de chaque module

Chaque module suit une progression pédagogique optimisée :

1. **📖 Théorie courte** - Concepts essentiels avec liens vers documentation
2. **🎥 Ressources vidéo** - Tutoriels Microsoft Learn
3. **💻 Exercice pratique** - Application concrète des concepts
4. **✅ Tests unitaires** - Validation et bonnes pratiques
5. **💪 Exercices bonus** - Codewars, défis supplémentaires
6. **🎯 Questions d'entretien** - Préparation aux entretiens techniques

---

## 📊 Suivi de progression

### Checklist globale

- [ ] Module 0 : Setup & Conventions ⏱️ (30-45 min - OBLIGATOIRE EN PREMIER)
- [ ] Module 1 : Fondamentaux C# ⏱️
- [ ] Module 2 : Programmation Orientée Objet ⏱️
- [ ] Module 3 : Collections et LINQ ⏱️
- [ ] Module 4 : Gestion d'Erreurs ⏱️
- [ ] Module 5 : C# Moderne ⏱️
- [ ] Module 6 : Programmation Asynchrone ⏱️

### Critères de validation par module

✅ **Théorie comprise** - Lecture complète des ressources
✅ **Exercice réalisé** - Fonctionnalités implémentées avec TODOs complétés
✅ **Tests implémentés** - Minimum 6 tests, code coverage ≥ 70%
✅ **Questions maîtrisées** - Capacité à expliquer les concepts

---

## 🛠️ Outils requis

### Environnement de développement

- **Visual Studio 2022** (Community suffisant) ou **VS Code** + C# Extension
- **.NET 9 SDK** (dernière version)
- **Git** pour le versioning

### Packages NuGet essentiels

```xml
<PackageReference Include="xunit" Version="2.9.3" />
<PackageReference Include="xunit.runner.visualstudio" Version="3.1.1" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.13.0" />
<PackageReference Include="Moq" Version="4.20.72" />
```

---

## 📖 Ressources générales

### Documentation officielle

- [Microsoft C# Guide](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [.NET API Browser](https://learn.microsoft.com/en-us/dotnet/api/)

### Pratique en ligne

- [Codewars C# Kata](https://www.codewars.com/kata/search/csharp)
- [LeetCode C#](https://leetcode.com/problemset/all/?difficulty=Easy)
- [HackerRank C#](https://www.hackerrank.com/domains/algorithms)

### Communauté

- [Reddit r/csharp](https://www.reddit.com/r/csharp/)
- [Microsoft C# Discord](https://discord.gg/dotnet)

---

## 💡 Conseils pour réussir

### ⏰ Organisation

- **Consistance > Intensité** : Alterner entre lecture et exercice
- **Prenez des notes** : créez votre aide-mémoire personnel

### 🎯 Apprentissage

- **Commencez par les bases** : ne sautez pas les modules
- **Tapez le code** : ne copiez-collez pas, écrivez à la main
- **Expérimentez** : modifiez les exemples, cassez le code, réparez-le
- **Testez tout** : chaque fonction doit avoir ses tests
- **Sans IA** : Module accessible sans IA

### 🚀 Motivation

- **Célébrez les petites victoires** : chaque module terminé renforce votre expertise
- **Réussissez vos entretiens** : maîtrisez les exercices techniques et coding games
- **Gagnez en confiance** : abordez sereinement vos premières missions client

### Pour aller plus loin

- **Apprendre la Programmation Orientée Objet avec le langage C#** de Luc Gervais
- **Clean Code** de Robert C. Martin
- **C# in Depth** de Jon Skeet

---

## 🆘 Support

### En cas de blocage

1. **Relisez la théorie** : souvent la réponse y est
2. **Consultez les liens** : documentation Microsoft très complète
3. **Cherchez sur Stack Overflow** : 99% des erreurs ont une solution
4. **Demandez de l'aide** : les TOs sont là pour çà !

### Signaler un problème

- **Lien cassé** : créez une issue avec le lien défaillant
- **Exercice unclear** : proposez une amélioration
- **Erreur dans le code** : soumettez une correction

---

## 🎓 Après le MECA

### Prochaines étapes recommandées

En discussion

**🚀 Prêt à commencer ?**

**ÉTAPE 1 (OBLIGATOIRE) :** Suivez d'abord le [Module 0 - Setup & Conventions](./0-Module-Setup/0-module-setup.md) pour préparer votre environnement.

**ÉTAPE 2 :** Une fois le Module 0 terminé, passez au [Module 1 - Fondamentaux C#](./1-Module-Fondamentaux/1-module-fondamentaux.md) !
