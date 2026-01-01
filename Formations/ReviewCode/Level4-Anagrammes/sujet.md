# 📝 Sujet – Anagrammes

## 🎯 Objectif
Écrire une fonction qui détermine si deux chaînes de caractères sont des **anagrammes** l'une de l'autre.

**Définition :** Deux chaînes sont des anagrammes si elles contiennent exactement les mêmes lettres, en quantité identique, peu importe l'ordre.

## ✅ Exemples de cas valides
```
"listen" et "silent" → true
"anagram" et "nagaram" → true  
"evil" et "vile" → true
```

## ❌ Exemples de cas invalides
```
"hello" et "world" → false
"Listen" et "silent" → false (sensible à la casse)
"a" et "aa" → false (quantités différentes)
"race" et "care " → false (espace en plus)
```

## ⚠️ Cas limites à gérer
```
"" et "" → true (chaînes vides)
null et "test" → false
"abc" et null → false
```

## 📋 Contraintes techniques
- Sensible à la casse et aux espaces
- Ne pas utiliser de librairie externe dédiée aux anagrammes
- Gérer les cas `null` et chaînes vides

## 🔧 Signature attendue (C#)
```csharp
public static bool AreAnagrams(string s1, string s2)
```