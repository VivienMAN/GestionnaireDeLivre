namespace GestionnaireDeLivre.Model;

public class Library
{    public int Id { get; set; }
    public List<Book> Books { get; set; } = new List<Book>(); // Liste des livres dans la bibliothèque
    public List<Author> Authors { get; set; } = new List<Author>(); // Liste des auteurs dans la bibliothèque


    // Méthode pour ajouter un livre à la bibliothèque
    public void AddBook(Book book)
    {
        Books.Add(book); // Ajout du livre à la liste
        if (!Authors.Contains(book.Author)) // Si l'auteur n'est pas déjà dans la liste
        {
            Authors.Add(book.Author); // Ajout de l'auteur à la liste
        }
    }

    // Méthode pour ajouter un auteur à la bibliothèque
    public void AddAuthor(Author author)
    {
        Authors.Add(author); // Ajout de l'auteur à la liste
    }
}