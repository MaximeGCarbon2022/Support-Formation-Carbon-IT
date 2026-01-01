# 🎯 MECA - Quiz des Capitales Européennes

## Introduction

Bienvenue dans ce MECA Next.js ! Vous allez créer une application de quiz interactive pour tester vos connaissances sur les capitales européennes.

**Prérequis** :

- ✅ Formation React complétée (composants, hooks, JSX)
- ✅ Notions de TypeScript
- 📚 **[NextJS-Fundamentals.md](./NextJS-Fundamentals.md)** (lecture obligatoire)

> 💡 **Important** : Ce MECA suppose que vous avez lu et compris le cours NextJS-Fundamentals.md.

## 🚀 Installation rapide

Un projet starter complet est fourni dans le dossier [quiz-capitales-starter](./quiz-capitales-starter/).

```bash
cd quiz-capitales-starter
npm install
npm run dev
```

Le projet est déjà configuré avec Next.js 15, React 19, Material UI v6 et les tests. Vous pouvez commencer directement l'exercice !

## 🎓 Objectifs pédagogiques

À la fin de ce MECA, vous serez capable de :

✅ Créer une application Next.js 15 complète avec App Router
✅ Distinguer et utiliser Server Components vs Client Components
✅ Gérer l'état local avec useState et useEffect
✅ Créer des API Routes et Server Actions
✅ Intégrer des APIs tierces (Open Trivia DB, OpenDataSoft)
✅ Optimiser les performances (cache, Server Actions)
✅ Tester vos composants React
✅ Déployer sur Vercel

## 📚 Concepts techniques couverts

- **App Router** : Navigation moderne Next.js 15
- **Server Components** : Rendu côté serveur pour les performances
- **Client Components** : Interactivité côté client
- **API Routes** : Création d'endpoints REST
- **Server Actions** : Actions serveur pour mutations
- **State Management** : useState, useEffect, custom hooks
- **Material UI** : Bibliothèque de composants React (non imposé)
- **Tests** : Testing Library pour composants React

## 📝 Parcours d'apprentissage progressif

Ce MECA est structuré en 5 étapes progressives. Chaque étape s'appuie sur la précédente. Prenez le temps de bien comprendre chaque concept avant de passer à l'étape suivante.

---

## Étape 1 : Setup et Page d'Accueil

### 🎯 Objectif

Créer une application Next.js 15 fonctionnelle avec une page d'accueil attrayante.

### 📖 Ce que vous allez apprendre

- Initialiser un projet Next.js 15 avec TypeScript
- Configurer Material UI v6
- Créer un Server Component (page.tsx)
- Utiliser le système de routing Next.js (App Router)

### ✅ Tâches

**1.1 - Initialiser le projet**

- Suivre les instructions du dossier [quiz-capitales-starter](./quiz-capitales-starter/) pour démarrer
- Les dépendances (Next.js 15, Material UI, TypeScript) sont déjà installées
- Vérifier que le serveur de développement démarre correctement avec `npm run dev`

**1.2 - Créer la page d'accueil (`app/page.tsx`)**

- Créer un Server Component avec :
  - Un titre accrocheur "Quiz des Capitales Européennes"
  - Une description du quiz
  - Un bouton "Commencer le Quiz" stylisé avec Material UI
- Utiliser les composants Material UI : `Box`, `Typography`, `Button`

**1.3 - Ajouter la navigation**

- Utiliser le composant `<Link>` de Next.js pour naviguer vers `/quiz`
- Comprendre la différence entre `<Link>` et `<a>`

**1.4 - Styling**

- Ajouter une image de fond depuis `/public`
- Centrer le contenu avec Flexbox (Material UI `Box`)
- Rendre la page responsive

<details>
<summary>📌 <b>Indices</b></summary>

- Le Server Component par défaut n'a PAS besoin de `'use client'`
- Material UI nécessite un ThemeProvider dans `layout.tsx`
- Utilisez `next/link` pour la navigation, pas `next/router`

</details>

**🎓 Concepts clés** :

- **Server Component** : Rendu côté serveur, pas d'interactivité, performances optimales
- **App Router** : Nouveau système de routing de Next.js 15 basé sur les dossiers

---

## Étape 2 : Page Quiz avec Données Statiques

### 🎯 Objectif

Afficher des questions de quiz avec des données en dur et gérer l'état local.

