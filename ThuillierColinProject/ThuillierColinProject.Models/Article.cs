using ThuillierColinProject.ServiceLogs;
using Newtonsoft.Json;

namespace ThuillierColinProject.Models;

[Path("./Data/Article.json")]
public class Article : ParentAttributeClass
{
    private Guid _id;
    private string _reference;
    private float _prix;
    
    public Article()
    {
        SingletonLog.GetInstance().Log("Création d'un article", LogClass.TypeMessage.Info);
        Id = Guid.NewGuid();
    }
    
    public Article(string reference, float prix)
    {
        Id = Guid.NewGuid();
        Reference = reference;
        Prix = prix;
    }
    
    public Guid Id
    {
        get { return _id; }
        set { _id = value; }
    }
    
    public string Reference
    {
        get { return _reference; }
        set
        {
            SingletonLog.GetInstance().Log("L'utilisateur a rentré " + value + " en référence", LogClass.TypeMessage.Info);
            if (value == null || value.Trim().Equals(""))
            {
                // Si la valeur est null ou vide
                throw new ExceptionClass("Erreur veuillez rentrer une référence");
            }
            _reference = value;
        }
    }
    
    public float Prix
    {
        get { return _prix; }
        set
        {
            SingletonLog.GetInstance().Log("L'utilisateur a rentré " + value + " en prix", LogClass.TypeMessage.Info);
            if(value < 0 || value == null)
            {
                // Si le prix est null ou vide
                throw new ExceptionClass("Erreur veuillez rentrer un prix");
            }
            _prix = value;
        }
    }
    
    public override string ToString()
    {
        return "Id : " + Id + " Reference : " + _reference + " value : " + _prix;
    }
    
    public override bool Equals(object obj)
    {
        SingletonLog.GetInstance().Log("Comparaison de deux articles", LogClass.TypeMessage.Info);
        if(obj == null)
        {
            return false;
        }
        if(obj is Article article)
        {
            return article.Id == Id;
        }
        return false;
    }
}