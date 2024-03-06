using GestionnaireDeLivre.Model;
using Microsoft.EntityFrameworkCore;

namespace GestionnaireDeLivre;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } // Définition de l'ensemble des livres
    public DbSet<Author> Authors { get; set; } // Définition de l'ensemble des auteurs
    public DbSet<Loan> Loans { get; set; } // Définition de l'ensemble des emprunts
    public DbSet<Library> Libraries { get; set; } // Définition de l'ensemble des bibliothèques

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasKey(a => a.Id);
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);

        modelBuilder.Entity<Book>()
            .HasKey(b => b.Id);
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Library) // Assurez-vous que cette navigation est définie dans Book
            .WithMany(l => l.Books)
            .HasForeignKey(b => b.LibraryId); // Assurez-vous que cette propriété existe dans Book

        modelBuilder.Entity<Loan>()
            .HasKey(l => l.Id);
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany()
            .HasForeignKey(l => l.BookId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("LocalDb");
    }
}