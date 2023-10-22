using ThuillierColinProject.ServiceLogs;
using Newtonsoft.Json;

namespace ThuillierColinProject.Models;

[Path("./Data/ArticleAchetes.json")]
public class ArticleAchetes : ParentAttributeClass
{
    // Guid idAcheteur, Guid IdChocolat, Quantité (int), DateAchat (datetime)
    private Guid _idAcheteur;
    private Guid _idArticle;
    private int _quantite;
    private DateTime _dateAchat;
    
    public ArticleAchetes()
    {
        SingletonLog.GetInstance().Log("Création d'un article acheté", LogClass.TypeMessage.Info);
        DateAchat = DateTime.Now;
    }
    
    public ArticleAchetes(Guid idAcheteur, Guid idArticle, int quantite)
    {
        IdAcheteur = idAcheteur;
        IdArticle = idArticle;
        Quantite = quantite;
        DateAchat = DateTime.Now;
    }
    
    public Guid IdAcheteur
    {
        get { return _idAcheteur; }
        set { _idAcheteur = value; }
    }
    
    public Guid IdArticle
    {
        get { return _idArticle; }
        set { _idArticle = value; }
    }
    
    public int Quantite
    {
        get { return _quantite; }
        set
        {
            SingletonLog.GetInstance().Log("L'utilisateur a rentré " + value + " en quantité", LogClass.TypeMessage.Info);
            if(value < 0)
            {
                // Si la quantité est null ou vide
                throw new ExceptionClass("Erreur veuillez rentrer une quantité valide");
            }
            _quantite = value;
        }
    }
    
    
    public DateTime DateAchat
    {
        get { return _dateAchat; }
        set
        {
            SingletonLog.GetInstance().Log("L'utilisateur a rentré " + value + " en date d'achat", LogClass.TypeMessage.Info);
            if (value == null || typeof(DateTime) != value.GetType())
            {
                // Si la date est null ou vide
                throw new ExceptionClass("Erreur veuillez rentrer une date");
            }
            _dateAchat = value;
        }
    }
    
    public override string ToString()
    {
        return "IdAcheteur : " + IdAcheteur + " ; IdArticle : " + IdArticle + " ; Quantite : " + Quantite + " ; DateAchat : " + DateAchat;
    }
}