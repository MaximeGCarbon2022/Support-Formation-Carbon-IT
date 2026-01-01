using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Création du contexte
            var context = new LibraryContext();

            try
            {
                var user = await context.Users
                    .Include(u => u.Borrowings)
                    .ThenInclude(b => b.Book)
                    .FirstOrDefaultAsync(u => u.Id == id);

                // Vérification si l'utilisateur existe
                if (user == null)
                {
                    return StatusCode(500, "User not found");
                }

                return Ok(user);
            }
            catch
            {
                // Gestion des erreurs
                return StatusCode(500);
            }
        }

        // POST: api/users
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            var db = new LibraryContext();

            // Validation du nom
            if (user.Name == null)
            {
                return BadRequest("Name is required");
            }

            db.Users.Add(user);

            db.SaveChanges();

            return Ok(user);
        }

        // Vérifie si un email existe
        private bool CheckEmail(string email)
        {
            // Nouvelle instance de contexte
            var context = new LibraryContext();

            var exists = context.Users.Any(u => u.Email == email);

            return exists;
        }

        // GET: api/users/stats
        // TODO: implémenter les stats
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            return Ok(new { message = "Coming soon" });
        }
    }
}
