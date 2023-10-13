namespace ThuillierColinProject.Models;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class Path : Attribute
{
    public string FilePath { get; }

    public Path(string filePath)
    {
        FilePath = filePath;
    }
    
}