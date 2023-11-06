namespace ThuillierColinProject.Core;

using ServicesInteraction;
using ServiceLogs;
using Models;

/// <summary>
/// Class qui permet de gérer les interactions avec l'utilisateur
/// </summary>
public class CoreGestion
{
    /// <summary>
    /// Choix du profil de l'utilisateur
    /// </summary>
    /// <remarks>
    /// L'utilisateur doit choisir entre administrateur et acheteur
    /// </remarks>
    /// <returns>
    /// Le choix de l'utilisateur sous forme de char
    /// </returns>
    public char ChoixProfil()
    {
        Console.WriteLine("Veuillez choisir entre : \t1:Administrateur  \t2: Utilisateur");
        SingletonLog.GetInstance().Log("Gestion du choix du profil", LogClass.TypeMessage.Info);
        bool choixOk = false;
        char choix = '0';
        Console.WriteLine("Votre choix : ");
        do
        {
            try
            {
                // Verifier si l'utilisateur a bien rentré un nombre
                choix = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (choix == '1' || choix == '2')
                {
                    choixOk = true;
                }
                else
                {
                    Console.WriteLine("Veuillez rentrer un nombre valide");
                    SingletonLog.GetInstance().Log("Le nombre entré n'est pas valide", LogClass.TypeMessage.Error);
                }
            }
            catch (Exception e)
            {
                SingletonLog.GetInstance()
                    .Log("Erreur lors de la saisie du choix du profil", LogClass.TypeMessage.Error);
                Console.WriteLine(e);
            }
        } while (!choixOk);

        SingletonLog.GetInstance().Log("Choix du profil : " + choix, LogClass.TypeMessage.Info);
        return choix;
    }

    /*********************************************************************************************************************
     * Gestion du profil administrateur
     *********************************************************************************************************************/

    /// <summary>
    /// Connection de l'administrateur
    /// </summary>
    /// <returns>
    /// True si l'administrateur est connecté
    /// </returns>
    public bool ConnectionAdmin()
    {
        Console.WriteLine("Veuillez rentrer votre login et votre mot de passe");
        SingletonLog.GetInstance().Log("L'utilisateur va se connecter", LogClass.TypeMessage.Info);
        BDD bdd = BDD.GetInstance();
        string login = "";
        bool tmpLogin = false;
        bool tmpPassword = false;
        int nbEssai = 0;
        do
        {
            try
            {
                if (!tmpLogin)
                {
                    // On verifie si le login existe dans la base de données
                    Console.WriteLine(nbEssai > 0 ? "Login incorrect veuillez réessayer" : "Login : ");
                    login = Console.ReadLine();
                    SingletonLog.GetInstance().Log("L'utilisateur a rentré " + login + " en login",
                        LogClass.TypeMessage.Info);
                    tmpLogin = bdd.administrateurs.Exists(x => x.Login == login);
                }

                if (!tmpPassword)
                {
                    // On verifie si le mot de passe existe dans la base de données
                    Console.WriteLine(nbEssai > 0 ? "Mot de passe incorrect veuillez réessayer" : "Mot de passe : ");
                    string password = Console.ReadLine();
                    SingletonLog.GetInstance().Log("L'utilisateur a rentré " + password + " en mot de passe",
                        LogClass.TypeMessage.Info);
                    tmpPassword = bdd.administrateurs.Exists(x => x.Password == password);
                }
            }
            catch (ExceptionClass e)
            {
                Console.WriteLine(e);
            }

            nbEssai++;
        } while (!(tmpLogin && tmpPassword));

        SingletonLog.GetInstance().Log("L'utilisateur s'est connecté", LogClass.TypeMessage.Info);
        Console.WriteLine("Vous êtes connecté, bienvenue " + login + " !");
        return true;
    }

