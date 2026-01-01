# Quiz des Capitales Européennes - Starter

Projet de démarrage pour l'exercice Next.js 15 + React 19 + Material UI.

## Installation

```bash
# Installer les dépendances
npm install
# ou
pnpm install
```

## Vérifier que tout fonctionne

Avant de commencer l'exercice, validez que le setup est correct :

```bash
# 1. Lancer les tests
npm test

# Si les 4 tests passent ✅, le projet est prêt !
```

## Lancer le serveur de développement

```bash
npm run dev
# ou
pnpm dev
```

Ouvrez [http://localhost:3000](http://localhost:3000) dans votre navigateur.

Vous verrez une page de test avec :
- ✅ Vérification du routing Next.js 15
- ✅ Composants Material UI v6
- ✅ Hooks React 19 (bouton interactif)
- ✅ TypeScript 5

## Technologies utilisées

- **Next.js 15** avec App Router
- **React 19** avec Server Components et Client Components
- **TypeScript 5** pour le typage statique
- **Material UI v6** pour les composants UI
- **Jest** + **React Testing Library** pour les tests

## Structure du projet

```
quiz-capitales-starter/
├── app/
│   ├── layout.tsx          # Layout principal avec ThemeProvider
│   ├── page.tsx            # Page de test du setup
│   ├── theme.ts            # Configuration du thème Material UI
│   └── globals.css         # Styles globaux
├── data/
│   └── QUESTIONS.json      # 20 questions sur les capitales européennes
├── __tests__/
│   └── setup.test.tsx      # Tests de validation du setup
├── public/                 # Fichiers statiques
├── package.json
├── tsconfig.json
├── next.config.ts
├── jest.config.js
└── jest.setup.js
```

## Commandes disponibles

- `npm test` - Lance les tests de validation du setup
- `npm run test:watch` - Lance les tests en mode watch
- `npm run dev` - Lance le serveur de développement
- `npm run build` - Compile l'application pour la production
- `npm run start` - Lance le serveur de production
- `npm run lint` - Vérifie la qualité du code avec ESLint

## Prochaines étapes

Une fois que les tests passent et que la page de test s'affiche correctement, consultez le fichier **[README.md](../README.md)** (dossier parent) pour les instructions détaillées de l'exercice.

Bon courage !
