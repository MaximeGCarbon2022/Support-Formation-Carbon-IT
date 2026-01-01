# Questions d'Entretien Technique - SQL / T-SQL

## ⚪ NÉCESSAIRE

### 1. [SQL Standard] Comment récupérer toutes les colonnes d'une table `Users` ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

```sql
SELECT * FROM Users
```

</details>

### 2. [SQL Standard] Comment récupérer uniquement les colonnes `Name` et `Email` ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

```sql
SELECT Name, Email FROM Users
```

</details>

### 3. [SQL Standard] Comment filtrer les utilisateurs avec `Age > 18` ?

**Thème :** Filtrage

<details>
<summary>Voir la réponse</summary>

```sql
SELECT * FROM Users WHERE Age > 18
```

</details>

### 4. [SQL Standard] Comment trier les résultats par ordre alphabétique sur `Name` ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

```sql
SELECT * FROM Users ORDER BY Name ASC
```

Note : `ASC` est l'ordre par défaut et peut être omis.

</details>

### 5. [SQL Standard] Comment insérer un nouvel utilisateur ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

```sql
INSERT INTO Users (Name, Email, Age)
VALUES ('John', 'john@email.com', 25)
```

</details>

### 6. [SQL Standard] Comment supprimer un utilisateur avec `Id = 5` ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

```sql
DELETE FROM Users WHERE Id = 5
```

</details>

---

## 🟢 BASIQUE

### 1. [SQL Standard] Quelle est la différence entre `WHERE` et `HAVING` ?

**Thème :** Filtrage

<details>
<summary>Voir la réponse</summary>

**WHERE** filtre les lignes **avant** le regroupement (`GROUP BY`), tandis que **HAVING** filtre les groupes **après** le regroupement.

```sql
-- WHERE : filtre les lignes individuelles
SELECT City, COUNT(*) as UserCount
FROM Users
WHERE Age > 18
GROUP BY City;

-- HAVING : filtre les groupes après regroupement
SELECT City, COUNT(*) as UserCount
FROM Users
GROUP BY City
HAVING COUNT(*) > 10;
```

</details>

### 2. [SQL Standard] Comment compter le nombre total d'utilisateurs ?

**Thème :** Agrégations

<details>
<summary>Voir la réponse</summary>

```sql
SELECT COUNT(*) FROM Users
```

Ou avec un alias :

```sql
SELECT COUNT(*) as TotalUsers FROM Users
```

Note : `COUNT(*)` compte toutes les lignes, tandis que `COUNT(Id)` ne compte que les lignes où `Id` n'est pas NULL.

</details>

### 3. [SQL Standard] Comment récupérer les utilisateurs avec un nom commençant par "A" ?

**Thème :** Filtrage

<details>
<summary>Voir la réponse</summary>

```sql
SELECT * FROM Users
WHERE Name LIKE 'A%'
```

Le symbole `%` représente n'importe quel nombre de caractères (0 ou plus).

</details>

### 4. [SQL Standard] Quelle est la différence entre `INNER JOIN` et `LEFT JOIN` ?

**Thème :** JOINs

<details>
<summary>Voir la réponse</summary>

**INNER JOIN** retourne uniquement les lignes qui ont une correspondance dans les **deux** tables.

**LEFT JOIN** retourne **toutes** les lignes de la table de gauche, plus les correspondances de la table de droite (NULL s'il n'y a pas de correspondance).

```sql
-- INNER JOIN : seulement les utilisateurs ayant des commandes
SELECT u.Name, o.OrderId
FROM Users u
INNER JOIN Orders o ON u.Id = o.UserId;

-- LEFT JOIN : tous les utilisateurs, même sans commande
SELECT u.Name, o.OrderId
FROM Users u
LEFT JOIN Orders o ON u.Id = o.UserId;
```

</details>

### 5. [SQL Standard] Comment récupérer les utilisateurs et leurs commandes ?

**Thème :** JOINs

<details>
<summary>Voir la réponse</summary>

```sql
SELECT u.*, o.*
FROM Users u
INNER JOIN Orders o ON u.Id = o.UserId
```

Ou avec des colonnes spécifiques :

```sql
SELECT u.Name, u.Email, o.OrderId, o.OrderDate, o.Amount
FROM Users u
INNER JOIN Orders o ON u.Id = o.UserId
```

</details>

### 6. [SQL Standard] Comment grouper par ville et compter le nombre d'utilisateurs ?

**Thème :** Agrégations

<details>
<summary>Voir la réponse</summary>

```sql
SELECT City, COUNT(*) as UserCount
FROM Users
GROUP BY City
```

Pour trier par nombre d'utilisateurs décroissant :

```sql
SELECT City, COUNT(*) as UserCount
FROM Users
GROUP BY City
ORDER BY UserCount DESC
```

</details>

### 7. [SQL Standard] Comment mettre à jour l'email d'un utilisateur avec `Id = 3` ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

```sql
UPDATE Users
SET Email = 'new@email.com'
WHERE Id = 3
```

**Important :** Toujours inclure une clause `WHERE` pour éviter de mettre à jour toutes les lignes.

</details>

### 8. [Multi] Comment récupérer les 10 premiers résultats ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

La syntaxe varie selon le SGBD :

