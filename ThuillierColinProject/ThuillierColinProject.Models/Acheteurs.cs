using ThuillierColinProject.ServiceLogs;
using Newtonsoft.Json;

namespace ThuillierColinProject.Models;

[Path("./Data/Acheteurs.json")]
public class Acheteurs : ParentAttributeClass
{
    private Guid _id;
    private string _nom;
    private string _prenom;
    private string _adresse;
    private string _telephone;
    
    public Acheteurs()
    {
        SingletonLog.GetInstance().Log("Création d'un acheteur", LogClass.TypeMessage.Info);
        _id = Guid.NewGuid();
    }
    
    public Acheteurs(string nom, string prenom, string adresse, string telephone)
    {
        _id = Guid.NewGuid();
        Nom = nom;
        Prenom = prenom;
        Adresse = adresse;
        Telephone = telephone;
    }
    
    public Guid Id
    {
        get { return _id; }
        set { _id = value; }
    }
    
    public string Nom
    {
        get { return _nom; }
        set
        {
            SingletonLog.GetInstance().Log("L'utilisateur a rentré " + value + " en nom", LogClass.TypeMessage.Info);
            if (value == null || value.Trim().Equals(""))
            {
                // Si la valeur est null ou vide
                throw new ExceptionClass("Erreur veuillez rentrer un nom");
            }
            _nom = value;
        }
    }
    
    public string Prenom
    {
        get { return _prenom; }
        set
        {
            SingletonLog.GetInstance().Log("L'utilisateur a rentré " + value + " en prenom", LogClass.TypeMessage.Info);
            if (value == null || value.Trim().Equals(""))
            {
                // Si la valeur est null ou vide
                throw new ExceptionClass("Erreur veuillez rentrer un prenom");
            }
            _prenom = value;
        }
    }
    
    public string Adresse
    {
        get { return _adresse; }
        set
        {
            SingletonLog.GetInstance().Log("L'utilisateur a rentré " + value + " en adresse", LogClass.TypeMessage.Info);
            if (value == null || value.Trim().Equals(""))
            {
                // Si la valeur est null ou vide
                throw new ExceptionClass("Erreur veuillez rentrer une adresse");
            }
            _adresse = value;
        }   
    }
    
    public string Telephone
    {
        get { return _telephone; }
        set
        {
            SingletonLog.GetInstance().Log("L'utilisateur a rentré " + value + " en telephone", LogClass.TypeMessage.Info);
            if(value == null || value.Trim().Equals(""))
            {
                // Si le telephone est null ou vide
                throw new ExceptionClass("Erreur veuillez rentrer un numéro de téléphone");
            }
            if(value.Length != 10)
            {
                // Si le telephone ne fait pas 10 caractères
                throw new ExceptionClass("Erreur veuillez rentrer un numéro de téléphone à 10 chiffres");
            }
            _telephone = value;
        }
    }
    
    public override string ToString()
    {
        return $"Id: {_id}, Nom: {_nom}, Prenom: {_prenom}, Adresse: {_adresse}, Telephone: {_telephone}";
    }
    
    public override bool Equals(object obj)
    {
        SingletonLog.GetInstance().Log("Comparaison d'acheteur", LogClass.TypeMessage.Info);
        if(obj == null)
        {
            return false;
        }
        if(obj is Acheteurs acheteurs)
        {
            return acheteurs.Nom.ToLower().Equals(Nom.ToLower()) && acheteurs.Prenom.ToLower().Equals(Prenom.ToLower()) && acheteurs.Adresse.ToLower().Equals(Adresse.ToLower()) && acheteurs.Telephone.ToLower().Equals(Telephone.ToLower());
        }
        return false;
    }
}