### 📖 Ce que vous allez apprendre

- Créer un Client Component avec `'use client'`
- Gérer l'état avec `useState`
- Afficher dynamiquement des données
- Valider les réponses utilisateur

### ✅ Tâches

**2.1 - Créer la route `/quiz`**

- Créer le dossier `app/quiz/`
- Créer `app/quiz/page.tsx` comme Client Component (`'use client'` en première ligne)
- Importer les questions depuis `QUESTIONS.json` (fourni)

**2.2 - Afficher une question**

- Créer un état pour :
  - La question actuelle (`currentQuestionIndex`)
  - Le score (`score`)
  - La réponse sélectionnée (`selectedAnswer`)
- Afficher la question et les choix de réponses sous forme de boutons Material UI

**2.3 - Valider la réponse**

- Au clic sur un bouton, vérifier si la réponse est correcte
- Afficher un feedback visuel (vert si correct, rouge si incorrect)
- Incrémenter le score si la réponse est correcte
- Afficher la bonne réponse en cas d'erreur

**2.4 - Navigation entre questions**

- Ajouter un bouton "Question Suivante"
- Passer à la question suivante et réinitialiser la sélection
- Désactiver le bouton tant qu'aucune réponse n'est sélectionnée

<details>
<summary>📌 <b>Indices</b></summary>

- Utilisez `useState` pour gérer l'état local
- Pour le feedback visuel : changez la couleur du bouton avec `color="success"` ou `color="error"`
- Structure de données suggérée pour une question :
  ```typescript
  {
    question: "Quelle est la capitale de la France ?",
    choices: ["Paris", "Londres", "Berlin", "Madrid"],
    correctAnswer: "Paris"
  }
  ```

</details>

**🎓 Concepts clés** :

- **Client Component** : Permet l'interactivité (useState, useEffect, événements)
- **useState** : Hook pour gérer l'état local d'un composant
- **Rendu conditionnel** : Afficher différents contenus selon l'état

---

## Étape 3 : Page de Résultats et Recommencer

### 🎯 Objectif

Afficher le score final et permettre de recommencer le quiz.

### 📖 Ce que vous allez apprendre

- Gérer la fin d'un flux utilisateur
- Utiliser `useRouter` pour la navigation programmatique
- Créer des composants réutilisables

### ✅ Tâches

**3.1 - Détecter la fin du quiz**

- Vérifier si `currentQuestionIndex` a atteint la dernière question
- Afficher un écran de résultats au lieu d'une question

**3.2 - Créer l'écran de résultats**

- Afficher le score final : "Vous avez obtenu X/10 !"
- Afficher un message personnalisé selon le score :
  - < 5 : "Continuez à apprendre !"
  - 5-7 : "Pas mal ! Encore un effort."
  - 8-10 : "Excellent ! Vous maîtrisez bien !"
- Styliser avec Material UI (`Card`, `Typography`)

**3.3 - Bouton "Recommencer"**

- Ajouter un bouton pour recommencer le quiz
- Utiliser `useRouter` de `next/navigation` pour recharger la page
- Alternative : réinitialiser tous les états manuellement

**3.4 - Bonus : Afficher un récapitulatif**

- Pour chaque question, afficher si la réponse était correcte ou non
- Utiliser un accordéon Material UI pour ne pas surcharger l'écran

<details>
<summary>📌 <b>Indices</b></summary>

- Pour recharger la page : `router.refresh()` ou `window.location.reload()`
- Pour réinitialiser l'état : créer une fonction `resetQuiz()` qui remet tout à zéro
- Utilisez `Accordion` de Material UI pour le récapitulatif

</details>

**🎓 Concepts clés** :

- **useRouter** : Hook pour la navigation programmatique Next.js
- **Logique conditionnelle** : Afficher différents composants selon l'état
- **Composants réutilisables** : Extraire la logique commune

---

## Étape 4 : API Routes et Server Actions

### 🎯 Objectif

Créer une API pour servir les questions et comprendre les Server Actions.

### 📖 Ce que vous allez apprendre

- Créer une API Route Next.js
- Faire un fetch depuis un Client Component
- Créer et utiliser des Server Actions
- Gérer les erreurs réseau

### ✅ Tâches

**4.1 - Créer une API Route**

