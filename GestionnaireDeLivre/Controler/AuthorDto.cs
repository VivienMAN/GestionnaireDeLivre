namespace GestionnaireDeLivre.Controler;

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    // Vous pouvez ajouter d'autres propriétés que vous souhaitez exposer
    // Par exemple, si vous souhaitez inclure le nombre de livres écrits par l'auteur
    public int NumberOfBooks { get; set; }
}