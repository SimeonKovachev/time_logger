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
    public class UsersController : Controller
    {
        private readonly TimeLoggerDbContext _context;

        public UsersController(TimeLoggerDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'TimeLoggerDbContext.Users'  is null.");
        }

    }
}
