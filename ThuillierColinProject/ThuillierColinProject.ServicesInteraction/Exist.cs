namespace ThuillierColinProject.ServicesInteraction;

using ThuillierColinProject.Models;

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