# Questions de revue + Réponses – MostFrequentNumber Bad Version

**Code analysé :**
```csharp
public static void Run()
{
    int[] array = [1, 3, 2, 1, 4, 1, 3, 2, 3, 3, 3, 2, 2, 2];
    int m = 0;
    int c = 0;
    for (int i = 0; i < array.Length; i++)
    {
        int count = 0;
        for (int j = 0; j < array.Length; j++)
        {
            if (array[j] == array[i])
            {
                count++;
            }
        }
        if (count > c)
        {
            c = count;
            m = array[i];
        }
    }
    Console.WriteLine("Most common number is: " + m);
}
```

---

## Compréhension

### Que fait ce code ?
Il trouve le nombre qui apparaît le plus souvent dans un tableau d'entiers et l'affiche à la console.

### Quel est l'objectif des variables `m` et `c` ?
**Noms cryptiques problématiques** :
- `m` = stocke le nombre le plus fréquent trouvé jusqu'à présent
- `c` = stocke la fréquence maximale trouvée jusqu'à présent

**Problème majeur** : Ces noms sont incompréhensibles ! `mostFrequent` et `maxCount` seraient bien plus clairs.

### Comment le code identifie-t-il le nombre le plus fréquent ?
**Algorithme brute force** :
1. Pour chaque élément du tableau (boucle externe)
2. Compter combien de fois cet élément apparaît (boucle interne)
3. Si ce compte est supérieur au maximum actuel, mettre à jour `m` et `c`
4. Afficher le résultat

---

## Logique et algorithme

### Quelle est la complexité temporelle ?
**O(n²)** - très inefficace !
- Boucle externe : n itérations
- Boucle interne : n itérations pour chaque élément externe
- Au total : n × n = n² opérations

Pour un tableau de 10,000 éléments = 100,000,000 opérations !

### La double boucle est-elle justifiée ?
**Non !** On peut faire bien mieux avec une approche O(n) utilisant un dictionnaire pour compter les occurrences en une seule passe.

### Test mental : que se passe-t-il avec le tableau donné ?
**Trace partielle** :
```
i=0, array[0]=1: compte 1 dans tout le tableau → count=3, c=3, m=1
i=1, array[1]=3: compte 3 dans tout le tableau → count=5, c=5, m=3
i=2, array[2]=2: compte 2 dans tout le tableau → count=4, c reste 5
...
Résultat: m=3 (correct, 3 apparaît 5 fois)
```

### Le code gère-t-il les égalités de fréquence ?
**Oui**, mais retourne le **premier** élément rencontré avec la fréquence maximale. Ce comportement n'est pas documenté.

---

## Lisibilité et structure

### Les noms des variables sont-ils explicites ?
**Absolument pas** :
- `m` → devrait être `mostFrequentNumber`
- `c` → devrait être `maxCount`
- `i`, `j` → acceptables pour des indices
- `count` → nom correct

### Le code respecte-t-il le principe de responsabilité unique ?
**Non** : La méthode `Run()` fait à la fois :
- Le calcul du nombre le plus fréquent
- L'affichage du résultat
- La définition des données de test

### Y a-t-il des problèmes de structure ?
**Oui** :
- Tableau codé en dur dans la méthode
- Logique métier mélangée avec l'affichage
- Pas de séparation des responsabilités

---

## Robustesse et cas limites

### Que se passe-t-il si le tableau est modifié pour être vide ?
**Bug !** Si `array = []` :
- Les boucles ne s'exécutent pas
- `m` reste à 0, `c` reste à 0
- Affiche "Most common number is: 0" (incorrect)

### Que se passe-t-il si le tableau était `null` ?
**NullReferenceException** sur `array.Length` ! Le code n'est pas défensif.

### Comment le code gère-t-il les nombres négatifs ?
**Correctement** : L'algorithme fonctionne avec tous les entiers, positifs ou négatifs.

### Le code gère-t-il un tableau avec un seul élément ?
**Oui** : Retourne correctement cet élément unique.

---

## Performance et optimisations

### Peut-on éviter les calculs redondants ?
**Oui !** Le code recalcule la fréquence du même nombre plusieurs fois.

Exemple : Le nombre `1` apparaît aux indices 0, 3, 5. Le code va compter sa fréquence 3 fois identiquement.

### Y a-t-il une approche plus efficace ?
**Oui** : Utiliser un dictionnaire pour compter chaque nombre une seule fois :
- Une passe pour compter : O(n)
- Une passe pour trouver le maximum : O(n)
- Total : O(n) au lieu de O(n²)

---

## Bugs et problèmes identifiés

### Bug #1 : Tableau vide
```csharp
// Si on change array = []
// Résultat: "Most common number is: 0" (incorrect)
```

### Bug #2 : Gestion des paramètres
Le tableau est codé en dur, pas d'entrée paramétrable ou de validation.

### Bug #3 : Performance O(n²)
Algorithme très inefficace pour de gros tableaux.

### Bug #4 : Responsabilités mélangées
Calcul + affichage dans la même méthode, difficile à tester et réutiliser.

---

## Tests révélateurs

```csharp
// Test normal (avec le tableau donné)
Run() → "Most common number is: 3" ✅

// Tests qui révèlent les problèmes
// Si array = []
Run() → "Most common number is: 0" ❌ (incorrect)

// Si array = [5]  
Run() → "Most common number is: 5" ✅

// Si array = [1, 2, 1, 2]
Run() → "Most common number is: 1" ✅ (premier trouvé en cas d'égalité)
```

---

## Problèmes à corriger

### Nommage
- `m` → `mostFrequentNumber`
- `c` → `maxCount`
- Méthode `Run` → nom plus descriptif

### Robustesse
- Valider que le tableau n'est pas vide
- Gérer les cas d'erreur appropriés
- Paramétrer l'entrée au lieu de la coder en dur

### Performance
- Utiliser une approche O(n) avec dictionnaire
- Éviter les calculs redondants

### Structure
- Séparer la logique de calcul de l'affichage
- Rendre la fonction testable et réutilisable
- Extraire la définition des données de test

### Réutilisabilité
- Faire une fonction qui prend un tableau en paramètre
- Retourner un résultat au lieu d'afficher directement

**Conclusion** : Algorithme correct mais très inefficace, avec des problèmes majeurs de performance, robustesse et structure du code.