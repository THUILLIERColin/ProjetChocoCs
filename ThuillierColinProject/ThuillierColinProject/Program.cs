using ThuillierColinProject.Models;

namespace ThuillierColinProject;

using ThuillierColinProject.Core;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Bienvenue dans l'application de gestion de stock");
            
            CoreModels models = new CoreModels();
            models.CreationBDD();
            List<Article> articles = models.RecupererArticle();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }

    public void Test1()
    {
        // CoreModels models = new CoreModels();
        // Console.WriteLine("Creation d'un administrateur : " + models.CreationAdministrateur());
        // Console.WriteLine("Creation d'un acheteur : " + models.CreationAcheteur());
        // Console.WriteLine("Creation d'un article : " + models.AjouterArticle());
            
        /*
        TestModels<Acheteurs> testAcheteurs = new TestModels<Acheteurs>();
        Acheteurs acheteurs = new Acheteurs("nom", "prenom", "adresse", "1234567890");
        testAcheteurs.TestEcriture(acheteurs);*/
        // testAcheteurs.TestLecture(acheteurs);
            
        /*
        TestModels<Administrateur> testAdmin = new TestModels<Administrateur>();
        Administrateur admin = new Administrateur("login", "password!");
        testAdmin.TestEcriture(admin);
        testAdmin.TestLecture(admin);
        */
        /*
        TestModels<Article> testArticle = new TestModels<Article>();
        Article article = new Article("Chocolat 100g", 5);
        testArticle.TestEcriture(article);
        testArticle.TestLecture(article); */
    }
}

