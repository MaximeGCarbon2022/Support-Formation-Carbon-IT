# Questions de revue + Réponses – FizzBuzz Bad Version

**Code analysé :**
```csharp
public static void Run()
{
    for (int i = 1; i <= 100; i++)
    {
        string result = "";
        if (i % 3 == 0)
        {
            result += "Fizz";
        }
        if (i % 5 == 0)
        {
            result += "Buzz";
        }
        Console.WriteLine(result);
    }
}
```

---

## Compréhension

### Que fait ce code ?
Il parcourt les nombres de 1 à 100 et tente d'afficher "Fizz" pour les multiples de 3, "Buzz" pour les multiples de 5, "FizzBuzz" pour les multiples des deux, et le nombre sinon.

### Est-ce qu'il respecte bien les règles classiques du FizzBuzz ?
**Partiellement** :
- ✅ "Fizz" pour multiples de 3
- ✅ "Buzz" pour multiples de 5  
- ✅ "FizzBuzz" pour multiples de 3 ET 5
- ❌ **BUG MAJEUR** : Affiche des lignes vides au lieu des nombres normaux !

### Test mental : Que va afficher ce code pour 1, 7, et 13 ?
**Résultat désastreux** :
- `1` → `result = ""` → affiche une ligne vide
- `7` → `result = ""` → affiche une ligne vide  
- `13` → `result = ""` → affiche une ligne vide

Au lieu d'afficher "1", "7", "13" !

---

## Logique et algorithme

### Que se passe-t-il quand un nombre n'est divisible ni par 3 ni par 5 ?
**C'est le bug principal !** La variable `result` reste `""` (chaîne vide), donc `Console.WriteLine("")` affiche une ligne vide au lieu du nombre.

### Pourquoi utilise-t-on une concaténation (`+=`) plutôt qu'un `if/else if` ?
**Stratégie intelligente** pour construire "FizzBuzz" automatiquement :
- Si divisible par 3 → ajoute "Fizz"
- Si divisible par 5 → ajoute "Buzz"  
- Si divisible par 3 ET 5 → obtient "FizzBuzz"

**Mais il manque le cas "ni l'un ni l'autre" !**

### Trace manuelle pour `i = 7` :
**Étape par étape** :
```
i = 7
result = ""
7 % 3 == 0 ? Non → result reste ""
7 % 5 == 0 ? Non → result reste ""  
Console.WriteLine("") → affiche ligne vide
```

Devrait afficher "7" !

### La logique de concaténation fonctionne-t-elle pour le cas `i = 15` ?
**Oui** :
```
i = 15
result = ""
15 % 3 == 0 ? Oui → result = "Fizz"
15 % 5 == 0 ? Oui → result = "FizzBuzz"
Console.WriteLine("FizzBuzz")
```

C'est le seul cas où la logique fonctionne parfaitement.

---

## Lisibilité et structure

### Le code mélange-t-il plusieurs responsabilités ?
**Oui !** La méthode fait à la fois :
- Logique métier (déterminer Fizz/Buzz/nombre)
- Logique d'affichage (Console.WriteLine)
- Contrôle de flux (boucle 1 à 100)

### Le nom des variables est-il suffisamment explicite ?
`result` est acceptable, mais `output` ou `fizzBuzzResult` seraient plus clairs. Les noms `i` dans une boucle simple sont standards.

### Y a-t-il des problèmes de structure ?
- Boucle et logique métier mélangées
- Affichage directement intégré
- Pas de séparation des préoccupations
- Code difficile à tester unitairement

---

## Robustesse et cas limites

### Quels nombres vont créer des lignes vides ?
**Tous les nombres non-multiples de 3 ou 5** :
- 1, 2, 4, 7, 8, 11, 13, 14, 16, 17, 19, 22, 23, 26, 28, 29...
- **Soit environ 53% des nombres !** Catastrophique !

### Quels cas de test essentiels révèlent le bug ?
**Tests critiques** :
- `3` → "Fizz" ✅
- `5` → "Buzz" ✅  
- `15` → "FizzBuzz" ✅
- **`1` → ligne vide ❌ (devrait être "1")**
- **`7` → ligne vide ❌ (devrait être "7")**
- **`13` → ligne vide ❌ (devrait être "13")**

### Y a-t-il des valeurs hardcodées qui limitent la réutilisabilité ?
**Oui** : 1, 100, 3, 5 sont tous en dur. Impossible de :
- Changer les bornes (ex: 1 à 50)
- Modifier les règles (ex: multiples de 4 et 7)
- Réutiliser la logique ailleurs

---

## Performance et optimisation

### Y a-t-il des inefficacités dans le code ?
**Non** du point de vue performance - la logique est simple et efficace O(n). Le problème est purement fonctionnel.

### La variable `result` est-elle bien utilisée ?
**Partiellement** : Elle fonctionne bien pour construire "Fizz", "Buzz", et "FizzBuzz", mais ne gère pas le cas par défaut.

---

## Bugs identifiés

### Bug #1 : Cas par défaut manquant
```csharp
// Pour tous les nombres non-multiples de 3 ou 5
// result reste "" → affiche ligne vide au lieu du nombre
```

### Bug #2 : Logique incomplète
Le code ne couvre que 47% des cas (multiples de 3 ou 5) et échoue sur 53% des cas.

### Bug #3 : Responsabilités mélangées
Impossible de tester la logique FizzBuzz séparément de l'affichage.

---

## Tests révélateurs

```csharp
// Tests qui fonctionnent
Run() pour i=3 → "Fizz" ✅
Run() pour i=5 → "Buzz" ✅
Run() pour i=15 → "FizzBuzz" ✅

// Tests qui révèlent le bug
Run() pour i=1 → ligne vide ❌
Run() pour i=2 → ligne vide ❌  
Run() pour i=4 → ligne vide ❌
Run() pour i=7 → ligne vide ❌
Run() pour i=8 → ligne vide ❌
```

---

## Problèmes à corriger

### Logique algorithmique
- Ajouter le cas par défaut : si `result` est vide, afficher le nombre
- Compléter la logique pour couvrir 100% des cas

### Structure du code
- Séparer la logique FizzBuzz de l'affichage
- Rendre la fonction testable unitairement
- Extraire une méthode pure

### Réutilisabilité
- Paramétrer les bornes (1 à 100)
- Permettre de modifier les règles (3, 5)
- Retourner des valeurs au lieu d'afficher directement

### Testabilité
- Créer une fonction qui retourne une string
- Permettre de tester chaque cas individuellement

**Conclusion** : Logique partiellement correcte avec un bug majeur qui rend le programme inutilisable pour plus de la moitié des cas. L'approche par concaténation est intelligente mais incomplète.