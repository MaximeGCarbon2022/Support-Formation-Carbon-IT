# 🃏 Poker Hands - Comparaison de Mains

## 🎯 Objectif

Le but est d'implémenter un programme qui lit les mains de poker pour chaque joueur, compare leurs rangs et affiche le gagnant ou s'il y a une égalité. Le programme devra respecter les règles de classement des mains du poker.

## 📖 Règles du Poker

Un jeu de poker contient **52 cartes**. Chaque carte possède une **couleur** qui peut être :

| Couleur | Nom | Notation |
|---------|-----|----------|
| ♣ | Trèfle | **C** (Clubs) |
| ♦ | Carreau | **D** (Diamonds) |
| ♥ | Cœur | **H** (Hearts) |
| ♠ | Pique | **S** (Spades) |

Chaque carte a également une **valeur** qui peut être :

| Valeur | Notation |
|--------|----------|
| 2, 3, 4, 5, 6, 7, 8, 9 | 2, 3, 4, 5, 6, 7, 8, 9 |
| 10 | **T** (Ten) |
| Valet | **J** (Jack) |
| Dame | **Q** (Queen) |
| Roi | **K** (King) |
| As | **A** (Ace) |

**As** est la carte de valeur la plus élevée et **2** la plus faible.

Les couleurs ne sont pas ordonnées pour les calculs, seules les **valeurs** des cartes sont prises en compte dans le classement des mains.

## 🏆 Classement des Mains de Poker

Voici le classement des mains de poker, **du plus faible au plus fort** :

### 1. High Card (Carte Haute)

Mains qui ne correspondent à aucune autre catégorie. Elles sont classées selon la carte la plus élevée. Si les cartes les plus élevées sont identiques, on compare les cartes suivantes, et ainsi de suite.

### 2. Pair (Paire)

Deux des cinq cartes de la main ont la même valeur. Si les deux joueurs ont une paire, on compare la valeur de la paire, puis les autres cartes si besoin.

### 3. Two Pairs (Deux Paires)

La main contient deux paires différentes. Si les deux joueurs ont deux paires, on compare la paire la plus élevée, puis la seconde paire, puis la carte restante si nécessaire.

### 4. Three of a Kind (Brelan)

Trois cartes de la main ont la même valeur. Si les deux joueurs ont un brelan, on compare la valeur des trois cartes.

### 5. Straight (Suite)

La main contient cinq cartes avec des valeurs consécutives. Les suites sont classées par la carte la plus haute.

### 6. Flush (Couleur)

La main contient cinq cartes de la même couleur. Les couleurs sont classées selon la carte la plus haute, puis la suivante si nécessaire (similaire à "High Card").

### 7. Full House (Main Pleine)

Trois cartes de la main ont la même valeur, et les deux autres forment une paire. Les Full House sont classés selon la valeur des trois cartes.

### 8. Four of a Kind (Carré)

Quatre des cinq cartes ont la même valeur. Si les deux joueurs ont un carré, on compare la valeur des quatre cartes.

### 9. Straight Flush (Quinte Flush)

Cinq cartes de la même couleur et de valeurs consécutives. Les mains sont classées par la carte la plus haute de la quinte flush.

## 📋 Description du Problème

Vous devez comparer les mains de deux joueurs pour déterminer le gagnant. Chaque ligne de l'entrée représente une partie avec deux joueurs. Les cinq premières cartes appartiennent au joueur **Black** et les cinq suivantes au joueur **White**.

## 📥 Format des données d'entrée

L'entrée se présente sous la forme :

```
Black: 2H 3D 5S 9C KD  White: 2C 3H 4S 8C AH
Black: 2H 4S 4C 2D 4H  White: 2S 8S AS QS 3S
Black: 2H 3D 5S 9C KD  White: 2C 3H 4S 8C KH
Black: 2H 3D 5S 9C KD  White: 2D 3H 5C 9S KH
```

Les cartes du joueur **Black** précèdent celles du joueur **White**.

Chaque carte est représentée par une valeur et une couleur, par exemple :
- `2H` : Deux de cœur
- `KD` : Roi de carreau

## 📤 Format des données de sortie

Le programme doit produire un résultat pour chaque ligne d'entrée, indiquant quel joueur a gagné ou s'il y a égalité. Le format de sortie attendu est :

```
White wins. - with high card: Ace
Black wins. - with full house: 4 over 2
Black wins. - with high card: 9
Tie.
```

## 🧪 Cas de Test

**Entrée Exemple**

```
Black: 2H 3D 5S 9C KD  White: 2C 3H 4S 8C AH
Black: 2H 4S 4C 2D 4H  White: 2S 8S AS QS 3S
Black: 2H 3D 5S 9C KD  White: 2C 3H 4S 8C KH
Black: 2H 3D 5S 9C KD  White: 2D 3H 5C 9S KH
```

**Sortie Attendue**

```
White wins. - with high card: Ace
Black wins. - with full house: 4 over 2
Black wins. - with high card: 9
Tie.
```

## 🚀 Instructions pour exécuter le programme

1. Placer le fichier d'entrée sous le nom `input.txt` dans le répertoire du programme.
2. Exécuter le programme, qui lira les mains depuis le fichier et produira le résultat des comparaisons.
3. Le programme affichera dans la console le joueur gagnant pour chaque partie ou indiquera une égalité.
