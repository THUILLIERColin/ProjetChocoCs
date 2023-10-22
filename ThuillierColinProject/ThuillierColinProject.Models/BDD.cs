namespace ThuillierColinProject.Models;

/// <summary>
/// Class qui va stocker les données de la base de données
/// </summary>
public class BDD
{
    private static BDD instance;
    
    // On crée un objet pour le lock cad que si un thread est en train d'utiliser l'objet, les autres threads doivent attendre
    private static readonly object LockObject = new object();
    
    public List<Acheteurs> acheteurs;
    public List<Administrateur> administrateurs;
    public List<Article> articles;
    public List<ArticleAchetes> articleAchetes;
    
    public char profil;
    public static BDD GetInstance()
    {
        // On le premier appel, on va créer l'instance
        lock (LockObject)
        {
            // On vérifie si l'instance est null si oui on la crée
            if(instance == null)
            {
                instance = new BDD();
            }
        }
        return instance;
    }
}