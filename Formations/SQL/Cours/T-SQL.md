# Formation T-SQL - SQL Server

## Introduction

T-SQL (Transact-SQL) est l'extension propriétaire de SQL développée par Microsoft pour SQL Server. Ce document couvre les spécificités de T-SQL et les fonctionnalités avancées de SQL Server.

**Prérequis** : Lisez d'abord le document [SQL.md](./SQL.md) pour maîtriser les bases SQL universelles.

---

## Table des Matières

1. [Différences Syntaxiques T-SQL vs SQL Standard](#différences-syntaxiques-t-sql-vs-sql-standard)
   - Variables
   - Gestion de NULL
   - Concaténation de Chaînes
   - Fonctions de Date
   - Identifiants Uniques (GUID)
   - TOP (Limiter les résultats)
2. [IDENTITY & SEQUENCES](#identity--sequences)
   - IDENTITY (Auto-incrémentation)
   - SEQUENCES (SQL Server 2012+)
   - Récupérer le dernier ID inséré
3. [Procédures Stockées](#procédures-stockées)
   - Procédure Simple
   - Paramètres de Sortie
   - Valeurs par Défaut
4. [Fonctions](#fonctions)
   - Fonctions Scalaires
   - Fonctions Table
5. [Gestion des Erreurs (TRY...CATCH)](#gestion-des-erreurs-trycatch)
   - Structure de Base
   - Avec Transactions
   - THROW
6. [Triggers](#triggers)
   - Trigger AFTER
   - Trigger INSTEAD OF
   - Tables inserted et deleted
7. [Variables de Table et Tables Temporaires](#variables-de-table-et-tables-temporaires)
   - Variables de Table
   - Tables Temporaires
   - Comparaison
8. [OUTPUT Clause](#output-clause)
   - Avec INSERT
   - Avec UPDATE
   - Avec DELETE
9. [MERGE Statement](#merge-statement)
   - Syntaxe UPSERT
   - Cas d'usage pratiques
10. [Pagination avec OFFSET/FETCH](#pagination-avec-offsetfetch)
11. [Support JSON](#support-json-sql-server-2016)
    - Parser du JSON
    - Générer du JSON
12. [CTE et Récursivité en T-SQL](#cte-et-récursivité-en-t-sql)
13. [Curseurs](#curseurs-à-éviter-si-possible)
14. [Statistiques et Informations Système](#statistiques-et-informations-système)
15. [Questions Spécifiques T-SQL pour Entretiens](#questions-spécifiques-t-sql-pour-entretiens)
16. [Optimisations Spécifiques SQL Server](#optimisations-spécifiques-sql-server)
17. [Sécurité T-SQL et Injection SQL](#️-sécurité-t-sql-et-injection-sql)
18. [Bonnes Pratiques T-SQL](#bonnes-pratiques-t-sql)
19. [Différences avec d'autres SGBD](#différences-avec-dautres-sgbd)
20. [Ressources](#ressources)

---

## Différences Syntaxiques T-SQL vs SQL Standard

### Variables

```sql
-- Déclaration de variables
DECLARE @last_name VARCHAR(100);
DECLARE @age INT = 30;  -- Avec initialisation
DECLARE @price DECIMAL(10,2), @quantity INT;  -- Plusieurs en une ligne

-- Affectation
SET @last_name = 'Smith';
SELECT @last_name = last_name FROM customers WHERE id = 1;

-- Utilisation
SELECT * FROM customers WHERE last_name = @last_name;
```

### Gestion de NULL

```sql
-- ISNULL (spécifique T-SQL, 2 paramètres seulement)
SELECT ISNULL(phone_number, 'N/A') FROM customers;

-- COALESCE (SQL standard, fonctionne partout, plusieurs paramètres)
SELECT COALESCE(phone_number, mobile_number, 'N/A') FROM customers;
```

### Concaténation de Chaînes

```sql
-- Opérateur + (T-SQL)
SELECT first_name + ' ' + last_name AS full_name FROM customers;

-- CONCAT (plus sûr, gère NULL automatiquement)
SELECT CONCAT(first_name, ' ', last_name) AS full_name FROM customers;

-- CONCAT_WS (avec séparateur, SQL Server 2017+)
SELECT CONCAT_WS(' ', first_name, last_name) AS full_name FROM customers;
```

### Fonctions de Date

```sql
-- Obtenir la date/heure actuelle
SELECT GETDATE();           -- Date et heure
SELECT GETUTCDATE();        -- UTC
SELECT SYSDATETIME();       -- Plus précis (jusqu'aux nanosecondes)

-- Extraire des parties de date
SELECT YEAR(order_date) FROM orders;
SELECT MONTH(order_date) FROM orders;
SELECT DAY(order_date) FROM orders;

-- DATEPART (plus flexible)
SELECT DATEPART(YEAR, order_date) FROM orders;
SELECT DATEPART(QUARTER, order_date) FROM orders;
SELECT DATEPART(WEEKDAY, order_date) FROM orders;

-- Ajouter/soustraire des dates
SELECT DATEADD(DAY, 7, order_date) FROM orders;
SELECT DATEADD(MONTH, 1, order_date) FROM orders;
SELECT DATEADD(YEAR, -1, GETDATE()) AS one_year_ago;

-- Différence entre dates
SELECT DATEDIFF(DAY, start_date, end_date) FROM projects;
SELECT DATEDIFF(MONTH, birth_date, GETDATE()) / 12 AS age FROM customers;
```

### Identifiants Uniques (GUID)

```sql
-- Type UNIQUEIDENTIFIER (équivalent de GUID en C#)
CREATE TABLE users (
    id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    username VARCHAR(100)
);

-- Générer un GUID
INSERT INTO users (id, username) VALUES (NEWID(), 'jsmith');

-- NEWSEQUENTIALID (plus performant pour les index)
CREATE TABLE logs (
    id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    message VARCHAR(MAX)
);
```

### TOP (Limiter les résultats)

```sql
-- TOP au lieu de LIMIT
SELECT TOP 10 * FROM products ORDER BY price DESC;

-- TOP avec pourcentage
SELECT TOP 10 PERCENT * FROM customers ORDER BY total_purchases DESC;

-- TOP avec TIES (inclut les ex-aequo)
SELECT TOP 5 WITH TIES * FROM employees ORDER BY salary DESC;
```

---

## IDENTITY

La génération automatique d'identifiants uniques est essentielle pour les clés primaires. SQL Server offre deux approches : IDENTITY (classique) et SEQUENCES (A titre d'information).

### IDENTITY (Auto-incrémentation)

IDENTITY génère automatiquement des valeurs numériques uniques pour chaque nouvelle ligne.

```sql
-- Création d'une table avec IDENTITY
CREATE TABLE customers (
    id INT IDENTITY(1,1) PRIMARY KEY,  -- Commence à 1, incrémente de 1
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    email VARCHAR(100)
);

-- Syntaxe : IDENTITY(seed, increment)
CREATE TABLE products (
    id INT IDENTITY(100, 5) PRIMARY KEY,  -- Commence à 100, incrémente de 5
    name VARCHAR(100)
);

-- Insertion (pas besoin de spécifier l'ID)
INSERT INTO customers (first_name, last_name, email)
VALUES ('John', 'Smith', 'john.smith@email.com');

-- L'ID est généré automatiquement
```

**Caractéristiques d'IDENTITY** :

- ✅ Simple à utiliser
- ✅ Performant
- ❌ Lié à une seule table/colonne
- ❌ Difficile de réutiliser des valeurs
- ❌ Les valeurs peuvent avoir des "trous" (transactions annulées)

### Récupérer le Dernier ID Inséré

Il existe plusieurs méthodes pour récupérer l'ID généré, avec des différences importantes :

```sql
-- ✅ SCOPE_IDENTITY() - RECOMMANDÉ
-- Retourne le dernier ID dans le scope actuel (procédure/batch)
-- N'est PAS affecté par les triggers
INSERT INTO customers (first_name, last_name, email)
VALUES ('Emma', 'Johnson', 'emma.johnson@email.com');
SELECT SCOPE_IDENTITY() AS new_id;

-- ❌ @@IDENTITY - À ÉVITER
-- Retourne le dernier ID généré dans la session
-- PEUT être affecté par les triggers (risque d'erreur)
INSERT INTO customers (first_name, last_name, email)
VALUES ('Michael', 'Brown', 'michael.brown@email.com');
SELECT @@IDENTITY() AS new_id;

-- ✅ IDENT_CURRENT('table_name')
-- Retourne le dernier ID généré pour une table spécifique
-- Fonctionne même dans une autre session
SELECT IDENT_CURRENT('customers') AS last_id;

-- ✅ OUTPUT (le plus fiable pour plusieurs insertions)
INSERT INTO customers (first_name, last_name, email)
OUTPUT INSERTED.id, INSERTED.first_name, INSERTED.last_name
VALUES
    ('Sarah', 'Davis', 'sarah.davis@email.com'),
    ('James', 'Wilson', 'james.wilson@email.com');
```

**Comparaison des méthodes** :

| Méthode            | Scope                  | Affecté par Triggers | Recommandé             |
| ------------------ | ---------------------- | -------------------- | ---------------------- |
| `SCOPE_IDENTITY()` | Session + scope actuel | ❌ Non               | ✅ Oui                 |
| `@@IDENTITY`       | Session (global)       | ✅ Oui               | ❌ Non                 |
| `IDENT_CURRENT()`  | Toutes les sessions    | N/A                  | ⚠️ Attention           |
| `OUTPUT`           | Insertion spécifique   | ❌ Non               | ✅ Oui (multiple rows) |

### IDENTITY vs SEQUENCE : Quand utiliser quoi ?

**Utilisez IDENTITY quand** :

- Clé primaire simple pour une seule table
- Pas besoin de prévisualiser la prochaine valeur
- Compatibilité avec old_iennes versions de SQL Server

**Utilisez SEQUENCE quand** :

- Besoin de partager des IDs entre plusieurs tables
- Besoin de contrôler finement la génération (CACHE, CYCLE)
- Besoin de récupérer la prochaine valeur avant l'insertion
- SQL Server 2012+ disponible

### Exemple Pratique : Numérotation de Factures

```sql
-- Créer une facture
INSERT INTO invoices (customer_id, total_amount)
VALUES (42, 1250.00);

-- Obtenir le numéro de facture généré
SELECT SCOPE_IDENTITY() AS invoice_number;
```

---

## Procédures Stockées

Les procédures stockées sont des ensembles d'instructions SQL précompilées et stockées dans la base de données.

### Procédure Simple

```sql
-- Créer une procédure
CREATE PROCEDURE sp_AddCustomer
    @first_name VARCHAR(100),
    @last_name VARCHAR(100),
    @email VARCHAR(100),
    @city VARCHAR(100)
AS
BEGIN
    INSERT INTO customers (first_name, last_name, email, city, created_at)
    VALUES (@first_name, @last_name, @email, @city, GETDATE());

    -- Retourner l'ID du new client
    SELECT SCOPE_IDENTITY() AS new_id;
END;

-- Appeler la procédure
EXEC sp_AddCustomer 'John', 'Smith', 'john.smith@email.com', 'London';

-- Avec des variables
DECLARE @first_name VARCHAR(100) = 'Emma';
EXEC sp_AddCustomer @first_name, 'Johnson', 'emma.johnson@email.com', 'Paris';
```

### Procédure avec Paramètres de Sortie

```sql
CREATE PROCEDURE sp_GetCustomerStatistics
    @customer_id INT,
    @order_count INT OUTPUT,
    @total_purchases DECIMAL(10,2) OUTPUT
AS
BEGIN
    SELECT
        @order_count = COUNT(*),
        @total_purchases = ISNULL(SUM(amount), 0)
    FROM orders
    WHERE customer_id = @customer_id;
END;

-- Utilisation
DECLARE @count INT, @total DECIMAL(10,2);
EXEC sp_GetCustomerStatistics 5, @count OUTPUT, @total OUTPUT;
SELECT @count AS order_count, @total AS total_purchases;
```

### Procédure avec Valeurs par Défaut

```sql
CREATE PROCEDURE sp_SearchProducts
    @category VARCHAR(50) = NULL,  -- Valeur par défaut
    @max_price DECIMAL(10,2) = 1000,
    @limit INT = 10
AS
BEGIN
    SELECT TOP (@limit) *
    FROM products
    WHERE (@category IS NULL OR category = @category)
      AND price <= @max_price
    ORDER BY price DESC;
END;

-- Appels possibles
EXEC sp_SearchProducts;  -- Tous les paramètres par défaut
EXEC sp_SearchProducts 'Electronics';
EXEC sp_SearchProducts @max_price = 500;
EXEC sp_SearchProducts @category = 'Books', @limit = 5;
```

### Modifier/Supprimer une Procédure

```sql
-- Modifier
ALTER PROCEDURE sp_AddCustomer
    @first_name VARCHAR(100),
    @last_name VARCHAR(100),
    @email VARCHAR(100)
AS
BEGIN
    -- Nouvelle implémentation
END;

-- Supprimer
DROP PROCEDURE sp_AddCustomer;
```

---

## Fonctions

### Fonctions Scalaires (retournent une valeur unique)

```sql
-- Créer une fonction
CREATE FUNCTION fn_CalculateDiscount(@amount DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @discount DECIMAL(10,2);

    IF @amount > 1000
        SET @discount = @amount * 0.10;  -- 10%
    ELSE IF @amount > 500
        SET @discount = @amount * 0.05;  -- 5%
    ELSE
        SET @discount = 0;

    RETURN @discount;
END;

-- Utiliser la fonction
SELECT
    amount,
    dbo.fn_CalculateDiscount(amount) AS discount,
    amount - dbo.fn_CalculateDiscount(amount) AS final_price
FROM orders;
```

### Fonctions Table (retournent une table)

```sql
-- Fonction table inline (plus performante)
CREATE FUNCTION fn_CustomersByCity(@city VARCHAR(100))
RETURNS TABLE
AS
RETURN (
    SELECT id, first_name, last_name, email
    FROM customers
    WHERE city = @city
);

-- Utilisation
SELECT * FROM dbo.fn_CustomersByCity('London');

-- Fonction table multi-instructions
CREATE FUNCTION fn_SalesStatistics(@year INT)
RETURNS @results TABLE (
    month INT,
    total_sales DECIMAL(10,2),
    order_count INT
)
AS
BEGIN
    INSERT INTO @results
    SELECT
        MONTH(order_date) AS month,
        SUM(amount) AS total_sales,
        COUNT(*) AS order_count
    FROM orders
    WHERE YEAR(order_date) = @year
    GROUP BY MONTH(order_date);

    RETURN;
END;

-- Utilisation
SELECT * FROM dbo.fn_SalesStatistics(2024);
```

---

## Gestion des Erreurs (TRY...CATCH)

### Structure de Base

```sql
BEGIN TRY
    -- Code susceptible de générer une erreur
    UPDATE accounts SET balance = balance - 100 WHERE id = 1;
    UPDATE accounts SET balance = balance + 100 WHERE id = 2;
END TRY
BEGIN CATCH
    -- Gestion de l'erreur
    SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_MESSAGE() AS ErrorMessage,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine;
END CATCH;
```

### Avec Transactions

```sql
CREATE PROCEDURE sp_TransferMoney
    @source_account INT,
    @destination_account INT,
    @amount DECIMAL(10,2)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Vérifier le solde
        DECLARE @balance DECIMAL(10,2);
        SELECT @balance = balance FROM accounts WHERE id = @source_account;

        IF @balance < @amount
        BEGIN
            RAISERROR('Insufficient balance', 16, 1);
            RETURN;
        END

        -- Débit
        UPDATE accounts SET balance = balance - @amount WHERE id = @source_account;

        -- Crédit
        UPDATE accounts SET balance = balance + @amount WHERE id = @destination_account;

        COMMIT TRANSACTION;
        PRINT 'Transfer successful';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Propager l'erreur
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
```

### THROW (SQL Server 2012+)

```sql
-- Plus simple que RAISERROR
BEGIN TRY
    IF @age < 18
        THROW 50001, 'Age must be >= 18', 1;
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE();
END CATCH;
```

---

## Triggers

Les triggers sont des procédures stockées exécutées automatiquement lors d'événements (INSERT, UPDATE, DELETE).

### Trigger AFTER (après l'événement)

```sql
-- Trigger d'audit
CREATE TRIGGER trg_customers_audit
ON customers
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;  -- Performance : éviter les messages de nombre de lignes

    INSERT INTO customers_history (
        customer_id,
        old_first_name,
        new_first_name,
        old_last_name,
        new_last_name,
        old_email,
        new_email,
        modified_at,
        modified_by
    )
    SELECT
        d.id,
        d.first_name,
        i.first_name,
        d.last_name,
        i.last_name,
        d.email,
        i.email,
        GETDATE(),
        SUSER_NAME()
    FROM deleted d
    INNER JOIN inserted i ON d.id = i.id
    WHERE d.first_name <> i.first_name OR d.last_name <> i.last_name OR d.email <> i.email;
END;
```

### Trigger INSTEAD OF (remplace l'événement)

```sql
-- Vue non-updatable rendue updatable
CREATE TRIGGER trg_customers_view_update
ON customers_view
INSTEAD OF UPDATE
AS
BEGIN
    UPDATE customers
    SET first_name = i.first_name, last_name = i.last_name, email = i.email
    FROM customers c
    INNER JOIN inserted i ON c.id = i.id;
END;
```

### Tables Spéciales : inserted et deleted

Dans un trigger :

- **inserted** : contient les nouvelles valeurs (INSERT, UPDATE)
- **deleted** : contient les old_iennes valeurs (DELETE, UPDATE)

```sql
CREATE TRIGGER trg_stock_verification
ON orders
AFTER INSERT
AS
BEGIN
    -- Vérifier que le stock est suffisant
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN products p ON i.product_id = p.id
        WHERE p.stock < i.quantity
    )
    BEGIN
        RAISERROR('Insufficient stock for some products', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
```

**⚠️ Attention** : Les triggers peuvent impacter les performances et rendre le code difficile à déboguer. Utilisez-les avec parcimonie.

---

## Variables de Table et Tables Temporaires

### Variables de Table

```sql
-- Déclaration
DECLARE @temp_customers TABLE (
    id INT,
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    total DECIMAL(10,2)
);

-- Insertion
INSERT INTO @temp_customers
SELECT id, first_name, last_name, SUM(amount)
FROM customers c
JOIN orders o ON c.id = o.customer_id
GROUP BY c.id, c.first_name, c.last_name;

-- Utilisation
SELECT * FROM @temp_customers WHERE total > 1000;
```

**Caractéristiques** :

- Stockées en mémoire (tempdb)
- Scope limité à la session/batch
- Pas d'index (sauf PRIMARY KEY/UNIQUE à la déclaration)
- Bonnes pour petites données

### Tables Temporaires Locales (#)

```sql
-- Création
CREATE TABLE #temp_customers (
    id INT PRIMARY KEY,
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    total DECIMAL(10,2)
);

-- Insertion
INSERT INTO #temp_customers
SELECT id, first_name, last_name, SUM(amount)
FROM customers c
JOIN orders o ON c.id = o.customer_id
GROUP BY c.id, c.first_name, c.last_name;

-- Utilisation
SELECT * FROM #temp_customers WHERE total > 1000;

-- Suppression automatique à la fin de la session
-- Ou manuelle :
DROP TABLE #temp_customers;
```

**Caractéristiques** :

- Stockées sur disque (tempdb)
- Peuvent avoir des index
- Meilleures pour grandes données
- Préfixe # = locale (une session), ## = globale (toutes les sessions)

### Quand utiliser quoi ?

- **Variables de table** : petites données (<1000 lignes), pas de statistiques
- **Tables temporaires** : grandes données, besoin d'index, opérations complexes

---

## OUTPUT Clause

Permet de capturer les valeurs insérées, supprimées ou modifiées.

### Avec INSERT

```sql
-- Récupérer les IDs insérés
INSERT INTO customers (first_name, last_name, email)
OUTPUT INSERTED.id, INSERTED.first_name, INSERTED.last_name, INSERTED.created_at
VALUES
    ('John', 'Smith', 'john.smith@email.com'),
    ('Emma', 'Johnson', 'emma.johnson@email.com');

-- Dans une table
DECLARE @new_ids TABLE (id INT, first_name VARCHAR(100), last_name VARCHAR(100));

INSERT INTO customers (first_name, last_name, email)
OUTPUT INSERTED.id, INSERTED.first_name, INSERTED.last_name INTO @new_ids
VALUES ('Michael', 'Brown', 'michael.brown@email.com');

SELECT * FROM @new_ids;
```

### Avec UPDATE

```sql
-- Historiser les modifications
UPDATE customers
SET city = 'Paris'
OUTPUT
    DELETED.id,
    DELETED.first_name,
    DELETED.last_name,
    DELETED.city AS old_city,
    INSERTED.city AS new_city,
    GETDATE() AS modified_at
WHERE id IN (5, 10, 15);
```

### Avec DELETE

```sql
-- Sauvegarder avant suppression
DELETE FROM customers
OUTPUT
    DELETED.id,
    DELETED.first_name,
    DELETED.last_name,
    DELETED.email
INTO customers_archives
WHERE last_login < DATEADD(YEAR, -2, GETDATE());
```

---

## MERGE Statement

MERGE est une instruction puissante qui permet de combiner INSERT, UPDATE et DELETE en une seule opération atomique. C'est l'équivalent SQL du pattern "UPSERT" (UPDATE or INSERT).

### Syntaxe de Base

```sql
MERGE INTO table_cible AS target
USING table_source AS source
ON condition_de_correspondance
WHEN MATCHED THEN
    -- Action si la ligne existe dans les deux tables
    UPDATE SET colonne = valeur
WHEN NOT MATCHED BY TARGET THEN
    -- Action si la ligne existe seulement dans la source
    INSERT (colonnes) VALUES (valeurs)
WHEN NOT MATCHED BY SOURCE THEN
    -- Action si la ligne existe seulement dans la cible
    DELETE;
```

### Exemple Simple : Synchronisation de Données

```sql
-- Table existante (cible)
CREATE TABLE customers (
    id INT PRIMARY KEY,
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    email VARCHAR(100),
    city VARCHAR(100),
    last_updated DATETIME
);

-- Table d'import (source)
CREATE TABLE customers_import (
    id INT,
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    email VARCHAR(100),
    city VARCHAR(100)
);

-- Synchroniser les données
MERGE INTO customers AS target
USING customers_import AS source
ON target.id = source.id

-- Si le client existe : mettre à jour
WHEN MATCHED THEN
    UPDATE SET
        target.first_name = source.first_name,
        target.last_name = source.last_name,
        target.email = source.email,
        target.city = source.city,
        target.last_updated = GETDATE()

-- Si le client n'existe pas : l'ajouter
WHEN NOT MATCHED BY TARGET THEN
    INSERT (id, first_name, last_name, email, city, last_updated)
    VALUES (source.id, source.first_name, source.last_name, source.email, source.city, GETDATE())

-- Si le client existe dans la cible mais pas dans la source : le supprimer
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
```

### Exemple Pratique : Gestion de Stock

```sql
-- Table de stock actuel
CREATE TABLE product_stock (
    product_id INT PRIMARY KEY,
    quantity INT,
    last_updated DATETIME
);

-- Mouvements de stock (entrées/sorties)
CREATE TABLE stock_movements (
    product_id INT,
    quantity_delta INT  -- Positif pour entrée, négatif pour sortie
);

-- Appliquer les mouvements de stock
MERGE INTO product_stock AS target
USING (
    SELECT product_id, SUM(quantity_delta) AS total_delta
    FROM stock_movements
    GROUP BY product_id
) AS source
ON target.product_id = source.product_id

WHEN MATCHED THEN
    UPDATE SET
        target.quantity = target.quantity + source.total_delta,
        target.last_updated = GETDATE()

WHEN NOT MATCHED BY TARGET THEN
    INSERT (product_id, quantity, last_updated)
    VALUES (source.product_id, source.total_delta, GETDATE());
```

### Erreurs Courantes et Bonnes Pratiques

**❌ Erreur : Condition ON ambiguë**

```sql
-- Peut causer plusieurs correspondances
MERGE INTO customers AS target
USING customers_import AS source
ON target.email = source.email  -- ⚠️ L'email peut ne pas être unique !
```

**✅ Solution : Utiliser une clé unique**

```sql
MERGE INTO customers AS target
USING customers_import AS source
ON target.id = source.id  -- ✅ Clé primaire
```

**⚠️ Attention : MERGE est atomique**

- Toutes les actions réussissent ou échouent ensemble
- Utilisez BEGIN TRANSACTION si besoin de contrôle supplémentaire

**✅ Toujours terminer par un point-virgule**

```sql
MERGE INTO customers AS target
USING customers_import AS source
ON target.id = source.id
WHEN MATCHED THEN UPDATE SET first_name = source.first_name, last_name = source.last_name;  -- ✅ Point-virgule obligatoire
```

### Cas d'Usage Typiques

1. **Synchronisation de données** entre environnements (dev → prod)
2. **Import de données** depuis des fichiers externes (CSV, Excel)
3. **Mises à jour de cache** (mettre à jour ou créer si inexistant)
4. **Gestion de dimensions** en Data Warehousing (SCD Type 1)
5. **Réconciliation de données** entre systèmes

### Performance : MERGE vs UPDATE/INSERT séparés

```sql
-- ❌ Approche traditionnelle (2 requêtes)
UPDATE clients
SET name = source.name
FROM customers_import source
WHERE customers.id = source.id;

INSERT INTO customers (id, first_name, last_name, email)
SELECT id, first_name, last_name, email
FROM customers_import
WHERE id NOT IN (SELECT id FROM customers);

-- ✅ MERGE (1 seule requête, plus performant)
MERGE INTO customers AS target
USING customers_import AS source
ON target.id = source.id
WHEN MATCHED THEN UPDATE SET first_name = source.first_name, last_name = source.last_name
WHEN NOT MATCHED THEN INSERT (id, first_name, last_name, email) VALUES (id, first_name, last_name, email);
```

**Avantages de MERGE** :

- ✅ Une seule passe sur les données
- ✅ Atomique (tout ou rien)
- ✅ Plus lisible et maintenable
- ✅ Supporte OUTPUT pour audit

---

## Exercice Guidé : Système de Gestion de Stock

Cet exercice vous permet de mettre en pratique les concepts T-SQL vus jusqu'à présent : IDENTITY, procédures stockées, gestion d'erreurs, triggers, OUTPUT et MERGE.

**Niveau** : Intermédiaire
**Prérequis** : Avoir lu les sections précédentes de ce document

---

### 📋 Contexte

Vous devez créer un système simple de gestion de stock pour une petite entreprise. Le système doit permettre :

- D'enregistrer des products
- De gérer les mouvements de stock (entrées/sorties)
- D'historiser automatiquement les modifications
- De synchroniser les stocks depuis un fichier d'import

---

### Partie 1 : Création des Tables (10 minutes)

**Objectif** : Créer les tables nécessaires au système.

**Instructions** :

1. Créez une table `products` avec :

   - `id` (clé primaire avec IDENTITY)
   - `name` (obligatoire, max 100 caractères)
   - `unit_price` (DECIMAL, doit être positif)
   - `current_stock` (INT, par défaut 0)
   - `created_at` (DATETIME2, par défaut la date actuelle)

2. Créez une table `stock_movements` avec :

   - `id` (clé primaire avec IDENTITY)
   - `product_id` (clé étrangère vers products)
   - `quantity` (INT, positif pour entrée, négatif pour sortie)
   - `movement_type` (VARCHAR: 'IN' ou 'OUT')
   - `movement_date` (DATETIME2, par défaut date actuelle)
   - `username` (NVARCHAR, qui a fait le mouvement)

3. Créez une table `products_history` pour l'audit avec :
   - `id` (IDENTITY)
   - `product_id` (INT)
   - `action` (VARCHAR: 'INSERT', 'UPDATE', 'DELETE')
   - `old_stock` (INT)
   - `new_stock` (INT)
   - `modified_at` (DATETIME2)
   - `modified_by` (NVARCHAR)

📌 **Indices** :

- Utilisez `IDENTITY(1,1)` pour les clés primaires
- Utilisez `DEFAULT` pour les valeurs par défaut
- N'oubliez pas les contraintes `FOREIGN KEY`

<details>
<summary><b>✅ Solution Partie 1</b></summary>

```sql
-- Table des products
CREATE TABLE products (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    unit_price DECIMAL(10,2) NOT NULL CHECK (unit_price > 0),
    current_stock INT NOT NULL DEFAULT 0 CHECK (current_stock >= 0),
    created_at DATETIME2 DEFAULT SYSDATETIME()
);

-- Table des mouvements de stock
CREATE TABLE stock_movements (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    movement_type VARCHAR(10) NOT NULL CHECK (movement_type IN ('IN', 'OUT')),
    movement_date DATETIME2 DEFAULT SYSDATETIME(),
    username NVARCHAR(100) DEFAULT SUSER_NAME(),
    FOREIGN KEY (product_id) REFERENCES products(id)
);

-- Table d'historique pour l'audit
CREATE TABLE products_history (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    action VARCHAR(10) NOT NULL,
    old_stock INT,
    new_stock INT,
    modified_at DATETIME2 DEFAULT SYSDATETIME(),
    modified_by NVARCHAR(100) DEFAULT SUSER_NAME()
);
```

**Points clés** :

- ✅ `IDENTITY(1,1)` pour les IDs auto-générés
- ✅ `CHECK` pour valider les contraintes métier
- ✅ `DEFAULT` avec `SYSDATETIME()` pour la date
- ✅ `SUSER_NAME()` pour capturer l'username automatiquement
- ✅ Contrainte `FOREIGN KEY` pour garantir l'intégrité référentielle

</details>

---

### Partie 2 : Procédure d'Ajout de Produit (10 minutes)

**Objectif** : Créer une procédure stockée sécurisée pour ajouter un produit.

**Instructions** :

Créez une procédure `sp_AddProduct` qui :

1. Accepte en paramètres : `name`, `unit_price`, `initial_stock`
2. Valide les paramètres (name non vide, price > 0, stock >= 0)
3. Insère le produit dans la table
4. Retourne l'ID du new produit avec OUTPUT
5. Gère les erreurs avec TRY...CATCH
6. En cas d'erreur, affiche un message clair et fait un ROLLBACK

📌 **Indices** :

- Utilisez `SET NOCOUNT ON` pour la performance
- Utilisez `SCOPE_IDENTITY()` pour récupérer le dernier ID
- Utilisez `RAISERROR` ou `THROW` pour les validations
- N'oubliez pas `BEGIN TRANSACTION`

<details>
<summary><b>✅ Solution Partie 2</b></summary>

```sql
CREATE PROCEDURE sp_AjouterProduit
    @name NVARCHAR(100),
    @unit_price DECIMAL(10,2),
    @stock_initial INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Validation des paramètres
        IF @name IS NULL OR LEN(TRIM(@name)) = 0
            THROW 50001, 'Le name du produit est obligatoire', 1;

        IF @unit_price IS NULL OR @unit_price <= 0
            THROW 50002, 'Le price doit être supérieur à 0', 1;

        IF @stock_initial < 0
            THROW 50003, 'Le stock initial ne peut pas être négatif', 1;

        -- Début de la transaction
        BEGIN TRANSACTION;

        -- Insertion du produit
        INSERT INTO products (name, unit_price, current_stock)
        VALUES (@name, @unit_price, @stock_initial);

        -- Récupérer l'ID du new produit
        DECLARE @new_id INT = SCOPE_IDENTITY();

        -- Valider la transaction
        COMMIT TRANSACTION;

        -- Retourner le résultat
        SELECT
            @new_id AS id,
            'Produit créé avec succès' AS message;

    END TRY
    BEGIN CATCH
        -- Annuler la transaction en cas d'erreur
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Afficher l'erreur
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
```

**Test de la procédure** :

```sql
-- ✅ Test valide
EXEC sp_AjouterProduit 'Laptop Dell XPS', 1299.99, 10;

-- ❌ Test avec price invalide (doit échouer)
EXEC sp_AjouterProduit 'Produit Test', -50, 5;

-- ❌ Test avec name vide (doit échouer)
EXEC sp_AjouterProduit '', 100, 0;
```

**Points clés** :

- ✅ Validation stricte des paramètres avant insertion
- ✅ `THROW` pour générer des erreurs personnalisées
- ✅ Transaction pour garantir l'atomicité
- ✅ `SCOPE_IDENTITY()` pour récupérer l'ID
- ✅ `@@TRANCOUNT` pour vérifier si une transaction est active avant ROLLBACK

</details>

---

### Partie 3 : Procédure de Mouvement de Stock (15 minutes)

**Objectif** : Créer une procédure pour enregistrer les entrées/sorties de stock.

**Instructions** :

Créez une procédure `sp_EnregistrerMouvement` qui :

1. Accepte : `product_id`, `quantity`, `movement_type` ('IN' ou 'OUT')
2. Vérifie que le produit existe
3. Vérifie qu'il y a assez de stock pour une sortie
4. Met à jour le stock du produit
5. Enregistre le mouvement dans `stock_movements`
6. Utilise OUTPUT pour retourner le new stock
7. Gère les erreurs avec TRY...CATCH

📌 **Indices** :

- Pour une OUT, la quantité doit être négative dans le calcul
- Vérifiez le stock AVANT de faire la mise à jour
- Utilisez une transaction pour garantir la cohérence

<details>
<summary><b>✅ Solution Partie 3</b></summary>

```sql
CREATE PROCEDURE sp_EnregistrerMouvement
    @product_id INT,
    @quantity INT,
    @movement_type VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Validation des paramètres
        IF @movement_type NOT IN ('IN', 'OUT')
            THROW 50004, 'Type de mouvement invalide. Utilisez IN ou OUT', 1;

        IF @quantity <= 0
            THROW 50005, 'La quantité doit être positive', 1;

        BEGIN TRANSACTION;

        -- Vérifier que le produit existe et récupérer le stock actuel
        DECLARE @current_stock INT;
        DECLARE @product_name NVARCHAR(100);

        SELECT @current_stock = current_stock, @product_name = name
        FROM products
        WHERE id = @product_id;

        IF @current_stock IS NULL
            THROW 50006, 'Produit introuvable', 1;

        -- Calculer la variation de stock
        DECLARE @variation INT;
        IF @movement_type = 'IN'
            SET @variation = @quantity;
        ELSE
            SET @variation = -@quantity;

        -- Vérifier qu'il y a assez de stock pour une sortie
        IF @current_stock + @variation < 0
        BEGIN
            DECLARE @msg NVARCHAR(200) =
                CONCAT('Stock insuffisant. Stock actuel: ', @current_stock,
                       ', Quantité demandée: ', @quantity);
            THROW 50007, @msg, 1;
        END

        -- Mettre à jour le stock du produit
        DECLARE @new_stock INT;

        UPDATE products
        SET current_stock = current_stock + @variation
        OUTPUT INSERTED.current_stock INTO @new_stock
        WHERE id = @product_id;

        -- Enregistrer le mouvement
        INSERT INTO stock_movements (product_id, quantity, movement_type)
        VALUES (@product_id, @variation, @movement_type);

        COMMIT TRANSACTION;

        -- Retourner le résultat
        SELECT
            @product_id AS product_id,
            @product_name AS product_name,
            @current_stock AS old_stock,
            @current_stock + @variation AS new_stock,
            @movement_type AS movement_type,
            'Mouvement enregistré avec succès' AS message;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
```

**Test de la procédure** :

```sql
-- Créer un produit de test
EXEC sp_AjouterProduit 'Clavier Mécanique', 89.99, 20;

-- ✅ Entrée de stock (+30)
EXEC sp_EnregistrerMouvement 1, 30, 'IN';

-- ✅ Sortie de stock (-5)
EXEC sp_EnregistrerMouvement 1, 5, 'OUT';

-- ❌ Sortie impossible (stock insuffisant)
EXEC sp_EnregistrerMouvement 1, 1000, 'OUT';

-- Vérifier l'état
SELECT * FROM products WHERE id = 1;
SELECT * FROM stock_movements WHERE product_id = 1;
```

**Points clés** :

- ✅ Validation du type de mouvement avec liste blanche
- ✅ Vérification du stock AVANT la modification
- ✅ Calcul de la variation (positif/négatif) selon le type
- ✅ Message d'erreur personnalisé avec détails
- ✅ OUTPUT pour retourner les informations sans requête supplémentaire

</details>

---

### Partie 4 : Trigger d'Audit (10 minutes)

**Objectif** : Créer un trigger pour historiser automatiquement toutes les modifications de stock.

**Instructions** :

Créez un trigger `trg_audit_produits` sur la table `products` qui :

1. Se déclenche AFTER UPDATE
2. Enregistre dans `products_history` :
   - L'ID du produit modifié
   - L'old_ien et le new stock
   - L'action ('UPDATE')
   - La date et l'username
3. N'enregistre que si le stock a réellement changé

📌 **Indices** :

- Utilisez les tables `inserted` et `deleted`
- Comparez les valeurs pour détecter un changement
- Utilisez `SUSER_NAME()` pour l'username

<details>
<summary><b>✅ Solution Partie 4</b></summary>

```sql
CREATE TRIGGER trg_audit_produits
ON products
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Enregistrer les modifications de stock dans l'historique
    INSERT INTO products_history (
        product_id,
        action,
        old_stock,
        new_stock,
        modified_at,
        username
    )
    SELECT
        i.id,
        'UPDATE',
        d.current_stock,
        i.current_stock,
        SYSDATETIME(),
        SUSER_NAME()
    FROM inserted i
    INNER JOIN deleted d ON i.id = d.id
    WHERE i.current_stock <> d.current_stock;  -- Seulement si le stock a changé
END;
```

**Test du trigger** :

```sql
-- Modifier le stock directement (pour tester le trigger)
UPDATE products SET current_stock = 100 WHERE id = 1;

-- Vérifier l'historique
SELECT * FROM products_history;

-- Ou utiliser la procédure (le trigger se déclenche aussi)
EXEC sp_EnregistrerMouvement 1, 10, 'IN';

-- Vérifier l'historique à new
SELECT
    h.id,
    p.name AS produit,
    h.action,
    h.old_stock,
    h.new_stock,
    h.new_stock - h.old_stock AS variation,
    h.modified_at,
    h.username
FROM products_history h
INNER JOIN products p ON h.product_id = p.id
ORDER BY h.modified_at DESC;
```

**Points clés** :

- ✅ `AFTER UPDATE` pour agir après la modification
- ✅ Tables `inserted` (nouvelles valeurs) et `deleted` (old_iennes valeurs)
- ✅ Condition `WHERE` pour n'enregistrer que les vrais changements
- ✅ `SUSER_NAME()` pour tracer automatiquement l'username
- ✅ Le trigger se déclenche même lors des UPDATE via procédures

</details>

---

### Partie 5 : MERGE pour Synchronisation (10 minutes)

**Objectif** : Utiliser MERGE pour synchroniser les stocks depuis un import.

**Instructions** :

1. Créez une table temporaire `produits_import` avec les mêmes colonnes que `products`
2. Insérez quelques données de test dans cette table
3. Créez une requête MERGE qui :
   - Met à jour les products existants (même name)
   - Insère les nouveaux products
   - Utilise OUTPUT pour voir les actions effectuées

📌 **Indices** :

- Utilisez `ON products.name = produits_import.name` comme condition
- MERGE nécessite un point-virgule à la fin
- OUTPUT avec `$action` montre l'action effectuée

<details>
<summary><b>✅ Solution Partie 5</b></summary>

```sql
-- Créer la table d'import
CREATE TABLE produits_import (
    name NVARCHAR(100),
    unit_price DECIMAL(10,2),
    current_stock INT
);

-- Insérer des données de test
INSERT INTO produits_import (name, unit_price, current_stock)
VALUES
    ('Laptop Dell XPS', 1399.99, 15),        -- Existe déjà, sera mis à jour
    ('Clavier Mécanique', 89.99, 25),        -- Existe déjà, sera mis à jour
    ('Souris Sans Fil', 29.99, 50),          -- Nouveau produit
    ('Écran 27 pouces', 299.99, 12);         -- Nouveau produit

-- Synchronisation avec MERGE
MERGE INTO products AS target
USING produits_import AS source
ON target.name = source.name

-- Si le produit existe : mettre à jour le price et le stock
WHEN MATCHED THEN
    UPDATE SET
        target.unit_price = source.unit_price,
        target.current_stock = source.current_stock

-- Si le produit n'existe pas : l'ajouter
WHEN NOT MATCHED BY TARGET THEN
    INSERT (name, unit_price, current_stock)
    VALUES (source.name, source.unit_price, source.current_stock)

-- Afficher les actions effectuées
OUTPUT
    $action AS action_effectuee,
    INSERTED.id AS product_id,
    INSERTED.name AS product_name,
    INSERTED.unit_price AS price,
    INSERTED.current_stock AS stock;
```

**Vérification** :

```sql
-- Voir tous les products après MERGE
SELECT * FROM products ORDER BY id;

-- Voir l'historique des modifications (le trigger s'est déclenché)
SELECT
    p.name,
    h.action,
    h.old_stock,
    h.new_stock,
    h.modified_at
FROM products_history h
INNER JOIN products p ON h.product_id = p.id
ORDER BY h.modified_at DESC;
```

**Nettoyage** :

```sql
-- Supprimer la table d'import si vous n'en avez plus besoin
DROP TABLE produits_import;
```

**Points clés** :

- ✅ `MERGE` combine UPDATE et INSERT en une seule opération
- ✅ `ON` définit la condition de correspondance
- ✅ `WHEN MATCHED` pour les mises à jour
- ✅ `WHEN NOT MATCHED BY TARGET` pour les insertions
- ✅ `OUTPUT $action` montre quelle action a été effectuée
- ✅ Le trigger d'audit s'est déclenché automatiquement pour les UPDATE
- ✅ Point-virgule obligatoire à la fin du MERGE

</details>

---

### 🎯 Récapitulatif

**Félicitations !** Vous avez créé un système complet de gestion de stock qui utilise :

✅ **IDENTITY** - Pour les clés primaires auto-générées
✅ **Procédures stockées** - Avec validation et gestion d'erreurs complète
✅ **TRY...CATCH** - Pour gérer les erreurs proprement
✅ **Transactions** - Pour garantir la cohérence des données
✅ **SCOPE_IDENTITY()** - Pour récupérer les IDs générés
✅ **Triggers** - Pour l'audit automatique
✅ **Tables inserted/deleted** - Pour capturer les changements
✅ **MERGE** - Pour synchroniser les données efficacement
✅ **OUTPUT** - Pour retourner des résultats sans requêtes supplémentaires

**Concepts de sécurité appliqués** :

- ✅ Validation stricte des paramètres
- ✅ Contraintes CHECK au level base de données
- ✅ Gestion appropriée des erreurs
- ✅ Traçabilité avec audit automatique

---

## Pagination avec OFFSET/FETCH

SQL Server 2012+ offre une pagination standard SQL.

```sql
-- Page 1 (résultats 1-10)
SELECT id, name, email
FROM clients
ORDER BY id
OFFSET 0 ROWS
FETCH NEXT 10 ROWS ONLY;

-- Page 2 (résultats 11-20)
SELECT id, name, email
FROM clients
ORDER BY id
OFFSET 10 ROWS
FETCH NEXT 10 ROWS ONLY;

-- Page 3 (résultats 21-30)
SELECT id, name, email
FROM clients
ORDER BY id
OFFSET 20 ROWS
FETCH NEXT 10 ROWS ONLY;

-- Pagination dynamique
DECLARE @page INT = 3;
DECLARE @taille_page INT = 10;

SELECT id, name, email
FROM clients
ORDER BY id
OFFSET (@page - 1) * @taille_page ROWS
FETCH NEXT @taille_page ROWS ONLY;
```

**Note** : ORDER BY est **obligatoire** avec OFFSET/FETCH.

---

## CTE et Récursivité en T-SQL

T-SQL supporte pleinement les CTE récursifs.

```sql
-- Hiérarchie d'employés
WITH EmployesCTE AS (
    -- Ancre : le PDG
    SELECT id, name, manager_id, 1 AS level
    FROM employes
    WHERE manager_id IS NULL

    UNION ALL

    -- Récursion : les subordonnés
    SELECT e.id, e.name, e.manager_id, cte.level + 1
    FROM employes e
    INNER JOIN EmployesCTE cte ON e.manager_id = cte.id
)
SELECT * FROM EmployesCTE
ORDER BY level, name;

-- Avec limite de récursion
WITH EmployesCTE AS (...)
SELECT * FROM EmployesCTE
OPTION (MAXRECURSION 10);  -- Maximum 10 niveaux
```

---

## Curseurs (à éviter si possible)

Les curseurs permettent de parcourir les résultats ligne par ligne, mais sont **lents**. Préférez les opérations set-based quand c'est possible.

```sql
-- Déclaration
DECLARE @id INT, @name VARCHAR(100);

DECLARE curseur_clients CURSOR FOR
    SELECT id, name FROM clients WHERE city = 'Paris';

-- Ouverture
OPEN curseur_clients;

-- Lecture de la première ligne
FETCH NEXT FROM curseur_clients INTO @id, @name;

-- Boucle
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Traitement
    PRINT CONCAT('Client: ', @name, ' (ID: ', @id, ')');

    -- Ligne suivante
    FETCH NEXT FROM curseur_clients INTO @id, @name;
END;

-- Fermeture et libération
CLOSE curseur_clients;
DEALLOCATE curseur_clients;
```

**⚠️ À éviter** : Les curseurs sont lents. Cherchez toujours une alternative set-based.

---

## Questions Spécifiques T-SQL pour Entretiens

### Questions Théoriques

**1. Différence entre une fonction et une procédure stockée ?**

<details>
<summary>Voir la réponse</summary>

**Fonction :**
- Retourne obligatoirement une valeur (scalaire ou table)
- Utilisable dans les SELECT
- Ne peut pas modifier les données (pas d'INSERT, UPDATE, DELETE)
- Pas de gestion des transactions

**Procédure stockée :**
- Peut modifier des données
- Retourne des résultats via SELECT ou paramètres OUTPUT
- Supporte les transactions
- Peut exécuter du code complexe

```sql
-- Fonction
CREATE FUNCTION fn_GetDiscount(@amount DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS BEGIN
    RETURN @amount * 0.1;
END;

-- Procédure
CREATE PROCEDURE sp_UpdatePrice
    @productId INT,
    @newPrice DECIMAL(10,2)
AS BEGIN
    UPDATE Products SET Price = @newPrice WHERE Id = @productId;
END;
```

</details>

**2. Qu'est-ce que SCOPE_IDENTITY() ?**

<details>
<summary>Voir la réponse</summary>

**SCOPE_IDENTITY()** retourne le dernier ID auto-incrémenté (IDENTITY) généré dans le **scope actuel** (procédure, fonction, batch).

**Avantage :** N'est **pas affecté** par les triggers, contrairement à `@@IDENTITY`.

```sql
INSERT INTO Users (Name, Email)
VALUES ('John Smith', 'john@example.com');

-- Récupérer l'ID généré
SELECT SCOPE_IDENTITY() AS NewUserId;
```

**Comparaison :**
- `SCOPE_IDENTITY()` : Scope actuel uniquement (recommandé)
- `@@IDENTITY` : Toute la session (peut être affecté par les triggers)
- `IDENT_CURRENT('table')` : Dernière valeur pour une table spécifique

</details>

**3. Différence entre table temporaire et variable de table ?**

<details>
<summary>Voir la réponse</summary>

**Table temporaire (#) :**
- Stockée sur disque (tempdb)
- Peut avoir des index
- Meilleures performances pour grandes quantités de données
- Statistiques disponibles

**Variable de table (@) :**
- Stockée en mémoire (principalement)
- Pas d'index (sauf à la déclaration)
- Meilleure pour petites données (<1000 lignes)
- Pas de statistiques

```sql
-- Table temporaire
CREATE TABLE #TempUsers (
    Id INT,
    Name VARCHAR(100)
);
INSERT INTO #TempUsers SELECT Id, Name FROM Users;

-- Variable de table
DECLARE @TempUsers TABLE (
    Id INT,
    Name VARCHAR(100)
);
INSERT INTO @TempUsers SELECT Id, Name FROM Users;
```

**Conseil :** Utilisez les tables temporaires pour de grandes quantités de données.

</details>

**4. Que sont les tables inserted et deleted ?**

<details>
<summary>Voir la réponse</summary>

**inserted** et **deleted** sont des tables spéciales disponibles dans les **triggers**.

**inserted :**
- Contient les **nouvelles** valeurs lors d'un INSERT ou UPDATE

**deleted :**
- Contient les **anciennes** valeurs lors d'un DELETE ou UPDATE

```sql
CREATE TRIGGER trg_Audit_Users
ON Users
AFTER UPDATE
AS
BEGIN
    INSERT INTO Audit_Log (UserId, OldEmail, NewEmail, ModifiedAt)
    SELECT
        d.Id,
        d.Email,        -- Ancienne valeur
        i.Email,        -- Nouvelle valeur
        GETDATE()
    FROM deleted d
    INNER JOIN inserted i ON d.Id = i.Id
    WHERE d.Email <> i.Email;
END;
```

**Tableau récapitulatif :**

| Opération | inserted | deleted |
|-----------|----------|---------|
| INSERT    | Nouvelles lignes | Vide |
| UPDATE    | Nouvelles valeurs | Anciennes valeurs |
| DELETE    | Vide | Lignes supprimées |

</details>

**5. Différence entre SET et SELECT pour l'affectation ?**

<details>
<summary>Voir la réponse</summary>

**SET :**
- Affecte **une seule** variable à la fois
- Génère une **erreur** si la requête retourne plusieurs résultats
- Plus strict et prévisible

**SELECT :**
- Peut affecter **plusieurs** variables simultanément
- Prend la **dernière** valeur si plusieurs résultats
- Pas d'erreur si plusieurs résultats (comportement silencieux)

```sql
DECLARE @name VARCHAR(100), @email VARCHAR(100);

-- SET : une variable à la fois
SET @name = (SELECT Name FROM Users WHERE Id = 1);
SET @email = (SELECT Email FROM Users WHERE Id = 1);

-- SELECT : plusieurs variables en une fois
SELECT @name = Name, @email = Email
FROM Users
WHERE Id = 1;

-- ⚠️ Problème avec SELECT : si plusieurs résultats
SELECT @name = Name FROM Users; -- Prend la dernière valeur (non prévisible)

-- ✅ SET génère une erreur
SET @name = (SELECT Name FROM Users); -- ERREUR si > 1 résultat
```

**Conseil :** Utilisez `SET` pour plus de sécurité et de clarté.

</details>

### Exercices Pratiques

**Exercice 1 : Procédure avec gestion d'erreur**

Créez une procédure stockée `sp_AjouterProduit` qui :
- Accepte les paramètres : nom, prix, stock
- Valide que le prix est positif et le stock non négatif
- Insère le produit et retourne son ID
- Gère les erreurs avec TRY...CATCH

<details>
<summary>Voir la réponse</summary>

```sql
CREATE PROCEDURE sp_AjouterProduit
    @name VARCHAR(100),
    @price DECIMAL(10,2),
    @stock INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validation des paramètres
        IF @price <= 0
            THROW 50001, 'Le prix doit être positif', 1;

        IF @stock < 0
            THROW 50002, 'Le stock ne peut pas être négatif', 1;

        -- Insertion
        INSERT INTO products (name, price, stock, created_at)
        VALUES (@name, @price, @stock, GETDATE());

        COMMIT TRANSACTION;

        -- Retourner l'ID généré
        SELECT SCOPE_IDENTITY() AS new_id;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Propager l'erreur
        THROW;
    END CATCH;
END;
```

**Test de la procédure :**
```sql
-- ✅ Test valide
EXEC sp_AjouterProduit 'Laptop', 1200.00, 10;

-- ❌ Test avec prix invalide
EXEC sp_AjouterProduit 'Produit', -50.00, 5;
```

</details>

**Exercice 2 : Fonction calculant un total avec remise**

Créez une fonction `fn_TotalAvecRemise` qui :
- Prend un montant en paramètre
- Applique une remise par palier (5%, 10%, 15%)
- Retourne le montant après remise

<details>
<summary>Voir la réponse</summary>

```sql
CREATE FUNCTION fn_TotalAvecRemise(@montant DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @remise DECIMAL(10,2) = 0;
    DECLARE @total DECIMAL(10,2);

    -- Remise par palier
    IF @montant > 1000
        SET @remise = 0.15;  -- 15% pour montant > 1000€
    ELSE IF @montant > 500
        SET @remise = 0.10;  -- 10% pour montant > 500€
    ELSE IF @montant > 100
        SET @remise = 0.05;  -- 5% pour montant > 100€

    SET @total = @montant * (1 - @remise);
    RETURN @total;
END;
```

**Utilisation :**
```sql
-- Utiliser la fonction dans une requête
SELECT
    OrderId,
    Amount,
    dbo.fn_TotalAvecRemise(Amount) AS FinalAmount,
    Amount - dbo.fn_TotalAvecRemise(Amount) AS Discount
FROM Orders;
```

**Tests :**
```sql
SELECT dbo.fn_TotalAvecRemise(50);    -- 50.00 (pas de remise)
SELECT dbo.fn_TotalAvecRemise(150);   -- 142.50 (5% de remise)
SELECT dbo.fn_TotalAvecRemise(600);   -- 540.00 (10% de remise)
SELECT dbo.fn_TotalAvecRemise(1500);  -- 1275.00 (15% de remise)
```

</details>

---

## Optimisations Spécifiques SQL Server

### Index avec colonnes incluses

```sql
-- Index couvrant (covering index)
CREATE INDEX idx_clients_ville_inclus
ON clients(city)
INCLUDE (name, email);

-- Évite de retourner à la table pour name et email
SELECT name, email FROM clients WHERE city = 'Paris';
```

### Index filtrés

```sql
-- Index seulement pour clients actifs (économise de l'espace)
CREATE INDEX idx_clients_actifs
ON clients(city)
WHERE statut = 'actif';
```

### NOLOCK (lecture sale - attention !)

```sql
-- Lecture sans verrou (peut lire des données non validées)
SELECT * FROM clients WITH (NOLOCK);

-- Équivalent :
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT * FROM clients;
```

**⚠️ Utiliser avec précaution** : Peut lire des données incohérentes.

### Forcer un index

```sql
-- Forcer l'utilisation d'un index spécifique
SELECT * FROM clients WITH (INDEX(idx_email))
WHERE email = 'test@email.com';
```

---

## ⚠️ Sécurité T-SQL et Injection SQL

> **CRITIQUE** : SQL Server a des spécificités de sécurité importantes (xp_cmdshell, sp_executesql, etc.)

### Règles Essentielles T-SQL

**✅ Utiliser sp_executesql au lieu de EXEC() pour le SQL dynamique**

```sql
-- ❌ DANGEREUX
DECLARE @sql NVARCHAR(MAX) = 'SELECT * FROM clients WHERE city = ''' + @city + '''';
EXEC(@sql);

-- ✅ SÉCURISÉ
DECLARE @sql NVARCHAR(MAX) = N'SELECT * FROM clients WHERE city = @city';
EXEC sp_executesql @sql, N'@city NVARCHAR(100)', @city = @city;
```

**✅ Toujours utiliser QUOTENAME() pour les identifiants dynamiques**

```sql
-- Protège contre l'injection dans les noms de tables/colonnes
DECLARE @sql NVARCHAR(MAX) = N'SELECT * FROM ' + QUOTENAME(@tableName);
```

### Pour aller plus loin sur la Sécurité

Les concepts de sécurité SQL couverts dans ce document (injection SQL, `sp_executesql`, `QUOTENAME`) constituent une base solide pour sécuriser vos applications.

**Pour une formation complète sur la sécurité SQL Server**, incluant les permissions avancées, l'audit, le chiffrement (TDE, Always Encrypted) et Row-Level Security, une formation dédiée sera disponible prochainement.

---

## Bonnes Pratiques T-SQL

1. **Préfixer les procédures/fonctions** avec un préfixe métier (pas `sp_` réservé système)
2. **Toujours SET NOCOUNT ON** dans les procédures (performance)
3. **Utiliser TRY...CATCH** pour la gestion d'erreurs robuste
4. **Éviter les curseurs** - privilégier les opérations set-based
5. **Utiliser OUTPUT** pour récupérer les valeurs au lieu de requêtes supplémentaires
6. **Utiliser sp_executesql** au lieu de EXEC() pour le SQL dynamique
7. **Valider par liste blanche** tous les identifiants dynamiques
8. **Documenter les procédures/fonctions** avec des commentaires
9. **Tester les procédures** avec différents cas limites et injections
10. **Utiliser SCOPE_IDENTITY()** plutôt que @@IDENTITY
11. **Nommer explicitement les paramètres** lors des appels de procédures
12. **Vérifier @@TRANCOUNT** avant ROLLBACK
13. **Appliquer le principe du moindre privilège** pour tous les comptes

---

## Différences avec d'autres SGBD

### MySQL

- MySQL utilise `DELIMITER` pour les procédures, T-SQL utilise `GO`
- MySQL : `LIMIT`, T-SQL : `TOP` ou `OFFSET/FETCH`
- MySQL : backticks \`, T-SQL : crochets []

### PostgreSQL

- PostgreSQL : `RETURNING`, T-SQL : `OUTPUT`
- PostgreSQL : fonctions en PL/pgSQL, T-SQL : procédures/fonctions T-SQL
- PostgreSQL : `SERIAL`, T-SQL : `IDENTITY`

### Oracle

- Oracle : packages, T-SQL : schémas + procédures
- Oracle : `ROWNUM`, T-SQL : `ROW_NUMBER()`
- Oracle : `SYSDATE`, T-SQL : `GETDATE()`

---

## Ressources

- [Documentation officielle Microsoft SQL Server](https://docs.microsoft.com/en-us/sql/t-sql/)
- [SQL Server Central](https://www.sqlservercentral.com/)

---

**Note finale** : T-SQL est puissant mais propriétaire. Pour du code portable, privilégiez SQL standard quand c'est possible et isolez les spécificités T-SQL dans des procédures stockées.
