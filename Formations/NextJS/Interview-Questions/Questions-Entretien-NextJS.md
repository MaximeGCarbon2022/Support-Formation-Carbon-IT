# Questions d'Entretien Technique - Next.js 13/14/15 (App Router)

## ⚪ NÉCESSAIRE

### 1. Comment créer une nouvelle page dans Next.js (App Router) ?

<details>
<summary>Voir la réponse</summary>

Créer un fichier `page.tsx` dans un dossier `/app/nom-page/`, structure dossiers = routes

```tsx
// app/about/page.tsx
export default function About() {
  return <h1>À propos</h1>
}
```

</details>

### 2. Quelle est la différence entre un Server Component et un Client Component ?

<details>
<summary>Voir la réponse</summary>

Server = rendu serveur, pas de JS client. Client (`'use client'`) = interactif, hooks React

```tsx
// Server Component (par défaut)
export default async function Page() {
  const data = await fetch('...')
  return <div>{data}</div>
}

// Client Component
'use client'
import { useState } from 'react'
export default function Counter() {
  const [count, setCount] = useState(0)
  return <button onClick={() => setCount(count + 1)}>{count}</button>
}
```

</details>

### 3. Quand devez-vous utiliser `'use client'` ?

<details>
<summary>Voir la réponse</summary>

Pour l'interactivité : useState, useEffect, événements (onClick), Context, browser APIs

```tsx
'use client'
import { useState, useEffect } from 'react'

export default function Interactive() {
  const [value, setValue] = useState('')

  useEffect(() => {
    console.log('Mounted')
  }, [])

  return <input value={value} onChange={(e) => setValue(e.target.value)} />
}
```

</details>

### 4. Comment optimiser une image dans Next.js ?

<details>
<summary>Voir la réponse</summary>

Utiliser `next/image`, optimisation auto, lazy loading, WebP/AVIF, `priority` pour above fold

```tsx
import Image from 'next/image'

export default function Hero() {
  return (
    <Image
      src="/hero.jpg"
      alt="Hero"
      width={1200}
      height={600}
      priority // Pour les images above the fold
    />
  )
}
```

</details>

### 5. Comment changer le titre et meta tags d'une page ?

<details>
<summary>Voir la réponse</summary>

Export `metadata` object ou fonction `generateMetadata` dans `page.tsx`

```tsx
// Metadata statique
export const metadata = {
  title: 'Ma Page',
  description: 'Description de ma page'
}

// Metadata dynamique
export async function generateMetadata({ params }) {
  const product = await fetch(`/api/products/${params.id}`)
  return {
    title: product.name,
    description: product.description
  }
}
```

</details>

### 6. Où placer les fichiers statiques (logo, favicon) ?

<details>
<summary>Voir la réponse</summary>

Dossier `/public`, accessible via `/fichier.png`

```tsx
import Image from 'next/image'

export default function Logo() {
  return <Image src="/logo.png" alt="Logo" width={100} height={100} />
}

// Le fichier doit être dans public/logo.png
```

</details>

---

## 🟢 BASIQUE

### 1. Comment créer un layout partagé entre plusieurs pages ?

<details>
<summary>Voir la réponse</summary>

Créer `layout.tsx` dans le dossier, entoure les pages enfants avec `{children}`

```tsx
// app/dashboard/layout.tsx
export default function DashboardLayout({ children }) {
  return (
    <div>
      <nav>Navigation du dashboard</nav>
      <main>{children}</main>
    </div>
  )
}
```

</details>

### 2. À quoi sert le fichier `loading.tsx` ?

<details>
<summary>Voir la réponse</summary>

UI de chargement automatique (Suspense), affiché pendant fetch de données


```tsx
// app/loading.tsx
export default function Loading() {
  return <div>Chargement...</div>
}
```

</details>

### 3. À quoi sert le fichier `error.tsx` ?

<details>
<summary>Voir la réponse</summary>

Error Boundary automatique, gère les erreurs des composants/pages enfants


```tsx
// app/error.tsx
'use client'
export default function Error({ error, reset }) {
  return (
    <div>
      <h2>Erreur: {error.message}</h2>
      <button onClick={() => reset()}>Réessayer</button>
    </div>
  )
}
```

</details>

### 4. Comment créer une route dynamique (ex: `/blog/[slug]`) ?

<details>
<summary>Voir la réponse</summary>

Créer dossier `[slug]` avec `page.tsx`, accéder via `params.slug`


```tsx
// app/blog/[slug]/page.tsx
export default function BlogPost({ params }: { params: { slug: string } }) {
  return <h1>Article: {params.slug}</h1>
}
```