    /// <summary>
    /// Choix de l'action que peut faire l'administrateur lorsqu'il est connecté
    /// </summary>
    /// <returns>
    /// Le choix de l'action sous forme de char
    /// </returns>
    public char ChoixAdmin()
    {
        int i = 1;
        Console.WriteLine("Que voulez-vous faire ?");
        Console.WriteLine( i + " : Saisir un article");
        Console.WriteLine((i++) +" : Ajouter un administrateur");
        Console.WriteLine((i++) +" : Créer un fichier txt (format facture) donnant la somme des articles vendus");
        Console.WriteLine(
            (i++) +" : Créer un fichier txt (format facture) donnant la somme des articles vendus par acheteurs");
        Console.WriteLine(
            (i++)+" : Créer un fichier txt (format facture) donnant la somme des articles vendus par date d'achat");
        Console.WriteLine((i+1) + " : Quitter");
        bool choixOk = false;
        char choix = '0';

        do
        {
            Console.WriteLine("Votre choix : ");
            choix = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (choix == '1' || choix == '2' || choix == '3' || choix == '4' || choix == '5' || choix == '6')
            {
                choixOk = true;
            }
            else
            {
                Console.WriteLine("Veuillez rentrer un nombre valide");
                SingletonLog.GetInstance().Log("Le nombre entré n'est pas valide", LogClass.TypeMessage.Error);
            }
        } while (!choixOk);

        SingletonLog.GetInstance().Log("Choix de l'action : " + choix, LogClass.TypeMessage.Info);
        return choix;
    }

    /// <summary>
    /// Fonction qui ajoute un administrateur
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public bool AjouterAdmin()
    {
        Administrateur newAdmin = CoreSingleton.GetInstance().CoreModels.CreationAdministrateur();
        Ecrire<Administrateur> ecrire = new Ecrire<Administrateur>();
        ecrire.Ecriture(newAdmin);
        SingletonLog.GetInstance().Log("Un administrateur a été ajouté : " + newAdmin, LogClass.TypeMessage.Info);
        return true;
    }

    /// <summary>
    /// Le programme de l'administrateur
    /// </summary>
    /// <remarks>
    /// C'est la suite d'action que peut faire l'administrateur
    /// </remarks>
    /// <returns>
    /// True si l'administrateur a fini son programme
    /// </returns>
    public bool ProgramAdministrateur()
    {
        CoreInteraction
            interaction =
                CoreSingleton.GetInstance()
                    .CoreInteraction; // Récuperation de l'objet qui permet d'utiliser les class d'interaction

        SingletonLog.GetInstance().Log("L'utilisateur a choisi le profil administateur", LogClass.TypeMessage.Info);
        Console.WriteLine("Bienvenue dans le profil administrateur");
        this.ConnectionAdmin();
        bool continuer = true;
        do
        {
            switch (this.ChoixAdmin())
            {
                case '1':
                    interaction.AjouterArticle();
                    break;
                case '2':
                    this.AjouterAdmin();
                    break;
                case '3':
                    // Créer un fichier txt (format facture) donnant la somme des articles vendus.
                    interaction.CreerFichierFactureArticle();
                    break;
                case '4':
                    // Créer un fichier txt (format facture) donnant la somme des articles vendus par acheteurs.
                    interaction.CreerFichierFactureAcheteur();
                    break;
                case '5':
                    // Créer un fichier txt (format facture) donnant la somme des articles vendus par date d'achat.
                    interaction.CreerFichierFactureDate();
                    break;
                case '6':
                    // Quitter
                    Console.WriteLine("Au revoir");
                    return false;
                    break;
                default:
                    Console.WriteLine("Erreur lors du choix de l'action");
                    continuer = false;
                    break;
            }
            Console.WriteLine();
        } while (continuer);

        return true;
    }

    /*********************************************************************************************************************
     * Gestion du profil acheteur
     *********************************************************************************************************************/

