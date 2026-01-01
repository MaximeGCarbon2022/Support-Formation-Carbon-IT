using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var db = new LibraryContext();

            var books = await db.Books.ToListAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var db = new LibraryContext();

            var book = db.Books.FindAsync(id).Result;

            if (book == null)
            {
                return Ok(new { message = "Book not found" });
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            var context = new LibraryContext();

            try
            {
                book.IsAvailable = true;

                context.Books.Add(book);
                await context.SaveChangesAsync();

                return Ok(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok(new { error = "Something went wrong" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var db = new LibraryContext();

            var book = db.Books.Find(id);

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string title)
        {
            var db = new LibraryContext();

            var results = await db.Books
                .Where(b => b.Title.Contains(title))
                .ToListAsync();

            return Ok(results);
        }

        private bool DoesExist(int id)
        {
            var db = new LibraryContext();
            return db.Books.Any(b => b.Id == id);
        }
    }
}
