# 🎯 Objectifs de review - TwoSum

## 📊 Compétences ciblées
- ✅ **Optimisation algorithmique** : Transformer O(n²) en O(n) avec HashMap
- ✅ **Structures de données avancées** : Utilisation intelligente du Dictionary
- ✅ **Early return** : Reconnaître les opportunités d'optimisation
- ✅ **Gestion d'erreur** : Choix entre exceptions vs valeurs nullables
- ✅ **API design** : Signature de méthode claire et utilisable

## 🔍 Points clés à identifier
- ❌ **Performance critique** : Double boucle avec comparaisons redondantes
- ❌ **Bug potentiel** : Continue à chercher après avoir trouvé la solution
- ❌ **Nommage cryptique** : `n`, `t`, `i1`, `i2` incompréhensibles
- ❌ **Validation manquante** : NullReferenceException sur array null
- ❌ **Retour incorrect** : `[0, 0]` si pas de solution (indices valides!)
- 💡 **Solution** : HashMap + early return + validation + exception explicite

## 🎯 Résultats attendus
À la fin de cet exercice, les consultants savent :
- Transformer un algorithme naïf en solution optimisée avec HashMap
- Utiliser la technique du "complément" pour des problèmes de recherche
- Concevoir des APIs robustes avec gestion d'erreur appropriée
- Reconnaître les patterns d'optimisation classiques (lookup tables)