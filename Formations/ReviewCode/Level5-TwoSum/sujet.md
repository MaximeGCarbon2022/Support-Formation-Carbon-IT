# 📝 Sujet – TwoSum

## 🎯 Objectif
Écrire une fonction qui, étant donné un tableau d'entiers et une valeur cible (`target`), retourne les **indices** de deux nombres dont la somme égale la cible.

## 📋 Règles
- Trouver deux indices distincts `i` et `j` tels que `numbers[i] + numbers[j] == target`
- **Contrainte** : Ne pas utiliser le même élément deux fois
- **Garantie** : Il existe exactement une solution valide

## ✅ Exemples
```
[2, 7, 11, 15], target = 9 → [0, 1] (car 2 + 7 = 9)
[3, 2, 4], target = 6 → [1, 2] (car 2 + 4 = 6)  
[3, 3], target = 6 → [0, 1] (car 3 + 3 = 6)
[-1, -2, -3, -4, -5], target = -8 → [2, 4] (car -3 + -5 = -8)
```

## ⚠️ Cas limites à considérer
- Tableau avec exactement 2 éléments
- Nombres négatifs, zéro, ou très grands
- Tableau `null` ou avec moins de 2 éléments
- Indices à retourner dans un ordre particulier ?

## 🔧 Signature attendue (C#)
```csharp
public static int[] TwoSum(int[] numbers, int target)
```
