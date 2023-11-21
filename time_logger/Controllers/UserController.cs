using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using time_logger.Data;
using time_logger.Models;

namespace time_logger.Controllers
{
    // UserController handles API requests related to user data.
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TimeLoggerDbContext _context;

        // Constructor injecting the database context.
        public UserController(TimeLoggerDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        // Retrieves a list of all users.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/users/{id}
        // Retrieves a specific user by their ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
