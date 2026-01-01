# 📝 Sujet – LongestSubstringWithoutRepeatingCharacters

## 🎯 Objectif
Écrire une méthode qui retourne la **longueur de la plus longue sous-chaîne** d'une chaîne de caractères donnée, **sans caractère répété**.

## 📋 Définition
- Trouver la plus longue séquence de **caractères consécutifs** sans aucune répétition
- Retourner la **longueur** de cette sous-chaîne (pas la sous-chaîne elle-même)

## ✅ Exemples détaillés
```
"abcabcbb" → "abc" (longueur 3)
"bbbbb" → "b" (longueur 1)  
"pwwkew" → "wke" (longueur 3)
"" → longueur 0
"a" → "a" (longueur 1)
"abcdef" → "abcdef" (longueur 6)
```

## ⚠️ Cas limites à considérer
- Chaîne vide ou `null`
- Chaîne d'un seul caractère
- Chaîne avec tous les caractères identiques
- Chaîne sans aucune répétition

## 🔧 Signature attendue (C#)
```csharp
public static int LengthOfLongestSubstring(string s)
```
