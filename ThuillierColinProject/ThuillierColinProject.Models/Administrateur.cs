using NLog;
using Newtonsoft.Json;

using ThuillierColinProject.ServiceLogs;

namespace ThuillierColinProject.Models;

[Path(@"./Data/Administrateur.json")]
public class Administrateur : ParentAttributeClass
{
    private Guid _id;
    private string _login;
    private string _password;
    
    public Administrateur()
    {
        SingletonLog.GetInstance().Log("Création d'un administrateur", LogClass.TypeMessage.Info);
        Id = Guid.NewGuid();
    }
    
    public Administrateur(string login, string password)
    {
        Id = Guid.NewGuid();
        Login = login;
        Password = password;
    }
    
    public Guid Id
    {
        get { return _id; }
        set { _id = value; }
    }
    
    public string Login
    {
        get { return _login; }
        set
        {
            SingletonLog.GetInstance().Log("L'utilisateur a rentré "+ value + " en login", LogClass.TypeMessage.Info);
            if(value == null || value.Trim().Equals(""))
            {
                throw new ExceptionClass("Erreur veuillez rentrer un login");
            }
            _login = value;
        }
    }
    
    public string Password
    {
        get { return _password; }
        set
        {
            SingletonLog.GetInstance().Log("L'utilisateur a rentré " + value + " en mot de passe",
                LogClass.TypeMessage.Info);
            if (value == null || value.Trim().Equals(""))
            {
                // Si le mot de passe est null ou vide
                throw new ExceptionClass("Erreur veuillez rentrer un mot de passe");
            }

            if (value.Length < 6)
            {
                // Si le mot de passe est inférieur à 6 caractères
                throw new ExceptionClass("Erreur veuillez rentrer un mot de passe supérieur à 6 caractères");
            }

            if (!value.Any(c => IsSpecialCharacter(c)))
            {
                // Si le mot de passe ne contient pas de chiffre
                throw new ExceptionClass("Erreur veuillez rentrer un mot de passe contenant un caractère spécial");
            }
            _password = value;
        }
    }
    
    static bool IsSpecialCharacter(char c)
    {
        // Définissez ici la liste de caractères spéciaux que vous souhaitez vérifier
        string caractèresSpeciaux = "!@#$%^&*()";

        // Utilisez la méthode Contains pour vérifier si le caractère est dans la liste
        return caractèresSpeciaux.Contains(c);
    }
    
    public override string ToString()
    {
        return "Id : " + _id + " / Login : " + _login + " / Password : " + _password;
    }
    
    public override bool Equals(object obj)
    {
        SingletonLog.GetInstance().Log("Comparaison d'administrateur",
            LogClass.TypeMessage.Info);
        if (obj == null)
        {
            return false;
        }
        if(obj is Administrateur administrateur)
        {
            return administrateur.Login.ToLower().Equals(Login.ToLower()) && administrateur.Password.ToLower().Equals(Password.ToLower());
        }
        return false;
    }
    
}