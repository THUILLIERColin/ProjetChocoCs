namespace ThuillierColinProject.ServiceLogs;

/// <summary>
/// Classe SingletonLog qui hérite de la classe LogClass
/// </summary>
/// <remarks>
/// Elle va permettre de créer un objet de type SingletonLog et donc de pouvoir utiliser
/// la méthodes de la classe LogClass qui permet d'inserer un message dans le fichier de log
/// </remarks>
public class SingletonLog : LogClass
{
    public static SingletonLog instance;
    
    // On crée un objet pour le lock cad que si un thread est en train d'utiliser l'objet, les autres threads doivent attendre
    private static readonly object LockObject = new object();
    
    public static SingletonLog GetInstance()
    {
        // On le premier appel, on va créer l'instance
        lock (LockObject)
        {
            // On vérifie si l'instance est null si oui on la crée
            if(instance == null)
            {
                instance = new SingletonLog();
            }
        }
        return instance;
    }
}