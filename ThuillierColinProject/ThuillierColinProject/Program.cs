namespace ThuillierColinProject;

using ServiceLogs;
using Core;
using Models;

public class Program
{
    public static void Main(string[] args)
    {
        Programe();
        // TestFacture();
     }

    public static void Programe()
    {
        try
        {
            BDD bdd = BDD.GetInstance(); // Creation de l'objet qui va stocker les données de la base de données
            
            CoreSingleton core = CoreSingleton.GetInstance(); // Creation de l'objet qui regroupe les objets de la couche core

            CoreModels models = core.coreModels; // Récuperation de l'objet qui permet d'utiliser les class de models
            CoreGestion gestion = core.coreGestion; // Récuperation de l'objet qui permet d'utiliser les class de gestion
            CoreInteraction interaction = core.coreInteraction; // Récuperation de l'objet qui permet d'utiliser les class d'interaction

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
            if(BDD.GetInstance().profil=='1')
            {
                gestion.ProgramAdministrateur();
            }
            else if(BDD.GetInstance().profil=='2')
            {
                gestion.ProgramAcheteur();
            }
            else
            {
                SingletonLog.GetInstance().Log("Erreur lors de la redirection du profil", LogClass.TypeMessage.Error); 
                Console.WriteLine("Erreur lors du choix du profil");
            }
            

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void Test1()
    {
        /********** Utilisation de la classe CoreModels qui permet d'utiliser les class de models **********/
        CoreModels models = new CoreModels();
        
        /********* Test de creation par l'utilisateur *********/
        // Console.WriteLine("Creation d'un administrateur : " + models.CreationAdministrateur());
        // Console.WriteLine("Creation d'un acheteur : " + models.CreationAcheteur());
        // Console.WriteLine("Creation d'un article : " + models.AjouterArticle());
        
        /********* Test de creation de la base de données *********/
        // Console.WriteLine("Creation de la base de données : " + models.CreationBDD());
        
        /********* Test de recuperation des données *********/
        List<Article> articles = models.RecupererArticle();
        List<Acheteurs> acheteurs = models.RecupererAcheteurs();
        List<Administrateur> administrateurs = models.RecupererAdministrateur();
    }
    
    public static void TestFacture()
    {
        BDD bdd = BDD.GetInstance(); // Creation de l'objet qui va stocker les données de la base de données
            
        CoreSingleton core = CoreSingleton.GetInstance(); // Creation de l'objet qui regroupe les objets de la couche core

        CoreModels models = core.coreModels; // Récuperation de l'objet qui permet d'utiliser les class de models
        CoreGestion gestion = core.coreGestion; // Récuperation de l'objet qui permet d'utiliser les class de gestion
        CoreInteraction interaction = core.coreInteraction; // Récuperation de l'objet qui permet d'utiliser les class d'interaction

        // Recuperation des articles et stockage dans le singleton
        BDD.GetInstance().articles = models.RecupererArticle();
            
        // Recuperation des administrateurs et stockage dans le singleton
        BDD.GetInstance().administrateurs = models.RecupererAdministrateur();
            
        // Recuperation des acheteurs et stockage dans le singleton
        BDD.GetInstance().acheteurs = models.RecupererAcheteurs();
            
        // Recuperation des articles achetés et stockage dans le singleton
        BDD.GetInstance().articleAchetes = models.RecupererArticleAchetes();
        
        interaction.CreerFichierFactureArticle();
    }
}

