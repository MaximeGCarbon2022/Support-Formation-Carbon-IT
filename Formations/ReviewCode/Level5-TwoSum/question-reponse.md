# Questions de revue + Réponses – TwoSum Bad Version

**Code analysé :**
```csharp
public static int[] Run(int[] n, int t)
{
    int i1 = 0;
    int i2 = 0;
    for (int i = 0; i < n.Length; i++)
    {
        for (int j = 0; j < n.Length; j++)
        {
            if (i != j && n[i] + n[j] == t)
            {
                i1 = i;
                i2 = j;
            }
        }
    }
    return [i1, i2];
}
```

---

## Compréhension

### Que fait ce code ?
Il tente de trouver deux nombres dans un tableau dont la somme égale une valeur cible, en utilisant une approche brute force avec double boucle.

### Que signifient les variables `n`, `t`, `i1`, `i2` ?
**Noms cryptiques problématiques** :
- `n` = le tableau d'entrée (nom incompréhensible)
- `t` = la valeur cible (target) 
- `i1`, `i2` = les indices des deux nombres trouvés
- **Problème majeur** : Impossible de comprendre le code sans deviner !

### Quelle est la logique générale ?
**Algorithme brute force** :
1. Parcourir chaque élément `i`
2. Pour chaque `i`, parcourir chaque élément `j`
3. Si `i ≠ j` et `n[i] + n[j] == t`, sauvegarder les indices
4. Retourner les derniers indices trouvés

---

## Logique et algorithme

### Le code fonctionne-t-il correctement ?
**Partiellement** :
- ✅ Trouve une solution si elle existe
- ❌ **Bug majeur** : Continue à chercher après avoir trouvé une solution
- ❌ Peut écraser la première solution trouvée avec une solution plus tardive
- ❌ Retourne `[0, 0]` si aucune solution (peut être confondu avec une vraie solution)

### Test mental : `Run([2, 7, 11, 15], 9)` - que se passe-t-il ?
**Trace d'exécution** :
```
i=0, j=1: n[0]+n[1] = 2+7 = 9 ✅ → i1=0, i2=1
i=0, j=2: 2+11 = 13 ≠ 9
i=0, j=3: 2+15 = 17 ≠ 9
i=1, j=0: 7+2 = 9 ✅ → i1=1, i2=0 (écrase la première solution!)
...
Résultat final: [1, 0] au lieu de [0, 1]
```

### Cas critique : que retourne `Run([1, 2, 3], 10)` ?
**Aucune solution possible** :
- Les boucles parcourent tout sans trouver de somme = 10
- `i1` et `i2` restent à `0`
- Retourne `[0, 0]` qui pourrait être interprété comme "solution aux indices 0 et 0"
- **Très trompeur !**

### Quelle est la complexité temporelle ?
**O(n²)** :
- Boucle externe : n itérations
- Boucle interne : n itérations pour chaque élément externe
- Au total : n × n = n² opérations
- **Très inefficace** pour de gros tableaux !

---

## Lisibilité et structure

### Le code est-il facile à lire ?
**Non** :
- Noms de variables cryptiques (`n`, `t`, `i1`, `i2`)
- Intention du code difficile à deviner
- Pas de commentaires explicatifs
- Logic flow peu claire (continue après avoir trouvé)

### Les noms respectent-ils les conventions C# ?
**Non** :
- `n` → devrait être `numbers` ou `array`
- `t` → devrait être `target` 
- `i1`, `i2` → devraient être `firstIndex`, `secondIndex`
- Méthode `Run` → nom générique, devrait être `FindTwoSum`

### Y a-t-il des problèmes de structure ?
**Oui** :
- Pas de early return quand solution trouvée
- Variables `i1`, `i2` réécrites inutilement
- Logique de "dernière solution trouvée" non intentionnelle

---

## Robustesse et cas limites

### Que se passe-t-il avec `Run(null, 5)` ?
**NullReferenceException** sur `n.Length` ! Aucune validation d'entrée.

### Que se passe-t-il avec un tableau vide `Run([], 5)` ?
Les boucles ne s'exécutent pas, retourne `[0, 0]` (incorrect).

### Comment distinguer "pas de solution" de "solution aux indices [0,0]" ?
**Impossible !** C'est un défaut de conception majeur :
```csharp
Run([5, 0], 5) → [0, 1] (vraie solution)
Run([1, 2], 10) → [0, 0] (pas de solution, mais même format!)
```

### Cas avec doublons : `Run([3, 3], 6)` ?
**Fonctionne par chance** :
- `i=0, j=1`: 3+3=6 ✅ → solution trouvée
- Condition `i != j` évite d'utiliser le même élément deux fois

---

## Bugs identifiés

### Bug #1 : Écrasement de solution
```csharp
// Si plusieurs solutions existent, garde seulement la dernière trouvée
Run([1, 2, 3, 4], 5) 
// Solutions possibles: [0,3] et [1,2]
// Retourne la dernière trouvée lors du parcours
```

### Bug #2 : Valeur de retour trompeuse
```csharp
// Impossible de distinguer "pas de solution" de "solution [0,0]"
Run([5, -5], 0) → [0, 1] (vraie solution)
Run([1, 2], 10) → [0, 0] (pas de solution, mais format identique)
```

### Bug #3 : Aucune validation
```csharp
Run(null, 5) → NullReferenceException
Run([], 5) → [0, 0] (incorrect)
```

---

## Problèmes à corriger

### Nommage
- Utiliser des noms explicites : `numbers`, `target`, `firstIndex`, `secondIndex`
- Renommer la méthode : `FindTwoSum` ou `GetTwoSumIndices`

### Logique
- Arrêter la recherche dès qu'une solution est trouvée (early return)
- Gérer le cas "aucune solution" avec une valeur de retour claire

### Robustesse
- Valider les paramètres d'entrée (`null`, tableau trop petit)
- Documenter le comportement attendu

### Performance
- L'algorithme O(n²) pourrait être optimisé en O(n) avec une HashMap
- Mais d'abord corriger la logique de base !

---

## Tests révélateurs

```csharp
// Tests qui montrent les bugs
Run([2, 7, 11, 15], 9) → Résultat imprévisible selon l'ordre de découverte
Run([1, 2, 3], 10) → [0, 0] ❌ (pas de solution mais format trompeur)
Run(null, 5) → Exception ❌ 
Run([], 5) → [0, 0] ❌ (incorrect)
Run([0, 5], 5) → [0, 1] mais indistinguable du cas "pas de solution"
```

**Conclusion** : Code avec logique partiellement correcte mais bugs majeurs dans la gestion des cas limites et la communication du résultat.