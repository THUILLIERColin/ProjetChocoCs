using ThuillierColinProject.Models;
using ThuillierColinProject.ServiceLogs;
using ThuillierColinProject.ServicesInteraction;

namespace ThuillierColinProject.Core;

using ThuillierColinProject.ServicesGestion;

public class CoreModels
{
    public CoreModels() { }
    
    public Acheteurs CreationAcheteur()
    {
        GestionAcheteur gestionAcheteur = new GestionAcheteur();
        return gestionAcheteur.CreationAcheteurs();
    }
    
    public Administrateur CreationAdministrateur()
    {
        GestionAdmin gestionAdmin = new GestionAdmin();
        return gestionAdmin.CreationAdministrateur();
    }
    
    public Article CreationArticle()
    {
        GestionArticle gestionArticle = new GestionArticle();
        return gestionArticle.CreerArticle();
    }
    
    public bool CreationBDD()
    {
        GestionAdmin gestionAdmin = new GestionAdmin();
        GestionArticle gestionArticle = new GestionArticle();
        bool tmp = gestionAdmin.CreationBDD();
        bool tmp2 = gestionArticle.CreationBDD();
        return tmp && tmp2;
    }
    
    public List<Article> RecupererArticle()
    {
        Lecture<Article> lecture = new Lecture<Article>();
        return lecture.LectureFichier();
    }
    
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
    
    public bool AfficherArticle(List<Article> articles)
    {
        if (articles.Count == 0)
        {
            SingletonLog.GetInstance().Log("Aucun article n'a été trouvé", LogClass.TypeMessage.Info);
            return false;
        }
        for (int i = 0; i < articles.Count; i++)
        {
            Console.WriteLine(i+1 + " : [" + articles[i].Reference + " ; " + articles[i].Prix + "€ ]");
        }
        return true;
    }
    
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