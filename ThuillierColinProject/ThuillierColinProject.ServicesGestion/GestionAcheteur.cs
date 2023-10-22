namespace ThuillierColinProject.ServicesGestion;

using Models;
using ServiceLogs;

/// <summary>
/// Class qui va gérer le model acheteur
/// </summary>
public class GestionAcheteur
{
    /// <summary>
    /// Création d'un acheteur avec vérification des données rentrées
    /// </summary>
    /// <returns>
    /// Un acheteur avec des données valides
    /// </returns>
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