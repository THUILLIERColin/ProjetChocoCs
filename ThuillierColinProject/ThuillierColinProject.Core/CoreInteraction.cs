namespace ThuillierColinProject.Core;

using Models;
using ServiceLogs;
using ServicesInteraction;

public class CoreInteraction
{
    /************************************************************************
     * Interaction des administrateurs
     ************************************************************************/
    
    /// <summary>
    /// Permet d'ajouter un article à la base de donnée
    /// </summary>
    /// <returns></returns>
    public bool AjouterArticle()
    {
        Console.WriteLine("Ajout d'un article à la base de donnée");
        SingletonLog.GetInstance().Log("L'utilisateur a choisi d'ajouter un article", LogClass.TypeMessage.Info);
        Ecrire<Article> ecrire = new Ecrire<Article>();
        ecrire.Ecriture(CoreSingleton.GetInstance().coreModels.CreationArticle());
        Console.WriteLine("Article ajouté");
        return true;
    }
    
    /// <summary>
    /// Créer un fichier txt (format facture) donnant la somme des articles vendus
    /// </summary>
    /// <returns></returns>
    public bool CreerFichierFactureArticle()
    {
        Console.WriteLine("Création du fichier facture donnant la somme des articles vendus");
        SingletonLog.GetInstance().Log("L'utilisateur a choisi de créer un fichier facture donnant la somme des articles vendus", LogClass.TypeMessage.Info);
        // On créer un fichier avec pour nom la date, l'heure et "_factureSum.txt"
        string nomFichier = "./Data/" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + "_factureSum.txt";
        // On créer le fichier
        File.Create(nomFichier).Close();
        // On récupère les articles achetés
        List<ArticleAchetes> articlesAchetes = BDD.GetInstance().articleAchetes;
        // On récupère les articles
        List<Article> articles = BDD.GetInstance().articles;
        // On parcours les articles achetés
        if(articlesAchetes.Count == 0)
        {
            SingletonLog.GetInstance().Log("Aucun article acheté", LogClass.TypeMessage.Info);
            return false;
        }
        // On parcours les articles et on regarde combien de fois ils ont été achetés (en prenant en compte la quantité et le nombre de fois où ils ont été achetés)
        foreach (Article article in articles)
        {
            int nbAchete = 0;
            foreach (ArticleAchetes articleAchete in articlesAchetes)
            {
                if (article.Id == articleAchete.IdArticle)
                {
                    nbAchete += articleAchete.Quantite;
                }
            }
            // On ajoute le prix de l'article acheté à la somme
            File.AppendAllText(nomFichier, article.Reference + " : " + article.Prix + "€ x " + nbAchete + " = " + article.Prix * nbAchete + "€\n");
        }
        return true;
    }
    
    /**
     * Créer un fichier txt (format facture) donnant la somme des articles vendus par acheteurs
     */
    public bool CreerFichierFactureAcheteur()
    {
        return true;
    }
    
    /**
     * Créer un fichier txt (format facture) donnant la somme des articles vendus par date d'achat
     */
    public bool CreerFichierFactureDate()
    {
        return true;
    }
    
    
    /************************************************************************
     * Interaction des acheteurs
     ************************************************************************/

    public bool RecapCommande(string filename, Acheteurs acheteur, List<ArticleAchetes> articlesAchetes)
    {
        // On vérifie si le dossier de l'acheteur existe
        string filepath = "./Data/RecapCommande/" + acheteur.Nom + "-" + acheteur.Prenom;
        if (!Directory.Exists(filepath))
        {
            // Si le dossier n'existe pas, on le créer
            Directory.CreateDirectory(filepath);
        }
        // On créer le fichier
        filepath = filepath + "/" + filename + ".txt";
        File.AppendAllText(filepath, acheteur.Nom + " " + acheteur.Prenom + "\nAdresse : " + acheteur.Adresse + "\nTelephone : " + acheteur.Telephone + "\n");
        File.AppendAllText(filepath, "\n\nArticles achetés : \n");
        File.AppendAllText(filepath, "----------------------------------------\n");
        // On parcours les articles achetés
        foreach (ArticleAchetes articleAchete in articlesAchetes)
        {
            // On parcours les articles
            foreach (Article article in BDD.GetInstance().articles)
            {
                // Si l'article acheté est le même que l'article
                if (articleAchete.IdArticle == article.Id)
                {
                    // On ajoute le prix de l'article acheté à la somme
                    File.AppendAllText(filepath, "\t* "+article.Reference + " : " + article.Prix + "€ x " + articleAchete.Quantite + " = " + article.Prix * articleAchete.Quantite + "€\n");
                }
            }
        }
        File.AppendAllText(filepath, "----------------------------------------\n");
        File.AppendAllText(filepath, "Prix total : " + CoreSingleton.GetInstance().coreModels.PrixCommande(articlesAchetes) + "€\n");
        File.AppendAllText(filepath, DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
        return true;
    }
}