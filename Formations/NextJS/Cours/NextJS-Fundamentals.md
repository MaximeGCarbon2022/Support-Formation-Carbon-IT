# Formation Next.js 15 - Les Fondamentaux

## Introduction

Next.js est un **framework React** développé par Vercel qui simplifie la création d'applications web modernes et performantes. Cette formation couvre les concepts fondamentaux de Next.js 15 avec l'App Router.

**Prérequis** :

- ✅ Maîtrise de React (composants, hooks, JSX)
- ✅ Notions de TypeScript
- ✅ Concepts HTTP/API

---

## Table des Matières

1. [Pourquoi Next.js ?](#pourquoi-nextjs)
2. [App Router - Le Nouveau Paradigme](#app-router---le-nouveau-paradigme)
3. [Server Components vs Client Components](#server-components-vs-client-components)
4. [Data Fetching](#data-fetching)
5. [Navigation et Routing](#navigation-et-routing)
6. [Layouts et Metadata](#layouts-et-metadata)
7. [Optimisations Next.js](#optimisations-nextjs)
8. [Déploiement sur Vercel](#déploiement-sur-vercel)
9. [Questions Types d'Entretien](#questions-types-dentretien)
10. [Bonnes Pratiques](#bonnes-pratiques)
11. [Ressources](#ressources)

---

## Pourquoi Next.js ?

### React pur vs Next.js

**React** est une bibliothèque pour créer des interfaces utilisateur. Pour une application complète, vous devez ajouter :

- Routing (React Router)
- SSR/SSG (configuration complexe)
- API Routes (serveur séparé)
- Optimisation images (plugin)
- SEO (configuration manuelle)
- Build optimization (Webpack/Vite)

**Next.js** est un framework qui inclut **tout ça par défaut** avec des conventions et optimisations intégrées.

### Les Avantages de Next.js

✅ **Performance** : Rendu côté serveur (SSR) et génération statique (SSG) par défaut
✅ **SEO** : Le HTML est généré côté serveur, Google peut l'indexer
✅ **Routing file-based** : Pas besoin de configurer les routes manuellement
✅ **API Routes** : Backend et frontend dans le même projet
✅ **Optimisations automatiques** : Images, fonts, code splitting
✅ **Developer Experience** : Hot reload, TypeScript, Fast Refresh
✅ **Déploiement facile** : Vercel en un clic

### Cas d'usage

**Utilisez Next.js pour** :

- Sites e-commerce (performance + SEO)
- Blogs et sites de contenu (SEO crucial)
- Dashboards d'entreprise (SSR pour données fraîches)
- Applications SaaS (tout-en-un)

**N'utilisez PAS Next.js pour** :

- Applications 100% client-side sans SEO (ex: admin interne)
- Projets très simples où React pur suffit
- Applications mobiles natives (utilisez React Native)

---

## App Router - Le Nouveau Paradigme

### Pages Router vs App Router

Next.js a deux systèmes de routing :

| Aspect            | Pages Router (ancien) | App Router (Next.js 13+) |
| ----------------- | --------------------- | ------------------------ |
| Dossier           | `pages/`              | `app/`                   |
| Fichier           | `pages/index.tsx`     | `app/page.tsx`           |
| Layouts           | Composant custom      | `layout.tsx` natif       |
| Server Components | ❌ Non                | ✅ Oui (par défaut)      |
| Recommandé        | ⚠️ Legacy             | ✅ Oui (Next.js 15)      |

**Dans cette formation, nous utilisons exclusivement l'App Router.**

### Structure de Base

```
app/
├── layout.tsx          # Layout racine (obligatoire)
├── page.tsx            # Page d'accueil (/)
├── globals.css         # Styles globaux
│
├── about/
│   └── page.tsx        # Route /about
│
├── blog/
│   ├── page.tsx        # Route /blog
│   └── [slug]/
│       └── page.tsx    # Route /blog/[slug] (dynamique)
│
└── api/
    └── users/
        └── route.ts    # API Route /api/users
```

### Convention de Nommage

| Fichier         | Rôle                         | URL                 |
| --------------- | ---------------------------- | ------------------- |
| `page.tsx`      | Page accessible publiquement | ✅ Crée une route   |
| `layout.tsx`    | Layout partagé               | ❌ Pas de route     |
| `loading.tsx`   | UI de chargement             | ❌ Pas de route     |
| `error.tsx`     | Gestion d'erreurs            | ❌ Pas de route     |
| `not-found.tsx` | Page 404                     | ❌ Pas de route     |
| `route.ts`      | API Route (backend)          | ✅ Crée un endpoint |

**Important** : Seuls `page.tsx` et `route.ts` créent des routes accessibles.

### Créer votre Première Page

**Structure** :

```
app/
├── layout.tsx          # Layout racine
└── page.tsx            # Page d'accueil
```

**`app/layout.tsx`** (obligatoire) :

```typescript
export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="fr">
      <body>{children}</body>
    </html>
  );
}
```

**`app/page.tsx`** :

```typescript
export default function Home() {
  return (
    <main>
      <h1>Bienvenue sur Next.js 15</h1>
      <p>Ceci est la page d'accueil</p>
    </main>
  );
}
```

**Résultat** : Accessible sur `http://localhost:3000`

### Routes Dynamiques

**Créer une route `/blog/[slug]`** :

```
app/
└── blog/
    ├── page.tsx              # /blog
    └── [slug]/
        └── page.tsx          # /blog/hello-world
```

**`app/blog/[slug]/page.tsx`** :

```typescript
export default function BlogPost({ params }: { params: { slug: string } }) {
  return (
    <article>
      <h1>Article : {params.slug}</h1>
    </article>
  );
}
```

**Exemples d'URLs** :

- `/blog/hello-world` → `params.slug = "hello-world"`
- `/blog/nextjs-tutorial` → `params.slug = "nextjs-tutorial"`

### Routes Catch-All (Optionnel)

**Attraper plusieurs segments** :

```
app/
└── docs/
    └── [...slug]/
        └── page.tsx          # /docs/getting-started/installation
```

```typescript
export default function Docs({ params }: { params: { slug: string[] } }) {
  // /docs/getting-started/installation
  // params.slug = ["getting-started", "installation"]
  return <h1>Docs: {params.slug.join("/")}</h1>;
}
```

---

## Server Components vs Client Components

> **LE concept le plus important de Next.js 15.**

### Le Changement de Paradigme

**Avant (React pur)** :

- Tout s'exécute côté client (navigateur)
- Le serveur envoie un HTML vide + du JavaScript
- React "hydrate" le HTML côté client

**Maintenant (Next.js 15)** :

- **Par défaut** : Tout est un Server Component (s'exécute sur le serveur)
- Vous ajoutez `'use client'` **uniquement** si vous avez besoin d'interactivité

### Server Components (par défaut)

**Un Server Component** :

- S'exécute sur le serveur (Node.js)
- Ne peut PAS utiliser useState, useEffect, événements (onClick, onChange)
- Peut fetch des données directement (async/await)
- Réduit le JavaScript envoyé au client

**Exemple** :

```typescript
// ✅ Server Component (pas besoin de 'use client')
export default async function UsersPage() {
  // ✅ Fetch direct, s'exécute sur le serveur
  const res = await fetch("https://api.example.com/users");
  const users = await res.json();

  return (
    <ul>
      {users.map((user) => (
        <li key={user.id}>{user.name}</li>
      ))}
    </ul>
  );
}
```

**Avantages** :

- ✅ Pas de JavaScript côté client pour le fetch
- ✅ Données fraîches à chaque requête
- ✅ Accès direct aux bases de données (si besoin)
- ✅ Meilleure performance (moins de JS à télécharger)

### Client Components

**Un Client Component** :

- S'exécute côté client (navigateur)
- Peut utiliser useState, useEffect, événements
- Marqué avec `'use client'` en première ligne

**Exemple** :

```typescript
"use client"; // ⚠️ Directive obligatoire

import { useState } from "react";

export default function Counter() {
  // ✅ useState fonctionne (Client Component)
  const [count, setCount] = useState(0);

  return (
    <div>
      <p>Count: {count}</p>
      <button onClick={() => setCount(count + 1)}>Increment</button>
    </div>
  );
}
```

### Quand utiliser quoi ?

| Besoin                | Type             | Directive         |
| --------------------- | ---------------- | ----------------- |
| Fetch de données      | Server Component | ❌ Aucune         |
| Affichage statique    | Server Component | ❌ Aucune         |
| useState, useEffect   | Client Component | ✅ `'use client'` |
| onClick, onChange     | Client Component | ✅ `'use client'` |
| Formulaires contrôlés | Client Component | ✅ `'use client'` |
| Animations            | Client Component | ✅ `'use client'` |

**Règle d'or** : Par défaut, **tout est Server Component**. Ajoutez `'use client'` uniquement si nécessaire.

### Composition : Le Pattern Recommandé

**❌ Mauvaise approche** : Tout mettre en Client Component

```typescript
"use client";

import { useState } from "react";

export default function Dashboard() {
  const [count, setCount] = useState(0);

  // ❌ Ce fetch côté client alors qu'il pourrait être côté serveur
  const users = await fetch("/api/users");

  return (
    <div>
      <UsersList users={users} />
      <Counter count={count} setCount={setCount} />
    </div>
  );
}
```

**✅ Bonne approche** : Composer Server + Client Components

```typescript
// ✅ Server Component (par défaut)
export default async function Dashboard() {
  // ✅ Fetch côté serveur (plus rapide)
  const res = await fetch("https://api.example.com/users");
  const users = await res.json();

  return (
    <div>
      {/* Server Component pour affichage statique */}
      <UsersList users={users} />

      {/* Client Component uniquement pour l'interactivité */}
      <Counter />
    </div>
  );
}
```

```typescript
// components/Counter.tsx
"use client"; // ✅ Client Component isolé

import { useState } from "react";

export default function Counter() {
  const [count, setCount] = useState(0);

  return <button onClick={() => setCount(count + 1)}>Count: {count}</button>;
}
```

### Restrictions Importantes

**Server Components NE PEUVENT PAS** :

- ❌ Utiliser useState, useEffect, useContext
- ❌ Utiliser des event handlers (onClick, onChange)
- ❌ Utiliser des hooks personnalisés qui dépendent de l'état client
- ❌ Utiliser des APIs du navigateur (window, document, localStorage)

**Client Components NE PEUVENT PAS** :

- ❌ Être async (pas de `async function Component()`)
- ❌ Accéder directement aux bases de données
- ❌ Utiliser des secrets serveur (process.env.SECRET_KEY)

### Exemple Complet : Page de Produit

**`app/products/[id]/page.tsx`** (Server Component) :

```typescript
// ✅ Server Component (pas de 'use client')
export default async function ProductPage({
  params,
}: {
  params: { id: string };
}) {
  // ✅ Fetch côté serveur
  const res = await fetch(`https://api.example.com/products/${params.id}`);
  const product = await res.json();

  return (
    <div>
      <h1>{product.name}</h1>
      <p>{product.description}</p>
      <p>Prix : {product.price}€</p>

      {/* Client Component pour le bouton interactif */}
      <AddToCartButton productId={product.id} />
    </div>
  );
}
```

**`components/AddToCartButton.tsx`** (Client Component) :

```typescript
"use client";

import { useState } from "react";

export default function AddToCartButton({ productId }: { productId: string }) {
  const [added, setAdded] = useState(false);

  const handleClick = () => {
    // Logique d'ajout au panier
    setAdded(true);
  };

  return (
    <button onClick={handleClick}>
      {added ? "✅ Ajouté" : "Ajouter au panier"}
    </button>
  );
}
```

---

## Data Fetching

Next.js 15 offre plusieurs façons de récupérer des données. Choisissez selon votre besoin.

### 1. Fetch dans un Server Component (Recommandé)

**Le plus simple et performant pour les données publiques.**

```typescript
// app/posts/page.tsx
export default async function PostsPage() {
  const res = await fetch("https://jsonplaceholder.typicode.com/posts");
  const posts = await res.json();

  return (
    <ul>
      {posts.map((post) => (
        <li key={post.id}>{post.title}</li>
      ))}
    </ul>
  );
}
```

**Avantages** :

- ✅ Simple
- ✅ Pas de state management
- ✅ Données fraîches à chaque requête

**Cache par défaut** :

```typescript
// Next.js cache automatiquement les fetch()
const res = await fetch(url); // Caché par défaut

// Pour désactiver le cache :
const res = await fetch(url, { cache: "no-store" });

// Pour revalider toutes les 10 secondes :
const res = await fetch(url, { next: { revalidate: 10 } });
```

### 2. API Routes (Route Handlers)

**Pour créer des endpoints backend dans Next.js.**

**Structure** :

```
app/
└── api/
    └── users/
        └── route.ts        # GET /api/users
```

**`app/api/users/route.ts`** :

```typescript
import { NextResponse } from "next/server";

// GET /api/users
export async function GET() {
  const users = [
    { id: 1, name: "Alice" },
    { id: 2, name: "Bob" },
  ];

  return NextResponse.json({ users });
}

// POST /api/users
export async function POST(request: Request) {
  const body = await request.json();

  // Logique de création
  const newUser = { id: 3, ...body };

  return NextResponse.json({ user: newUser }, { status: 201 });
}
```

**Utilisation depuis un Client Component** :

```typescript
"use client";

import { useEffect, useState } from "react";

export default function UsersList() {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    const fetchUsers = async () => {
      const res = await fetch("/api/users");
      const data = await res.json();
      setUsers(data.users);
    };
    fetchUsers();
  }, []);

  return (
    <ul>
      {users.map((user) => (
        <li key={user.id}>{user.name}</li>
      ))}
    </ul>
  );
}
```

### 3. Server Actions

**Pour les mutations (créer, modifier, supprimer) depuis un Client Component.**

**`app/actions/users.ts`** :

```typescript
"use server";

export async function createUser(formData: FormData) {
  const name = formData.get("name");
  const email = formData.get("email");

  // Logique de création (DB, API, etc.)
  const newUser = { id: Date.now(), name, email };

  return { success: true, user: newUser };
}
```

**Utilisation dans un formulaire** :

```typescript
"use client";

import { createUser } from "@/app/actions/users";

export default function CreateUserForm() {
  const handleSubmit = async (formData: FormData) => {
    const result = await createUser(formData);
    console.log(result);
  };

  return (
    <form action={handleSubmit}>
      <input name="name" placeholder="Nom" required />
      <input name="email" type="email" placeholder="Email" required />
      <button type="submit">Créer</button>
    </form>
  );
}
```

### Comparaison des Approches

| Approche                        | Cas d'usage                 | Avantages                | Inconvénients      |
| ------------------------------- | --------------------------- | ------------------------ | ------------------ |
| **Fetch dans Server Component** | Affichage de données        | Simple, performant       | Pas pour mutations |
| **API Routes**                  | Endpoints publics, webhooks | Standard REST            | Plus verbeux       |
| **Server Actions**              | Mutations depuis forms      | Type-safe, moins de code | Next.js spécifique |

**Recommandation** :

- **Lecture** : Fetch dans Server Component
- **Écriture** : Server Actions (ou API Routes si besoin d'un endpoint public)

### Gestion des Erreurs

**Avec try/catch** :

```typescript
export default async function PostsPage() {
  try {
    const res = await fetch("https://api.example.com/posts");
    if (!res.ok) throw new Error("Failed to fetch");
    const posts = await res.json();

    return <PostsList posts={posts} />;
  } catch (error) {
    return <div>Erreur lors du chargement des articles</div>;
  }
}
```

**Avec error.tsx (plus propre)** :

```typescript
// app/posts/error.tsx
"use client";

export default function Error({
  error,
  reset,
}: {
  error: Error;
  reset: () => void;
}) {
  return (
    <div>
      <h2>Une erreur est survenue</h2>
      <p>{error.message}</p>
      <button onClick={reset}>Réessayer</button>
    </div>
  );
}
```

### Loading States

**`app/posts/loading.tsx`** :

```typescript
export default function Loading() {
  return <div>Chargement des articles...</div>;
}
```

Next.js affiche automatiquement ce composant pendant le fetch.

---

## Navigation et Routing

### Link Component

**Le composant `<Link>` de Next.js fait du prefetching automatique.**

```typescript
import Link from "next/link";

export default function Navbar() {
  return (
    <nav>
      <Link href="/">Accueil</Link>
      <Link href="/about">À propos</Link>
      <Link href="/blog">Blog</Link>

      {/* Route dynamique */}
      <Link href={`/blog/${postSlug}`}>Article</Link>
    </nav>
  );
}
```

**❌ N'utilisez PAS `<a>`** : Cela recharge toute la page (perte de performance).

### Navigation Programmatique

**Dans un Client Component** :

```typescript
"use client";

import { useRouter } from "next/navigation";

export default function LoginForm() {
  const router = useRouter();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    // Logique de connexion
    await login();

    // Redirection
    router.push("/dashboard");
  };

  return <form onSubmit={handleSubmit}>...</form>;
}
```

**Dans un Server Component** :

```typescript
import { redirect } from "next/navigation";

export default async function ProfilePage() {
  const user = await getCurrentUser();

  if (!user) {
    redirect("/login");
  }

  return <div>Profil de {user.name}</div>;
}
```

### useRouter vs redirect()

|               | useRouter              | redirect()               |
| ------------- | ---------------------- | ------------------------ |
| **Type**      | Client Component       | Server Component         |
| **Directive** | `'use client'`         | Aucune                   |
| **Import**    | `next/navigation`      | `next/navigation`        |
| **Usage**     | Navigation côté client | Redirection côté serveur |

---

## Layouts et Metadata

### Root Layout (Obligatoire)

**`app/layout.tsx`** est le layout racine qui entoure **toutes** les pages.

```typescript
export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="fr">
      <body>
        <header>
          <nav>...</nav>
        </header>

        <main>{children}</main>

        <footer>...</footer>
      </body>
    </html>
  );
}
```

### Nested Layouts

**Créer un layout spécifique pour `/dashboard`** :

```
app/
├── layout.tsx              # Layout racine
├── page.tsx                # /
└── dashboard/
    ├── layout.tsx          # Layout pour /dashboard/*
    ├── page.tsx            # /dashboard
    └── settings/
        └── page.tsx        # /dashboard/settings
```

**`app/dashboard/layout.tsx`** :

```typescript
export default function DashboardLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className="dashboard">
      <aside>
        <nav>
          <Link href="/dashboard">Dashboard</Link>
          <Link href="/dashboard/settings">Settings</Link>
        </nav>
      </aside>

      <div className="content">{children}</div>
    </div>
  );
}
```

**Résultat** :

- `/` utilise seulement le Root Layout
- `/dashboard` et `/dashboard/settings` utilisent Root Layout + Dashboard Layout

### Metadata Statique

**`app/page.tsx`** :

```typescript
import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Accueil - Mon Site",
  description: "Bienvenue sur mon site Next.js",
};

export default function Home() {
  return <h1>Accueil</h1>;
}
```

**Résultat HTML** :

```html
<head>
  <title>Accueil - Mon Site</title>
  <meta name="description" content="Bienvenue sur mon site Next.js" />
</head>
```

### Metadata Dynamique

**Pour les routes dynamiques** :

```typescript
import type { Metadata } from "next";

export async function generateMetadata({
  params,
}: {
  params: { id: string };
}): Promise<Metadata> {
  // Fetch des données
  const product = await fetch(
    `https://api.example.com/products/${params.id}`
  ).then((res) => res.json());

  return {
    title: product.name,
    description: product.description,
  };
}

export default async function ProductPage({
  params,
}: {
  params: { id: string };
}) {
  const product = await fetch(
    `https://api.example.com/products/${params.id}`
  ).then((res) => res.json());

  return <div>{product.name}</div>;
}
```

---

## Optimisations Next.js

### Image Optimization

**Le composant `<Image>` optimise automatiquement les images.**

```typescript
import Image from "next/image";

export default function Avatar() {
  return (
    <Image
      src="/avatar.jpg"
      alt="Avatar"
      width={200}
      height={200}
      priority // Charger en priorité (above the fold)
    />
  );
}
```

**Avantages** :

- ✅ Formats modernes (WebP, AVIF)
- ✅ Lazy loading automatique
- ✅ Responsive (srcset)
- ✅ Évite le Cumulative Layout Shift (CLS)

### Font Optimization

**`next/font` optimise le chargement des polices.**

```typescript
// app/layout.tsx
import { Inter } from "next/font/google";

const inter = Inter({ subsets: ["latin"] });

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="fr" className={inter.className}>
      <body>{children}</body>
    </html>
  );
}
```

**Avantages** :

- ✅ Pas de Flash of Unstyled Text (FOUT)
- ✅ Fonts hébergées automatiquement
- ✅ Meilleur performance score

### Caching et Revalidation

**Par défaut, Next.js cache les fetch()** :

```typescript
// ✅ Caché jusqu'au prochain build
const res = await fetch("https://api.example.com/posts");

// ⚠️ Pas de cache (données toujours fraîches)
const res = await fetch("https://api.example.com/posts", {
  cache: "no-store",
});

// ✅ Cache avec revalidation toutes les 60 secondes
const res = await fetch("https://api.example.com/posts", {
  next: { revalidate: 60 },
});
```

---

## Déploiement sur Vercel

### Pourquoi Vercel ?

- ✅ Créé par les développeurs de Next.js
- ✅ Déploiement en un clic
- ✅ Preview deployments automatiques (branches Git)
- ✅ CDN global
- ✅ Gratuit pour usage personnel

### Étapes de Déploiement

1. **Créer un compte** sur [vercel.com](https://vercel.com)

2. **Connecter votre repo GitHub/GitLab**

3. **Importer le projet** : Vercel détecte automatiquement Next.js

4. **Déployer** : Un clic, c'est fait ! 🎉

### Variables d'Environnement

**Dans Vercel Dashboard** :

```
Settings → Environment Variables

NEXT_PUBLIC_API_URL=https://api.example.com
DATABASE_URL=postgresql://...
SECRET_KEY=xxx
```

**Dans votre code** :

```typescript
// ✅ Accessible côté client (préfixe NEXT_PUBLIC_)
const apiUrl = process.env.NEXT_PUBLIC_API_URL;

// ✅ Accessible uniquement côté serveur
const dbUrl = process.env.DATABASE_URL;
```

### Preview Deployments

**Chaque push sur une branche génère une URL de preview** :

```
main → https://mon-site.vercel.app (production)
feat/new-feature → https://mon-site-git-feat-new-feature.vercel.app (preview)
```

---

## Questions Types d'Entretien

### Questions Théoriques

**1. Quelle est la différence entre un Server Component et un Client Component ?**

<details>
<summary>Voir la réponse</summary>

- **Server Component** : S'exécute sur le serveur, pas d'interactivité, peut fetch directement
- **Client Component** : S'exécute côté client, utilise useState/useEffect/événements
- Par défaut = Server Component, ajouter `'use client'` si besoin d'interactivité

```tsx
// Server Component (par défaut)
export default async function ServerPage() {
  const data = await fetch('https://api.example.com/data')
  return <div>{data.title}</div>
}

// Client Component
'use client'
import { useState } from 'react'
export default function ClientPage() {
  const [count, setCount] = useState(0)
  return <button onClick={() => setCount(count + 1)}>{count}</button>
}
```

</details>

**2. Pourquoi utiliser Next.js plutôt que React pur ?**

<details>
<summary>Voir la réponse</summary>

- Routing file-based intégré
- SSR/SSG par défaut (meilleur SEO)
- API Routes (backend + frontend dans un projet)
- Optimisations automatiques (images, fonts)
- Meilleures performances

```tsx
// React pur - Configuration manuelle requise
// - React Router pour routing
// - Webpack/Vite pour build
// - Serveur séparé pour API

// Next.js - Tout inclus par défaut
// app/page.tsx - Routing automatique
// app/api/route.ts - API intégrée
// Optimisations automatiques
```

</details>

**3. Comment gérer le routing dans Next.js avec l'App Router ?**

<details>
<summary>Voir la réponse</summary>

- File-based routing dans le dossier `app/`
- `page.tsx` crée une route
- `[slug]` pour routes dynamiques
- `layout.tsx` pour layouts partagés

```
app/
├── page.tsx              # Route: /
├── about/
│   └── page.tsx          # Route: /about
└── blog/
    ├── page.tsx          # Route: /blog
    └── [slug]/
        └── page.tsx      # Route: /blog/:slug
```

</details>

**4. Quand utiliser une API Route vs une Server Action ?**

<details>
<summary>Voir la réponse</summary>

- **API Route** : Endpoint public, webhooks, appelé par des clients externes
- **Server Action** : Mutations depuis un formulaire, plus simple, type-safe

```tsx
// API Route - Endpoint public
// app/api/users/route.ts
export async function GET() {
  return Response.json({ users: [] })
}

// Server Action - Mutations internes
// app/actions.ts
'use server'
export async function createUser(formData: FormData) {
  const name = formData.get('name')
  // Logique
  return { success: true }
}
```

</details>

**5. Comment optimiser les images dans Next.js ?**

<details>
<summary>Voir la réponse</summary>

- Utiliser `<Image>` de `next/image`
- Formats modernes automatiques (WebP, AVIF)
- Lazy loading par défaut
- Spécifier width/height pour éviter CLS

```tsx
import Image from 'next/image'

export default function Gallery() {
  return (
    <Image
      src="/photo.jpg"
      alt="Photo"
      width={800}
      height={600}
      priority // Pour images above the fold
      quality={90}
    />
  )
}
```

</details>

### Exercices Pratiques

**Exercice 1 : Créer une page produit dynamique**

Créer une route `/products/[id]` qui :

- Fetch les données d'un produit depuis une API
- Affiche le nom, description, prix
- Inclut un bouton "Ajouter au panier" interactif

<details>
<summary><b>✅ Solution</b></summary>

```typescript
// app/products/[id]/page.tsx
export default async function ProductPage({
  params,
}: {
  params: { id: string };
}) {
  const res = await fetch(`https://api.example.com/products/${params.id}`);
  const product = await res.json();

  return (
    <div>
      <h1>{product.name}</h1>
      <p>{product.description}</p>
      <p>Prix : {product.price}€</p>
      <AddToCartButton productId={product.id} />
    </div>
  );
}
```

```typescript
// components/AddToCartButton.tsx
"use client";

import { useState } from "react";

export default function AddToCartButton({ productId }: { productId: string }) {
  const [added, setAdded] = useState(false);

  return (
    <button onClick={() => setAdded(true)}>
      {added ? "✅ Ajouté" : "Ajouter au panier"}
    </button>
  );
}
```

</details>

---

## Bonnes Pratiques

### 1. Structure de Projet

```
app/
├── (auth)/                 # Route group (n'affecte pas l'URL)
│   ├── login/
│   └── register/
├── (marketing)/
│   ├── about/
│   └── contact/
├── dashboard/
│   ├── layout.tsx
│   └── page.tsx
├── api/
│   └── users/
│       └── route.ts
├── layout.tsx
└── page.tsx

components/
├── ui/                     # Composants UI réutilisables
│   ├── Button.tsx
│   └── Card.tsx
└── features/               # Composants métier
    └── ProductCard.tsx

lib/
├── utils.ts                # Fonctions utilitaires
└── constants.ts

types/
└── index.ts                # Types TypeScript globaux
```

### 2. Conventions de Nommage

- **Composants** : PascalCase (`ProductCard.tsx`)
- **Fichiers Next.js** : kebab-case (`page.tsx`, `loading.tsx`)
- **Dossiers** : kebab-case (`blog-posts/`)
- **Fonctions** : camelCase (`fetchUserData()`)

### 3. Performance

✅ **À faire** :

- Utiliser Server Components par défaut
- Mettre `'use client'` uniquement où nécessaire
- Utiliser `<Image>` de Next.js
- Utiliser `loading.tsx` pour les feedbacks de chargement

❌ **À éviter** :

- Mettre `'use client'` sur toutes les pages
- Utiliser `<img>` au lieu de `<Image>`
- Fetch côté client si possible côté serveur

### 4. SEO

```typescript
// Metadata pour chaque page
export const metadata = {
  title: "Page Title",
  description: "Page description",
  openGraph: {
    images: ["/og-image.jpg"],
  },
};
```

### 5. TypeScript

**Toujours typer les props et params** :

```typescript
interface PageProps {
  params: { id: string };
  searchParams: { [key: string]: string | string[] | undefined };
}

export default function Page({ params, searchParams }: PageProps) {
  // ...
}
```

---

## Ressources

### Documentation Officielle

- [Next.js 15 Documentation](https://nextjs.org/docs)
- [App Router](https://nextjs.org/docs/app)
- [Server Components](https://nextjs.org/docs/app/building-your-application/rendering/server-components)
- [API Routes](https://nextjs.org/docs/app/building-your-application/routing/route-handlers)

### Guides et Tutoriels

- [Next.js Learn](https://nextjs.org/learn) - Tutoriel officiel interactif
- [Vercel Blog](https://vercel.com/blog) - Articles sur Next.js et web performance

### Exemples

- [Next.js Examples](https://github.com/vercel/next.js/tree/canary/examples) - Repo officiel avec des exemples

### Outils

- [Next.js DevTools](https://nextjs.org/docs/app/building-your-application/optimizing/instrumentation) - Debug et performance
- [Vercel](https://vercel.com) - Plateforme de déploiement

---

## Prochaines Étapes

Vous maîtrisez maintenant les fondamentaux de Next.js 15 ! 🎉

**Pour pratiquer** :

- 📖 Consultez le fichier **[STARTER.md](./STARTER.md)** pour configurer votre environnement
- 🎯 Faites l'exercice **[Exercice-Capitales.md](./Exercice-Capitales.md)** pour mettre en pratique

**Bon apprentissage !** 💪
