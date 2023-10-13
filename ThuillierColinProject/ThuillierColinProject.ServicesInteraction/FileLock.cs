namespace ThuillierColinProject.ServicesInteraction;

public class FileLock
{
    private static readonly object LockObjectFile = new object();
    private static readonly object LockObject = new object();
    private static FileLock _fileLockInstance;
    
    public static FileLock GetInstance()
    {
        lock (LockObject)
        {
            if (_fileLockInstance == null)
            {
                _fileLockInstance = new FileLock();
            }
        }
        return _fileLockInstance;
    }
    
    public object GetLockObjectFile()
    {
        return LockObjectFile;
    }
}