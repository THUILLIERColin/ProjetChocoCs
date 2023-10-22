namespace ThuillierColinProject.Core;

using ServicesGestion;
using Models;
using ServiceLogs;
using ServicesInteraction;

/// <summary>
/// Class qui s'occupe de la gestion des models
/// </summary>
public class CoreModels
{
    /// <summary>
    /// Création d'un acheteur à partir de la console
    /// </summary>
    /// <returns>
    /// L'acheteur créé
    /// </returns>
    public Acheteurs CreationAcheteur()
    {
        GestionAcheteur gestionAcheteur = new GestionAcheteur();
        return gestionAcheteur.CreationAcheteurs();
    }

    /// <summary>
    /// Création d'un administrateur à partir de la console
    /// </summary>
    /// <returns>
    /// L'administrateur créé
    /// </returns>
    public Administrateur CreationAdministrateur()
    {
        GestionAdmin gestionAdmin = new GestionAdmin();
        return gestionAdmin.CreationAdministrateur();
    }

    /// <summary>
    /// Création d'un article à partir de la console
    /// </summary>
    /// <returns>
    /// L'article créé
    /// </returns>
    public Article CreationArticle()
    {
        GestionArticle gestionArticle = new GestionArticle();
        return gestionArticle.CreerArticle();
    }

    /// <summary>
    /// Création des fichiers de la base de données si ils n'existent pas
    /// </summary>
    /// <remarks>
    /// Uniquement les fichiers administrateurs et articles sont créés
    /// </remarks>
    /// <returns>
    /// True si les fichiers ont été créés sinon false
    /// </returns>
    public bool CreationBDD()
    {
        GestionAdmin gestionAdmin = new GestionAdmin();
        GestionArticle gestionArticle = new GestionArticle();
        bool tmp = gestionAdmin.CreationBDD();
        bool tmp2 = gestionArticle.CreationBDD();
        return tmp && tmp2;
    }

    /// <summary>
    /// Permet de récupérer les articles à partir du fichier
    /// </summary>
    /// <returns>
    /// La liste des articles si le fichier existe sinon une liste vide
    /// </returns>
    public List<Article> RecupererArticle()
    {
        Lecture<Article> lecture = new Lecture<Article>();
        return lecture.LectureFichier();
    }

    /// <summary>
    /// Permet de récupérer les acheteurs
    /// </summary>
    /// <returns>
    /// La liste des acheteurs si le fichier existe sinon une liste vide
    /// </returns>
    public List<Acheteurs> RecupererAcheteurs()
    {
        Lecture<Acheteurs> lecture = new Lecture<Acheteurs>();
        return lecture.LectureFichier();
    }

    /// <summary>
    /// Permet de récupérer les administrateurs
    /// </summary>
    /// <returns>
    /// La liste des administrateurs si le fichier existe sinon une liste vide
    /// </returns>
    public List<Administrateur> RecupererAdministrateur()
    {
        Lecture<Administrateur> lecture = new Lecture<Administrateur>();
        return lecture.LectureFichier();
    }

    /// <summary>
    /// Permet de récupérer les articles achetés
    /// </summary>
    /// <returns>
    /// La liste des articles achetés si le fichier existe sinon une liste vide
    /// </returns>
    public List<ArticleAchetes> RecupererArticleAchetes()
    {
        Lecture<ArticleAchetes> lecture = new Lecture<ArticleAchetes>();
        return lecture.LectureFichier();
    }

    /// <summary>
    /// Calcul le prix de la commande
    /// </summary>
    /// <param name="articleAchetesList">
    /// La liste des articles achetés
    /// </param>
    /// <returns>
    /// Le prix de la commande
    /// </returns>
    public float PrixCommande(List<ArticleAchetes> articleAchetesList)
    {
        float prix = 0;
        if (articleAchetesList.Count == 0)
        {
            SingletonLog.GetInstance().Log("Aucun article acheté", LogClass.TypeMessage.Info);
            return prix;
        }

        // On parcours les articles achetés
        foreach (ArticleAchetes articleAchete in articleAchetesList)
        {
            // On parcours les articles
            foreach (Article article in BDD.GetInstance().articles)
            {
                // Si l'article acheté est le même que l'article
                if (articleAchete.IdArticle == article.Id)
                {
                    // On ajoute le prix de l'article acheté à la somme
                    prix += article.Prix * articleAchete.Quantite;
                }
            }
        }

        return prix;
    }

    /// <summary>
    /// Affichage des articles en paramètre
    /// </summary>
    /// <param name="articles">
    /// La liste des articles à afficher
    /// </param>
    /// <returns>
    /// True si des articles ont été trouvés sinon false
    /// </returns>
    public bool AfficherArticle(List<Article> articles)
    {
        if (articles.Count == 0)
        {
            SingletonLog.GetInstance().Log("Aucun article n'a été trouvé", LogClass.TypeMessage.Info);
            return false;
        }

        for (int i = 0; i < articles.Count; i++)
        {
            Console.WriteLine(i + 1 + " : [" + articles[i].Reference + " ; " + articles[i].Prix + "€ ]");
        }

        return true;
    }

    /// <summary>
    /// Affichage des acheteurs en paramètre
    /// </summary>
    /// <param name="acheteurs"> La liste des acheteurs à afficher </param>
    /// <returns>
    /// True si des acheteurs ont été trouvés sinon false
    /// </returns>
    public bool AfficherAcheteurs(List<Acheteurs> acheteurs)
    {
        if (acheteurs == null || acheteurs.Count == 0)
        {
            SingletonLog.GetInstance().Log("Aucun acheteur n'a été trouvé", LogClass.TypeMessage.Info);
            return false;
        }

        foreach (var acheteur in acheteurs)
        {
            Console.WriteLine(acheteur);
        }

        return true;
    }

    /// <summary>
    /// Affichage des administrateurs en paramètre
    /// </summary>
    /// <param name="administrateurs"> La liste des administrateurs à afficher </param>
    /// <returns>
    /// True si des administrateurs ont été trouvés sinon false
    /// </returns>
    public bool AfficherAdministrateur(List<Administrateur> administrateurs)
    {
        if (administrateurs.Count == 0)
        {
            SingletonLog.GetInstance().Log("Aucun administrateur n'a été trouvé", LogClass.TypeMessage.Info);
            return false;
        }

        foreach (var admin in administrateurs)
        {
            Console.WriteLine(admin);
        }

        return true;
    }
}