```sql
-- T-SQL (SQL Server)
SELECT TOP 10 * FROM Users
ORDER BY Name;

-- MySQL / PostgreSQL / SQLite
SELECT * FROM Users
ORDER BY Name
LIMIT 10;

-- Oracle
SELECT * FROM Users
WHERE ROWNUM <= 10
ORDER BY Name;
```

</details>

### 9. [SQL Standard] Quelle est la différence entre `DELETE` et `TRUNCATE` ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

**DELETE** :
- Supprime des lignes spécifiques (avec clause `WHERE`)
- Transactionnel (peut être annulé avec `ROLLBACK`)
- Plus lent
- Les triggers se déclenchent

**TRUNCATE** :
- Vide la table complète (pas de `WHERE` possible)
- Plus rapide
- Réinitialise les compteurs IDENTITY/AUTO_INCREMENT
- Non transactionnel dans certains SGBD
- Les triggers ne se déclenchent pas

```sql
-- DELETE : supprime des lignes spécifiques
DELETE FROM Users WHERE Age < 18;

-- TRUNCATE : vide toute la table
TRUNCATE TABLE Users;
```

</details>

### 10. [SQL Standard] Comment éviter les doublons dans les résultats ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

```sql
SELECT DISTINCT City FROM Users
```

Pour plusieurs colonnes :

```sql
SELECT DISTINCT City, Country FROM Users
```

Alternative avec `GROUP BY` :

```sql
SELECT City FROM Users
GROUP BY City
```

</details>

---

## 🟡 INTERMÉDIAIRE

### 1. [SQL Standard] Qu'est-ce qu'une clé primaire (PRIMARY KEY) ?

**Thème :** Modélisation

<details>
<summary>Voir la réponse</summary>

Une **clé primaire** (PRIMARY KEY) est un identifiant unique pour chaque ligne d'une table.

Caractéristiques :
- Valeur unique pour chaque ligne
- Ne peut jamais être NULL
- Une seule clé primaire par table
- Automatiquement indexée pour les performances

```sql
CREATE TABLE Users (
    Id INT PRIMARY KEY,
    Name VARCHAR(100),
    Email VARCHAR(100)
);
```

</details>

### 2. [SQL Standard] Qu'est-ce qu'une clé étrangère (FOREIGN KEY) ?

**Thème :** Modélisation

<details>
<summary>Voir la réponse</summary>

Une **clé étrangère** (FOREIGN KEY) est une colonne qui référence la clé primaire d'une autre table. Elle garantit l'**intégrité référentielle** des données.

```sql
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY,
    UserId INT,
    Amount DECIMAL(10,2),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

Avec une clé étrangère, il est impossible d'insérer une commande avec un `UserId` qui n'existe pas dans la table `Users`.

</details>

### 3. [SQL Standard] Comment créer un index sur la colonne `Email` ?

**Thème :** Index & Performance

<details>
<summary>Voir la réponse</summary>

```sql
CREATE INDEX idx_email ON Users(Email)
```

Pour un index unique (valeurs uniques obligatoires) :

```sql
CREATE UNIQUE INDEX idx_email_unique ON Users(Email)
```

</details>

### 4. [SQL Standard] Pourquoi créer des index ? Quel inconvénient ?

**Thème :** Index & Performance

<details>
<summary>Voir la réponse</summary>

**Avantages des index :**
- Accélère considérablement les `SELECT`, `WHERE`, `JOIN` et `ORDER BY`
- Améliore les performances des recherches

**Inconvénients :**
- Ralentit les opérations `INSERT`, `UPDATE` et `DELETE` (l'index doit être mis à jour)
- Prend de l'espace disque supplémentaire
- Trop d'index peut dégrader les performances globales

**Conseil :** Créez des index uniquement sur les colonnes fréquemment utilisées dans les filtres et jointures.

</details>

### 5. [SQL Standard] Qu'est-ce qu'une transaction ?

**Thème :** Transactions

<details>
<summary>Voir la réponse</summary>

Une **transaction** est un ensemble d'opérations SQL traitées comme une **unité atomique** : soit toutes les opérations réussissent, soit aucune.

```sql
BEGIN TRANSACTION;

UPDATE Accounts SET Balance = Balance - 100 WHERE Id = 1;
UPDATE Accounts SET Balance = Balance + 100 WHERE Id = 2;

-- Si tout s'est bien passé
COMMIT;

-- Ou en cas d'erreur
-- ROLLBACK;
```

Les transactions garantissent la cohérence des données (propriétés ACID).

</details>

### 6. [SQL Standard] Comment utiliser une transaction pour un transfert d'argent ?

**Thème :** Transactions

<details>
<summary>Voir la réponse</summary>

```sql
BEGIN TRANSACTION;

-- Retirer 100 du compte 1
UPDATE Accounts SET Balance = Balance - 100 WHERE Id = 1;

-- Ajouter 100 au compte 2
UPDATE Accounts SET Balance = Balance + 100 WHERE Id = 2;

-- Valider si tout s'est bien passé
COMMIT;