    /// <summary>
    /// Connection de l'acheteur
    /// </summary>
    /// <returns>
    /// L'acheteur connecté
    /// </returns>
    public Acheteurs ConnectionAcheteur()
    {
        // L'utilisateur doit saisir son nom, prenom, adresse et téléphone avant de pouvoir ajouter un article.
        Console.WriteLine("Veuillez rentrer votre nom, prenom, adresse et téléphone");
        SingletonLog.GetInstance().Log("L'utilisateur va se connecter", LogClass.TypeMessage.Info);
        Acheteurs acheteur = new Acheteurs();
        bool tmpNom = false;
        bool tmpPrenom = false;
        bool tmpAdresse = false;
        bool tmpTelephone = false;
        int nbEssai = 0;
        do
        {
            try
            {
                if (!tmpNom)
                {
                    Console.WriteLine(nbEssai > 0 ? "Nom incorrect veuillez réessayer" : "Nom : ");
                    acheteur.Nom = Console.ReadLine();
                    tmpNom = true;
                }
                if (!tmpPrenom)
                {
                    Console.WriteLine(nbEssai > 0 ? "Prenom incorrect veuillez réessayer" : "Prenom : ");
                    acheteur.Prenom = Console.ReadLine();
                    tmpPrenom = true;
                }
                if (!tmpAdresse)
                {
                    Console.WriteLine(nbEssai > 0 ? "Adresse incorrect veuillez réessayer" : "Adresse : ");
                    acheteur.Adresse = Console.ReadLine();
                    tmpAdresse = true;
                }
                if (!tmpTelephone)
                {
                    Console.WriteLine(nbEssai > 0 ? "Telephone incorrect veuillez réessayer" : "Telephone : ");
                    acheteur.Telephone = Console.ReadLine();
                    tmpTelephone = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        } while (!(tmpNom && tmpPrenom && tmpAdresse && tmpTelephone));

        // On vérifie si l'acheteur existe dans la base de données
        return InscriptionAcheteur(acheteur);
    }


    /// <summary>
    /// On verifie si l'acheteur existe dans la base de données et on l'ajoute si il n'existe pas
    /// </summary>
    /// <param name="acheteur">
    /// L'acheteur à vérifier
    /// </param>
    /// <returns>
    /// L'acheteur après vérification
    /// </returns>
    public Acheteurs InscriptionAcheteur(Acheteurs acheteur)
    {
        // On verifie si l'acheteur existe dans la base de données
        foreach (var a in BDD.GetInstance().acheteurs)
        {
            if (acheteur.Equals(a))
            {
                SingletonLog.GetInstance().Log("l'utilisateur existe déjà dans la base de données",
                    LogClass.TypeMessage.Info);
                acheteur.Id = a.Id;
                return acheteur;
            }
        }

        // Sinon on ajoute l'acheteur dans la base de données
        Ecrire<Acheteurs> ecrire = new Ecrire<Acheteurs>();
        ecrire.Ecriture(acheteur);
        SingletonLog.GetInstance().Log("l'utilisateur a été ajouté dans la base de données", LogClass.TypeMessage.Info);
        return acheteur;
    }

    /// <summary>
    /// Choix de l'article que l'acheteur veut acheter
    /// </summary>
    /// <param name="acheteur">
    /// L'acheteur qui achete l'article
    /// </param>
    /// <param name="articlesAchetes">
    /// La liste des articles achetés
    /// </param>
    /// <returns>
    /// True si l'acheteur veut continuer à acheter
    /// </returns>
    /// <exception cref="ExceptionClass">
    /// Si l'utilisateur se trompe lors de la saisie du numéro de l'article ou de la quantité
    /// </exception>
    public bool ChoisirArticle(Acheteurs acheteur, List<ArticleAchetes> articlesAchetes)
    {
        char choix = LireCaractereDepuisConsole();
        Console.WriteLine();

        if (choix == 'F')
        {
            Console.WriteLine("Fin de la commande, au revoir");
            return false;
        }

        if (choix == 'P')
        {
            AfficherPrixCommande(articlesAchetes);
            return true;
        }

        if (EstChoixArticleValide(choix))
        {
            int quantite = SaisirQuantite();
            AjouterArticleAchete(acheteur, articlesAchetes, choix, quantite);
            return true;
        }

        return true;
    }

    private char LireCaractereDepuisConsole()
    {
        return Console.ReadKey().KeyChar;
    }

    private void AfficherPrixCommande(List<ArticleAchetes> articlesAchetes)
    {
        double prix = CoreSingleton.GetInstance().CoreModels.PrixCommande(articlesAchetes);
        Console.WriteLine("Prix de la commande : " + prix);
        SingletonLog.GetInstance().Log("L'utilisateur a demandé le prix de la commande", LogClass.TypeMessage.Info);
    }

    private bool EstChoixArticleValide(char choix)
    {
        if (!int.TryParse(choix.ToString(), out int choixInt))
        {
            throw new ExceptionClass("Veuillez rentrer un nombre valide");
        }

        BDD bdd = BDD.GetInstance();

        if (choixInt <= 0 || choixInt > bdd.articles.Count)
        {
            throw new ExceptionClass("Veuillez rentrer un nombre valide");
        }

        return true;
    }

    private int SaisirQuantite()
    {
        Console.WriteLine("Quantité : ");
        if (!int.TryParse(Console.ReadLine(), out int quantite) || quantite <= 0)
        {
            throw new ExceptionClass("Veuillez rentrer un nombre valide");
        }

        return quantite;
    }

    private void AjouterArticleAchete(Acheteurs acheteur, List<ArticleAchetes> articlesAchetes, char choix,
        int quantite)
    {
        int choixInt = int.Parse(choix.ToString());
        Article article = BDD.GetInstance().articles[choixInt - 1];

        ArticleAchetes articleAchete = new ArticleAchetes
        {
            IdArticle = article.Id,
            IdAcheteur = acheteur.Id,
            Quantite = quantite
        };

        BDD.GetInstance().articleAchetes.Add(articleAchete);
        articlesAchetes.Add(articleAchete);

        Ecrire<ArticleAchetes> ecrire = new Ecrire<ArticleAchetes>();
        ecrire.Ecriture(articleAchete);

        SingletonLog.GetInstance().Log(
            "L'utilisateur a acheté l'article " + article.Reference + " en " + quantite + " exemplaire(s)",
            LogClass.TypeMessage.Info);
    }


    /// <summary>
    /// Lorque l'acheteur est connecté, il peut commander des articles
    /// </summary>
    /// <param name="acheteur">
    /// L'acheteur qui commande
    /// </param>
    /// <returns>
    /// True si l'acheteur a fini sa commande
    /// </returns>
    public bool Commander(Acheteurs acheteur)
    {
        Console.WriteLine("Veuillez choisir un article après l'autre et saisir la quantité de chacun.");
        List<ArticleAchetes> articlesAchetes = new List<ArticleAchetes>();
        bool continuer = true;
        do
        {
            CoreSingleton.GetInstance().CoreModels.AfficherArticle(BDD.GetInstance().articles);
            Console.WriteLine("F : Finir la commande");
            Console.WriteLine("P : Voir le prix de la commande");
            Console.WriteLine("Votre choix : ");
            try
            {
                continuer = this.ChoisirArticle(acheteur, articlesAchetes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine();
        } while (continuer);

        // On crée le fichier de la facture
        string nomFichier = acheteur.Nom + "-" + acheteur.Prenom + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") +
                            ".txt";

        // On recapitule la commande de l'acheteur à l'aide du CoreInteraction
        CoreSingleton.GetInstance().CoreInteraction.RecapCommande(nomFichier, acheteur, articlesAchetes);
        SingletonLog.GetInstance().Log("L'utilisateur a fini sa commande", LogClass.TypeMessage.Info);
        return true;
    }

    /// <summary>
    ///  Le programme de l'acheteur
    /// </summary>
    /// <returns>
    /// True si l'acheteur a fini son programme
    /// </returns>
    public bool ProgramAcheteur()
    {
        SingletonLog.GetInstance().Log("L'utilisateur a choisi le profil acheteur", LogClass.TypeMessage.Info);
        Acheteurs profileAcheteur = this.ConnectionAcheteur();
        // On salut l'acheteur avant de lui montrer les articles
        Console.WriteLine("Bienvenue " + profileAcheteur.Prenom + " " + profileAcheteur.Nom);
        // On affiche les articles et on demande à l'acheteur de choisir (on lui fait passer la commande)
        this.Commander(profileAcheteur);
        return true;
    }
}