namespace GestionnaireDeLivre.Model;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    
    public int LibraryId { get; set; } // Assurez-vous d'ajouter cette propriété
    public Library Library { get; set; } // Navigation vers Library
}