</details>

### 5. Comment fetcher des données dans un Server Component ?

<details>
<summary>Voir la réponse</summary>

`async/await` directement dans le composant avec `fetch()`, pas besoin de useEffect


```tsx
export default async function Page() {
  const res = await fetch('https://api.example.com/data')
  const data = await res.json()
  return <div>{data.title}</div>
}
```

</details>

### 6. Comment rendre un fetch non-caché (toujours à jour) ?

<details>
<summary>Voir la réponse</summary>

`fetch(url, { cache: 'no-store' })` ou `{ next: { revalidate: 0 } }`


```tsx
const res = await fetch(url, { cache: 'no-store' })
// ou
const res = await fetch(url, { next: { revalidate: 0 } })
```

</details>

### 7. Comment créer une API Route (endpoint) dans App Router ?

<details>
<summary>Voir la réponse</summary>

Créer `route.ts` avec exports `GET`, `POST`, etc. (pas dans `/pages/api`)


```tsx
// app/api/users/route.ts
import { NextResponse } from 'next/server'

export async function GET() {
  return NextResponse.json({ users: [] })
}

export async function POST(request: Request) {
  const body = await request.json()
  return NextResponse.json({ success: true })
}
```

</details>

### 8. Quelle est la différence entre `layout.tsx` et `template.tsx` ?

<details>
<summary>Voir la réponse</summary>

Layout persiste entre navigations, Template se re-monte à chaque navigation


```tsx
// layout.tsx - Persiste entre navigations
export default function Layout({ children }) {
  return <div>{children}</div>
}

// template.tsx - Re-monte à chaque navigation
export default function Template({ children }) {
  return <div>{children}</div>
}
```

</details>

### 9. Comment générer des pages statiques pour des routes dynamiques ?

<details>
<summary>Voir la réponse</summary>

Export `generateStaticParams` qui retourne array de params à pré-générer

```tsx
export async function generateStaticParams() {
  const posts = await fetch('https://api.example.com/posts').then(r => r.json())
  return posts.map((post) => ({ slug: post.slug }))
}

export default function Page({ params }: { params: { slug: string } }) {
  return <div>{params.slug}</div>
}
```

</details>

### 10. Comment forcer le rendu statique d'une page ?

<details>
<summary>Voir la réponse</summary>

Pas de `cookies()`, `headers()`, ou fetch non-caché → automatiquement statique

```tsx
// ✅ Rendu statique (pas d'appels dynamiques)
export default async function Page() {
  const data = await fetch('https://api.example.com/data')
  return <div>{data.title}</div>
}

// ❌ Rendu dynamique (utilise cookies)
import { cookies } from 'next/headers'
export default function Page() {
  const cookieStore = cookies()
  return <div>Dynamic</div>
}
```

</details>

---

## 🟡 INTERMÉDIAIRE

### 1. Qu'est-ce qu'une Server Action et comment l'utiliser ?

<details>
<summary>Voir la réponse</summary>

Fonction `'use server'` appelable depuis client, pour mutations, utilisable dans `<form action={...}>`


```tsx
// app/actions.ts
'use server'

export async function createUser(formData: FormData) {
  const name = formData.get('name')
  // Logique de création
  return { success: true }
}

// Utilisation
<form action={createUser}>
  <input name="name" />
  <button type="submit">Créer</button>
</form>
```

</details>

### 2. Comment revalider le cache d'une page après une mutation ?

<details>
<summary>Voir la réponse</summary>

`revalidatePath('/chemin')` dans Server Action ou API Route

```tsx
'use server'
import { revalidatePath } from 'next/cache'

export async function updatePost() {
  // Logique de mise à jour
  revalidatePath('/blog')
}
```

</details>

### 3. Quelle est la différence entre `revalidatePath` et `revalidateTag` ?

<details>
<summary>Voir la réponse</summary>

Path = invalide un chemin. Tag = invalide tous les fetch avec ce tag (`next: { tags: [...] }`)

```tsx
// Fetch avec tag
fetch(url, { next: { tags: ['posts'] } })

// Revalidation par tag
'use server'
import { revalidateTag } from 'next/cache'
revalidateTag('posts')

// Revalidation par path
import { revalidatePath } from 'next/cache'
revalidatePath('/blog')
```

</details>

### 4. Comment afficher un loading progressif avec Suspense ?

<details>
<summary>Voir la réponse</summary>

Wrapper composant async dans `<Suspense fallback={...}>`, streaming du reste


```tsx
import { Suspense } from 'react'

export default function Page() {
  return (
    <Suspense fallback={<div>Chargement...</div>}>
      <AsyncComponent />
    </Suspense>
  )
}
```

