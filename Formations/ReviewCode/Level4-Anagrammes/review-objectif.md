# 🎯 Objectifs de review - Anagrammes

## 📊 Compétences ciblées
- ✅ **Logique algorithmique** : Identifier les bugs de fréquence vs présence
- ✅ **Analyse critique** : Détecter les algorithmes qui "semblent" corrects
- ✅ **Optimisation** : Comparer force brute O(n²) vs tri O(n log n)
- ✅ **Validation robuste** : Gestion complète des edge cases
- ✅ **Choix d'approche** : Tri vs dictionnaire selon le contexte

## 🔍 Points clés à identifier
- ❌ **Bug subtil** : `Contains()` vérifie la présence, pas la fréquence
- ❌ **Performance** : Double boucle avec `Contains()` = O(n × m)
- ❌ **Validation manquante** : NullReferenceException sur input null
- ⚠️ **Edge cases** : Casse, espaces, chaînes vides non gérés
- 💡 **Solution** : Tri + SequenceEqual ou dictionnaire de fréquences

## 🎯 Résultats attendus
À la fin de cet exercice, les consultants savent :
- Détecter des bugs algorithmiques subtils par des tests mentaux
- Différencier "présence" vs "fréquence" dans les algorithmes
- Choisir entre plusieurs approches valides selon les contraintes
- Écrire des validations robustes pour éviter les exceptions