# 🎾 Kata Tennis

## 🎯 Objectif

Modéliser un match de tennis entre deux joueurs.

Le programme doit permettre :
- De faire gagner des points à l'un des deux joueurs.
- De déterminer et afficher le score.
- De fournir le statut actuel du jeu en cours (ex: 0-0, 15-0, Deuce, Avantage).
- De donner le statut du match (ex: En cours, Player 1 gagne, Player 2 gagne).

## 📥 Exemple d'entrée

Le programme lit une séquence de points depuis un fichier d'entrée.

**Format du fichier input.txt :**

```
nom_du_joueur1
nom_du_joueur2
1
2
1
1
1
2
1
1
```

Chaque ligne après les noms de joueurs représente un point gagné :
- **1** = Point pour le joueur 1
- **2** = Point pour le joueur 2

**Exemple complet :**

Pour simuler le score de l'exemple 1 `(6-1) (7-5) (1-0)` avec le jeu en cours à `15-30`, le fichier d'entrée contiendrait la séquence complète de tous les points joués depuis le début du match.

## 📊 Exemples de sortie

**Exemple 1**

```
Player 1 : nom_du_joueur1
Player 2 : nom_du_joueur2
Score : (6-1) (7-5) (1-0)
Current game status : 15-30
Match Status : in progress
```

**Exemple 2**

```
Player 1 : nom_du_joueur1
Player 2 : nom_du_joueur2
Score : (6-1) (7-5) (0-0)
Current game status : deuce
Match Status : in progress
```

**Exemple 3**

```
Player 1 : nom_du_joueur1
Player 2 : nom_du_joueur2
Score : (6-1) (7-5) (0-0)
Current game status : advantage
Match Status : in progress
```

**Exemple 4**

```
Player 1 : nom_du_joueur1
Player 2 : nom_du_joueur2
Score : (6-1) (7-5) (6-0)
Match Status : Player 1 wins
```

**Exemple 5**

```
Player 1 : nom_du_joueur1
Player 2 : nom_du_joueur2
Score : (6-1) (7-5) (2-6) (6-7) (4-6)
Match Status : Player 2 wins
```

## 📖 Règles du Tennis

### 🏆 Gagner un Jeu

Dans un jeu standard (hors tie-break), chaque point augmente le score du joueur selon cette séquence :

```
0 → 15 → 30 → 40
```

Si un joueur atteint 40 et marque un point, il gagne le jeu.

### ⚖️ Deuce (Égalité à 40-40)

- Si les deux joueurs ont 40 points, le score est **deuce** (égalité).
- Le joueur qui marque ensuite obtient l'**avantage**.
- Si ce joueur marque encore un point, il gagne le jeu.
- Si l'autre joueur marque, l'avantage est annulé et on revient à **deuce**.

### 🎯 Jeu Décisif (Tie-break)

Dans un tie-break, les points sont comptés sous forme d'entiers (1, 2, 3…) plutôt que selon la séquence classique (0, 15, 30, 40).

**Conditions pour gagner un tie-break :**
- Marquer au moins 7 points.
- Avoir 2 points d'avance sur l'adversaire.

### 📦 Gagner un Set

Un joueur doit remplir les **deux conditions** suivantes pour gagner un set :

1. Remporter **au moins 6 jeux**.
2. Avoir une **avance de 2 jeux** sur l'adversaire.

#### 🔄 Tie-break dans un Set

Si les deux joueurs sont à 6 jeux partout, un tie-break est joué. Le vainqueur du tie-break remporte le set.

### 🏅 Gagner le Match

Le premier joueur à gagner **3 sets** remporte le match.
