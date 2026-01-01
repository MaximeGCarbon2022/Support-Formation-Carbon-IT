# 🤖 Kata - Tondeuse Automatique

## 📋 Contexte

La société MowItNow a décidé de développer une tondeuse à gazon automatique, destinée aux surfaces rectangulaires.

## ⚙️ Fonctionnement de la tondeuse

### 📍 Position et Orientation

La tondeuse peut être programmée pour parcourir l'intégralité de la surface.

- La position de la tondeuse est représentée par une combinaison de **coordonnées (x,y)** et d'une lettre indiquant l'**orientation** selon la notation cardinale (N,E,W,S).
- La pelouse est divisée en grille pour simplifier la navigation.

### 🎮 Commandes disponibles

| Commande | Action |
|----------|--------|
| **D** | Pivote de 90° à droite |
| **G** | Pivote de 90° à gauche |
| **A** | Avance d'une case dans la direction actuelle |

### 🗺️ Contrainte de la grille

- La position directement au Nord de (x, y) est (x, y+1).
- Pour éviter que la tondeuse ne sorte de la pelouse, si une commande la ferait sortir de la grille, **la tondeuse ne bouge pas** et traite la commande suivante.

## 📥 Exemple d'entrée

```
5 5
1 2 N
GAGAGAGAA
3 3 E
AADAADADDA
```

**Explication :**
- Ligne 1 : Dimensions de la pelouse (largeur 5, hauteur 5)
- Ligne 2 : Position initiale de la tondeuse 1 (x=1, y=2, orientation=Nord)
- Ligne 3 : Séquence d'instructions pour la tondeuse 1
- Ligne 4 : Position initiale de la tondeuse 2 (x=3, y=3, orientation=Est)
- Ligne 5 : Séquence d'instructions pour la tondeuse 2

## 🎯 Objectif

Concevoir un programme qui implémente la logique décrite ci-dessus et qui teste le jeu de données ci-après.

## 📄 Format du fichier d'entrée

Le fichier suit le format suivant :

- La première ligne correspond aux coordonnées du coin supérieur droit de la pelouse, celles du coin inférieur gauche sont supposées être (0,0).
- La suite du fichier permet de piloter toutes les tondeuses qui ont été déployées.
- Chaque tondeuse a deux lignes la concernant :
  - La **première ligne** donne la position initiale de la tondeuse, ainsi que son orientation.
  - La **seconde ligne** est une série d'instructions ordonnant à la tondeuse d'explorer la pelouse.

La position et l'orientation sont fournies sous la forme de 2 chiffres et une lettre, séparés par un espace.

## 📤 Format de sortie attendu

À la fin de l'exécution du programme, le résultat de l'exécution de toutes les tondeuses doit être affiché.

Chaque tondeuse affiche sa position finale et son orientation.

## 🧪 Test

### Données d'entrée

```
5 5
1 2 N
GAGAGAGAA
3 3 E
AADAADADDA
```

### Résultat attendu

```
1 3 N
5 1 E
```

## 🚀 Instructions pour exécuter le programme

1. Placer le fichier d'entrée sous le nom `input.txt` dans le répertoire du programme.
2. Exécuter le programme, qui lira les instructions depuis le fichier et affichera les positions finales dans la console.
3. Le programme affichera la position finale de chaque tondeuse au format `x y orientation`.
