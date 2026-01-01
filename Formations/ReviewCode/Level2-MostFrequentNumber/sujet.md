# 📝 Sujet – MostFrequentNumber

## 🎯 Objectif
Écrire un programme qui prend en entrée un tableau d'entiers et retourne le nombre le plus fréquent dans ce tableau.

## 📋 Règles
- Trouver le nombre qui apparaît le **plus souvent** dans le tableau
- En cas d'égalité, retourner **n'importe lequel** des nombres les plus fréquents
- Le tableau contient au moins un élément

## ✅ Exemples
```
[1, 3, 2, 1, 4, 1, 3, 2, 3, 3, 3, 2, 2, 2] → 3 (apparaît 5 fois)
[5, 5, 1, 1] → 5 ou 1 (les deux apparaissent 2 fois)
[7] → 7 (seul élément)
[1, 2, 3, 4, 5] → 1, 2, 3, 4 ou 5 (tous apparaissent 1 fois)
```

## ⚠️ Cas limites à considérer
- Tableau avec un seul élément
- Tableau avec tous les éléments identiques  
- Tableau avec plusieurs nombres ex-aequo
- Tableau `null` ou vide (selon les besoins métier)

## 🔧 Signature attendue (C#)
```csharp
public static int MostFrequentNumber(int[] numbers)
```