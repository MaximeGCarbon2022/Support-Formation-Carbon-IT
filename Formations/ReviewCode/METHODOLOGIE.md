# 📋 Guide de Code Review

## 🎯 Qu'est-ce qu'une bonne code review ?

Une bonne code review permet de :
- Détecter les bugs avant qu'ils n'atteignent la production
- Améliorer la qualité et la maintenabilité du code
- Partager les connaissances entre développeurs
- Garantir la cohérence du code dans le projet

**Principe clé** : Être constructif, pas destructif. L'objectif est d'aider, pas de critiquer.

---

## 🔍 Les 6 axes d'analyse

### 1. Correction fonctionnelle
**Questions à se poser :**
- Le code fait-il ce qu'il doit faire ?
- Y a-t-il des bugs logiques évidents ?
- Les cas limites sont-ils gérés (null, chaînes vides, listes vides, divisions par zéro) ?
- Les conditions `if/else` sont-elles correctes ?

**Exemple de bug courant :**
```csharp
// Bug : ne gère pas le cas où result est vide
if (items.Count > 0) {
    return items[0];
}
// Manque : return null; ou throw exception
```

### 2. Lisibilité et clarté
**Questions à se poser :**
- Le code est-il facile à comprendre au premier coup d'œil ?
- Les noms de variables/méthodes sont-ils explicites ?
- Y a-t-il de la complexité inutile ?
- Le code nécessite-t-il des commentaires pour être compris ?

**Bon vs Mauvais :**
```csharp
// Mauvais
var x = list.Where(i => i.p > 100).ToList();

// Bon
var expensiveProducts = products.Where(p => p.Price > 100).ToList();
```

### 3. Architecture et design
**Questions à se poser :**
- Les responsabilités sont-elles bien séparées ?
- Une méthode fait-elle plusieurs choses à la fois ?
- Y a-t-il du code dupliqué ?
- Le code respecte-t-il les principes du projet ?

**Mauvais exemple :**
```csharp
// Méthode qui mélange validation, calcul ET affichage
public void ProcessOrder(Order order) {
    if (order == null) return;
    var total = order.Items.Sum(i => i.Price);
    Console.WriteLine($"Total: {total}");
    SaveToDatabase(order);
}
```

### 4. Testabilité
**Questions à se poser :**
- Le code peut-il être testé unitairement ?
- Les dépendances sont-elles injectables ?
- La logique métier est-elle isolée de l'infrastructure ?

**Astuce** : Si une méthode fait des appels directs à `Console.WriteLine`, `DateTime.Now`, ou `new HttpClient()`, elle sera difficile à tester.

### 5. Performance
**Questions à se poser :**
- Y a-t-il des inefficacités évidentes (boucles imbriquées inutiles, requêtes N+1) ?
- La complexité algorithmique est-elle acceptable ?
- Les ressources sont-elles bien libérées ?

**Attention** : Ne pas optimiser prématurément. La lisibilité prime sur la performance sauf si un vrai problème est identifié.

### 6. Sécurité et robustesse
**Questions à se poser :**
- Les entrées utilisateur sont-elles validées ?
- Y a-t-il des risques d'injection SQL, XSS ?
- Les erreurs sont-elles gérées correctement ?
- Les données sensibles sont-elles protégées ?

---

## ✅ Méthode en 4 étapes

### Étape 1 : Vue d'ensemble (30 secondes)
- Quel est le but de ce code ?
- Quelle est la taille du changement ?
- Est-ce que la structure générale a du sens ?

### Étape 2 : Analyse détaillée (le cœur de la review)
- Lire le code ligne par ligne
- Tester mentalement avec des valeurs concrètes
- Vérifier les 6 axes ci-dessus

### Étape 3 : Tests mentaux
Simuler l'exécution avec des cas spécifiques :
- Cas nominal (le chemin heureux)
- Cas limites (null, vide, zéro, négatif)
- Cas d'erreur

### Étape 4 : Feedback constructif
- Pointer les problèmes avec des exemples
- Proposer des solutions quand c'est possible
- Prioriser : bugs critiques > design > suggestions

