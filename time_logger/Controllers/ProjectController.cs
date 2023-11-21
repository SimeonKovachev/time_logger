using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using time_logger.Data;
using time_logger.Models;

namespace time_logger.Controllers
{
    // ProjectController handles API requests related to project data.
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly TimeLoggerDbContext _context;

        // Constructor injecting the database context.
        public ProjectController(TimeLoggerDbContext context)
        {
            _context = context;
        }

        // GET: api/projects
        // Retrieves a list of all projects.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        // GET: api/projects/{id}
        // Retrieves a specific project by its ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }
    }
}
