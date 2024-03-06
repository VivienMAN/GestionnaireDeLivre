using Microsoft.EntityFrameworkCore;

namespace GestionnaireDeLivre;


public class DataTester
{
    private readonly AppDbContext _context;

    public DataTester(AppDbContext context)
    {
        _context = context;
    }

    public void TestSeeding()
    {
        // Test pour vérifier si les auteurs ont été ajoutés
        var authorsCount = _context.Authors.Count();
        Console.WriteLine($"Nombre d'auteurs dans la base de données: {authorsCount}");

        // Test pour vérifier si les livres ont été ajoutés
        var booksCount = _context.Books.Count();
        Console.WriteLine($"Nombre de livres dans la base de données: {booksCount}");

        // Test pour vérifier si la bibliothèque a été ajoutée
        var librariesCount = _context.Libraries.Count();
        Console.WriteLine($"Nombre de bibliothèques dans la base de données: {librariesCount}");

        // Afficher les détails des livres
        var books = _context.Books.Include(b => b.Author).ToList();
        foreach (var book in books)
        {
            Console.WriteLine($"Livre: {book.Title}, Genre: {book.Genre}, Auteur: {book.Author.Name}");
        }
    }
}