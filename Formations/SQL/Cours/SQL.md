# Formation SQL - Les Fondamentaux

## Introduction

SQL (Structured Query Language) est le langage standard pour interagir avec les bases de données relationnelles. Ce document couvre les concepts SQL universels, applicables à tous les systèmes de gestion de bases de données (MySQL, PostgreSQL, SQL Server, Oracle, SQLite, etc.).

---

## Table des Matières

1. [Conventions de Nommage SQL](#conventions-de-nommage-sql)
   - Pourquoi coder en anglais ?
   - Conventions pour les tables
   - Conventions pour les colonnes
   - Glossaire français-anglais
2. [Concepts Théoriques Fondamentaux](#concepts-théoriques-fondamentaux)
   - Base de données relationnelle
   - Clés (Primary Key, Foreign Key)
   - Relations entre tables
   - Types de données
   - Contraintes d'intégrité
3. [Comprendre SELECT en Profondeur](#comprendre-select-en-profondeur)
   - Anatomie d'une requête SELECT
   - SELECT - Les Bases
   - WHERE - Filtrer les Données
   - HAVING vs WHERE
   - ORDER BY et LIMIT
4. [Les Commandes de Modification](#les-commandes-de-modification)
   - INSERT
   - UPDATE
   - DELETE
5. [Les Jointures (JOIN)](#les-jointures-join)
   - INNER JOIN
   - LEFT JOIN
   - RIGHT JOIN
   - FULL OUTER JOIN
   - Jointures Multiples
6. [Fonctions d'Agrégation et GROUP BY](#fonctions-dagrégation-et-group-by)
   - COUNT, SUM, AVG, MIN, MAX
   - GROUP BY
   - Règles importantes
7. [Sous-requêtes](#sous-requêtes)
   - Sous-requête dans WHERE
   - Sous-requête dans FROM
   - Sous-requête corrélée
8. [CTE (Common Table Expressions)](#cte-common-table-expressions)
   - CTE Simple
   - CTE Multiple
   - CTE Récursif
9. [Calculs et Expressions](#calculs-et-expressions)
   - Opérations mathématiques
   - Fonctions sur texte
   - Fonctions sur dates
   - CASE - Logique conditionnelle
10. [Index - Notions de Base](#index---notions-de-base)
    - Qu'est-ce qu'un index ?
    - Avantages et inconvénients
11. [Transactions et ACID](#transactions-et-acid)
    - Qu'est-ce qu'une transaction ?
    - Propriétés ACID
    - Niveaux d'isolation
12. [Normalisation des Bases de Données](#normalisation-des-bases-de-données)
    - Première Forme Normale (1NF)
    - Deuxième Forme Normale (2NF)
    - Troisième Forme Normale (3NF)
13. [Vues (Views)](#vues-views)
14. [Questions Types d'Entretien](#questions-types-dentretien)
    - Questions théoriques
    - Exercices pratiques
    - Pièges courants
15. [Bonnes Pratiques](#bonnes-pratiques)
16. [Ressources](#ressources)

---

## Conventions de Nommage SQL

### Pourquoi Coder en Anglais ?

Dans cette formation, **tous les exemples SQL utilisent l'anglais** pour les noms de tables, colonnes et variables. Voici pourquoi :

**Raisons professionnelles** :

- ✅ **Standard universel** : 99% du code SQL en entreprise est en anglais
- ✅ **Collaboration internationale** : facilite le travail en équipe globale
- ✅ **Documentation** : Stack Overflow, tutoriels, documentation officielle → tout en anglais
- ✅ **Bonnes pratiques** : respecte les standards de l'industrie
- ✅ **Transférabilité** : vos compétences sont directement applicables partout

> **Note** : Les explications, commentaires et instructions restent en **français** pour faciliter l'apprentissage.

---

### Conventions pour les Tables

**Format recommandé** : `snake_case` (minuscules, mots séparés par `_`) et **pluriel**

```sql
-- ✅ BIEN
customers
orders
products
order_items
user_profiles
```

```sql
-- ❌ À ÉVITER
Client           -- Pas en anglais
COMMANDES        -- Majuscules
orderItems       -- camelCase (à éviter en SQL)
product          -- Singulier (moins clair)
```

**Pourquoi le pluriel ?**

- Une table contient **plusieurs** enregistrements
- Plus naturel en lecture : `SELECT * FROM customers` (de tous les clients)

---

### Conventions pour les Colonnes

**Format recommandé** : `snake_case` (minuscules, mots séparés par `_`)

**Colonnes standards** :

```sql
-- Clé primaire
id                -- Simple et universel

-- Informations personnelles
first_name        -- Prénom
last_name         -- Nom de famille
email
phone_number
date_of_birth     -- Date de naissance

-- Adresses
address_line_1    -- Ligne d'adresse 1
address_line_2    -- Ligne d'adresse 2 (optionnel)
city
postal_code       -- Code postal
country

-- Montants et quantités
price
amount
quantity
total_amount

-- Dates de suivi
created_at        -- Date de création
updated_at        -- Date de dernière modification
deleted_at        -- Pour soft delete
```

**Clés étrangères** : Toujours suffixer par `_id`

```sql
customer_id       -- Référence à customers(id)
product_id        -- Référence à products(id)
order_id          -- Référence à orders(id)
category_id       -- Référence à categories(id)
```

---

### Conventions pour les Contraintes

```sql
-- Index
idx_customers_email           -- Index sur customers.email
idx_orders_customer_id        -- Index sur orders.customer_id

-- Clés étrangères
fk_orders_customer_id         -- Foreign key de orders vers customers
fk_order_items_product_id     -- Foreign key de order_items vers products

-- Contraintes uniques
uq_customers_email            -- UNIQUE sur customers.email
```

---

### Bonnes Pratiques Supplémentaires

1. **Évitez les mots réservés SQL** : `user`, `order`, `table`, etc.

   - Utilisez plutôt : `users`, `orders`, `user_account`

2. **Soyez cohérent** : Si vous utilisez `created_at`, n'utilisez pas `creation_date` ailleurs

3. **Préférez les noms explicites** :

   - ✅ `customer_id` (clair)
   - ❌ `cust_id` (abréviation)

4. **Booléens** : Préfixez par `is_`, `has_`, `can_`

   ```sql
   is_active
   has_discount
   can_login
   ```

5. **Dates/Timestamps** : Suffixez par `_at` ou `_date`
   ```sql
   created_at
   updated_at
   birth_date
   hire_date
   ```

---

> 💡 **Dans toute la formation, nous respectons ces conventions**. Prenez le temps de vous familiariser avec ces termes anglais, vous les utiliserez quotidiennement en entreprise !

---

## Concepts Théoriques Fondamentaux

### Qu'est-ce qu'une base de données relationnelle ?

Une base de données relationnelle organise les données en **tables** (relations). Chaque table contient :

- **Colonnes** : les attributs ou champs (ex: nom, prénom, email)
- **Lignes** : les enregistrements (chaque client, chaque produit...)

### Les Clés - Fondation du Relationnel

**Clé Primaire (Primary Key)**

- Identifie de manière **unique** chaque ligne d'une table
- Ne peut **jamais** être NULL
- Ne peut **jamais** être dupliquée

```sql
CREATE TABLE clients (
    id INT PRIMARY KEY,
    nom VARCHAR(100),
    email VARCHAR(100)
);
```

**Clé Étrangère (Foreign Key)**

- Crée un **lien** entre deux tables
- Fait référence à la clé primaire d'une autre table
- Assure l'**intégrité référentielle**

```sql
CREATE TABLE commandes (
    id INT PRIMARY KEY,
    client_id INT,
    montant DECIMAL(10,2),
    FOREIGN KEY (client_id) REFERENCES clients(id)
);
```

### Les Relations Entre Tables

**One-to-Many (1 à N)** - La plus courante

- Un client peut avoir plusieurs commandes
- Une commande appartient à un seul client

**Many-to-Many (N à N)** - Nécessite une table de liaison

- Un étudiant peut suivre plusieurs cours
- Un cours peut avoir plusieurs étudiants
- Solution : table `inscriptions` avec `etudiant_id` et `cours_id`

**One-to-One (1 à 1)** - Rare

- Un utilisateur a un seul profil détaillé

### Types de Données Courants

**Numériques**

- `INT` : nombres entiers (-2147483648 à 2147483647)
- `BIGINT` : grands entiers
- `DECIMAL(p,s)` : nombres décimaux précis (ex: prix avec `DECIMAL(10,2)`)
- `FLOAT` / `DOUBLE` : nombres à virgule flottante

**Texte**

- `VARCHAR(n)` : texte variable (max n caractères) - le plus utilisé
- `CHAR(n)` : texte fixe (toujours n caractères)
- `TEXT` : texte long sans limite fixe

**Date et Heure**

- `DATE` : date seulement (YYYY-MM-DD)
- `TIME` : heure seulement (HH:MM:SS)
- `DATETIME` / `TIMESTAMP` : date + heure

**Autres**

- `BOOLEAN` : vrai/faux (TRUE/FALSE)
- `BLOB` : données binaires (images, fichiers)

### Contraintes d'Intégrité

```sql
CREATE TABLE products (
    id INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,              -- Obligatoire
    price DECIMAL(10,2) CHECK (price > 0),   -- Doit être positif (MySQL 8.0.16+)
    stock INT DEFAULT 0,                      -- Valeur par défaut
    code VARCHAR(50) UNIQUE                   -- Doit être unique
);
```

---

## Comprendre SELECT en Profondeur

### Anatomie Complète d'une Requête SELECT

```sql
SELECT colonnes              -- 5. Ce qu'on affiche
FROM table                   -- 1. Source des données
WHERE conditions             -- 2. Filtrer les lignes
GROUP BY colonnes            -- 3. Regrouper
HAVING conditions            -- 4. Filtrer les groupes
ORDER BY colonnes            -- 6. Trier
LIMIT nombre;                -- 7. Limiter les résultats
```

**CRITIQUE : L'ordre d'exécution ≠ l'ordre d'écriture !**

SQL exécute réellement dans cet ordre :

1. **FROM** : récupère les données
2. **WHERE** : filtre les lignes individuelles
3. **GROUP BY** : regroupe les lignes
4. **HAVING** : filtre les groupes
5. **SELECT** : sélectionne les colonnes
6. **ORDER BY** : trie les résultats
7. **LIMIT** : limite le nombre de résultats

### SELECT - Les Bases

```sql
-- ❌ Éviter en production (lourd, peu clair)
SELECT * FROM customers;

-- ✅ Toujours sélectionner explicitement
SELECT id, first_name, last_name, email FROM customers;
```

**DISTINCT - Éliminer les doublons**

```sql
-- Chaque ville une seule fois
SELECT DISTINCT city FROM customers;

-- DISTINCT sur plusieurs colonnes
SELECT DISTINCT city, country FROM customers;
```

**Alias - Renommer pour la clarté**

```sql
-- Alias de colonnes
SELECT
    first_name AS customer_first_name,
    price * quantity AS total
FROM orders;

-- Alias de tables (essentiel pour les jointures)
SELECT c.first_name, o.amount
FROM customers AS c
JOIN orders AS o ON c.id = o.customer_id;
```

### WHERE - Filtrer les Données

**Opérateurs de comparaison**

```sql
=       -- égal
<> ou !=  -- différent
>       -- plus grand
<       -- plus petit
>=      -- plus grand ou égal
<=      -- plus petit ou égal
```

**Opérateurs logiques - ATTENTION à la priorité !**

```sql
-- Priorité : NOT > AND > OR

-- Sans parenthèses (peut être ambigu)
WHERE price < 20 AND stock > 0 OR category = 'promotion'
-- Signifie : (price < 20 AND stock > 0) OR category = 'promotion'

-- ✅ Toujours utiliser des parenthèses pour clarifier
WHERE (price < 20 OR category = 'promotion') AND stock > 0
```

**NULL - Le cas particulier**

```sql
-- ❌ NE FONCTIONNE PAS
SELECT * FROM customers WHERE phone_number = NULL;

-- ✅ CORRECT
SELECT * FROM customers WHERE phone_number IS NULL;
SELECT * FROM customers WHERE phone_number IS NOT NULL;

-- NULL dans les calculs : tout devient NULL
SELECT price * quantity FROM orders;
-- Si price OU quantity est NULL → résultat NULL
```

**LIKE - Recherche de motifs**

```sql
-- % : n'importe quel nombre de caractères (0 ou plus)
SELECT * FROM customers WHERE last_name LIKE 'Smi%';     -- commence par "Smi"
SELECT * FROM customers WHERE last_name LIKE '%son';     -- finit par "son"
SELECT * FROM customers WHERE last_name LIKE '%and%';    -- contient "and"

-- _ : exactement UN caractère
SELECT * FROM customers WHERE code LIKE 'A_3';           -- A + 1 caractère + 3
```

**IN - Liste de valeurs**

```sql
-- Plus lisible que plusieurs OR
SELECT * FROM customers
WHERE city IN ('Paris', 'London', 'Berlin');

-- Équivalent à :
WHERE city = 'Paris' OR city = 'London' OR city = 'Berlin'
```

**BETWEEN - Intervalle (inclusif)**

```sql
-- Inclut les bornes (10 et 50)
SELECT * FROM products WHERE price BETWEEN 10 AND 50;

-- Équivalent à :
WHERE price >= 10 AND price <= 50

-- Fonctionne aussi avec les dates
WHERE order_date BETWEEN '2024-01-01' AND '2024-12-31'
```

### HAVING vs WHERE - La Différence Cruciale

- **WHERE** : filtre les **lignes** AVANT le regroupement
- **HAVING** : filtre les **groupes** APRÈS le regroupement

```sql
-- WHERE : filtrer les commandes > 100€ puis compter
SELECT customer_id, COUNT(*) as order_count
FROM orders
WHERE amount > 100
GROUP BY customer_id;

-- HAVING : compter TOUTES les commandes puis garder clients avec > 5 commandes
SELECT customer_id, COUNT(*) as order_count
FROM orders
GROUP BY customer_id
HAVING COUNT(*) > 5;

-- Combinaison : commandes > 100€ ET clients avec > 5 de ces commandes
SELECT customer_id, COUNT(*) as order_count
FROM orders
WHERE amount > 100
GROUP BY customer_id
HAVING COUNT(*) > 5;
```

### ORDER BY - Trier les Résultats

```sql
-- Ordre croissant (par défaut)
SELECT * FROM products ORDER BY price;
SELECT * FROM products ORDER BY price ASC;

-- Ordre décroissant
SELECT * FROM products ORDER BY price DESC;

-- Tri multiple
SELECT * FROM customers ORDER BY city ASC, last_name DESC;
```

### LIMIT - Limiter les Résultats

```sql
-- Les 10 premiers résultats
SELECT * FROM products ORDER BY price DESC LIMIT 10;

-- Avec OFFSET pour la pagination
SELECT * FROM products
ORDER BY id
LIMIT 10 OFFSET 20;  -- Sauter 20, prendre 10 (résultats 21-30)
```

---

## Les Commandes de Modification

### INSERT - Ajouter des Données

```sql
-- Insertion simple
INSERT INTO customers (first_name, last_name, email, city)
VALUES ('John', 'Smith', 'john.smith@email.com', 'London');

-- Insertion multiple (plus efficace)
INSERT INTO customers (first_name, last_name, email, city)
VALUES
    ('Emma', 'Johnson', 'emma.johnson@email.com', 'Paris'),
    ('Michael', 'Brown', 'michael.brown@email.com', 'Berlin'),
    ('Sarah', 'Davis', 'sarah.davis@email.com', 'Madrid');
```

### UPDATE - Modifier des Données

```sql
-- ⚠️ TOUJOURS avec WHERE (sinon TOUTE la table est modifiée !)
UPDATE customers
SET city = 'London'
WHERE id = 5;

-- Modifier plusieurs colonnes
UPDATE customers
SET city = 'Paris', postal_code = '75001'
WHERE id = 5;

-- Avec calcul
UPDATE products
SET price = price * 1.1
WHERE category = 'electronics';
```

### DELETE - Supprimer des Données

```sql
-- ⚠️ TOUJOURS avec WHERE (sinon TOUTE la table est supprimée !)
DELETE FROM customers WHERE id = 5;

-- Supprimer avec condition
DELETE FROM orders WHERE order_date < '2020-01-01';

-- ⚠️ DANGEREUX - supprime TOUT
DELETE FROM table_name;  -- À éviter !
```

---

## Les Jointures (JOIN)

Les jointures permettent de combiner des données de plusieurs tables.

### INNER JOIN - Données Communes Uniquement

Retourne uniquement les lignes ayant une correspondance dans **les deux** tables.

```sql
SELECT
    customers.first_name,
    customers.last_name,
    orders.amount,
    orders.order_date
FROM customers
INNER JOIN orders ON customers.id = orders.customer_id;

-- Avec alias (plus lisible)
SELECT c.first_name, c.last_name, o.amount, o.order_date
FROM customers c
INNER JOIN orders o ON c.id = o.customer_id;
```

### LEFT JOIN - Toutes les Données de Gauche

Retourne **toutes** les lignes de la table de gauche + correspondances de droite (NULL si pas de correspondance).

```sql
-- Tous les clients, avec leurs commandes s'ils en ont
SELECT c.first_name, c.last_name, o.amount
FROM customers c
LEFT JOIN orders o ON c.id = o.customer_id;

-- Trouver les clients SANS commande
SELECT c.first_name, c.last_name
FROM customers c
LEFT JOIN orders o ON c.id = o.customer_id
WHERE o.id IS NULL;
```

### RIGHT JOIN - Toutes les Données de Droite

Inverse du LEFT JOIN (moins utilisé, souvent remplaçable par LEFT JOIN en inversant l'ordre).

```sql
SELECT c.first_name, c.last_name, o.amount
FROM orders o
RIGHT JOIN customers c ON c.id = o.customer_id;
```

### FULL OUTER JOIN - Toutes les Données des Deux Côtés

Retourne toutes les lignes des deux tables (rare, pas supporté par tous les SGBD).

```sql
SELECT c.first_name, c.last_name, o.amount
FROM customers c
FULL OUTER JOIN orders o ON c.id = o.customer_id;
```

### Jointures Multiples

```sql
SELECT
    c.first_name AS customer_first_name,
    c.last_name AS customer_last_name,
    o.order_date,
    p.name AS product_name,
    oi.quantity
FROM customers c
INNER JOIN orders o ON c.id = o.customer_id
INNER JOIN order_items oi ON o.id = oi.order_id
INNER JOIN products p ON oi.product_id = p.id;
```

---

## Fonctions d'Agrégation et GROUP BY

### Fonctions d'Agrégation

```sql
-- Compter
SELECT COUNT(*) FROM customers;
SELECT COUNT(phone_number) FROM customers;  -- Ne compte pas les NULL

-- Somme
SELECT SUM(amount) FROM orders;

-- Moyenne
SELECT AVG(price) FROM products;

-- Min et Max
SELECT MIN(price), MAX(price) FROM products;
```

### GROUP BY - Regrouper les Données

```sql
-- Nombre de clients par ville
SELECT city, COUNT(*) as customer_count
FROM customers
GROUP BY city;

-- Total des ventes par client
SELECT customer_id, SUM(amount) as total_purchases
FROM orders
GROUP BY customer_id;

-- Avec tri
SELECT city, COUNT(*) as customer_count
FROM customers
GROUP BY city
ORDER BY customer_count DESC;

-- GROUP BY sur plusieurs colonnes
SELECT city, country, COUNT(*) as customer_count
FROM customers
GROUP BY city, country;
```

**Règle importante** : Toute colonne dans SELECT doit être soit :

- Dans GROUP BY
- Dans une fonction d'agrégation

```sql
-- ❌ ERREUR : 'first_name' n'est ni dans GROUP BY ni dans une agrégation
SELECT first_name, city, COUNT(*)
FROM customers
GROUP BY city;

-- ✅ CORRECT
SELECT city, COUNT(*) as customer_count
FROM customers
GROUP BY city;
```

---

## Sous-requêtes

Une sous-requête est une requête **à l'intérieur** d'une autre requête.

### Sous-requête dans WHERE

```sql
-- Clients qui ont passé au moins une commande
SELECT * FROM customers
WHERE id IN (SELECT customer_id FROM orders);

-- Produits plus chers que la moyenne
SELECT * FROM products
WHERE price > (SELECT AVG(price) FROM products);

-- Clients ayant dépensé plus de 1000€
SELECT first_name, last_name FROM customers
WHERE id IN (
    SELECT customer_id
    FROM orders
    GROUP BY customer_id
    HAVING SUM(amount) > 1000
);
```

### Sous-requête dans FROM

```sql
-- Moyenne des ventes moyennes par client
SELECT AVG(customer_average) as global_average
FROM (
    SELECT customer_id, AVG(amount) as customer_average
    FROM orders
    GROUP BY customer_id
) AS subquery;
```

### Sous-requête Corrélée

```sql
-- Produits plus chers que la moyenne de leur catégorie
SELECT p1.name, p1.price, p1.category
FROM products p1
WHERE p1.price > (
    SELECT AVG(p2.price)
    FROM products p2
    WHERE p2.category = p1.category
);
```

---

## CTE (Common Table Expressions)

Les CTE créent des "tables temporaires" qui n'existent que pour la durée de la requête. Plus lisibles que les sous-requêtes imbriquées.

### CTE Simple

```sql
-- Plus lisible qu'une sous-requête
WITH active_customers AS (
    SELECT id, first_name, last_name, email
    FROM customers
    WHERE last_login > '2024-01-01'
)
SELECT * FROM active_customers
WHERE email LIKE '%@gmail.com';

-- Multiple CTE
WITH
    sales_2024 AS (
        SELECT customer_id, SUM(amount) as total
        FROM orders
        WHERE YEAR(order_date) = 2024
        GROUP BY customer_id
    ),
    top_customers AS (
        SELECT customer_id, total
        FROM sales_2024
        WHERE total > 1000
    )
SELECT c.first_name, c.last_name, tc.total
FROM top_customers tc
JOIN customers c ON tc.customer_id = c.id;
```

### CTE Récursif

Utile pour les hiérarchies (organigrammes, catégories imbriquées, etc.).

```sql
WITH RECURSIVE employees_hierarchy AS (
    -- Cas de base : le PDG (pas de manager)
    SELECT id, first_name, last_name, manager_id, 1 as level
    FROM employees
    WHERE manager_id IS NULL

    UNION ALL

    -- Cas récursif : les subordonnés
    SELECT e.id, e.first_name, e.last_name, e.manager_id, eh.level + 1
    FROM employees e
    INNER JOIN employees_hierarchy eh ON e.manager_id = eh.id
)
SELECT * FROM employees_hierarchy
ORDER BY level, last_name;
```

---

## Calculs et Expressions

### Opérations Mathématiques

```sql
SELECT
    price,
    quantity,
    price * quantity AS total,
    price * 1.2 AS price_with_tax,
    ROUND(price * 0.9, 2) AS discounted_price
FROM orders;
```

### Fonctions sur Texte

```sql
-- Concaténation (syntaxe varie selon SGBD)
SELECT CONCAT(first_name, ' ', last_name) AS full_name FROM customers;
SELECT first_name || ' ' || last_name AS full_name FROM customers;  -- PostgreSQL/SQLite

-- Manipulations
SELECT
    UPPER(last_name) AS uppercase_name,
    LOWER(email) AS lowercase_email,
    LENGTH(description) AS description_length,
    TRIM(first_name) AS trimmed_name,
    SUBSTRING(code, 1, 3) AS prefix
FROM products;
```

### Fonctions sur Dates

```sql
-- Extraire des parties de date (syntaxe universelle)
SELECT
    order_date,
    YEAR(order_date) AS year,
    MONTH(order_date) AS month,
    DAY(order_date) AS day,
    CURRENT_DATE AS today
FROM orders;

-- Ajouter des jours (syntaxe varie selon SGBD)
-- PostgreSQL / MySQL :
SELECT order_date + INTERVAL '7 days' AS delivery_date FROM orders;

-- SQL Server :
SELECT DATEADD(day, 7, order_date) AS delivery_date FROM orders;

-- Oracle :
SELECT order_date + 7 AS delivery_date FROM orders;
```

### CASE - Logique Conditionnelle

```sql
-- CASE simple
SELECT
    name,
    price,
    CASE
        WHEN price < 10 THEN 'Cheap'
        WHEN price < 50 THEN 'Medium'
        ELSE 'Expensive'
    END AS price_category
FROM products;

-- CASE pour créer des colonnes calculées
SELECT
    name,
    stock,
    CASE
        WHEN stock = 0 THEN 'Out of stock'
        WHEN stock < 10 THEN 'Low stock'
        ELSE 'Available'
    END AS status
FROM products;
```

---

## Index - Notions de Base

### Qu'est-ce qu'un Index ?

Un **index** est une structure de données qui accélère les recherches dans une table, comme l'index d'un livre permet de trouver rapidement un sujet.

**Analogie** : Sans index, SQL doit lire toutes les lignes (comme lire un livre page par page). Avec un index, SQL va directement à la bonne ligne (comme utiliser l'index d'un livre).

**Quand les index sont utiles ?**

- Colonnes souvent utilisées dans `WHERE`
- Colonnes dans les `JOIN`
- Colonnes dans `ORDER BY`

**Avantages et inconvénients**

✅ **Avantages** :

- Accélère les `SELECT` (recherches)
- Améliore les performances des requêtes

❌ **Inconvénients** :

- Ralentit les `INSERT`, `UPDATE`, `DELETE`
- Prend de l'espace disque supplémentaire

**Règle importante** : Ne créez pas trop d'index ! Créez uniquement ceux dont vous avez vraiment besoin.

> **Note** : La création et gestion avancée des index sera vue dans la formation T-SQL avancée.

---

## Transactions et ACID

### Qu'est-ce qu'une Transaction ?

Une transaction est un ensemble d'opérations SQL qui doivent être exécutées comme une **unité atomique**.

```sql
-- Commencer une transaction
BEGIN TRANSACTION;
-- ou simplement : BEGIN;

-- Opérations
UPDATE accounts SET balance = balance - 100 WHERE id = 1;
UPDATE accounts SET balance = balance + 100 WHERE id = 2;

-- Valider les changements
COMMIT;

-- OU annuler tout en cas d'erreur
-- ROLLBACK;
```

### Propriétés ACID

Les transactions garantissent 4 propriétés fondamentales :

**A - Atomicité**

- Tout ou rien
- Si une opération échoue, tout est annulé

**C - Cohérence**

- Les données passent d'un état valide à un autre état valide
- Les contraintes sont respectées

**I - Isolation**

- Les transactions concurrentes n'interfèrent pas entre elles
- Une transaction ne voit pas les modifications non validées d'une autre

**D - Durabilité**

- Une fois validée (COMMIT), la transaction est permanente
- Même en cas de panne système

---

## Normalisation des Bases de Données

La normalisation évite la redondance et garantit l'intégrité des données.

### Première Forme Normale (1NF)

**Règle** : Chaque colonne contient des valeurs **atomiques** (indivisibles).

```sql
-- ❌ Pas en 1NF : plusieurs produits dans une colonne
CREATE TABLE orders (
    id INT PRIMARY KEY,
    products VARCHAR(200)  -- "Product1, Product2, Product3"
);

-- ✅ En 1NF : une ligne par produit
CREATE TABLE order_items (
    order_id INT,
    product_id INT,
    quantity INT,
    PRIMARY KEY (order_id, product_id)
);
```

### Deuxième Forme Normale (2NF)

**Règle** : 1NF + pas de dépendance partielle (toutes les colonnes dépendent de **toute** la clé primaire).

### Troisième Forme Normale (3NF)

**Règle** : 2NF + pas de dépendance transitive (pas de colonne dépendant d'une colonne non-clé).

```sql
-- ❌ Pas en 3NF : postal_code dépend de city
CREATE TABLE customers (
    id INT PRIMARY KEY,
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    city VARCHAR(100),
    postal_code VARCHAR(10)  -- Dépend de city, pas de id
);

-- ✅ En 3NF : séparer
CREATE TABLE customers (
    id INT PRIMARY KEY,
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    city_id INT,
    FOREIGN KEY (city_id) REFERENCES cities(id)
);

CREATE TABLE cities (
    id INT PRIMARY KEY,
    name VARCHAR(100),
    postal_code VARCHAR(10)
);
```

---

## Vues (Views)

Une vue est une "table virtuelle" basée sur une requête.

```sql
-- Créer une vue
CREATE VIEW active_customers AS
SELECT id, first_name, last_name, email, city
FROM customers
WHERE status = 'active'
  AND last_login > CURRENT_DATE - INTERVAL '30 days';

-- Utiliser la vue comme une table normale
SELECT * FROM active_customers WHERE city = 'Paris';

-- Supprimer une vue
DROP VIEW active_customers;
```

**Avantages des vues :**

- Simplifie l'accès aux données complexes
- Masque la complexité des jointures
- Sécurité : limiter l'accès aux colonnes sensibles
- Réutilisabilité

---

## Questions Types d'Entretien

### Questions Théoriques

**1. Quelle est la différence entre WHERE et HAVING ?**

<details>
<summary>Voir la réponse</summary>

- **WHERE** filtre les lignes individuelles **AVANT** le regroupement
- **HAVING** filtre les groupes **APRÈS** le regroupement
- HAVING s'utilise avec GROUP BY

```sql
-- WHERE : filtre avant GROUP BY
SELECT City, COUNT(*) as UserCount
FROM Users
WHERE Age > 18
GROUP BY City;

-- HAVING : filtre après GROUP BY
SELECT City, COUNT(*) as UserCount
FROM Users
GROUP BY City
HAVING COUNT(*) > 10;
```

</details>

**2. Expliquez les propriétés ACID**

<details>
<summary>Voir la réponse</summary>

Les propriétés **ACID** garantissent la fiabilité des transactions :

- **A**tomicité : Tout ou rien - Une transaction s'exécute complètement ou pas du tout
- **C**ohérence : Les données passent d'un état valide à un autre état valide
- **I**solation : Les transactions concurrentes n'interfèrent pas entre elles
- **D**urabilité : Une fois validée (COMMIT), la transaction est permanente même en cas de panne

```sql
BEGIN TRANSACTION;
-- Opérations
UPDATE Accounts SET Balance = Balance - 100 WHERE Id = 1;
UPDATE Accounts SET Balance = Balance + 100 WHERE Id = 2;
COMMIT; -- Garantit ACID
```

</details>

**3. Différence entre DELETE, TRUNCATE et DROP ?**

<details>
<summary>Voir la réponse</summary>

| Commande | Action | WHERE possible | Transactionnel | Vitesse |
|----------|--------|----------------|----------------|---------|
| **DELETE** | Supprime des lignes | Oui | Oui | Lent |
| **TRUNCATE** | Vide la table | Non | Limité | Rapide |
| **DROP** | Supprime la table | Non | Oui | Rapide |

```sql
-- DELETE : supprime des lignes spécifiques
DELETE FROM Users WHERE Age < 18;

-- TRUNCATE : vide toute la table, réinitialise IDENTITY
TRUNCATE TABLE Users;

-- DROP : supprime la table complète (structure + données)
DROP TABLE Users;
```

**Note :** TRUNCATE réinitialise les compteurs auto-incrémentés (IDENTITY).

</details>

**4. Différence entre INNER JOIN et LEFT JOIN ?**

<details>
<summary>Voir la réponse</summary>

- **INNER JOIN** : Retourne uniquement les lignes ayant une correspondance dans les **DEUX** tables
- **LEFT JOIN** : Retourne **TOUTES** les lignes de la table de gauche + correspondances (NULL si pas de match à droite)

```sql
-- INNER JOIN : seulement les utilisateurs avec commandes
SELECT u.Name, o.OrderId
FROM Users u
INNER JOIN Orders o ON u.Id = o.UserId;

-- LEFT JOIN : tous les utilisateurs, même sans commande
SELECT u.Name, o.OrderId
FROM Users u
LEFT JOIN Orders o ON u.Id = o.UserId;
```

</details>

**5. Qu'est-ce qu'un index ? Avantages et inconvénients ?**

<details>
<summary>Voir la réponse</summary>

Un **index** est une structure de données qui accélère les recherches dans une table.

**Avantages :**
- ✅ Accélère considérablement les SELECT, WHERE, JOIN, ORDER BY
- ✅ Améliore les performances des requêtes

**Inconvénients :**
- ❌ Ralentit les INSERT, UPDATE, DELETE (l'index doit être mis à jour)
- ❌ Prend de l'espace disque supplémentaire

```sql
-- Créer un index
CREATE INDEX idx_email ON Users(Email);

-- Utiliser l'index (automatique)
SELECT * FROM Users WHERE Email = 'user@example.com';
```

**Conseil :** Créez des index uniquement sur les colonnes fréquemment utilisées dans les filtres.

</details>

**6. Différence entre COUNT(\*) et COUNT(colonne) ?**

<details>
<summary>Voir la réponse</summary>

- **COUNT(\*)** : Compte **toutes** les lignes, y compris celles avec des NULL
- **COUNT(colonne)** : Compte uniquement les valeurs **non-NULL** de la colonne

```sql
-- Table Users avec 100 lignes, dont 20 ont PhoneNumber NULL

SELECT COUNT(*) FROM Users;           -- Résultat : 100
SELECT COUNT(PhoneNumber) FROM Users; -- Résultat : 80 (exclut les NULL)
```

</details>

### Exercices Pratiques

Testez vos connaissances avec ces exercices. Essayez de les résoudre avant de regarder les solutions !

---

**Exercice 1 : Trouver les doublons**

**Énoncé** : Vous avez une table `clients` avec une colonne `email`. Trouvez tous les emails qui apparaissent plus d'une fois dans la table, avec le nombre d'occurrences.

**Ce qu'on attend** : Une liste des emails en doublon avec leur nombre d'apparitions.

<details>
<summary>📌 <b>Indices</b></summary>

- Utilisez `GROUP BY` sur la colonne email
- Utilisez `HAVING` pour filtrer les groupes
- `COUNT(*)` compte le nombre de lignes dans chaque groupe

</details>

<details>
<summary><b>✅ Solution</b></summary>

```sql
SELECT email, COUNT(*) as occurrence_count
FROM customers
GROUP BY email
HAVING COUNT(*) > 1;
```

**Explication** :

- `GROUP BY email` regroupe toutes les lignes ayant le même email
- `COUNT(*)` compte combien de fois chaque email apparaît
- `HAVING COUNT(*) > 1` ne garde que les emails qui apparaissent plus d'une fois

</details>

---

**Exercice 2 : Deuxième salaire le plus élevé**

**Énoncé** : Dans la table `employees`, trouvez le deuxième salaire le plus élevé (pas le maximum, mais le deuxième).

**Ce qu'on attend** : Une seule valeur : le deuxième plus haut salaire.

<details>
<summary>📌 <b>Indices</b></summary>

- Pensez aux sous-requêtes
- Ou utilisez `ORDER BY` avec `LIMIT` et `OFFSET`
- Le salaire maximum se trouve avec `MAX(salary)`

</details>

<details>
<summary><b>✅ Solution (2 méthodes)</b></summary>

**Méthode 1 : Sous-requête**

```sql
SELECT MAX(salary)
FROM employees
WHERE salary < (SELECT MAX(salary) FROM employees);
```

**Explication** : On cherche le MAX des salaires qui sont inférieurs au salaire maximum.

---

**Méthode 2 : ORDER BY avec OFFSET**

```sql
SELECT DISTINCT salary
FROM employees
ORDER BY salary DESC
LIMIT 1 OFFSET 1;
```

**Explication** :

- `ORDER BY salary DESC` trie du plus grand au plus petit
- `LIMIT 1` prend une seule ligne
- `OFFSET 1` saute la première ligne (le maximum)
- `DISTINCT` élimine les doublons si plusieurs personnes ont le même salaire

</details>

---

**Exercice 3 : Clients sans commande**

**Énoncé** : Vous avez deux tables : `customers` (id, first_name, last_name, email) et `orders` (id, customer_id, amount). Trouvez tous les clients qui n'ont jamais passé de commande.

**Ce qu'on attend** : La liste des clients sans aucune commande.

<details>
<summary>📌 <b>Indices</b></summary>

- Pensez aux jointures (LEFT JOIN)
- Ou utilisez une sous-requête avec `NOT EXISTS`
- Un client sans commande aura NULL dans la jointure

</details>

<details>
<summary><b>✅ Solution (2 méthodes)</b></summary>

**Méthode 1 : LEFT JOIN**

```sql
SELECT c.*
FROM customers c
LEFT JOIN orders o ON c.id = o.customer_id
WHERE o.id IS NULL;
```

**Explication** :

- `LEFT JOIN` retourne tous les clients, même sans commande
- Quand un client n'a pas de commande, `o.id` sera NULL
- `WHERE o.id IS NULL` ne garde que ces clients-là

---

**Méthode 2 : NOT EXISTS (plus performante)**

```sql
SELECT *
FROM customers c
WHERE NOT EXISTS (
    SELECT 1 FROM orders o WHERE o.customer_id = c.id
);
```

**Explication** :

- Pour chaque client, on vérifie s'il existe une commande
- `NOT EXISTS` retourne vrai si la sous-requête ne trouve rien
- Généralement plus rapide que LEFT JOIN sur de grandes tables

</details>

---

### Pièges Courants

**1. NULL dans les comparaisons**

```sql
-- ❌ Ne fonctionne pas
WHERE column = NULL

-- ✅ Correct
WHERE column IS NULL
```

**2. UNION vs UNION ALL**

```sql
-- UNION : élimine les doublons (plus lent)
SELECT last_name FROM customers
UNION
SELECT last_name FROM suppliers;

-- UNION ALL : garde tout (plus rapide si pas de doublons attendus)
SELECT last_name FROM customers
UNION ALL
SELECT last_name FROM suppliers;
```

**3. Ordre AND/OR sans parenthèses**

```sql
-- Peut donner des résultats inattendus
WHERE price < 20 OR category = 'promotion' AND stock > 0

-- ✅ Toujours clarifier avec des parenthèses
WHERE (price < 20 OR category = 'promotion') AND stock > 0
```

---

## Bonnes Pratiques

1. **Toujours WHERE avec UPDATE/DELETE** pour éviter les catastrophes
2. **Utiliser des alias explicites** pour la lisibilité
3. **Indenter les requêtes** pour faciliter la maintenance
4. **Sélectionner uniquement les colonnes nécessaires** (éviter SELECT \*)
5. **Utiliser les transactions** pour garantir la cohérence
6. **Normaliser les données** pour éviter la redondance
7. **Tester sur un échantillon** avant d'exécuter sur toute la base

> **Note sur la sécurité** : Pour apprendre à protéger vos bases de données contre les injections SQL et autres vulnérabilités, consultez le document dédié **[SQL-Security.md](./SQL-Security.md)**.

---

## Ressources

- Documentation officielle de votre SGBD (MySQL, PostgreSQL, etc.)
- [SQLZoo](https://sqlzoo.net/) - Tutoriels interactifs
- [LeetCode SQL](https://leetcode.com/problemset/database/) - Exercices pratiques
- [Mode Analytics SQL Tutorial](https://mode.com/sql-tutorial/)

---

**Conseil final** : La meilleure façon d'apprendre SQL est de **pratiquer régulièrement** avec de vraies données. Créez une base de données test et expérimentez !
