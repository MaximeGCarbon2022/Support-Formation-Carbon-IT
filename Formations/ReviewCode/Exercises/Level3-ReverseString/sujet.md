# 📝 Sujet – ReverseString

## 🎯 Objectif
Écrire une fonction qui prend une chaîne de caractères en entrée et retourne cette chaîne **inversée** (caractères dans l'ordre inverse).

## 📋 Règles
- Inverser l'ordre des caractères : le premier devient le dernier, etc.
- Conserver tous les caractères (espaces, ponctuation, caractères spéciaux)
- Gérer les cas limites appropriés

## ✅ Exemples
```
"hello" → "olleh"
"world!" → "!dlrow"
"a" → "a"
"" → ""
"Hello World" → "dlroW olleH"
```

## ⚠️ Cas limites à considérer
- Chaîne vide
- Chaîne d'un seul caractère
- Chaîne `null`
- Chaînes avec espaces et caractères spéciaux

## 🔧 Signature attendue (C#)
```csharp
public static string Reverse(string input)
```
