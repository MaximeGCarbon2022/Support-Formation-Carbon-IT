# 🗄️ Formation SQL & T-SQL - Fondamentaux et SQL Server

## 🎯 Objectif de la formation

**Maîtriser SQL et T-SQL** pour gérer efficacement les bases de données relationnelles. De la syntaxe universelle SQL aux fonctionnalités avancées de SQL Server avec T-SQL, cette formation vous prépare pour vos missions consultant et entretiens techniques.

---

## 📋 Contenu de la formation

Cette formation est organisée en **2 niveaux progressifs** + exercices et questions d'entretien :

### 📖 **[Cours SQL - Les Fondamentaux](./Cours/SQL.md)**

_Le langage SQL universel pour tous les SGBD_

**Ce que vous apprendrez :**

- Conventions de nommage et bonnes pratiques SQL
- Concepts théoriques (PK, FK, relations, normalisation)
- SELECT approfondi (WHERE, HAVING, GROUP BY, ORDER BY)
- Commandes DML (INSERT, UPDATE, DELETE)
- Jointures (INNER, LEFT, RIGHT, FULL OUTER)
- Fonctions d'agrégation et GROUP BY
- Sous-requêtes (WHERE, FROM, corrélées)
- CTE (Common Table Expressions, récursivité)
- Calculs et expressions (math, texte, dates, CASE)
- Index et optimisation de base
- Transactions et propriétés ACID
- Normalisation (1NF, 2NF, 3NF)
- Vues (Views)

**Applicable à :**

- ✅ MySQL
- ✅ PostgreSQL
- ✅ SQL Server
- ✅ Oracle
- ✅ SQLite
- ✅ MariaDB

---

### ⚡ **[Cours T-SQL - SQL Server Avancé](./Cours/T-SQL.md)**

_Extension Microsoft SQL Server avec fonctionnalités avancées_

**Ce que vous apprendrez :**

- Différences syntaxiques T-SQL vs SQL standard
- IDENTITY et SEQUENCES (auto-incrémentation)
- Procédures stockées (paramètres INPUT/OUTPUT, défauts)
- Fonctions (scalaires, table-valued)
- Gestion des erreurs (TRY...CATCH, THROW)
- Triggers (AFTER, INSTEAD OF, inserted/deleted)
- Variables de table vs tables temporaires
- OUTPUT clause (INSERT, UPDATE, DELETE)
- MERGE statement (UPSERT, synchronisation)
- Pagination (OFFSET/FETCH)
- Support JSON (SQL Server 2016+)
- CTE récursif en T-SQL
- Curseurs (avec avertissements)
- Statistiques et informations système
- Optimisations SQL Server
- Sécurité et injection SQL

**Parfait pour :**

- ✅ Développeurs .NET utilisant SQL Server
- ✅ Projets enterprise avec SQL Server
- ✅ Stored procedures et logique métier en base
- ✅ Migrations et ETL SQL Server
- ✅ Optimisation de requêtes critiques

**Prérequis :** Avoir lu [SQL.md](./Cours/SQL.md) pour les bases universelles

---

### 🎯 **[Exercice Pratique - Système de Réservation Hôtel](./Exercice/T-SQL-Exercice.md)**

_Mini-projet avancé pour mettre en pratique T-SQL_

**Ce que vous construirez :**

- Système de réservation d'hôtel complet
- Procédure stockée `sp_CreateReservation` avec validation de chevauchement de dates
- Procédure stockée `sp_CancelReservation` avec gestion de statuts
- Fonction table `fn_GetAvailableRooms` pour recherche de disponibilité
- Scénarios de test complets

**Compétences pratiques :**

- ✅ IDENTITY et gestion des IDs
- ✅ Procédures avec paramètres et logique métier
- ✅ Validation de contraintes complexes (overlap de dates)
- ✅ Gestion d'erreurs avec TRY...CATCH
- ✅ Fonctions table-valued
- ✅ Transactions et ROLLBACK
- ✅ Tests et validation

**💡 Note importante :** Cette formation se concentre sur les compétences SQL/T-SQL. **Les solutions seront apportées par les consultants en formation** - l'exercice est conçu pour développer votre capacité d'analyse et d'implémentation autonome.

---

### 💼 **[Questions d'Entretien Technique](./Interview-Questions/Questions-Entretien-SQL.md)**

_25+ questions pour préparer vos entretiens et missions SQL/T-SQL_

**Organisation par niveaux de difficulté** (NÉCESSAIRE, BASIQUE, INTERMÉDIAIRE, AVANCÉ)

---

## 🤔 Pourquoi SQL et T-SQL ?

### **SQL (universel) :**

SQL est le langage **standard** pour interagir avec les bases de données relationnelles. Maîtriser SQL vous permet de :

- Travailler avec n'importe quel SGBD (MySQL, PostgreSQL, Oracle, etc.)
- Comprendre les fondements de la gestion de données
- Écrire des requêtes portables et maintenables

### **T-SQL (SQL Server) :**

T-SQL est l'extension **Microsoft** qui ajoute :

- Programmation procédurale (variables, conditions, boucles)
- Gestion d'erreurs avancée
- Logique métier directement en base (procédures, triggers)
- Optimisations spécifiques SQL Server
- Intégration parfaite avec l'écosystème .NET

### **Utilisez SQL/T-SQL pour :**

- Applications .NET avec SQL Server
- Systèmes enterprise nécessitant logique métier en base
- Reporting et analytics
- Migrations et ETL (Extract, Transform, Load)
- Optimisation de requêtes critiques
- Projets nécessitant transactions ACID strictes

---

## 🛠️ Outils nécessaires

- SQL Server Express ou version complète
- SQL Server Management Studio (SSMS) ou Azure Data Studio
- Bases de données relationnelles (concepts)
- Logique de programmation

---

À la fin de cette formation, vous saurez :

### **Compétences techniques**

- ✅ Écrire des requêtes SQL complexes (jointures, sous-requêtes, CTE)
- ✅ Créer et optimiser des procédures stockées T-SQL
- ✅ Gérer les erreurs et transactions professionnellement
- ✅ Implémenter des triggers et fonctions
- ✅ Optimiser les performances avec index et plans d'exécution
- ✅ Sécuriser les requêtes contre l'injection SQL
- ✅ Modéliser des bases de données normalisées

### **Compétences métier**

- ✅ Analyser et résoudre des problèmes de performance
- ✅ Architecturer une couche de données SQL Server
- ✅ Déboguer et optimiser des requêtes lentes
- ✅ Répondre aux questions techniques en entretien
- ✅ Intervenir en mission consultant SQL/T-SQL
- ✅ Maintenir et refactoriser du code SQL legacy

---

## 🚀 C'est parti !

**Prêt à maîtriser SQL et T-SQL ?**

1. 📖 Commencez par le [Cours SQL - Fondamentaux](./Cours/SQL.md)
2. ⚡ Approfondissez avec le [Cours T-SQL - SQL Server](./Cours/T-SQL.md)
3. 🎯 Pratiquez avec l'[Exercice Système Hôtel](./Exercice/T-SQL-Exercice.md)
4. 💼 Préparez-vous avec les [Questions d'Entretien](./Interview-Questions/Questions-Entretien-SQL.md)

**Bon dev ! 💪**
