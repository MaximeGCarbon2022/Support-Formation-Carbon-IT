# Mini-Projet Final T-SQL : Système de Réservation d'Hôtel

## 📋 Informations Générales

**Niveau** : Avancé
**Objectif** : Évaluer l'acquisition complète des concepts T-SQL
**Prérequis** : Avoir complété la formation T-SQL et l'exercice guidé

---

## 🎯 Contexte

Vous êtes développeur dans une société de conseil IT. Votre client, un hôtel 4 étoiles, souhaite moderniser son système de réservation. Vous devez concevoir et implémenter la base de données SQL Server avec les procédures stockées nécessaires.

**Contraintes métier** :

- L'hôtel dispose de 50 rooms de différents types
- Les réservations peuvent être effectuées en ligne ou par téléphone
- Le système doit gérer les annulations

---

## 📊 Structure de la Base de Données

### Tables à Créer

#### 1. Table `rooms`

Stocke les informations des rooms de l'hôtel.

**Colonnes attendues** :

- `id` : clé primaire (IDENTITY)
- `room_number` : numéro de chambre (unique, ex: "101", "205")
- `type` : type de chambre (STANDARD, DELUXE, SUITE)
- `price_per_night` : prix par nuit en euros
- `capacity` : nombre de personnes max
- `status` : état de la chambre (AVAILABLE, OCCUPIED, MAINTENANCE)
- `created_at` : date d'ajout de la chambre

**Contraintes** :

- Le numéro de chambre doit être unique
- Le type doit être l'une des valeurs : STANDARD, DELUXE, SUITE
- Le prix doit être positif
- Le status doit être : AVAILABLE, OCCUPIED, MAINTENANCE

---

#### 2. Table `customers`

Stocke les informations des customers.

**Colonnes attendues** :

- `id` : clé primaire (IDENTITY)
- `name` : name du client (obligatoire)
- `first_name` : prénom du client (obligatoire)
- `email` : email (unique, obligatoire)
- `phone_number` : numéro de téléphone
- `birth_date` : date de naissance (optionnel)
- `created_at` : date d'inscription

**Contraintes** :

- L'email doit être unique
- Le name et prénom sont obligatoires

---

#### 3. Table `reservations`

Stocke les réservations.

**Colonnes attendues** :

- `id` : clé primaire (IDENTITY)
- `customer_id` : référence vers la table customers
- `room_id` : référence vers la table rooms
- `start_date` : date de début de séjour
- `end_date` : date de fin de séjour
- `guest_count` : nombre de personnes
- `total_price` : prix total de la réservation
- `status` : status de la réservation (CONFIRMED, CANCELLED, COMPLETED)
- `reservation_date` : date de création de la réservation
- `comments` : comments optionnels

**Contraintes** :

- `end_date` doit être après `start_date`
- `guest_count` doit être positif
- Le status doit être : CONFIRMED, CANCELLED, COMPLETED
- Relations avec `customers` et `rooms`

---

## 🔧 Fonctionnalités à Implémenter

### 1. Procédure Stockée : `sp_CreateReservation`

**Objectif** : Créer une nouvelle réservation avec toutes les validations nécessaires.

**Paramètres** :

- `@customer_id INT`
- `@room_id INT`
- `@start_date DATE`
- `@end_date DATE`
- `@guest_count INT`
- `@comments NVARCHAR(500) = NULL` (optionnel)

**Logique métier** :

1. Valider que le client existe
2. Valider que la chambre existe et récupérer son prix
3. **Vérifier la disponibilité** de la chambre pour la période demandée :
   - La chambre ne doit pas avoir de réservation CONFIRMED qui chevauche ces dates
   - Formule de chevauchement : `(nouvelle_date_debut < existante_date_fin) AND (nouvelle_date_fin > existante_date_debut)`
4. Calculer le prix total : `(end_date - start_date) * price_per_night`
5. Créer la réservation avec le status CONFIRMED
6. Retourner l'ID de la réservation créée avec OUTPUT

**Gestion d'erreurs** :

- Utiliser TRY...CATCH
- Utiliser des transactions
- Retourner des messages d'erreur clairs

**Exemple d'utilisation attendue** :

```sql
EXEC sp_CreateReservation
    @customer_id = 1,
    @room_id = 5,
    @start_date = '2025-06-01',
    @end_date = '2025-06-05',
    @guest_count = 2;
```

---

### 2. Procédure Stockée : `sp_CancelReservation`

**Objectif** : Annuler une réservation existante.

**Paramètres** :

- `@reservation_id INT`
- `@motif NVARCHAR(200) = NULL` (optionnel)

**Logique métier** :

1. Vérifier que la réservation existe
2. Vérifier que le status actuel est CONFIRMED (on ne peut annuler que les réservations confirmées)
3. Mettre à jour le status à CANCELLED
4. Enregistrer le motif dans les comments
5. Retourner un message de confirmation

**Gestion d'erreurs** :

- TRY...CATCH avec transactions

---

### 3. Fonction Table : `fn_GetAvailableRooms`

**Objectif** : Retourner toutes les rooms disponibles pour une période donnée.

**Paramètres** :

- `@start_date DATE`
- `@end_date DATE`

**Retour** : Table avec les colonnes :