-- Ou annuler en cas d'erreur
-- ROLLBACK;
```

Si une des deux opérations échoue, le `ROLLBACK` annule tout et les comptes restent inchangés.

</details>

### 7. [SQL Standard] Qu'est-ce qu'une sous-requête (subquery) ?

**Thème :** Sous-requêtes

<details>
<summary>Voir la réponse</summary>

Une **sous-requête** est une requête SQL imbriquée à l'intérieur d'une autre requête. Elle peut être utilisée dans les clauses `WHERE`, `FROM` ou `SELECT`.

```sql
-- Sous-requête dans WHERE
SELECT * FROM Users
WHERE Id IN (SELECT UserId FROM Orders);

-- Sous-requête dans FROM
SELECT AVG(TotalOrders) FROM (
    SELECT UserId, COUNT(*) as TotalOrders
    FROM Orders
    GROUP BY UserId
) AS Subquery;
```

</details>

### 8. [SQL Standard] Comment récupérer les utilisateurs ayant passé au moins une commande ?

**Thème :** Sous-requêtes

<details>
<summary>Voir la réponse</summary>

Plusieurs approches possibles :

**Méthode 1 : Sous-requête avec IN**
```sql
SELECT * FROM Users
WHERE Id IN (SELECT UserId FROM Orders);
```

**Méthode 2 : EXISTS (plus performant)**
```sql
SELECT * FROM Users u
WHERE EXISTS (
    SELECT 1 FROM Orders o WHERE o.UserId = u.Id
);
```

**Méthode 3 : INNER JOIN avec DISTINCT**
```sql
SELECT DISTINCT u.*
FROM Users u
INNER JOIN Orders o ON u.Id = o.UserId;
```

</details>

### 9. [SQL Standard] Quelle est la différence entre `UNION` et `UNION ALL` ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

**UNION** :
- Élimine les doublons
- Plus lent (nécessite un tri)

**UNION ALL** :
- Garde tous les résultats, y compris les doublons
- Plus rapide

```sql
-- UNION : élimine les doublons
SELECT City FROM Customers
UNION
SELECT City FROM Suppliers;

-- UNION ALL : garde tout
SELECT City FROM Customers
UNION ALL
SELECT City FROM Suppliers;
```

**Conseil :** Utilisez `UNION ALL` si vous savez qu'il n'y a pas de doublons ou si vous voulez les garder.

</details>

### 10. [SQL Standard] Qu'est-ce qu'une vue (VIEW) ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

Une **vue** est une requête SQL sauvegardée qui se comporte comme une table virtuelle. Elle ne stocke pas de données physiquement, mais simplifie l'accès à des requêtes complexes.

```sql
-- Créer une vue
CREATE VIEW ActiveUsers AS
SELECT Id, Name, Email
FROM Users
WHERE IsActive = 1;

-- Utiliser la vue
SELECT * FROM ActiveUsers WHERE Email LIKE '%@gmail.com';
```

**Avantages :**
- Simplifie les requêtes complexes
- Masque la complexité
- Améliore la sécurité (limite l'accès aux colonnes)

</details>

### 11. [SQL Standard] Comment créer une vue des utilisateurs actifs ?

**Thème :** Bases

<details>
<summary>Voir la réponse</summary>

```sql
CREATE VIEW ActiveUsers AS
SELECT Id, Name, Email, City
FROM Users
WHERE IsActive = 1;
```

Utilisation :

```sql
-- Utiliser la vue comme une table normale
SELECT * FROM ActiveUsers;

-- Avec des filtres supplémentaires
SELECT * FROM ActiveUsers WHERE City = 'Paris';
```

</details>

### 12. [SQL Standard] Qu'est-ce que la normalisation de base de données ?

**Thème :** Modélisation

<details>
<summary>Voir la réponse</summary>

La **normalisation** est le processus d'organisation des données pour :
- Réduire la redondance
- Éviter les anomalies de mise à jour
- Améliorer l'intégrité des données

**Les formes normales principales :**
- **1NF (Première Forme Normale)** : Valeurs atomiques, pas de colonnes répétitives
- **2NF** : 1NF + pas de dépendance partielle
- **3NF** : 2NF + pas de dépendance transitive

Exemple : Séparer les informations client et commandes en deux tables reliées par une clé étrangère.

</details>

### 13. [Multi] Comment gérer les valeurs NULL ?

**Thème :** Filtrage

<details>
<summary>Voir la réponse</summary>

**Tester les NULL :**
```sql
-- Vérifier si NULL
SELECT * FROM Users WHERE PhoneNumber IS NULL;

-- Vérifier si NOT NULL
SELECT * FROM Users WHERE PhoneNumber IS NOT NULL;
```

**Remplacer les NULL :**
```sql
-- SQL Standard (tous SGBD)
SELECT COALESCE(PhoneNumber, 'N/A') AS Phone FROM Users;

-- T-SQL (SQL Server)
SELECT ISNULL(PhoneNumber, 'N/A') AS Phone FROM Users;

-- MySQL
SELECT IFNULL(PhoneNumber, 'N/A') AS Phone FROM Users;
```

**Important :** Ne jamais utiliser `= NULL`, toujours `IS NULL`.

</details>

### 14. [SQL Standard] Quelle est la différence entre `VARCHAR` et `CHAR` ?

**Thème :** Types de données

<details>
<summary>Voir la réponse</summary>

**VARCHAR (longueur variable) :**
- Stocke uniquement les caractères utilisés
- Économise de l'espace
- Plus efficace pour des données de taille variable

**CHAR (longueur fixe) :**
- Remplit avec des espaces jusqu'à la longueur déclarée
- Prend toujours la même place
- Légèrement plus rapide pour des données de taille fixe

```sql
-- VARCHAR(10) avec "Hello" stocke 5 caractères
-- CHAR(10) avec "Hello" stocke "Hello     " (10 caractères)