---

## 💬 Comment donner un bon feedback

### Structure d'un bon commentaire
```
[Catégorie] Description du problème

Pourquoi c'est un problème
Suggestion de correction (optionnel)
```

**Exemples :**

✅ **Bon feedback**
```
[Bug] Cette méthode retourne une chaîne vide pour les nombres
non-divisibles par 3 ou 5, au lieu de retourner le nombre lui-même.

Ligne 18 : Il manque un cas par défaut.
Suggestion : Ajouter `return result.Length > 0 ? result : number.ToString();`
```

❌ **Mauvais feedback**
```
Ce code est nul, ça marche pas
```

### Ton à adopter
- **Positif** : "Ce code pourrait être amélioré en..." plutôt que "C'est mal fait"
- **Objectif** : Se concentrer sur les faits, pas sur la personne
- **Pédagogique** : Expliquer le "pourquoi" derrière chaque remarque
- **Collaboratif** : Proposer des solutions, pas juste critiquer

---

## 🎯 Questions systématiques à se poser

**Compréhension**
- Est-ce que je comprends ce que fait chaque ligne ?
- Si non, est-ce un problème de code ou est-ce que je dois creuser le contexte ?

**Fonctionnalité**
- Que se passe-t-il si l'entrée est null ?
- Que se passe-t-il si la liste est vide ?
- Que se passe-t-il en cas d'erreur réseau/base de données ?

**Maintenance**
- Si je dois modifier ce code dans 6 mois, est-ce que je vais comprendre ?
- Si un bug survient, est-ce que je peux facilement le déboguer ?
- Est-ce que je peux ajouter une fonctionnalité sans tout casser ?

**Tests**
- Quels sont les cas de test critiques ?
- Est-ce que ces tests existent ?
- Peut-on écrire facilement ces tests ?

---

## 🚫 Pièges à éviter

### 1. Réécrire à sa façon
Le code fonctionne mais "vous l'auriez fait autrement" → Ce n'est pas une raison valable pour refuser.

### 2. Pinailler sur le style
Si le projet a un linter/formatter, laissez-le gérer les espaces et accolades.

### 3. Demander la perfection
Un code "assez bon" qui fonctionne vaut mieux qu'un code "parfait" qui n'arrive jamais.

### 4. Ignorer le contexte
Parfois, du code "imparfait" est justifié par des contraintes (deadline, legacy, performance).

### 5. Faire la review trop tard
Reviewer un fichier de 2000 lignes est inefficace. Les reviews doivent être fréquentes et petites.

---

## 💡 Conseils pratiques

### Pour le reviewer
- Prendre le temps nécessaire (ne pas bâcler)
- Tester localement en cas de doute
- Demander des clarifications plutôt que supposer
- Reconnaître le bon code aussi ("J'aime bien cette approche")

### Pour l'auteur du code
- Faire des petites pull requests
- Ajouter une description claire du changement
- Répondre aux commentaires de manière constructive
- Ne pas prendre les remarques personnellement

### Pour l'équipe
- Définir des standards clairs
- Utiliser des checklists pour les reviews
- Faire des reviews régulières et rapides
- Considérer la review comme un moment d'apprentissage

---

## 📊 Priorisation des problèmes

### 🔴 Critique (bloquant)
- Bugs qui empêchent le fonctionnement
- Failles de sécurité
- Perte de données possible

### 🟠 Important (à corriger)
- Code non maintenable
- Violations des principes du projet
- Performance dégradée significativement

### 🟡 Suggestion (à discuter)
- Améliorations possibles
- Optimisations mineures
- Préférences de style

---

## 🎓 En résumé

Une bonne code review, c'est :
1. **Systématique** : Utiliser une grille d'analyse
2. **Bienveillant** : Aider, pas démolir
3. **Pragmatique** : Prioriser les vrais problèmes
4. **Pédagogique** : Expliquer le pourquoi
5. **Rapide** : Ne pas bloquer l'équipe

**Objectif final** : Livrer du code de qualité tout en faisant progresser l'équipe.