</details>

### 5. Pourquoi ce code crash : `useState()` dans un Server Component ?

<details>
<summary>Voir la réponse</summary>

Server Component = pas de hooks/interactivité, ajouter `'use client'` ou déplacer logique


```tsx
// ❌ Erreur
export default function ServerComponent() {
  const [state, setState] = useState(0) // Crash!
  return <div>{state}</div>
}

// ✅ Solution
'use client'
export default function ClientComponent() {
  const [state, setState] = useState(0)
  return <div>{state}</div>
}
```

</details>

### 6. Comment accéder aux cookies dans un Server Component ?

<details>
<summary>Voir la réponse</summary>

Importer `cookies()` de `next/headers`, rend la page dynamique (SSR)


```tsx
import { cookies } from 'next/headers'

export default async function Page() {
  const cookieStore = cookies()
  const token = cookieStore.get('token')
  return <div>{token?.value}</div>
}
```

</details>

### 7. Comment accéder aux headers de la requête ?

<details>
<summary>Voir la réponse</summary>

Importer `headers()` de `next/headers`, rend la page dynamique


```tsx
import { headers } from 'next/headers'

export default async function Page() {
  const headersList = headers()
  const userAgent = headersList.get('user-agent')
  return <div>{userAgent}</div>
}
```

</details>

### 8. Comment créer des métadatas dynamiques (selon l'ID produit) ?

<details>
<summary>Voir la réponse</summary>

Export fonction `generateMetadata({ params })` async dans `page.tsx`


</details>

### 9. Quelle est la différence entre `redirect()` et `useRouter()` ?

<details>
<summary>Voir la réponse</summary>

`redirect()` = Server Components/Actions. `useRouter()` = Client Components uniquement


```tsx
// Server Component
import { redirect } from 'next/navigation'
if (!user) redirect('/login')

// Client Component
'use client'
import { useRouter } from 'next/navigation'
const router = useRouter()
router.push('/dashboard')
```

</details>

### 10. Comment optimiser un composant lourd (chart) ?

<details>
<summary>Voir la réponse</summary>

Dynamic import avec `next/dynamic`, `ssr: false`, lazy loading


</details>

### 11. Comment protéger une route avec authentification ?

<details>
<summary>Voir la réponse</summary>

Middleware avec vérification token/cookie, ou check dans layout/page + redirect

```tsx
// middleware.ts
import { NextResponse } from 'next/server'

export function middleware(request) {
  const token = request.cookies.get('token')
  if (!token) {
    return NextResponse.redirect(new URL('/login', request.url))
  }
}

export const config = {
  matcher: '/dashboard/:path*'
}

// Ou dans un layout/page
import { cookies } from 'next/headers'
import { redirect } from 'next/navigation'

export default async function ProtectedPage() {
  const token = cookies().get('token')
  if (!token) redirect('/login')
  return <div>Contenu protégé</div>
}
```

</details>

### 12. À quoi sert `middleware.ts` à la racine ?

<details>
<summary>Voir la réponse</summary>

S'exécute avant requête, pour auth, redirects, rewrites, i18n, A/B testing


```tsx
// middleware.ts
import { NextResponse } from 'next/server'

export function middleware(request) {
  const token = request.cookies.get('token')
  if (!token) {
    return NextResponse.redirect(new URL('/login', request.url))
  }
}

export const config = {
  matcher: '/dashboard/:path*'
}
```

</details>

### 13. Comment gérer les routes parallèles (Parallel Routes) ?

<details>
<summary>Voir la réponse</summary>

Dossiers `@nom`, rendu simultané dans layout via slots (`{nom}`), dashboard/modals

```tsx
// Structure
app/
└── dashboard/
    ├── layout.tsx
    ├── @analytics/
    │   └── page.tsx
    └── @team/
        └── page.tsx

// app/dashboard/layout.tsx
export default function Layout({ analytics, team }) {
  return (
    <div>
      {analytics}
      {team}
    </div>
  )
}
```

</details>

### 14. Comment gérer les intercepting routes (modals) ?

<details>
<summary>Voir la réponse</summary>

Convention `(.)` ou `(..)`, intercepte navigation soft, garde URL, modal + page séparée

```tsx
// Structure
app/
├── photos/
│   ├── page.tsx
│   └── [id]/
│       └── page.tsx           # /photos/123 (page complète)
└── @modal/
    └── (.)photos/
        └── [id]/
            └── page.tsx       # Modal intercepté

// app/layout.tsx
export default function Layout({ children, modal }) {
  return (
    <>
      {children}
      {modal}
    </>
  )
}
```

