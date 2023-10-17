using ThuillierColinProject.ServiceLogs;

namespace ThuillierColinProject.ServicesInteraction;

using ThuillierColinProject.Models;
using Newtonsoft.Json;

public class Lecture<T> : Exist<T>
{
    /// <summary>
    /// Fonction qui permet de lire un fichier
    /// </summary>
    /// <returns>
    /// La liste des éléments du fichier si le fichier existe sinon une liste vide
    /// </returns>
    public List<T> LectureFichier()
    {
        if (FileExist())
        {
            lock (FileLock.GetInstance().GetLockObjectFile())
            {
                List<T> elements = new List<T>();
                using (StreamReader sr = new StreamReader(new ParentAttributeClass().GetAttribute(typeof(T))))
                {
                    string line;
                    SingletonLog.GetInstance().Log("Lecture du fichier " + new ParentAttributeClass().GetAttribute(typeof(T)), LogClass.TypeMessage.Info);
                    SingletonLog.GetInstance().Log("------------------------------------", LogClass.TypeMessage.Info);
                    while ((line = sr.ReadLine()) != null)
                    {
                        T element = JsonConvert.DeserializeObject<T>(line);
                        SingletonLog.GetInstance().Log("Lecture de " + element, LogClass.TypeMessage.Info);
                        elements.Add(element);
                    }
                    SingletonLog.GetInstance().Log("------------------------------------", LogClass.TypeMessage.Info);
                }

                return elements;
            }
        }
        SingletonLog.GetInstance().Log("Le fichier " + new ParentAttributeClass().GetAttribute(typeof(T)) + " n'existe pas", LogClass.TypeMessage.Warn);
        return new List<T>();
    }
}