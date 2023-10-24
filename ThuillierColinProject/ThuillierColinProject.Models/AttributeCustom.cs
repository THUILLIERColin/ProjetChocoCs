namespace ThuillierColinProject.Models;

/// <summary>
/// Class qui va gérer les attributs des classes
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class Path : Attribute
{
    public string FilePath { get; }

    public Path(string filePath)
    {
        FilePath = filePath;
    }
    
}