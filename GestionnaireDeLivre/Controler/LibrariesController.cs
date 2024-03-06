using GestionnaireDeLivre.Model;
using GestionnaireDeLivre.Repositorys;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionnaireDeLivre.Controler;

[Route("api/[controller]")]
[ApiController]
public class LibrariesController : ControllerBase
{
    private readonly Repository<Library> _libraryRepository;

    public LibrariesController(Repository<Library>  libraryRepository)
    {
        _libraryRepository = libraryRepository;
    }

    // GET: api/Libraries/5/Books
    [HttpGet("{id}/Books")]
    public async Task<ActionResult<List<BookDTO>>> GetBooksOfLibrary(int id)
    {
        
        var books = await GetBooksOfLibraryAsync(id);

        if (books == null)
        {
            return null; // Corrigé ici
        }
        var booksDTO = books.Select(book => new BookDTO
        {
            Id = book.Id,
            Title = book.Title,
            Genre = book.Genre,
            // Copiez les autres champs nécessaires
        }).ToList();

        return booksDTO;
    }
    [HttpGet("MostFrequentAuthor")]
    public async Task<ActionResult<AuthorDto>> GetMostFrequentAuthorInAllLibraries()
    {
        var mostFrequentAuthor = await _libraryRepository.GetContext().Books
            .Include(b => b.Author)
            .ThenInclude(b => b.Books)
            .GroupBy(b => b.AuthorId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.First().Author)
            .FirstOrDefaultAsync();

        if (mostFrequentAuthor == null)
        {
            return null;
        }

        var sd = new AuthorDto
        {
            Id = mostFrequentAuthor.Id,
            Name = mostFrequentAuthor.Name,
            NumberOfBooks = mostFrequentAuthor.Books.Count // Supposant que chaque auteur a une collection de livres
        };
        return sd;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Library>>> GetAllLibraries()
    {
        return await _libraryRepository.GetContext().Libraries.ToListAsync();
    }
    
    [HttpPost]
    public async Task<ActionResult<Library>> CreateLibrary(Library library)
    {
        _libraryRepository.GetContext().Libraries.Add(library);
        await _libraryRepository.GetContext().SaveChangesAsync();

        return library;
    }
    
    [HttpGet("{id}/MostFrequentAuthor")]
    public async Task<ActionResult<AuthorDto>> GetMostFrequentAuthorInLibrary(int id)
    {
        var library = await _libraryRepository.GetContext().Libraries
            .Include(l => l.Books)
            .ThenInclude(b => b.Author)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (library == null)
        {
            return null;
        }

        var mostFrequentAuthor = library.Books
            .GroupBy(b => b.AuthorId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.First().Author)
            .FirstOrDefault();

        if (mostFrequentAuthor == null)
        {
            return null;
        }
        var sd = new AuthorDto
        {
            Id = mostFrequentAuthor.Id,
            Name = mostFrequentAuthor.Name,
            NumberOfBooks = mostFrequentAuthor.Books.Count // Supposant que chaque auteur a une collection de livres
        };
        return sd;
    }

    private async Task<List<Book>> GetBooksOfLibraryAsync(int libraryId)
    {
        var libraryWithBooks = await _libraryRepository.GetContext().
            Libraries
            .Include(lib => lib.Books)
            .ThenInclude(book => book.Author) 
            .FirstOrDefaultAsync(lib => lib.Id == libraryId);

        return libraryWithBooks?.Books.ToList();
    }
}