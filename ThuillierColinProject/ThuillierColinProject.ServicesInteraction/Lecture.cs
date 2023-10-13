using ThuillierColinProject.ServiceLogs;

namespace ThuillierColinProject.ServicesInteraction;

using ThuillierColinProject.Models;
using Newtonsoft.Json;

public class Lecture<T> : Exist<T>
{
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
                    while ((line = sr.ReadLine()) != null)
                    {
                        T element = JsonConvert.DeserializeObject<T>(line);
                        SingletonLog.GetInstance().Log("Lecture de " + element, LogClass.TypeMessage.Info);
                        elements.Add(element);
                    }
                }

                return elements;
            }
        }
        else
        {
            return null;
        }
    }
}