</details>

### 15. Comment configurer le cache de fetch par défaut ?

<details>
<summary>Voir la réponse</summary>

Dans `fetch()` avec `{ cache: 'force-cache' }` (défaut) ou `no-store`, ou `revalidate: X`

```tsx
// Cache par défaut (force-cache)
const data1 = await fetch('https://api.example.com/data')

// Pas de cache
const data2 = await fetch('https://api.example.com/data', {
  cache: 'no-store'
})

// Revalidation toutes les 60 secondes
const data3 = await fetch('https://api.example.com/data', {
  next: { revalidate: 60 }
})

// Avec tags pour revalidation ciblée
const data4 = await fetch('https://api.example.com/data', {
  next: { tags: ['posts'] }
})
```

</details>

---

## 🔴 AVANCÉ

### 1. Expliquez les 4 types de cache dans Next.js 13+ ?

<details>
<summary>Voir la réponse</summary>

Request Memoization, Data Cache, Full Route Cache, Router Cache (client)


```
1. Request Memoization: Déduplique les fetch identiques
2. Data Cache: Cache les résultats de fetch
3. Full Route Cache: Cache le HTML généré
4. Router Cache: Cache côté client des routes visitées
```

</details>

### 2. Comment désactiver complètement le cache pour une route ?

<details>
<summary>Voir la réponse</summary>

Export `export const dynamic = 'force-dynamic'` ou utiliser `cookies()`/`headers()`


```tsx
// Option 1
export const dynamic = 'force-dynamic'

// Option 2
import { cookies } from 'next/headers'
// L'utilisation de cookies() désactive le cache
```

</details>

### 3. Comment forcer une route en Edge Runtime ?

<details>
<summary>Voir la réponse</summary>

Export `export const runtime = 'edge'` dans page/route, limité mais ultra-rapide


```tsx
export const runtime = 'edge'

export default function Page() {
  return <div>Page en Edge Runtime</div>
}
```

</details>

### 4. Quelle est la différence entre `generateStaticParams` et ISR ?

<details>
<summary>Voir la réponse</summary>

StaticParams = pre-render au build. ISR = revalidate après build avec `revalidate: X`


```tsx
export async function generateStaticParams() {
  const posts = await fetch('https://api.example.com/posts').then(r => r.json())
  return posts.map((post) => ({ slug: post.slug }))
}
```

</details>

### 5. Comment implémenter un système de streaming de données ?

<details>
<summary>Voir la réponse</summary>

Server Component async + Suspense boundaries, React streaming, améliore TTFB


</details>

### 6. Comment optimiser les waterfalls de fetch ?

<details>
<summary>Voir la réponse</summary>

Paralléliser avec `Promise.all()`, ou Suspense boundaries séparées pour streaming


</details>

### 7. Comment passer des données entre Server Component parent et enfant ?

<details>
<summary>Voir la réponse</summary>

Props directement, pas besoin de Context, fetch peut être dédupliqué automatiquement


</details>

### 8. Peut-on utiliser Context API dans Server Components ?

<details>
<summary>Voir la réponse</summary>

Non, Context = Client Component uniquement, mais props drilling simplifié côté serveur


</details>

### 9. Comment sécuriser une Server Action ?

<details>
<summary>Voir la réponse</summary>

Validation inputs (Zod), vérifier auth dans action, rate limiting, CSRF automatique


```tsx
// app/actions.ts
'use server'

export async function createUser(formData: FormData) {
  const name = formData.get('name')
  // Logique de création
  return { success: true }
}

// Utilisation
<form action={createUser}>
  <input name="name" />
  <button type="submit">Créer</button>
</form>
```

</details>

### 10. Comment gérer les formulaires avec Server Actions ?

<details>
<summary>Voir la réponse</summary>

`<form action={serverAction}>`, FormData automatique, progressive enhancement, `useFormStatus`


```tsx
// app/actions.ts
'use server'

export async function createUser(formData: FormData) {
  const name = formData.get('name')
  // Logique de création
  return { success: true }
}

// Utilisation
<form action={createUser}>
  <input name="name" />
  <button type="submit">Créer</button>
</form>
```

</details>

### 11. Quelle stratégie pour migrer de Pages Router vers App Router ?

<details>
<summary>Voir la réponse</summary>

Migration progressive, coexistence possible, route par route, adapter data fetching


</details>

### 12. Comment implémenter l'optimistic UI avec Server Actions ?

<details>
<summary>Voir la réponse</summary>