- Créer `app/api/questions/route.ts`
- Exporter une fonction `GET` qui retourne les questions en JSON
- Tester l'endpoint avec le navigateur : `http://localhost:3000/api/questions`

**4.2 - Fetch depuis le Client Component**

- Dans `app/quiz/page.tsx`, utiliser `useEffect` pour fetch les questions au montage
- Gérer les états de chargement (`loading`) et d'erreur (`error`)
- Afficher un spinner Material UI pendant le chargement

**4.3 - Créer une Server Action (optionnel mais recommandé)**

- Créer `app/actions/quiz.ts` avec une fonction `getQuestions()`
- Marquer la fonction avec `'use server'`
- Remplacer le fetch par un appel à cette Server Action
- Comprendre les avantages (pas d'exposition d'endpoint, plus sécurisé)

**4.4 - Gestion d'erreurs**

- Utiliser `try...catch` pour gérer les erreurs
- Afficher un message d'erreur convivial si le fetch échoue
- Ajouter un bouton "Réessayer"

<details>
<summary>📌 <b>Indices</b></summary>

- Structure d'une API Route :

  ```typescript
  export async function GET() {
    return Response.json({ questions: [...] })
  }
  ```

- Fetch dans useEffect avec async/await :

  ```typescript
  useEffect(() => {
    const fetchQuestions = async () => {
      try {
        const res = await fetch("/api/questions");
        if (!res.ok) throw new Error("Erreur de chargement");
        const data = await res.json();
        setQuestions(data.questions);
      } catch (error) {
        console.error(error);
      }
    };
    fetchQuestions();
  }, []);
  ```

- Server Action :
  ```typescript
  "use server";
  export async function getQuestions() {
    return questions;
  }
  ```

</details>

**🎓 Concepts clés** :

- **API Routes** : Endpoints REST dans Next.js
- **useEffect** : Hook pour les effets de bord (fetch, timers, etc.)
- **Server Actions** : Fonctions serveur appelables depuis le client
- **Async/await** : Gestion des opérations asynchrones

---

## Étape 5 : Features Avancées

### 🎯 Objectif

Ajouter des fonctionnalités avancées pour enrichir l'expérience utilisateur.

### 📖 Ce que vous allez apprendre

- Gérer des timers avec useEffect
- Intégrer des APIs tierces (Open Trivia DB, OpenDataSoft)
- Optimiser les performances avec le cache
- Créer un custom hook

### ✅ Tâches

**5.1 - Tests**

- Installer `@testing-library/react` et `jest`
- Écrire un test pour le custom hook `useQuiz`
- Écrire un test pour un composant de question
- Vérifier que le score s'incrémente correctement
- S'assurer que les composants se comportent correctement

**5.2 - Mode Défi avec Timer**

- Ajouter un état `timeLeft` (ex: 15 secondes par question)
- Utiliser `useEffect` pour décrémenter le timer chaque seconde
- Afficher une barre de progression Material UI (`LinearProgress`)
- Passer automatiquement à la question suivante si le temps est écoulé
- Nettoyer le timer dans le cleanup de useEffect

**5.3 - Intégration Open Trivia DB**

- Créer une Server Action pour fetch depuis Open Trivia DB
- Transformer les données au format de votre application
- Sauvegarder les questions dans un fichier JSON pour cache
- Ajouter un bouton "Nouvelles Questions" sur la page d'accueil

**5.4 - Affichage de la population**

- Lors d'une bonne réponse, fetch la population depuis OpenDataSoft API
- Afficher l'info dans une carte Material UI
- Gérer le cas où la ville n'est pas trouvée

**5.5 - Custom Hook `useQuiz`**

- Extraire toute la logique du quiz dans un custom hook
- Le hook doit exposer :
  - `currentQuestion`, `score`, `isFinished`
  - `selectAnswer()`, `nextQuestion()`, `resetQuiz()`
- Rendre le composant page.tsx plus propre et lisible

<details>
<summary>📌 <b>Indices</b></summary>

- Timer avec useEffect :

  ```typescript
  useEffect(() => {
    const timer = setInterval(() => {
      setTimeLeft((t) => t - 1);
    }, 1000);

    return () => clearInterval(timer); // Cleanup !
  }, [currentQuestionIndex]);
  ```

- Open Trivia DB endpoint :

  ```
  https://opentdb.com/api.php?amount=10&category=22&type=multiple
  ```