CREATE TABLE Example (
    Code CHAR(5),           -- Toujours 5 caractères (ex: codes postaux)
    Name VARCHAR(100)       -- Longueur variable (noms)
);
```

**Conseil :** Utilisez `VARCHAR` sauf si vous savez que toutes les valeurs ont exactement la même longueur.

</details>

### 15. [SQL Standard] Comment récupérer uniquement les villes uniques ?

**Thème :** Agrégations

<details>
<summary>Voir la réponse</summary>

**Méthode 1 : DISTINCT**
```sql
SELECT DISTINCT City FROM Users;
```

**Méthode 2 : GROUP BY**
```sql
SELECT City FROM Users
GROUP BY City;
```

Les deux méthodes donnent le même résultat, mais `DISTINCT` est plus concis pour ce cas simple.

</details>

---

## 🔴 AVANCÉ

### 1. [SQL Standard] Qu'est-ce qu'un plan d'exécution (execution plan) ?

**Thème :** Index & Performance

<details>
<summary>Voir la réponse</summary>

Un **plan d'exécution** (execution plan) est une représentation visuelle de la façon dont le moteur SQL exécute une requête.

Il montre :
- Les **scans** (parcours complet de table - lent)
- Les **seeks** (recherche par index - rapide)
- Les types de **joins** utilisés
- L'ordre des opérations
- Les coûts estimés de chaque étape

**Utilité :** Identifier les bottlenecks et optimiser les requêtes lentes.

```sql
-- T-SQL (SQL Server)
SET SHOWPLAN_ALL ON;
SELECT * FROM Orders WHERE CustomerId = 123;
SET SHOWPLAN_ALL OFF;

-- Ou dans SSMS : Ctrl + M pour afficher le plan graphique
```

</details>

### 2. [T-SQL] Quelle est la différence entre CLUSTERED et NON-CLUSTERED index ?

**Thème :** Index & Performance

<details>
<summary>Voir la réponse</summary>

**CLUSTERED Index :**
- Trie physiquement les données sur le disque
- Une seule par table
- Généralement la clé primaire
- Plus rapide pour les plages de valeurs

**NON-CLUSTERED Index :**
- Structure séparée contenant des pointeurs vers les données
- Plusieurs par table possibles
- Contient une copie des colonnes indexées + pointeur vers la ligne

```sql
-- CLUSTERED (généralement sur la PK)
CREATE TABLE Users (
    Id INT PRIMARY KEY CLUSTERED,
    Name VARCHAR(100)
);

-- NON-CLUSTERED
CREATE NONCLUSTERED INDEX idx_name ON Users(Name);
```

**Analogie :** L'index CLUSTERED est comme un dictionnaire (ordonné), le NON-CLUSTERED est comme l'index d'un livre (pointeurs).

</details>

### 3. [SQL Standard] Comment optimiser une requête lente avec `WHERE` sur plusieurs colonnes ?

**Thème :** Index & Performance

<details>
<summary>Voir la réponse</summary>

**Solutions principales :**

1. **Créer un index composite** (multi-colonnes)
```sql
CREATE INDEX idx_city_age ON Users(City, Age);
```

2. **Analyser le plan d'exécution** pour identifier les problèmes

3. **Éviter les fonctions sur les colonnes indexées**
```sql
-- ❌ Mauvais (empêche l'utilisation de l'index)
SELECT * FROM Users WHERE YEAR(BirthDate) = 1990;

