namespace ThuillierColinProject.Core;

using Models;
using ServiceLogs;
using ServicesInteraction;

/// <summary> Class qui va gérer les interactions avec les fichiers  </summary>
/// <remarks>
/// C'est à dire gérer les écritures et les lectures.
/// Gestions des factures et du récapitulatif de commande.
/// </remarks>
public class CoreInteraction
{
    public CoreInteraction()
    {
        if(!Directory.Exists("./Data/Facture"))
        {
            Directory.CreateDirectory("./Data/Facture");
        }
        if(!Directory.Exists("./Data/RecapCommande"))
        {
            Directory.CreateDirectory("./Data/RecapCommande");
        }
    }
    
    /************************************************************************
     * Le recap de l'acheteur
     ************************************************************************/
    /// <summary>
    /// Permet de créer un fichier txt (format facture) donnant la somme des articles achetés par un acheteur
    /// </summary>
    /// <param name="filepath"> Le chemin du fichier  </param>
    /// <param name="acheteur"> L'acheteur </param>
    /// <param name="articlesAchetes"> Les articles achetés </param>
    /// <returns> Retourne true si le fichier a été créé </returns>
    public bool RecapCommandeAcheteur(string filepath, Acheteurs acheteur, List<ArticleAchetes> articlesAchetes)
    {
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
        File.AppendAllText(filepath, "Prix total : " + CoreSingleton.GetInstance().CoreModels.PrixCommande(articlesAchetes) + "€\n");
        File.AppendAllText(filepath, "\nDate d'achat : " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
        return true;
    }
    
    
    /************************************************************************
     * Interaction des administrateurs
     ************************************************************************/
    
    /// <summary>
    /// Permet d'ajouter un article à la base de donnée
    /// </summary>
    /// <returns> Retourne true si l'article a été ajouté </returns>
    public bool AjouterArticle()
    {
        Console.WriteLine("Ajout d'un article à la base de donnée");
        SingletonLog.GetInstance().Log("L'utilisateur a choisi d'ajouter un article", LogClass.TypeMessage.Info);
        Ecrire<Article> ecrire = new Ecrire<Article>();
        ecrire.Ecriture(CoreSingleton.GetInstance().CoreModels.CreationArticle());
        Console.WriteLine("Article ajouté");
        return true;
    }
    
    /// <summary>
    /// Créer un fichier txt (format facture) donnant la somme des articles vendus
    /// </summary>
    /// <returns>
    /// true si le fichier a été créé
    /// </returns>
    public bool CreerFichierFactureArticle()
    {
        Console.WriteLine("Création du fichier facture donnant la somme des articles vendus");
        SingletonLog.GetInstance().Log("L'utilisateur a choisi de créer un fichier facture donnant la somme des articles vendus", LogClass.TypeMessage.Info);
        // On créer un fichier avec pour nom la date, l'heure et "_factureArticle.txt"
        string nomFichier = "./Data/" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + "_factureArticle.txt";
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
    
    /// <summary>
    /// Créer un fichier txt (format facture) donnant la somme des articles vendus par acheteurs
    /// </summary>
    /// <returns>
    /// Retourne true si le fichier a été créé
    /// </returns>
    public bool CreerFichierFactureAcheteur()
    {
        Console.WriteLine("Création du fichier facture donnant la somme des articles vendus par acheteurs");
        SingletonLog.GetInstance().Log("L'utilisateur a choisi de créer un fichier facture donnant la somme des articles vendus par acheteurs", LogClass.TypeMessage.Info);
        // On créer un fichier avec pour nom la date, l'heure et "_factureAcheteur.txt"
        string nomFichier = "./Data/Facture/" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + "_factureAcheteur.txt";
        // On créer le fichier
        File.Create(nomFichier).Close();
        // On récupère les articles achetés
        List<ArticleAchetes> articlesAchetes = BDD.GetInstance().articleAchetes;
        // On récupère les acheteurs
        List<Acheteurs> acheteurs = BDD.GetInstance().acheteurs;
        // On trie les articles achetés par acheteurs dans un dictionnaire
        Dictionary<Acheteurs, List<ArticleAchetes>> dico = new Dictionary<Acheteurs, List<ArticleAchetes>>();
        foreach (ArticleAchetes articleAchete in articlesAchetes)
        {
            Acheteurs tmp = acheteurs.Find(x => x.Id == articleAchete.IdAcheteur);
            if (dico.ContainsKey(tmp))
            {
                dico[tmp].Add(articleAchete);
            }
            else
            {
                dico.Add(tmp, new List<ArticleAchetes>());
                dico[tmp].Add(articleAchete);
            }
        }
        // On parcours les acheteurs
        foreach (KeyValuePair<Acheteurs, List<ArticleAchetes>> acheteur in dico)
        {
            this.RecapCommandeAcheteur(nomFichier, acheteur.Key, acheteur.Value);
            File.AppendAllText(nomFichier, "\n\n");
        }

        return true;
    }
    
    /// <summary>
    /// Créer un fichier txt (format facture) donnant la somme des articles vendus par date d'achat
    /// </summary>
    /// <returns>
    /// Retourne true si le fichier a été créé
    /// </returns>
    public bool CreerFichierFactureDate()
    { 
        Console.WriteLine("Création du fichier facture donnant la somme des articles vendus par date d'achat");
        SingletonLog.GetInstance().Log("Création du fichier facture donnant la somme des articles vendus par date d'achat", LogClass.TypeMessage.Info);
        // On créer un fichier avec pour nom la date, l'heure et "_factureDate.txt"
        string nomFichier = "./Data/Facture/" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + "_factureDate.txt";
        // On créer le fichier
        File.Create(nomFichier).Close();
        // On récupère les articles achetés
        List<ArticleAchetes> articlesAchetes = BDD.GetInstance().articleAchetes;
        // On trie les articles achetés par date d'achat dans un dictionnaire
        Dictionary<string, List<ArticleAchetes>> dico = new Dictionary<string, List<ArticleAchetes>>();
        foreach (ArticleAchetes articleAchete in articlesAchetes)
        {
            string tmp = articleAchete.DateAchat.ToString("dd-MM-yyyy");
            if (dico.ContainsKey(tmp))
            {
                dico[tmp].Add(articleAchete);
            }
            else
            {
                dico.Add(tmp, new List<ArticleAchetes>());
                dico[tmp].Add(articleAchete);
            }
        }
        // On parcours les dates
        foreach (KeyValuePair<string, List<ArticleAchetes>> date in dico)
        {
            File.AppendAllText(nomFichier, "Date d'achat : " + date.Key + "\n");
            File.AppendAllText(nomFichier, "----------------------------------------\n");
            // On parcours les articles achetés
            foreach (ArticleAchetes articleAchete in date.Value)
            {
                // On parcours les articles
                foreach (Article article in BDD.GetInstance().articles)
                {
                    // Si l'article acheté est le même que l'article
                    if (articleAchete.IdArticle == article.Id)
                    {
                        // On ajoute le prix de l'article acheté à la somme
                        File.AppendAllText(nomFichier, "\t* "+article.Reference + " : " + article.Prix + "€ x " + articleAchete.Quantite + " = " + article.Prix * articleAchete.Quantite + "€\n");
                    }
                }
            }
            File.AppendAllText(nomFichier, "----------------------------------------\n");
            File.AppendAllText(nomFichier, "Prix total : " + CoreSingleton.GetInstance().CoreModels.PrixCommande(date.Value) + "€\n");
            File.AppendAllText(nomFichier, "\n\n");
        }
        return true;
    }
    
    
    /************************************************************************
     * Interaction des acheteurs
     ************************************************************************/

    /// <summary>
    /// Permet de créer un fichier txt (format facture) donnant la somme des articles achetés par un acheteur
    /// </summary>
    /// <param name="filename"> Le nom du fichier </param>
    /// <param name="acheteur"> L'acheteur </param>
    /// <param name="articlesAchetes"> Les articles achetés </param>
    /// <returns> Retourne true si le fichier a été créé </returns>
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
        this.RecapCommandeAcheteur(filepath, acheteur, articlesAchetes);
        return true;
    }
}