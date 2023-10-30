using System.Diagnostics;

namespace ThuillierColinProject;

using ServiceLogs;
using Core;
using Models;

public class Program
{
    public static void Main(string[] args)
    {
        Programe();
    }

    public static void Programe()
    {
        try
        {
            BDD bdd = BDD.GetInstance(); // Creation de l'objet qui va stocker les données de la base de données

            CoreSingleton
                core = CoreSingleton.GetInstance(); // Creation de l'objet qui regroupe les objets de la couche core

            CoreModels models = core.CoreModels; // Récuperation de l'objet qui permet d'utiliser les class de models
            CoreGestion
                gestion = core.CoreGestion; // Récuperation de l'objet qui permet d'utiliser les class de gestion
            CoreInteraction
                interaction =
                    core.CoreInteraction; // Récuperation de l'objet qui permet d'utiliser les class d'interaction

            /****************** Creation de la base de données si elle n'existe pas ******************/
            models.CreationBDD(); // Si les fichiers n'existent pas, ils sont créés sinon rien ne se passe
            Console.Clear();

            /****************** Récuperation des données ******************/

            // Recuperation des articles et stockage dans le singleton
            bdd.articles = models.RecupererArticle();

            // Recuperation des administrateurs et stockage dans le singleton
            bdd.administrateurs = models.RecupererAdministrateur();

            // Recuperation des acheteurs et stockage dans le singleton
            bdd.acheteurs = models.RecupererAcheteurs();

            // Recuperation des articles achetés et stockage dans le singleton
            bdd.articleAchetes = models.RecupererArticleAchetes();


            Console.WriteLine("Bienvenue dans l'application de gestion de stock");

            /****************** Choix du profil ******************/
            bdd.profil = gestion.ChoixProfil(); 
            
            /****************** Redirection vers le profil ******************/
            // 1 pour administrateur et 2 pour acheteur
            if (BDD.GetInstance().profil == '1')
            {
                gestion.ProgramAdministrateur();
            }
            else if (BDD.GetInstance().profil == '2')
            {
                gestion.ProgramAcheteur();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}