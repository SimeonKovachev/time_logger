using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using time_logger.Data;
using time_logger.Models;
using time_logger.ViewModels;

namespace time_logger.Controllers
{
    public class TimeLogsController : Controller
    {
        private readonly TimeLoggerDbContext _context;

        public TimeLogsController(TimeLoggerDbContext context)
        {
            _context = context;
        }

        // GET: TimeLogs
        public async Task<IActionResult> Index()
        {
            var timeLogs = await _context.TimeLogs
                .Include(t => t.User)
                .Include(t => t.Project)
                .Select(t => new TimeLogViewModel
                {
                    UserName = t.User.FirstName + " " + t.User.LastName,
                    Email = t.User.Email,
                    ProjectName = t.Project.ProjectName,
                    Date = t.Date,
                    TimeWorked = t.HoursWorked
                }).ToListAsync();

            return View(timeLogs);
        }

        // GET: TimeLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TimeLogs == null)
            {
                return NotFound();
            }

            var timeLog = await _context.TimeLogs
                .Include(t => t.Project)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TimeLogId == id);
            if (timeLog == null)
            {
                return NotFound();
            }

            return View(timeLog);
        }

        // GET: TimeLogs/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: TimeLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeLogId,UserId,ProjectId,Date,HoursWorked")] TimeLog timeLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", timeLog.ProjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", timeLog.UserId);
            return View(timeLog);
        }

        // GET: TimeLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TimeLogs == null)
            {
                return NotFound();
            }

            var timeLog = await _context.TimeLogs.FindAsync(id);
            if (timeLog == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", timeLog.ProjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", timeLog.UserId);
            return View(timeLog);
        }

        // POST: TimeLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TimeLogId,UserId,ProjectId,Date,HoursWorked")] TimeLog timeLog)
        {
            if (id != timeLog.TimeLogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeLogExists(timeLog.TimeLogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", timeLog.ProjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", timeLog.UserId);
            return View(timeLog);
        }

        // GET: TimeLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TimeLogs == null)
            {
                return NotFound();
            }

            var timeLog = await _context.TimeLogs
                .Include(t => t.Project)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TimeLogId == id);
            if (timeLog == null)
            {
                return NotFound();
            }

            return View(timeLog);
        }

        // POST: TimeLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TimeLogs == null)
            {
                return Problem("Entity set 'TimeLoggerDbContext.TimeLogs'  is null.");
            }
            var timeLog = await _context.TimeLogs.FindAsync(id);
            if (timeLog != null)
            {
                _context.TimeLogs.Remove(timeLog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeLogExists(int id)
        {
          return (_context.TimeLogs?.Any(e => e.TimeLogId == id)).GetValueOrDefault();
        }
    }
}
