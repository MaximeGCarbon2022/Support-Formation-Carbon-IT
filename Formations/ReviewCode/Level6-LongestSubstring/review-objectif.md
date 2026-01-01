# 🎯 Objectifs de review - LongestSubstring

## 📊 Compétences ciblées
- ✅ **Algorithmes avancés** : Comprendre et améliorer le sliding window
- ✅ **Refactoring massif** : Transformer du code cryptique en code lisible
- ✅ **Nommage expressif** : Variables descriptives pour algorithmes complexes
- ✅ **Séparation des responsabilités** : Validation vs logique métier
- ✅ **Optimisation fine** : `TryGetValue` vs `ContainsKey`, `Math.Max` vs if

## 🔍 Points clés à identifier
- ❌ **Nommage catastrophique** : `x`, `y`, `dict` rendent le code illisible
- ❌ **Logique condensée** : Tout mélangé sans extraction de méthodes
- ❌ **Validation absente** : NullReferenceException sur string null
- ⚠️ **API Dictionary** : `ContainsKey + []` moins efficace que `TryGetValue`
- ⚠️ **Variables temporaires** : `len` peut être évitée avec `Math.Max`
- 💡 **Solution** : Refactoring complet avec noms expressifs + validation

## 🎯 Résultats attendus
À la fin de cet exercice, les consultants savent :
- Refactoriser du code complexe sans changer la logique
- Appliquer les principes du clean code à des algorithmes avancés
- Optimiser l'utilisation d'APIs (.NET Dictionary, Math)
- Séparer validation et logique métier même dans du code algorithmique
- Rendre du code maintenant maintenable par d'autres développeurs