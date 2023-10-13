namespace ThuillierColinProject.ServicesInteraction;

using Newtonsoft.Json;
using ThuillierColinProject.Models;
using ThuillierColinProject.ServiceLogs;

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
