# 📝 Sujet – Library API

## 🎯 Objectif
Analyser une API REST de gestion de bibliothèque développée en .NET avec Entity Framework Core.

## 📋 Contexte
Une petite bibliothèque municipale souhaite digitaliser la gestion de ses livres et emprunts. Un développeur junior a créé une API REST fonctionnelle mais qui présente plusieurs problèmes de qualité.

**L'API fonctionne** mais nécessite une revue approfondie avant mise en production.

## 🔧 Fonctionnalités implémentées

### Books (Livres)
- `GET /api/books` - Liste tous les livres
- `GET /api/books/{id}` - Détails d'un livre
- `POST /api/books` - Ajouter un livre
- `DELETE /api/books/{id}` - Supprimer un livre

### Users (Utilisateurs)
- `GET /api/users/{id}` - Détails d'un utilisateur
- `POST /api/users` - Créer un utilisateur

### Borrowings (Emprunts)
- `POST /api/borrow` - Emprunter un livre
- `POST /api/return` - Retourner un livre
- `GET /api/borrow/check/{isbn}` - Vérifier disponibilité via API externe

## 🎯 Mission de code review

Analyser le code fourni et identifier :
- Les problèmes d'architecture et de structure
- Les anti-patterns .NET
- Les problèmes de nommage et lisibilité
- Les failles potentielles (sécurité, performance, robustesse)
- Les problèmes de maintenabilité

**Note** : Il n'y a pas de "solution parfaite" attendue. L'objectif est d'identifier les problèmes et de proposer des améliorations justifiées.

## 📁 Fichiers fournis
- `BookController.cs` - Gestion des livres
- `usercontroller.cs` - Gestion des utilisateurs
- `Borrow_Controller.cs` - Gestion des emprunts
- `Models.cs` - Modèles de données
- `Program.cs` - Configuration de l'application

## ⚠️ Contraintes
- L'API est fonctionnelle mais présente de nombreux problèmes
- Certains endpoints sont plus problématiques que d'autres
- Le code mélange bonnes et mauvaises pratiques volontairement
