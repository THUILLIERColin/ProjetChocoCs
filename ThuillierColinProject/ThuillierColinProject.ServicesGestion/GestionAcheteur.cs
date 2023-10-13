namespace ThuillierColinProject.ServicesGestion;

using ThuillierColinProject.Models;
using ThuillierColinProject.ServiceLogs;

public class GestionAcheteur
{
    public Acheteurs CreationAcheteurs()
    {
        Acheteurs acheteur = new Acheteurs();
        bool tmpNom = false;
        bool tmpPrenom = false;
        bool tmpAdresse = false;
        bool tmpTelephone = false;
        
        Console.WriteLine("Veuillez rentrer un nom, un prénom, une adresse et un numéro de téléphone");
        do
        {
            try
            {
                if (!tmpNom)
                {
                    Console.WriteLine("Nom : ");
                    acheteur.Nom = Console.ReadLine();
                    tmpNom = true;
                    
                }
                if (!tmpPrenom)
                {
                    Console.WriteLine("Prenom : ");
                    acheteur.Prenom= Console.ReadLine();
                    tmpPrenom = true;
                }
                if (!tmpAdresse)
                {
                    Console.WriteLine("Adresse : ");
                    acheteur.Adresse = Console.ReadLine();
                    tmpAdresse = true;
                }
                if (!tmpTelephone)
                {
                    Console.WriteLine("Telephone : ");
                    acheteur.Telephone = Console.ReadLine();
                    tmpTelephone = true;
                }
            }
            catch (ExceptionClass e)
            {
                Console.WriteLine(e);
            }
        } while (!(tmpNom && tmpPrenom && tmpAdresse && tmpTelephone));
        SingletonLog.GetInstance().Log("Un acheteur a été créé :" + acheteur, LogClass.TypeMessage.Info);
        return acheteur;
    }
    
}