-- ✅ Bon (utilise l'index)
SELECT * FROM Users
WHERE BirthDate >= '1990-01-01' AND BirthDate < '1991-01-01';
```

4. **Ordre des colonnes dans l'index** : La colonne la plus sélective en premier

</details>

### 4. [SQL Standard] Qu'est-ce qu'une injection SQL ? Comment l'éviter ?

**Thème :** Sécurité

<details>
<summary>Voir la réponse</summary>

**Injection SQL** : Attaque où un utilisateur malveillant insère du code SQL via les champs de saisie.

**Exemple d'attaque :**
```sql
-- Input utilisateur : admin' OR '1'='1
SELECT * FROM Users WHERE Username = 'admin' OR '1'='1' AND Password = '...';
-- Résultat : connexion sans mot de passe !
```

**Comment l'éviter :**

1. **Toujours utiliser des requêtes paramétrées** (prepared statements)
```csharp
// ❌ DANGEREUX
string query = "SELECT * FROM Users WHERE Username = '" + username + "'";

// ✅ SÉCURISÉ
string query = "SELECT * FROM Users WHERE Username = @username";
cmd.Parameters.AddWithValue("@username", username);
```

2. **Validation des entrées** (whitelist, pas blacklist)
3. **Principe du moindre privilège** pour les comptes SQL
4. **Ne jamais afficher les erreurs SQL** à l'utilisateur

</details>

### 5. [SQL Standard] Qu'est-ce que le problème N+1 queries ?

**Thème :** Performance

<details>
<summary>Voir la réponse</summary>

Le **problème N+1** survient quand :
1. On exécute 1 requête pour récupérer N éléments
2. Puis N requêtes supplémentaires (une par élément) pour charger les données liées

**Exemple du problème :**
```sql
-- 1 requête : récupérer tous les utilisateurs
SELECT * FROM Users; -- 100 utilisateurs

-- Puis 100 requêtes (une par utilisateur) :
SELECT * FROM Orders WHERE UserId = 1;
SELECT * FROM Orders WHERE UserId = 2;
...
SELECT * FROM Orders WHERE UserId = 100;
-- Total : 101 requêtes !
```

**Solution : Utiliser un JOIN**
```sql
-- 1 seule requête
SELECT u.*, o.*
FROM Users u
LEFT JOIN Orders o ON u.Id = o.UserId;
```

**En ORM (Entity Framework) :** Utiliser `.Include()` pour l'eager loading.

</details>

### 6. [SQL Standard] Comment fonctionne le verrouillage (locking) ?

**Thème :** Transactions

<details>
<summary>Voir la réponse</summary>

Le **verrouillage** (locking) empêche l'accès concurrent à des données pendant une transaction pour garantir la cohérence.

**Types de verrous :**

1. **Shared Lock (lecture)** :
   - Permet à plusieurs transactions de lire les mêmes données
   - Empêche les modifications pendant la lecture

2. **Exclusive Lock (écriture)** :
   - Une seule transaction peut modifier les données
   - Bloque toute lecture et écriture par d'autres transactions

```sql
-- Transaction 1 : Exclusive lock
BEGIN TRANSACTION;
UPDATE Accounts SET Balance = Balance - 100 WHERE Id = 1;
-- Les autres transactions doivent attendre
COMMIT;
```

**Attention :** Des verrous mal gérés peuvent causer des deadlocks.

</details>

### 7. [SQL Standard] Quels sont les niveaux d'isolation des transactions ?

**Thème :** Transactions

<details>
<summary>Voir la réponse</summary>

Les **niveaux d'isolation** contrôlent la visibilité des données entre transactions concurrentes.

**Du moins strict au plus strict :**

1. **READ UNCOMMITTED** :
   - Lit les données non validées (dirty read)
   - Le plus rapide, le moins sûr

2. **READ COMMITTED** (défaut) :
   - Lit uniquement les données validées
   - Évite les dirty reads

3. **REPEATABLE READ** :
   - Garantit la même lecture pendant toute la transaction
   - Évite les non-repeatable reads

4. **SERIALIZABLE** :
   - Niveau le plus strict
   - Transactions s'exécutent comme si elles étaient séquentielles
   - Le plus lent

```sql
-- T-SQL
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
BEGIN TRANSACTION;
-- ...
COMMIT;
```

</details>

### 8. [SQL Standard] Qu'est-ce qu'un deadlock ? Comment le gérer ?

**Thème :** Transactions

<details>
<summary>Voir la réponse</summary>

Un **deadlock** survient quand deux transactions s'attendent mutuellement et se bloquent.

**Exemple :**
```
Transaction 1 : Lock Table A → Attend Table B
Transaction 2 : Lock Table B → Attend Table A
→ Deadlock !
```

**Comment l'éviter :**

1. **Ordre cohérent d'accès aux tables**
```sql
-- Toujours accéder aux tables dans le même ordre
-- Transaction 1 et 2 : Table A puis Table B
```

2. **Garder les transactions courtes** (moins de temps = moins de risque)

3. **Utiliser des index** pour réduire les verrous

4. **Timeout et retry logic** en cas de deadlock détecté

5. **Niveau d'isolation approprié**

</details>

### 9. [SQL Standard] Comment partitionner une table de 100 millions de lignes ?

**Thème :** Performance

<details>
<summary>Voir la réponse</summary>

Le **partitionnement** divise une grande table en plusieurs parties plus petites (partitions).

**Types de partitionnement :**

1. **Range Partitioning** (le plus courant)
```sql
-- Partitionner par date
CREATE PARTITION FUNCTION pf_OrderDate (DATE)
AS RANGE RIGHT FOR VALUES
('2023-01-01', '2023-04-01', '2023-07-01', '2023-10-01');
```

2. **Hash Partitioning** : Distribution uniforme des données

3. **List Partitioning** : Par liste de valeurs spécifiques

**Avantages :**
- Requêtes plus rapides (parcours de partition uniquement)
- Maintenance facilitée (archivage, suppression par partition)
- Amélioration des performances d'index

**Exemple d'utilisation :** Tables de logs, commandes, historiques.

</details>

### 10. [T-SQL] Qu'est-ce qu'une procédure stockée (stored procedure) ?

**Thème :** Programmation

<details>
<summary>Voir la réponse</summary>

Une **procédure stockée** est un bloc de code SQL précompilé et stocké dans la base de données.

**Avantages :**
- Code SQL réutilisable
- Performance (plan d'exécution mis en cache)
- Sécurité (paramètres évitent l'injection SQL)
- Logique métier centralisée
- Réduction du trafic réseau

```sql
CREATE PROCEDURE sp_GetUserOrders
    @UserId INT
AS
BEGIN
    SELECT * FROM Orders
    WHERE UserId = @UserId
    ORDER BY OrderDate DESC;
END;

-- Appel
EXEC sp_GetUserOrders @UserId = 123;
```

</details>

### 11. [T-SQL] Comment créer une procédure stockée pour récupérer un utilisateur ?

**Thème :** Programmation

<details>
<summary>Voir la réponse</summary>

```sql
CREATE PROCEDURE GetUserById
    @UserId INT
AS
BEGIN
    SELECT Id, Name, Email, City
    FROM Users
    WHERE Id = @UserId;
END;

-- Appel de la procédure
EXEC GetUserById @UserId = 5;
```

Avec gestion d'erreur :

```sql
CREATE PROCEDURE GetUserById
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId)
    BEGIN
        RAISERROR('User not found', 16, 1);
        RETURN;
    END

    SELECT Id, Name, Email, City
    FROM Users
    WHERE Id = @UserId;
