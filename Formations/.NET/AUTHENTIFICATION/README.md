# Formation Authentification .NET

## 📚 Vue d'ensemble

Cette formation vous apprend l'authentification moderne en .NET, de la compréhension des mécanismes JWT jusqu'à l'utilisation d'Auth0 en production.

**Approche pédagogique :**

1. **Module 1** : Comprendre en implémentant soi-même
2. **Module 2** : Utiliser les outils professionnels

---

## 🗺️ Parcours de formation

### Module 1 : JWT - Les fondamentaux ⏱️

**Fichier :** [1-module-jwt-fondamentaux.md](1-module-jwt-fondamentaux.md)

**Objectif :** Comprendre comment fonctionne l'authentification JWT en l'implémentant soi-même.

**Ce que vous allez apprendre :**

- Qu'est-ce qu'un JWT (Header, Payload, Signature)
- Authentification vs Autorisation
- Implémenter Register/Login avec BCrypt
- Générer des JWT manuellement
- Protéger des endpoints avec [Authorize]
- Gérer les rôles (User, Admin)
- "Je ne vois que mes données" (filtrage par userId)

**Projet pratique :** TaskAPI avec authentification JWT custom

**Prérequis :** Module C# Fondamentaux ✅

---

### Module 2 : Auth0 - Production Ready 🚀

**Fichier :** [2-module-auth0-production.md](2-module-auth0-production.md)

**Objectif :** Utiliser Auth0 pour gérer l'authentification en production comme dans les entreprises.

**Ce que vous allez apprendre :**

- Pourquoi Auth0 en entreprise
- Setup Auth0 (tenant, API, users)
- Migration du projet Module 1 vers Auth0
- Access Token vs ID Token vs Refresh Token
- Refresh Token Rotation
- SSO (Single Sign-On) entre plusieurs apps
- OIDC (OpenID Connect)
- Actions Auth0 pour personnaliser les tokens
- Bonnes pratiques de sécurité

**Projet pratique :** Migration TaskAPI vers Auth0

**Prérequis :** Module 1 ✅

---

## 📊 Comparaison des modules

| Aspect                     | Module 1 (JWT Custom)             | Module 2 (Auth0)                  |
| -------------------------- | --------------------------------- | --------------------------------- |
| **Objectif**               | Apprendre les mécanismes          | Production ready                  |
| **Complexité**             | Moyenne                           | Simple (Auth0 gère tout)          |
| **Code à écrire**          | Beaucoup (AuthService, BCrypt...) | Peu (configuration)               |
| **Sécurité**               | Basique (vous gérez tout)         | Professionnelle (OWASP, RGPD)     |
| **2FA**                    | À implémenter soi-même            | Inclus                            |
| **SSO**                    | Complexe à implémenter            | Inclus et facile                  |
| **Refresh Tokens**         | À implémenter soi-même            | Géré automatiquement              |
| **Monitoring**             | À implémenter                     | Dashboard Auth0                   |
| **Quand utiliser ?**       | Apprentissage, POC                | Production, apps professionnelles |
| **Temps de mise en œuvre** | Long (plusieurs jours)            | Court (quelques heures)           |

---

## 🎯 Par où commencer ?

**Commencez par le Module 1** pour comprendre les bases :

- Comment fonctionne un JWT ?
- Pourquoi hasher les mots de passe ?
- Comment protéger des endpoints ?

**Puis passez au Module 2** pour découvrir les solutions professionnelles.

Si vous connaissez déjà JWT, vous pouvez directement passer au **Module 2** pour apprendre Auth0.

---

## 💡 Concepts clés

### Authentification vs Autorisation

**Authentification :** "Qui êtes-vous ?"

- Login avec email/password
- Vérification du JWT
- Prouver son identité

**Autorisation :** "Que pouvez-vous faire ?"

- Vérifier les rôles (Admin, User)
- Vérifier les permissions (read, write, delete)
- Filtrer les données ("Je vois que MES données")

### JWT (JSON Web Token)

**Structure :** `Header.Payload.Signature`

- **Header** : Algorithme (HS256, RS256)
- **Payload** : Claims (sub, email, role, exp)
- **Signature** : Garantit l'authenticité

**Visualiser :** [https://jwt.io/](https://jwt.io/)

### Auth0

**Solution SaaS d'authentification professionnelle :**

- Gestion des utilisateurs
- 2FA, SSO, passwordless
- Refresh tokens automatiques
- Conformité RGPD/OWASP
- Dashboard d'administration

---

## 📖 Ressources complémentaires

### Documentation

- [JWT Introduction](https://jwt.io/introduction)
- [ASP.NET Core Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/)
- [Auth0 Documentation](https://auth0.com/docs)

### Outils

- [JWT.io](https://jwt.io/) - Décoder et debugger vos JWT
- [Postman](https://www.postman.com/) - Tester vos APIs
- [Auth0 Dashboard](https://manage.auth0.com/) - Gérer vos utilisateurs

### Vidéos

- [Auth0 YouTube Channel](https://www.youtube.com/@auth0)
- [What is OAuth 2.0?](https://www.youtube.com/watch?v=996OiexHze0)

---

## ✅ Checklist de progression

### Module 1 : JWT - Les fondamentaux

- [ ] Comprendre la structure d'un JWT (Header, Payload, Signature)
- [ ] Implémenter Register avec BCrypt
- [ ] Implémenter Login et génération de JWT
- [ ] Protéger des endpoints avec [Authorize]
- [ ] Gérer les rôles (User, Admin)
- [ ] Filtrer les données par userId
- [ ] Tester avec Swagger/Postman
- [ ] Écrire des tests d'intégration

### Module 2 : Auth0 - Production

- [ ] Créer un compte Auth0 gratuit
- [ ] Configurer une API Auth0
- [ ] Migrer le projet vers Auth0
- [ ] Tester avec un token Auth0
- [ ] Ajouter un rôle custom au token (Action)
- [ ] Comprendre Access Token vs Refresh Token
- [ ] Configurer Refresh Token Rotation
- [ ] Tester le SSO (optionnel)

---

## 🎓 Pour aller plus loin

Après avoir complété ces modules, vous pouvez explorer :

- **ASP.NET Core Identity** : Solution Microsoft pour l'authentification
- **OAuth 2.0 avancé** : Flows complexes (PKCE, Device Flow)
- **Authorization Policies** : Permissions granulaires en .NET
- **Multi-tenancy** : Applications multi-clients
- **API Gateway** : Centraliser l'authentification (Azure API Management, Kong)

---

## 💬 Besoin d'aide ?

Si vous rencontrez des difficultés :

1. Relisez la section "Questions d'entretien" des modules
2. Consultez les ressources complémentaires
3. Testez vos tokens sur [JWT.io](https://jwt.io/)
4. Vérifiez les logs Auth0 (Dashboard → Monitoring → Logs)

---

**🎯 Bonne formation !**
