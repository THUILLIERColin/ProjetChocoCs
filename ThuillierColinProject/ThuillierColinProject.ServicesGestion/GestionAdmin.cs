using NLog;

namespace ThuillierColinProject.ServicesGestion;

using ThuillierColinProject.Models;
using ThuillierColinProject.ServiceLogs;
using ThuillierColinProject.ServicesInteraction;

public class GestionAdmin
{
    public Administrateur CreationAdministrateur()
    {
        Console.WriteLine("Veuillez rentrer un login et un mot de passe");
        Administrateur admin = new Administrateur(); 
        bool tmpLogin = false;
        bool tmpPassword = false;
        do
        {
            try
            {
                if (!tmpLogin)
                {
                    Console.WriteLine("Login : ");
                    admin.Login = Console.ReadLine();
                    tmpLogin = true;
                }
                if (!tmpPassword)
                {
                    Console.WriteLine("Password : ");
                    admin.Password = Console.ReadLine();
                    tmpPassword = true;
                }
            }catch(ExceptionClass e)
            {
                Console.WriteLine(e);
            }
        } while (!(tmpLogin && tmpPassword));
        SingletonLog.GetInstance().Log("Un administrateur a été créé :" + admin, LogClass.TypeMessage.Info);
        return admin;
    }

    public bool CreationBDD()
    {
        Ecrire<Administrateur> ecrire = new Ecrire<Administrateur>();
        if (!ecrire.FileExist())
        {
            SingletonLog.GetInstance().Log("Création d'un administrateur", LogClass.TypeMessage.Info);
            Administrateur admin = new Administrateur("admin", "amdin!");
            ecrire.Ecriture(admin);
            return true;
        }
        SingletonLog.GetInstance().Log("Le fichier Administrateur.json existe déjà", LogClass.TypeMessage.Info);
        return false;
    }
}