END;
```

</details>

### 12. [T-SQL] Qu'est-ce qu'un trigger ?

**Thème :** Programmation

<details>
<summary>Voir la réponse</summary>

Un **trigger** est du code SQL qui s'exécute automatiquement lors d'événements (`INSERT`, `UPDATE`, `DELETE`).

**Cas d'usage :**
- Audit des modifications
- Validation de données complexes
- Synchronisation de tables
- Historisation automatique

```sql
-- Trigger d'audit
CREATE TRIGGER trg_Users_Audit
ON Users
AFTER UPDATE
AS
BEGIN
    INSERT INTO Users_History (UserId, OldEmail, NewEmail, ModifiedAt)
    SELECT
        d.Id,
        d.Email,
        i.Email,
        GETDATE()
    FROM deleted d
    INNER JOIN inserted i ON d.Id = i.Id
    WHERE d.Email <> i.Email;
END;
```

**Important :** Tables spéciales `inserted` (nouvelles valeurs) et `deleted` (anciennes valeurs).

</details>

### 13. [SQL Standard] Comment débugger une requête qui prend 30 secondes ?

**Thème :** Performance

<details>
<summary>Voir la réponse</summary>

**Étapes de débugage :**

1. **Analyser le plan d'exécution**
```sql
-- SQL Server : Ctrl + M dans SSMS
-- Ou
SET STATISTICS TIME ON;
SET STATISTICS IO ON;
-- Votre requête
```

2. **Identifier les problèmes courants :**
   - **Table Scan** au lieu de Index Seek (mauvais signe)
   - Fonctions sur colonnes indexées
   - Jointures sur colonnes non indexées
   - Sous-requêtes corrélées

3. **Vérifier les index manquants**
```sql
-- T-SQL
SELECT * FROM sys.dm_db_missing_index_details;
```

4. **Statistiques à jour**
```sql
-- T-SQL
UPDATE STATISTICS Users;
```

5. **Réécrire la requête** si nécessaire (JOINs au lieu de sous-requêtes, etc.)

</details>

### 14. [SQL Standard] Quelle est la différence entre `EXISTS` et `IN` ?

**Thème :** Sous-requêtes

<details>
<summary>Voir la réponse</summary>

**EXISTS :**
- S'arrête dès qu'une correspondance est trouvée
- Plus rapide pour de gros résultats
- Retourne TRUE/FALSE

**IN :**
- Récupère toutes les valeurs puis compare
- Peut être plus lent si la sous-requête retourne beaucoup de lignes
- Retourne une liste de valeurs

```sql
-- EXISTS (recommandé pour grandes tables)
SELECT * FROM Users u
WHERE EXISTS (
    SELECT 1 FROM Orders o WHERE o.UserId = u.Id
);

-- IN (pratique pour petites listes)
SELECT * FROM Users
WHERE Id IN (1, 2, 3, 4, 5);
```

**Conseil :** Utilisez `EXISTS` pour les sous-requêtes corrélées sur de grandes tables.

</details>

### 15. [Multi] Comment gérer la pagination sur 10 millions de lignes ?

**Thème :** Performance

<details>
<summary>Voir la réponse</summary>

**Méthode 1 : OFFSET/FETCH (SQL Server 2012+)**
```sql
SELECT * FROM Orders
ORDER BY OrderId
OFFSET 1000 ROWS
FETCH NEXT 100 ROWS ONLY;
```

**Problème :** Sur de grandes tables, `OFFSET` parcourt toutes les lignes avant de retourner les résultats (lent).

**Méthode 2 : Keyset Pagination (recommandé)**
```sql
-- Page 1
SELECT TOP 100 * FROM Orders
WHERE OrderId > 0
ORDER BY OrderId;

-- Page 2 (lastId = 100)
SELECT TOP 100 * FROM Orders
WHERE OrderId > 100
ORDER BY OrderId;
```

**Avantage :** Performances constantes, même sur des millions de lignes.

</details>

### 16. [SQL Standard] Qu'est-ce que la dénormalisation ? Quand l'utiliser ?

**Thème :** Modélisation

<details>
<summary>Voir la réponse</summary>

La **dénormalisation** consiste à ajouter intentionnellement de la redondance pour améliorer les performances en évitant des JOINs coûteux.

**Exemple :**
```sql
-- Normalisé (2 tables, JOIN nécessaire)
Orders: OrderId, CustomerId, Total
Customers: CustomerId, CustomerName

