namespace ThuillierColinProject.Core;

public class CoreSingleton
{
    private static CoreSingleton instance = null;
    
    // On crée un objet pour le lock cad que si un thread est en train d'utiliser l'objet, les autres threads doivent attendre
    private static readonly object LockObject = new object();
    
    public CoreInteraction coreInteraction = new CoreInteraction();
    public CoreModels coreModels = new CoreModels();
    public CoreGestion coreGestion = new CoreGestion();
    
    public static CoreSingleton GetInstance()
    {
        // On le premier appel, on va créer l'instance
        lock (LockObject)
        {
            // On vérifie si l'instance est null si oui on la crée
            if(instance == null)
            {
                instance = new CoreSingleton();
            }
        }
        return instance;
    }
}