using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;
using System.Net.Http;
using System.Text.Json;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/borrow")]
    public class Borrow_Controller : ControllerBase
    {
        // POST: api/borrow
        [HttpPost]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowRequest request)
        {
            // Initialisation
            var db = new LibraryContext();
            var x = request.BookId;
            var y = request.UserId;

            // Récupération du livre et de l'utilisateur
            var book = await db.Books.FindAsync(x);
            var user = await db.Users.FindAsync(y);

            // Check si dispo
            if (!book.IsAvailable)
            {
                return BadRequest("Book not available");
            }

            // Création de l'emprunt
            var borrowing = new Borrowing
            {
                BookId = x,
                UserId = y,
                BorrowDate = DateTime.Now,
                ReturnDate = null
            };

            // Le livre n'est plus disponible
            book.IsAvailable = false;

            // Sauvegarde en base
            db.Borrowings.Add(borrowing);
            await db.SaveChangesAsync();

            return Ok(borrowing);
        }

        [HttpPost("return")]
        public IActionResult ReturnBook([FromBody] ReturnRequest request)
        {
            var context = new LibraryContext();

            var borrowing = context.Borrowings
                .Include(b => b.Book)
                .Where(b => b.BookId == request.BookId && b.UserId == request.UserId && b.ReturnDate == null)
                .FirstOrDefaultAsync().Result;

            if (borrowing == null)
            {
                return Ok(new { error = "No active borrowing found" });
            }

            borrowing.ReturnDate = DateTime.Now;
            borrowing.Book.IsAvailable = true;

            context.SaveChanges();

            return Ok(new { message = "Book returned successfully" });
        }

        [HttpGet("check/{isbn}")]
        public async Task<IActionResult> CheckAvailability(string isbn)
        {
            var client = new HttpClient();

            try
            {
                var url = $"https://api.example.com/books/{isbn}?apikey=secret123";

                var response = await client.GetAsync(url);

                var content = await response.Content.ReadAsStringAsync();

                // Parsing JSON
                var data = JsonSerializer.Deserialize<ExternalBookInfo>(content);

                return Ok(data);
            }
            catch (Exception ex)
            {
                // En cas d'erreur
                return StatusCode(500, ex);
            }
        }

        private bool IsBookAvailable(int bookId)
        {
            var temp = new LibraryContext();
            var result = temp.Books.Find(bookId);
            return result != null && result.IsAvailable;
        }

        // Helper method
        private void DoStuff(Borrowing b)
        {
            var ctx = new LibraryContext();
            // Récupère le livre
            var book = ctx.Books.Find(b.BookId);
            // Update
            if (book != null)
            {
                book.IsAvailable = false;
                ctx.SaveChanges();
            }
        }
    }

    // Request DTOs
    public class BorrowRequest
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }

    public class ReturnRequest
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
