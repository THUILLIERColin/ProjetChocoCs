namespace ThuillierColinProject.ServicesInteraction;

using Newtonsoft.Json;
using ThuillierColinProject.Models;
using ThuillierColinProject.ServiceLogs;

/// <summary>
/// Class qui va permettre d'écrire dans un fichier
/// </summary>
/// <typeparam name="T"> Type de l'objet à écrire</typeparam>
/// <remarks>
/// Le type de l'objet doit être un type de la classe ParentAttributeClass pour que la méthode GetAttribute fonctionne.
/// Car le filepath est récupéré grâce à la méthode GetAttribute de la classe ParentAttributeClass
/// </remarks>
public class Ecrire<T> : Exist<T>
{
    public bool Ecriture(T element)
    {
        lock (FileLock.GetInstance().GetLockObjectFile())
        {
            using (StreamWriter sw = new StreamWriter(new ParentAttributeClass().GetAttribute(typeof(T)), true))
            {
                string json = JsonConvert.SerializeObject(element);
                SingletonLog.GetInstance().Log("Ecriture de " + element, LogClass.TypeMessage.Info);
                sw.WriteLine(json);
            }

            return true;
        }
    }
}
