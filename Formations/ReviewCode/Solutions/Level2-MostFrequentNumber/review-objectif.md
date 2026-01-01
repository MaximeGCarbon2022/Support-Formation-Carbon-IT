# 🎯 Objectifs de review - MostFrequentNumber

## 📊 Compétences ciblées
- ✅ **Analyse de complexité** : Identifier les algorithmes O(n²) vs O(n)
- ✅ **Structures de données** : Choisir Dictionary vs double boucle
- ✅ **Nommage expressif** : Remplacer variables cryptiques par noms clairs
- ✅ **Séparation des responsabilités** : Logique métier vs affichage
- ✅ **Gestion d'erreurs** : Validation des inputs (null, vide)

## 🔍 Points clés à identifier
- ❌ **Performance critique** : Double boucle = O(n²) très inefficace
- ❌ **Nommage catastrophique** : `m`, `c` incompréhensibles
- ❌ **Bug edge case** : Tableau vide → affiche "0" incorrectement
- ⚠️ **Structure rigide** : Array hardcodé, pas de réutilisabilité
- 💡 **Solution** : Dictionary + LINQ + validation

## 🎯 Résultats attendus
À la fin de cet exercice, les consultants savent :
- Identifier et corriger des problèmes de performance majeurs
- Choisir les bonnes structures de données (Dictionary)
- Écrire du code avec un nommage expressif
- Gérer les cas limites (null/vide) proprement