-- Dénormalisé (1 table, pas de JOIN)
Orders: OrderId, CustomerId, CustomerName, Total
```

**Quand l'utiliser :**
- Systèmes de reporting (read-heavy)
- Data warehouses
- Caches de données
- Tables de grande taille avec lectures fréquentes

**Attention :** Augmente la complexité de maintenance (mise à jour dans plusieurs endroits).

</details>

### 17. [T-SQL] Comment analyser l'utilisation des index ?

**Thème :** Index & Performance

<details>
<summary>Voir la réponse</summary>

**Utiliser les DMV (Dynamic Management Views) :**

```sql
-- Index non utilisés
SELECT
    OBJECT_NAME(i.object_id) AS TableName,
    i.name AS IndexName,
    s.user_seeks,
    s.user_scans,
    s.user_lookups,
    s.user_updates
FROM sys.indexes i
LEFT JOIN sys.dm_db_index_usage_stats s
    ON i.object_id = s.object_id AND i.index_id = s.index_id
WHERE s.user_seeks = 0
  AND s.user_scans = 0
  AND s.user_lookups = 0
  AND i.name IS NOT NULL;

-- Index manquants suggérés
SELECT * FROM sys.dm_db_missing_index_details;
```

**Action :** Supprimer les index inutilisés, créer les index manquants.

</details>

### 18. [SQL Standard] Qu'est-ce qu'un CTE (Common Table Expression) ?

**Thème :** Programmation

<details>
<summary>Voir la réponse</summary>

Un **CTE** (Common Table Expression) est une requête temporaire nommée avec la clause `WITH`. Elle améliore la lisibilité et permet la récursivité.

```sql
-- CTE simple
WITH ActiveUsers AS (
    SELECT Id, Name, Email
    FROM Users
    WHERE IsActive = 1
)
SELECT * FROM ActiveUsers
WHERE Email LIKE '%@gmail.com';

-- CTE multiple
WITH
    Sales2024 AS (
        SELECT CustomerId, SUM(Amount) AS Total
        FROM Orders
        WHERE YEAR(OrderDate) = 2024
        GROUP BY CustomerId
    ),
    TopCustomers AS (
        SELECT CustomerId, Total
        FROM Sales2024
        WHERE Total > 10000
    )
SELECT c.Name, tc.Total
FROM TopCustomers tc
JOIN Customers c ON tc.CustomerId = c.Id;
```

**Avantages :** Code plus lisible, réutilisable dans la même requête, supporte la récursivité.

</details>

### 19. [SQL Standard] Comment implémenter une requête récursive (hiérarchie) ?

**Thème :** Programmation

<details>
<summary>Voir la réponse</summary>

Les **CTE récursifs** permettent de parcourir des structures hiérarchiques (organigrammes, catégories, etc.).

```sql
WITH EmployeeHierarchy AS (
    -- Ancre : point de départ (le PDG)
    SELECT Id, Name, ManagerId, 1 AS Level
    FROM Employees
    WHERE ManagerId IS NULL

    UNION ALL

    -- Récursion : les subordonnés
    SELECT e.Id, e.Name, e.ManagerId, eh.Level + 1
    FROM Employees e
    INNER JOIN EmployeeHierarchy eh ON e.ManagerId = eh.Id
)
SELECT * FROM EmployeeHierarchy
ORDER BY Level, Name;
```

**Structure :**
1. **Ancre** : Requête initiale (racine de la hiérarchie)
2. **UNION ALL**
3. **Récursion** : Jointure avec le CTE lui-même
4. **Terminaison** : S'arrête quand plus de résultats

</details>

### 20. [SQL Standard] Comment gérer les performances en production ?

**Thème :** Performance

<details>
<summary>Voir la réponse</summary>

**Stratégies de gestion des performances :**

1. **Monitoring continu**
   - CPU, mémoire, I/O disque
   - Temps d'attente (waits)
   - Requêtes les plus lentes

2. **Analyse des plans d'exécution**
   - Identifier les requêtes problématiques
   - Détecter les table scans

3. **Index tuning**
   - Créer les index manquants
   - Supprimer les index inutilisés
   - Analyser la fragmentation

4. **Statistiques à jour**
```sql
-- T-SQL
UPDATE STATISTICS TableName;
```

5. **Query optimization**
   - Réécrire les requêtes inefficaces
   - Utiliser des procédures stockées
   - Éviter SELECT *

6. **Scaling**
   - Vertical : Augmenter CPU/RAM
   - Horizontal : Réplication, sharding

</details>

### 21. [SQL Standard] Qu'est-ce que le sharding ?

**Thème :** Architecture

<details>
<summary>Voir la réponse</summary>

Le **sharding** (partitionnement horizontal) divise les données sur plusieurs serveurs physiques pour améliorer la scalabilité.

**Exemple :**
```
Shard 1 (Serveur A) : Utilisateurs avec Id 1-1000000
Shard 2 (Serveur B) : Utilisateurs avec Id 1000001-2000000
Shard 3 (Serveur C) : Utilisateurs avec Id 2000001-3000000
```

**Avantages :**
- Scalabilité horizontale (ajouter des serveurs)
- Répartition de la charge
- Isolation des pannes

**Inconvénients :**
- Complexité applicative (routage des requêtes)
- Jointures cross-shard difficiles
- Transactions distribuées complexes

**Cas d'usage :** Applications à très forte charge (millions d'utilisateurs).

</details>

### 22. [T-SQL] Comment implémenter un audit des modifications ?

**Thème :** Administration

<details>
<summary>Voir la réponse</summary>

**Méthodes d'audit :**

**1. Triggers (classique)**
```sql
CREATE TRIGGER trg_Audit_Users
ON Users
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    INSERT INTO Audit_Log (TableName, Action, UserId, ModifiedAt)
    SELECT
        'Users',
        CASE
            WHEN EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted) THEN 'UPDATE'
            WHEN EXISTS (SELECT * FROM inserted) THEN 'INSERT'
            ELSE 'DELETE'
        END,
        COALESCE(i.Id, d.Id),
        GETDATE()
    FROM inserted i
    FULL OUTER JOIN deleted d ON i.Id = d.Id;
