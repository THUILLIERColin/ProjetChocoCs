namespace ThuillierColinProject.Models;

public class ParentAttributeClass
{
    public string GetAttribute(Type t)
    {
        Path[] MyAttributes = (Path[]) Attribute.GetCustomAttributes(t, typeof (Path));

        if (MyAttributes.Length == 0)
        {
            throw new ExceptionClass("Erreur, l'attribut n'existe pas le fichier ne peut pas être chargé");
        }
        return MyAttributes[0].FilePath;
    }
    
}