- Custom Hook :
  ```typescript
  function useQuiz(initialQuestions) {
    const [score, setScore] = useState(0);
    // ... toute la logique
    return { score, selectAnswer, nextQuestion };
  }
  ```

</details>

**🎓 Concepts clés** :

- **useEffect cleanup** : Nettoyer les timers/listeners pour éviter les memory leaks
- **Custom Hooks** : Réutiliser la logique entre composants
- **API Integration** : Transformer et adapter des données tierces
- **Caching** : Optimiser les performances en évitant les appels répétés

---

## Étape Finale : Déploiement

### 🎯 Objectif

Déployer votre application sur Vercel et la rendre accessible au monde entier.

### ✅ Tâches

1. Créer un compte Vercel (gratuit)
2. Connecter votre repository GitHub
3. Déployer en un clic
4. Partager le lien avec vos collègues !

**Guide** : [Documentation Vercel](https://vercel.com/docs/deployments/overview)

---

## 💡 Conseils Généraux

### 🎯 Approche recommandée

1. **Suivez l'ordre des étapes** : Chaque étape s'appuie sur la précédente
2. **Testez régulièrement** : Vérifiez que tout fonctionne avant de passer à l'étape suivante
3. **Lisez la documentation** : Next.js et Material UI ont d'excellentes docs
4. **Consultez les indices** : Ne restez pas bloqué, utilisez les `<details>` !
5. **Expérimentez** : Modifiez le code, cassez des choses, apprenez !

### 📚 Ressources Utiles

**Documentation officielle** :

- [Next.js 15 Documentation](https://nextjs.org/docs/15/app/getting-started)
- [React 19 Documentation](https://react.dev/)
- [Material UI v6](https://v6.mui.com/material-ui/getting-started/?_gl=1*1vn4iq0*_ga*MTk4OTA2NjE2My4xNzUzODIwMjUw*_ga_5NXDQLC2ZK*czE3NjI4Njk3MTIkbzEwJGcxJHQxNzYyODY5NzE0JGo1OCRsMCRoMA..)

**Concepts clés** :

- [Server Components vs Client Components](https://nextjs.org/docs/15/app/getting-started/server-and-client-components)
- [API Routes](https://nextjs.org/docs/15/app/api-reference/file-conventions/route)
- [Server Actions/Functions](https://nextjs.org/docs/15/app/getting-started/updating-data)

**Guides pratiques** :

- [useState Hook](https://react.dev/reference/react/useState)
- [useEffect Hook](https://react.dev/reference/react/useEffect)
- [Custom Hooks](https://react.dev/learn/reusing-logic-with-custom-hooks)

### 🤔 Questions fréquentes

**Q : Quand utiliser un Server Component vs Client Component ?**

<details>
<summary>Voir la réponse</summary>

- **Server Component** : Pas d'interactivité, fetch de données, SEO important
- **Client Component** : État local (useState), événements (onClick), hooks (useEffect)

```tsx
// Server Component - Fetch de données
export default async function PostsList() {
  const posts = await fetch("https://api.example.com/posts");
  return (
    <ul>
      {posts.map((p) => (
        <li>{p.title}</li>
      ))}
    </ul>
  );
}

// Client Component - Interactivité
'use client'
import { useState } from "react";
export default function Counter() {
  const [count, setCount] = useState(0);
  return <button onClick={() => setCount(count + 1)}>{count}</button>;
}
```

</details>

**Q : Comment déboguer mon code Next.js ?**

<details>
<summary>Voir la réponse</summary>

- Utilisez `console.log()` côté serveur et client
- Installez React DevTools pour inspecter les composants
- Vérifiez la console du navigateur pour les erreurs

```tsx
// Débogage Server Component
export default async function Page() {
  const data = await fetch("...");
  console.log("Server log:", data); // Visible dans terminal
  return <div>{data.title}</div>;
}

// Débogage Client Component
'use client'
export default function Component() {
  console.log("Client log"); // Visible dans console navigateur
  return <div>Test</div>;
}
```

</details>

**Q : Mon composant ne se met pas à jour, pourquoi ?**

<details>
<summary>Voir la réponse</summary>

- Vérifiez que vous appelez bien le setter de useState
- Assurez-vous que le composant est un Client Component (`'use client'`)
- Vérifiez les dépendances de useEffect

```tsx
'use client'
import { useState, useEffect } from "react";

export default function Component() {
  const [data, setData] = useState([]);

  useEffect(() => {
    fetch("/api/data")
      .then((r) => r.json())
      .then(setData); // ✅ Utilise le setter
  }, []); // ✅ Dépendances correctes

  return <div>{data.length}</div>;
}
```

</details>

**Q : Comment gérer les erreurs réseau ?**

<details>
<summary>Voir la réponse</summary>

- Utilisez `try...catch` avec async/await
- Gérez les états `loading` et `error` avec useState
- Affichez des messages conviviaux à l'utilisateur

```tsx
'use client'
import { useState, useEffect } from "react";

export default function DataList() {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function fetchData() {
      try {
        const res = await fetch("/api/data");
        if (!res.ok) throw new Error("Erreur de chargement");
        const result = await res.json();
        setData(result);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    }
    fetchData();
  }, []);

  if (loading) return <div>Chargement...</div>;
  if (error) return <div>Erreur: {error}</div>;
  return (
    <ul>
      {data.map((item) => (
        <li key={item.id}>{item.name}</li>
      ))}
    </ul>
  );
}
```

</details>

### 🎨 Conseils de Design

- **Cohérence** : Utilisez le même thème Material UI partout
- **Feedback visuel** : Indiquez clairement les réponses correctes/incorrectes
- **Responsive** : Testez sur mobile et desktop
- **Accessibilité** : Utilisez des boutons et labels appropriés
- **Loading states** : Toujours afficher un spinner pendant les fetch

## 📁 Structure du projet

Voici la structure recommandée pour ce projet :

```
quiz-capitales/
├── app/
│   ├── page.tsx                    # Page d'accueil (Server Component)
│   ├── layout.tsx                  # Layout racine avec ThemeProvider
│   ├── globals.css                 # Styles globaux
│   │
│   ├── quiz/
│   │   └── page.tsx                # Page du quiz (Client Component)
│   │
│   ├── api/
│   │   └── questions/
│   │       └── route.ts            # API Route pour les questions
│   │
│   └── actions/
│       └── quiz.ts                 # Server Actions
│
├── components/                     # Composants réutilisables (optionnel)
│   ├── QuestionCard.tsx
│   ├── ResultsScreen.tsx
│   └── Timer.tsx
│
├── hooks/                          # Custom hooks (optionnel)
│   └── useQuiz.ts
│
├── lib/                            # Utilitaires et helpers
│   └── questions.ts                # Logique métier
│
├── public/
│   └── background.jpg              # Images statiques
│
├── data/
│   └── QUESTIONS.json              # Données des questions
│
├── __tests__/                      # Tests (optionnel)
│   ├── components/
│   └── hooks/
│
├── package.json
├── tsconfig.json
├── next.config.ts
└── README.md
```

**📝 Notes** :

- La structure `components/`, `hooks/`, `lib/` est optionnelle mais recommandée pour garder le code organisé
- Vous êtes libre d'adapter cette structure selon vos préférences
- L'important est de bien séparer Server Components, Client Components et logique métier

---

## 📝 Fichiers Complémentaires

Pour vous aider dans ce MECA, consultez ces ressources :

- **[quiz-capitales-starter/](./quiz-capitales-starter/)** : Projet de démarrage avec toutes les dépendances
- **`QUESTIONS.json`** : Données des questions déjà fournies dans le starter
- **Indices intégrés** : Chaque étape contient des sections `<details>` avec des indices pour vous débloquer

---

## 🎉 Conclusion

Félicitations d'avoir pris le temps de lire ce MECA ! Vous êtes maintenant prêt à créer votre application de quiz.

**Rappelez-vous** :

- ✅ Prenez votre temps, c'est un apprentissage, pas une course
- ✅ N'hésitez pas à expérimenter et à faire des erreurs
- ✅ Consultez la documentation quand vous êtes bloqué
- ✅ Demandez de l'aide à vos collègues (ou TO) si nécessaire
- ✅ Amusez-vous ! 🚀

**Prochaines étapes** :

1. Accédez au dossier [quiz-capitales-starter](./quiz-capitales-starter/)
2. Suivez les instructions d'installation dans le README du starter
3. Suivez les étapes du MECA une par une
4. Présentez votre solution finale

Bon courage et bon apprentissage ! 💪