END;
```

**2. Temporal Tables (SQL Server 2016+, recommandé)**
```sql
CREATE TABLE Users (
    Id INT PRIMARY KEY,
    Name VARCHAR(100),
    Email VARCHAR(100),
    ValidFrom DATETIME2 GENERATED ALWAYS AS ROW START,
    ValidTo DATETIME2 GENERATED ALWAYS AS ROW END,
    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.Users_History));
```

**3. Change Data Capture (CDC)** : Pour capturer toutes les modifications sans impact sur les performances.

</details>

### 23. [SQL Standard] Différence entre `RANK()`, `DENSE_RANK()` et `ROW_NUMBER()` ?

**Thème :** Fonctions avancées

<details>
<summary>Voir la réponse</summary>

Ces fonctions de fenêtrage numérotent les lignes, mais différemment en cas d'égalité.

```sql
SELECT
    Name,
    Score,
    ROW_NUMBER() OVER (ORDER BY Score DESC) AS RowNum,
    RANK() OVER (ORDER BY Score DESC) AS Rank,
    DENSE_RANK() OVER (ORDER BY Score DESC) AS DenseRank
FROM Students;
```

**Résultats avec scores : 100, 95, 95, 90 :**

| Name  | Score | ROW_NUMBER | RANK | DENSE_RANK |
|-------|-------|------------|------|------------|
| Alice | 100   | 1          | 1    | 1          |
| Bob   | 95    | 2          | 2    | 2          |
| Carol | 95    | 3          | 2    | 2          |
| David | 90    | 4          | 4    | 3          |

**Différences :**
- **ROW_NUMBER** : Numéros uniques (1, 2, 3, 4)
- **RANK** : Gaps en cas d'égalité (1, 2, 2, 4)
- **DENSE_RANK** : Pas de gaps (1, 2, 2, 3)

</details>

### 24. [T-SQL] Comment optimiser les agrégations sur grandes tables ?

**Thème :** Performance

<details>
<summary>Voir la réponse</summary>

**Techniques d'optimisation :**

1. **Index sur colonnes GROUP BY**
```sql
CREATE INDEX idx_category_date ON Orders(Category, OrderDate);
```

2. **Indexed Views (vues matérialisées)**
```sql
CREATE VIEW vw_SalesByCategory
WITH SCHEMABINDING
AS
SELECT Category, SUM(Amount) AS TotalSales, COUNT_BIG(*) AS OrderCount
FROM dbo.Orders
GROUP BY Category;

CREATE UNIQUE CLUSTERED INDEX idx_vw ON vw_SalesByCategory(Category);
```

3. **Columnstore Index** (SQL Server 2012+)
```sql
CREATE COLUMNSTORE INDEX idx_columnstore ON Orders(OrderDate, Category, Amount);
```

4. **Pré-calcul/Matérialisation**
   - Tables de reporting pré-calculées
   - Rafraîchissement périodique (nuit)

5. **Partitionnement** pour grandes tables

</details>

### 25. [SQL Standard] Comment gérer les migrations de schéma sans downtime ?

**Thème :** Administration

<details>
<summary>Voir la réponse</summary>

**Stratégie de migration sans interruption :**

**Phase 1 : Ajouter la nouvelle structure (rétrocompatible)**
```sql
-- Ajouter nouvelle colonne en NULL
ALTER TABLE Users ADD NewEmail VARCHAR(100) NULL;
```

**Phase 2 : Déployer le code qui gère les deux colonnes**
```csharp
// Code qui écrit dans les deux colonnes
user.Email = email;
user.NewEmail = email;
```

**Phase 3 : Migration des données**
```sql
UPDATE Users SET NewEmail = Email WHERE NewEmail IS NULL;
```

**Phase 4 : Basculer le code vers la nouvelle colonne uniquement**
```csharp
// Code mis à jour
user.NewEmail = email;
```

**Phase 5 : Supprimer l'ancienne structure**
```sql
ALTER TABLE Users DROP COLUMN Email;
```

**Principes clés :**
- Toujours ajouter en NULL d'abord
- Backward compatibility obligatoire
- Déploiement progressif (canary, blue-green)
- Toujours pouvoir rollback

</details>
