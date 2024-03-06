using GestionnaireDeLivre.Model;

namespace GestionnaireDeLivre;

public class DataSeeder
{
    private readonly AppDbContext _context;

    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        // Vérifie si la base de données a déjà été peuplée
        if (!_context.Books.Any())
        {
            // Auteurs
            var authors = new List<Author>
            {
                new Author { Name = "Claude Bit" },
                new Author { Name = "Java Scripto" },
                new Author { Name = "Algo Rithm" },
                new Author { Name = "Py Thon" },
                new Author { Name = "C Sharpe" }
            };

            // Livres
            var books = new List<Book>
            {
                new Book { Title = "Les Misérables Bugs", Genre = "Roman", Author = authors[0] },
                new Book { Title = "L'Étranger dans le Code", Genre = "Roman", Author = authors[1] },
                new Book { Title = "Guerre et Paix dans le Cloud", Genre = "Science-Fiction", Author = authors[2] },
                new Book { Title = "À la recherche du temps de Compilation", Genre = "Fantaisie", Author = authors[3] },
                new Book { Title = "Les Aventures d'Array Potter", Genre = "Fantaisie", Author = authors[4] },
                new Book
                {
                    Title = "Le Seigneur des Algorithmes: La Tour de CPU", Genre = "Fantaisie", Author = authors[0]
                },
                new Book { Title = "Moby-Click: La Baleine du Web", Genre = "Aventure", Author = authors[1] },
                new Book { Title = "Les Trois Mousquetaires du DevOps", Genre = "Aventure", Author = authors[2] },
                new Book
                {
                    Title = "Vingt Mille Lignes de Code sous les Mers", Genre = "Science-Fiction", Author = authors[3]
                },
                new Book { Title = "Le Code Da Vinci", Genre = "Mystère", Author = authors[4] }
            };
            // Création d'une bibliothèque
            var library = new Library
            {
                Id = 2,
                Books = books,
                Authors = authors
            };

            // Ajout des entités à l'DbContext
            _context.Libraries.Add(library);

            // Enregistrement des modifications dans la base de données
            _context.SaveChanges();
        }
    }
}