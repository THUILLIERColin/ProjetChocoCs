using NLog;
using ThuillierColinProject.ServicesInteraction;

namespace ThuillierColinProject.Core;

using ServiceLogs;
using Models;

public class CoreGestion
{

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
                SingletonLog.GetInstance().Log("Erreur lors de la saisie du choix du profil", LogClass.TypeMessage.Error);
                Console.WriteLine(e);
            }
        } while (!choixOk);
        SingletonLog.GetInstance().Log("Choix du profil : " + choix, LogClass.TypeMessage.Info);
        return choix;
    }

    /*********************************************************************************************************************
     * Gestion du profil administrateur
     *********************************************************************************************************************/
    
    /**
     * Se connecter en tant qu'administrateur
     */
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
                    SingletonLog.GetInstance().Log("L'utilisateur a rentré " + login + " en login", LogClass.TypeMessage.Info);
                    tmpLogin = bdd.administrateurs.Exists(x => x.Login == login);
                }

                if (!tmpPassword)
                {
                    // On verifie si le mot de passe existe dans la base de données
                    Console.WriteLine(nbEssai > 0 ? "Mot de passe incorrect veuillez réessayer" : "Mot de passe : ");
                    string password = Console.ReadLine();
                    SingletonLog.GetInstance().Log("L'utilisateur a rentré " + password + " en mot de passe", LogClass.TypeMessage.Info);
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

    public char ChoixAdmin()
    {
        Console.WriteLine("Que voulez-vous faire ?");
        Console.WriteLine("1 : Saisir un article");
        Console.WriteLine("2 : Créer un fichier txt (format facture) donnant la somme des articles vendus");
        Console.WriteLine("3 : Créer un fichier txt (format facture) donnant la somme des articles vendus par acheteurs");
        Console.WriteLine("4 : Créer un fichier txt (format facture) donnant la somme des articles vendus par date d'achat");
        Console.WriteLine("5 : Quitter");
        bool choixOk = false;
        char choix = '0';

        do
        {
            Console.WriteLine("Votre choix : ");
            choix = Console.ReadKey().KeyChar;
            if(choix=='1' || choix=='2' || choix=='3' || choix=='4' || choix=='5')
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
        Console.WriteLine();
        return choix;
    }

    /// <summary>
    /// Suite d'action que peut faire l'administrateur lorsqu'il est connecté
    /// </summary>
    /// <returns></returns>
    public bool ProgramAdministrateur()
    {
        CoreInteraction interaction = CoreSingleton.GetInstance().coreInteraction; // Récuperation de l'objet qui permet d'utiliser les class d'interaction

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
                    // Créer un fichier txt (format facture) donnant la somme des articles vendus.
                    interaction.CreerFichierFactureArticle();
                    break;
                case '3':
                    // Créer un fichier txt (format facture) donnant la somme des articles vendus par acheteurs.
                    interaction.CreerFichierFactureAcheteur();
                    break;
                case '4':
                    // Créer un fichier txt (format facture) donnant la somme des articles vendus par date d'achat.
                    interaction.CreerFichierFactureDate();
                    break;
                case '5':
                    // Quitter
                    Console.WriteLine("\nAu revoir");
                    return false;
                    break;
                default:
                    Console.WriteLine("Erreur lors du choix de l'action");
                    break;
            }
            Console.WriteLine("Voulez-vous continuer ? (o/n)");
            if (Console.ReadKey().KeyChar != 'o')
            {
                continuer = false;
                Console.WriteLine("\nAu revoir");
                SingletonLog.GetInstance()
                    .Log("L'utilisateur a quitté le programme", LogClass.TypeMessage.Info);
            }
        } while (continuer);
        return true;
    }

    /*********************************************************************************************************************
     * Gestion du profil acheteur
     *********************************************************************************************************************/

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
        } while (!(tmpNom && tmpPrenom && tmpAdresse && tmpTelephone));
        // On verfie si l'acheteur existe dans la base de données
        return InscriptionAcheteur(acheteur);
    }

    public Acheteurs InscriptionAcheteur(Acheteurs acheteur)
    {
        // On verifie si l'acheteur existe dans la base de données
        foreach (var a in BDD.GetInstance().acheteurs)
        {
            if (acheteur.Equals(a))
            {
                SingletonLog.GetInstance().Log("l'utilisateur existe déjà dans la base de données", LogClass.TypeMessage.Info);
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

    public bool ChoisirArticle(Acheteurs acheteur, List<ArticleAchetes> articlesAchetes)
    {
        char choix = Console.ReadKey().KeyChar;
        Console.WriteLine();
        if (choix == 'F')
        {
            Console.WriteLine("Fin de la commande, au revoir");
            return false;
        }
        else if (choix == 'P')
        {
            // On affiche le prix de la commande
            Console.WriteLine("Prix de la commande : " +
                              CoreSingleton.GetInstance().coreModels.PrixCommande(articlesAchetes));
        }
        else
        {
            // On verifie si l'utilisateur a bien rentré un nombre et si le nombre est bien dans la liste des articles
            if (!int.TryParse(choix.ToString(), out int choixInt) || choixInt > BDD.GetInstance().articles.Count || choixInt <= 0)
            {
                throw new ExceptionClass("Veuillez rentrer un nombre valide");
            }

            Console.WriteLine("Quantité : ");
            int quantite = int.Parse(Console.ReadLine());
            if (quantite <= 0)
            {
                throw new ExceptionClass("Veuillez rentrer un nombre valide");
            }
            // On ajoute l'article acheté dans la base de données
            ArticleAchetes articleAchete = new ArticleAchetes();
            articleAchete.IdArticle = BDD.GetInstance().articles[choixInt - 1].Id;
            articleAchete.IdAcheteur = acheteur.Id;
            articleAchete.Quantite = quantite;
            // On ajoute l'article acheté dans la base de données
            BDD.GetInstance().articleAchetes.Add(articleAchete);
            articlesAchetes.Add(articleAchete);
            // On ajoute l'article acheté dans le fichier de la base de données
            Ecrire<ArticleAchetes> ecrire = new Ecrire<ArticleAchetes>();
            ecrire.Ecriture(articleAchete);
            SingletonLog.GetInstance()
                .Log(
                    "L'utilisateur a acheté l'article " + BDD.GetInstance().articles[choixInt - 1].Reference +
                    " en " + quantite + " exemplaire(s)", LogClass.TypeMessage.Info);
        }
        return true;
    }

    public bool Commander(Acheteurs acheteur)
    {
        Console.WriteLine("Veuillez choisir un article après l'autre et saisir la quantité de chacun.");
        List<ArticleAchetes> articlesAchetes = new List<ArticleAchetes>();
        bool continuer = true;
        do
        {
            CoreSingleton.GetInstance().coreModels.AfficherArticle(BDD.GetInstance().articles);
            Console.WriteLine("F : Finir la commande");
            Console.WriteLine("P : Voir le prix de la commande");
            Console.WriteLine("Votre choix : ");
            try
            {
                continuer = this.ChoisirArticle(acheteur, articlesAchetes);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Veuillez rentrer un nombre valide");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (continuer)
            {
                Console.WriteLine("Voulez-vous continuer ? (o/n)");
                if (Console.ReadKey().KeyChar != 'o')
                {
                    Console.WriteLine("\nVous avez quitté le programme, au revoir");
                    SingletonLog.GetInstance()
                        .Log("L'utilisateur a quitté le programme", LogClass.TypeMessage.Info);
                    continuer = false;
                }
                Console.WriteLine();
            }
            // Verifier si l'utilisateur a bien rentré un nombre
        } while (continuer);
        // Quand l'utilisateur a fini sa commande, un fichier texte doit etre fait avec un recapitulatif de ses articles,
        // le prix de chacun et le prix total. Le fichier sera sauvegardé dans un dossier à son nom et le nom du fichier
        // aura le format suivant "Nom-Prenom-Jour-Mois-Annee-Heure-Minute.txt"
        string nomFichier = acheteur.Nom + "-" + acheteur.Prenom + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") + ".txt";
        CoreSingleton.GetInstance().coreInteraction.RecapCommande(nomFichier, acheteur, articlesAchetes);
        SingletonLog.GetInstance().Log("L'utilisateur a fini sa commande", LogClass.TypeMessage.Info);
        return true;
    }
    public bool ProgramAcheteur()
    {
        SingletonLog.GetInstance().Log("L'utilisateur a choisi le profil acheteur", LogClass.TypeMessage.Info);
        // Acheteurs profileAcheteur = this.ConnectionAcheteur();
        // On salut l'acheteur avant de lui montrer les articles
        // Console.WriteLine("Bienvenue " + profileAcheteur.Prenom + " " + profileAcheteur.Nom);
        this.Commander( this.InscriptionAcheteur(new Acheteurs("Thuillier", "Colin", "10 rue ", "0987654321")) );
        return true;
    }
}