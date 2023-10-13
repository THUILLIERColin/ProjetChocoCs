using ThuillierColinProject.Models;
using ThuillierColinProject.ServiceLogs;
using ThuillierColinProject.ServicesInteraction;

namespace ThuillierColinProject.ServicesGestion;

public class GestionArticle
{
    public Article AjouterArticle()
    {
        Article article = new Article();
        
        Console.WriteLine("Création d'un article");
        
        bool tmpReference = false;
        bool tmpPrix = false;
        do
        {
            try
            {
                if (!tmpReference)
                {
                    Console.WriteLine("Reference : ");
                    article.Reference = Console.ReadLine();
                    tmpReference = true;
                }
                if (!tmpPrix)
                {
                    Console.WriteLine("Prix : ");
                    float prix = float.Parse(Console.ReadLine());
                    article.Prix = prix;
                    tmpPrix = true;
                }
            }
            catch (ExceptionClass e)
            {
                Console.WriteLine(e);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Prix au mauvais format");
            }
        } while (!(tmpReference && tmpPrix));
        return article;
    }
    
    public bool CreationBDD()
    {
        Ecrire<Article> ecrire = new Ecrire<Article>();
        Console.WriteLine("Holla");
        if (!ecrire.FileExist())
        {
            SingletonLog.GetInstance().Log("Création des articles", LogClass.TypeMessage.Info);
            List<Article> articles = new List<Article>();
            articles.Add(new Article("Kinder Bueno 100g", 2.99f));
            articles.Add(new Article("Milka Chocolat au Lait 200g", 3.49f));
            articles.Add(new Article("Lindt Lindor Chocolats Assortis 250g", 5.99f));
            articles.Add(new Article("Toblerone Chocolat Noir 100g", 2.79f));
            articles.Add(new Article("Ferrero Rocher 16 pièces", 8.99f));
            foreach (var article in articles)
            {
                ecrire.Ecriture(article);
            }

            return true;
        }
        SingletonLog.GetInstance().Log("Le fichier Article.json existe déjà", LogClass.TypeMessage.Info);
        return false;
    }
}