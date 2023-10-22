namespace ThuillierColinProject.Core;

/// <summary>
/// Class qui va permettre de créer un singleton pour la couche core
/// </summary>
public class CoreSingleton
{
    private static CoreSingleton _instance = null;
    
    // On crée un objet pour le lock cad que si un thread est en train d'utiliser l'objet, les autres threads doivent attendre
    private static readonly object LockObject = new object();
    
    public readonly CoreInteraction CoreInteraction = new CoreInteraction();
    public readonly CoreModels CoreModels = new CoreModels();
    public readonly CoreGestion CoreGestion = new CoreGestion();
    
    public static CoreSingleton GetInstance()
    {
        // On le premier appel, on va créer l'instance
        lock (LockObject)
        {
            // On vérifie si l'instance est null si oui on la crée
            if (_instance == null)
            {
                _instance = new CoreSingleton();
            }
        }
        return _instance;
    }
}