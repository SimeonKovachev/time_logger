using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using time_logger.Data;
using time_logger.Models;

namespace time_logger.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly TimeLoggerDbContext _context;

        public ProjectsController(TimeLoggerDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
              return _context.Projects != null ? 
                          View(await _context.Projects.ToListAsync()) :
                          Problem("Entity set 'TimeLoggerDbContext.Projects'  is null.");
        }

       
    }
}
