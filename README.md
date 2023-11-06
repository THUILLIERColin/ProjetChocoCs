# Projet de Gestion de Commande de Chocolat

Ce projet vise à créer une application de gestion de commande de chocolat avec les fonctionnalités suivantes : 

- Création et gestion d'une base de données en format JSON.
- Identification des utilisateurs comme Administrateurs ou Utilisateurs standards.
- Fonctionnalités spécifiques pour les Administrateurs et les Utilisateurs.
- Journalisation de toutes les actions effectuées dans l'application.

## Prérequis

Pour exécuter cette application, vous aurez besoin de :
- Un environnement de développement compatible avec .NET Core.
- Les fichiers JSON de la base de données seront créés au premier lancement de l'application

## Installation

1. Clonez ce dépôt GitHub sur votre machine locale.
2. Ouvrez le projet dans votre environnement de développement préféré. (Rider ou VisualStudio)

## Lancement de l'Application

L'application doit être vide lors du premier démarrage et créera la base de données ainsi que l'initialisation de toutes les valeurs dans cette base de données. Toutes les actions de création de la base de données seront visibles au démarrage et dans les logs.

Une fois l'installation terminée, l'écran de la console sera vidé pour afficher les options suivantes :
1. Utilisateur
2. Administrateur

## Utilisation de l'Application

### Administrateur

- L'administrateur doit se connecter avec un Login et un mot de passe.
- Le mot de passe doit contenir au moins 6 caractères alphanumériques et un caractère spécial.
- Seul l'administrateur peut saisir des articles.
- Seul l'administrateur peut créer un nouvel administrateur.
- Seul l'administrateur peut créer un fichier texte (format facture) donnant la somme des articles vendus.
- Seul l'administrateur peut créer un fichier texte (format facture) donnant la somme des articles vendus par acheteur.
- Seul l'administrateur peut créer un fichier texte (format facture) donnant la somme des articles vendus par date d'achat.

### Utilisateur

- L'utilisateur n'a pas besoin de se connecter.
- L'utilisateur doit saisir son nom, prénom, adresse et téléphone avant de pouvoir ajouter un article.
- L'utilisateur doit choisir un article à la fois et saisir la quantité de chaque article.
- Tant que l'utilisateur n'a pas terminé sa commande, il peut ajouter de nouveaux articles.
- Pour terminer sa commande, l'utilisateur doit appuyer sur la touche 'F'.
- Pour afficher le prix de sa commande en cours, l'utilisateur doit appuyer sur la touche 'P'.
- Une fois que l'utilisateur a terminé sa commande, un fichier texte est généré avec un récapitulatif de ses articles, le prix de chaque article et le prix total. Le fichier est sauvegardé dans un dossier à son nom et porte le format suivant : "Nom-Prenom-Jour-Mois-Annee-Heure-Minute.txt".

## Structure du Projet

Le projet est organisé en plusieurs composants :

- **Models** : Contient les modèles de données pour les administrateurs, les acheteurs, les articles et les articles achetés.
- **Services** : Contient les services d'interaction avec les fichiers (écriture, lecture), la gestion des différentes listes (Administrateurs, Acheteurs, Articles) et les services de journalisation.
- **Projet.Core** : Contient les différents appels vers les services.
- **Application** : Contient l'application console ou Visuel.

## Contrôle des Données

Toutes les données saisies sont contrôlées pour garantir leur validité. Les utilisateurs ne peuvent pas saisir de données incorrectes.

## Exemple de Journalisation

Chaque action effectuée dans l'application est journalisée de manière lisible. Par exemple :
- Un utilisateur ajoute un chocolat à sa liste, le log doit être : "2023-11-06 10:13:07.7801|INFO|ThuillierColinProject.ServiceLogs.LogClass|[msg]"
- msg = L'utilisateur a acheté l'article Toblerone Chocolat Noir 100g en 3 exemplaire(s)

---

  Bonne utilisation de l'application de gestion de commande de chocolat !
