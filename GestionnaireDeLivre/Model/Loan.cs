namespace GestionnaireDeLivre.Model;

public class Loan
{
    public int Id { get; set; } // Identifiant de l'emprunt
    public int BookId { get; set; } // Identifiant du livre emprunté
    public Book Book { get; set; } // Livre emprunté
    public DateTime LoanDate { get; set; } // Date de l'emprunt
    public DateTime? ReturnDate { get; set; } // Date de retour (nullable pour gérer les livres non encore retournés)
}