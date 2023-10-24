namespace ThuillierColinProject.ServicesInteraction;

using ThuillierColinProject.Models;

/// <summary>
/// Class qui va permettre de vérifier si un fichier existe.
/// Il sera le parent de la class Ecrire et Lecture
/// </summary>
/// <typeparam name="T"> Type de l'objet à écrire</typeparam>
public class Exist<T>
{
    
    public bool FileExist()
    {
        lock (FileLock.GetInstance().GetLockObjectFile())
        {
            return File.Exists(new ParentAttributeClass().GetAttribute(typeof(T)));
        }
    }

    public bool CreateFile()
    {
        lock (FileLock.GetInstance().GetLockObjectFile())
        {
            File.Create(new ParentAttributeClass().GetAttribute(typeof(T)));
        }
        return true;
    }
}