- `id`, `room_number`, `type`, `price_per_night`, `capacity`

**Logique** :

- Retourner les rooms qui n'ont PAS de réservation CONFIRMED chevauchant la période

**Exemple d'utilisation attendue** :

```sql
-- Toutes les rooms disponibles
SELECT * FROM dbo.fn_GetAvailableRooms('2025-06-01', '2025-06-05');
```

---

## 🧪 Tests à Effectuer

Une fois toutes les fonctionnalités implémentées, vous devez les tester :

### Jeu de Données Initial

```sql
-- Insérer des rooms
INSERT INTO rooms (room_number, type, price_per_night, capacity, status)
VALUES
    ('101', 'STANDARD', 80.00, 2, 'AVAILABLE'),
    ('102', 'STANDARD', 80.00, 2, 'AVAILABLE'),
    ('201', 'DELUXE', 120.00, 3, 'AVAILABLE'),
    ('202', 'DELUXE', 120.00, 3, 'AVAILABLE'),
    ('301', 'SUITE', 200.00, 4, 'AVAILABLE');

-- Insérer des customers
INSERT INTO customers (name, first_name, email, phone_number)
VALUES
    ('Dupont', 'Jean', 'jean.dupont@email.com', '0601020304'),
    ('Martin', 'Sophie', 'sophie.martin@email.com', '0612345678'),
    ('Bernard', 'Luc', 'luc.bernard@email.com', '0623456789');
```

### Scénarios de Test

**Test 1 : Créer une réservation valide**

```sql
EXEC sp_CreateReservation
    @customer_id = 1,
    @room_id = 1,
    @start_date = '2025-07-01',
    @end_date = '2025-07-05',
    @guest_count = 2;
```

**Résultat attendu** : Réservation créée avec succès, total_price = 320.00 (4 nuits \* 80€)

---

**Test 2 : Tentative de double réservation (doit échouer)**

```sql
EXEC sp_CreateReservation
    @customer_id = 2,
    @room_id = 1,
    @start_date = '2025-07-03',
    @end_date = '2025-07-07',
    @guest_count = 2;
```

**Résultat attendu** : Erreur "Chambre non disponible pour cette période"

---

**Test 3 : Annuler une réservation**

```sql
EXEC sp_CancelReservation
    @reservation_id = 1,
    @motif = 'Client a annulé par téléphone';
```

**Résultat attendu** : Réservation annulée

---

**Test 4 : Trouver les rooms disponibles**

```sql
SELECT * FROM dbo.fn_GetAvailableRooms('2025-07-01', '2025-07-05');
```

**Résultat attendu** : Toutes les rooms sauf celle réservée (si pas annulée)

---

## 📦 Livrables Attendus

1. **Script de création des tables** (`01_create_tables.sql`)
2. **Script des procédures stockées** (`02_procedures.sql`)
3. **Script des fonctions** (`03_functions.sql`)
4. **Script de tests complets** (`04_tests.sql`) avec le jeu de données et les scénarios

---

## 💡 Conseils

- ⏱️ **Gestion du temps** : Commencez par les tables, puis les procédures, puis la fonction
- 🧪 **Testez au fur et à mesure** : Ne passez pas à la fonctionnalité suivante sans avoir testé la précédente
- 📝 **Prenez des notes** : Si vous bloquez sur quelque chose, notez-le pour y revenir
- 🔍 **Relisez le cours** : N'hésitez pas à consulter les sections du T-SQL.md
- 🚫 **Pas de recherche externe** : Essayez de faire l'exercice uniquement avec le cours fourni

---

## 🚀 Bonus (Optionnel)

Si vous terminez avant 2h, ajoutez ces fonctionnalités supplémentaires :

### 1. Procédure `sp_CompleteReservation`

Marque une réservation comme terminée (check-out).

- Paramètre : `@reservation_id`
- Vérifie que la réservation est CONFIRMED
- Change le status à COMPLETED

### 2. Fonction Scalaire `fn_CalculateRevenue`

Calcule le chiffre d'affaires total pour une période.

- Paramètres : `@start_date`, `@end_date`
- Retourne : `DECIMAL(10,2)`
- Somme les `total_price` des réservations CONFIRMED ou COMPLETED

### 3. Table d'Audit `reservations_history`

Créer une table pour tracer les modifications :

- `id`, `reservation_id`, `action`, `old_status`, `new_status`, `action_date`, `utilisateur`, `details`

### 4. Trigger `trg_AuditReservations`

Tracer automatiquement toutes les modifications sur `reservations`.

- Type : AFTER INSERT, UPDATE, DELETE
- Enregistre dans `reservations_history`

### 5. MERGE : Synchronisation des Clients

Créer `customers_import` et un script MERGE pour synchroniser les customers depuis un CRM externe.

### 6. Vue `vw_ActiveReservations`

Liste les réservations en cours avec les infos client et chambre.

### 7. Index

Créez des index appropriés sur les colonnes fréquemment utilisées dans les WHERE/JOIN.

---

## 📞 Aide

**Si vous êtes bloqué** :

1. Relisez l'exercice guidé dans T-SQL.md
2. Consultez les sections pertinentes du cours
3. Vérifiez vos messages d'erreur SQL Server
4. Décomposez le problème en plus petites étapes

---

**Bon courage ! 🎓**
