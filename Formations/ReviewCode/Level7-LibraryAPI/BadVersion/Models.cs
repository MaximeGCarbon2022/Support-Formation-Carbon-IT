using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public bool IsAvailable { get; set; }

        public List<Borrowing> Borrowings { get; set; }
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public List<Borrowing> Borrowings { get; set; }
    }

    public class Borrowing
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }

    // Database context
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }

        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configuration par défaut
                optionsBuilder.UseSqlServer("Server=localhost;Database=LibraryDB;User Id=admin;Password=Admin123!;");
            }
        }
    }

    // DTO pour l'API externe
    public class ExternalBookInfo
    {
        public string title { get; set; }
        public string author { get; set; }


        
        public bool available { get; set; }
    }
}
