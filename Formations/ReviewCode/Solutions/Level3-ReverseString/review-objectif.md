# 🎯 Objectifs de review - ReverseString

## 📊 Compétences ciblées
- ✅ **Performance des strings** : Comprendre l'immutabilité et ses impacts
- ✅ **Optimisation mémoire** : StringBuilder vs concaténation vs char array
- ✅ **Gestion des types** : Validation null/vide appropriée
- ✅ **Lisibilité** : Nommage expressif des variables
- ✅ **Choix d'algorithme** : Évaluer différentes approches (performance vs lisibilité)

## 🔍 Points clés à identifier
- ❌ **Piège performance** : `string +=` crée un nouvel objet à chaque itération → O(n²)
- ❌ **Gestion d'erreur** : NullReferenceException sur input null
- ⚠️ **Nommage cryptique** : `s`, `r` peu expressifs
- ⚠️ **Edge case** : Pas d'optimisation pour chaînes courtes
- 💡 **Solution** : Char array + validation + noms expressifs

## 🎯 Résultats attendus
À la fin de cet exercice, les consultants savent :
- Reconnaître les pièges de performance avec l'immutabilité des strings
- Choisir la bonne approche d'optimisation selon le contexte
- Valider correctement les inputs pour éviter les exceptions
- Écrire du code robuste et performant pour des opérations simples