using ThuillierColinProject.Models;
using ThuillierColinProject.ServiceLogs;
using ThuillierColinProject.ServicesInteraction;

namespace ThuillierColinProject.Core;

using ThuillierColinProject.ServicesGestion;

public class CoreModels
{
    public CoreModels() { }
    
    public Acheteurs CreationAcheteur()
    {
        GestionAcheteur gestionAcheteur = new GestionAcheteur();
        return gestionAcheteur.CreationAcheteurs();
    }
    
    public Administrateur CreationAdministrateur()
    {
        GestionAdmin gestionAdmin = new GestionAdmin();
        return gestionAdmin.CreationAdministrateur();
    }
    
    public Article AjouterArticle()
    {
        GestionArticle gestionArticle = new GestionArticle();
        return gestionArticle.AjouterArticle();
    }
    
    public bool CreationBDD()
    {
        GestionAdmin gestionAdmin = new GestionAdmin();
        GestionArticle gestionArticle = new GestionArticle();
        return gestionAdmin.CreationBDD() && gestionArticle.CreationBDD();
    }
    
    public List<Article> RecupererArticle()
    {
        Lecture<Article> lecture = new Lecture<Article>();
        return lecture.LectureFichier();
    }
    
    public List<Acheteurs> RecupererAcheteurs()
    {
        Lecture<Acheteurs> lecture = new Lecture<Acheteurs>();
        return lecture.LectureFichier();
    }
    
    public List<Administrateur> RecupererAdministrateur()
    {
        Lecture<Administrateur> lecture = new Lecture<Administrateur>();
        return lecture.LectureFichier();
    }
}