`useOptimistic` hook, update UI immédiat, rollback si erreur


```tsx
// app/actions.ts
'use server'

export async function createUser(formData: FormData) {
  const name = formData.get('name')
  // Logique de création
  return { success: true }
}

// Utilisation
<form action={createUser}>
  <input name="name" />
  <button type="submit">Créer</button>
</form>
```

</details>

### 13. Comment débugger les problèmes de cache Next.js ?

<details>
<summary>Voir la réponse</summary>

Vérifier `force-dynamic`, logs fetch, `.next/cache`, headers cache, désactiver si debug


</details>

### 14. Comment gérer l'internationalisation (i18n) dans App Router ?

<details>
<summary>Voir la réponse</summary>

Route `[lang]` manuelle, middleware détection, `next-intl`, dictionaries, `generateStaticParams`


</details>

### 15. Quand utiliser `unstable_cache` ?

<details>
<summary>Voir la réponse</summary>

Pour cacher résultats de DB queries ou calculs coûteux hors fetch, avec tags pour revalidation


</details>

### 16. Comment implémenter un système multi-tenant ?

<details>
<summary>Voir la réponse</summary>

Middleware détection subdomain, headers custom, DB isolation, cache par tenant


</details>

### 17. Comment optimiser les Core Web Vitals (LCP, CLS, INP) ?

<details>
<summary>Voir la réponse</summary>

`next/image`, `next/font`, streaming, Suspense, code splitting, preload critical


</details>

### 18. Comment gérer les variables d'environnement serveur vs client ?

<details>
<summary>Voir la réponse</summary>

`NEXT_PUBLIC_` = client. Sans préfixe = serveur uniquement, jamais exposé


```tsx
// .env
NEXT_PUBLIC_API_URL=https://api.example.com  // Accessible côté client
DATABASE_URL=postgresql://...                 // Serveur uniquement

// Utilisation
const apiUrl = process.env.NEXT_PUBLIC_API_URL // Client OK
const dbUrl = process.env.DATABASE_URL         // Serveur uniquement
```

</details>

### 19. Quelle est la différence entre `notFound()` et `redirect()` ?

<details>
<summary>Voir la réponse</summary>

`notFound()` = affiche `not-found.tsx` (404). `redirect()` = redirige vers URL


```tsx
import { notFound } from 'next/navigation'

export default async function Page({ params }) {
  const data = await fetch(`/api/posts/${params.id}`)
  if (!data) notFound()
  return <div>{data.title}</div>
}

// app/not-found.tsx
export default function NotFound() {
  return <h1>404 - Page non trouvée</h1>
}
```

</details>

### 20. Comment implémenter du partial prerendering (PPR) ?

<details>
<summary>Voir la réponse</summary>

Feature expérimentale Next.js 14+, mix statique + dynamique dans même page, Suspense boundaries


</details>

### 21. Comment monitorer les Server Components en production ?

<details>
<summary>Voir la réponse</summary>

APM (DataDog), Sentry, logs structurés, `instrumentation.ts`, metrics custom


</details>

### 22. Comment gérer les timeouts de Server Components/Actions ?

<details>
<summary>Voir la réponse</summary>

Config runtime, déplacer vers queue si long, streaming pour UX, edge pour rapidité


</details>

### 23. Comment implémenter une recherche avec URL state (filtres) ?

<details>
<summary>Voir la réponse</summary>

`useSearchParams` (client) ou `searchParams` prop (server), `useRouter` pour update


```tsx
'use client'
import { useSearchParams, useRouter } from 'next/navigation'

export default function Search() {
  const searchParams = useSearchParams()
  const router = useRouter()

  const query = searchParams.get('q')

  function updateSearch(value: string) {
    const params = new URLSearchParams(searchParams)
    params.set('q', value)
    router.push(`?${params.toString()}`)
  }

  return <input value={query || ''} onChange={(e) => updateSearch(e.target.value)} />
}
```

</details>

### 24. Quelle stratégie de déploiement Next.js sur infrastructure custom ?

<details>
<summary>Voir la réponse</summary>

Docker `output: 'standalone'`, containers, CDN assets, cache layers, monitoring


</details>

### 25. Comment gérer les race conditions avec Server Actions ?

<details>
<summary>Voir la réponse</summary>

`useTransition`, disabled state, optimistic updates, validation côté serveur


```tsx
// app/actions.ts
'use server'

export async function createUser(formData: FormData) {
  const name = formData.get('name')
  // Logique de création
  return { success: true }
}

// Utilisation
<form action={createUser}>
  <input name="name" />
  <button type="submit">Créer</button>
</form>
```

</details>
