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
        private readonly DbInitializer _dbInitializer;

        public TimeLogsController(TimeLoggerDbContext context, DbInitializer dbInitializer)
        {
            _context = context;
            _dbInitializer = dbInitializer;
        }


        // GET: TimeLogs
        [HttpGet]
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

            // Top 10 Users
            var topUsers = await _context.TimeLogs
                .GroupBy(t => t.User)
                .Select(g => new ChartData
                {
                    Name = g.Key.FirstName + " " + g.Key.LastName,
                    TotalHours = g.Sum(t => t.HoursWorked)
                })
                .OrderByDescending(t => t.TotalHours)
                .Take(10)
                .ToListAsync();

            // Top 10 Projects
            var topProjects = await _context.TimeLogs
                .GroupBy(t => t.Project)
                .Select(g => new ChartData
                {
                    Name = g.Key.ProjectName,
                    TotalHours = g.Sum(t => t.HoursWorked)
                })
                .OrderByDescending(t => t.TotalHours)
                .Take(10)
                .ToListAsync();

            ViewBag.UserDataSource = topUsers;
            ViewBag.ProjectDataSource = topProjects;


            return View(timeLogs);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserComparisonData(int userId)
        {
            var userTotalHours = await _context.TimeLogs
       .Where(t => t.UserId == userId)
       .SumAsync(t => t.HoursWorked);

            return Json(new { TotalHours = userTotalHours });
        }

        //Logic for the DatabaseInitializer button
        [HttpPost]
        public async Task<IActionResult> InitializeDatabase()
        {
            try
            {
                await _dbInitializer.SeedDatabaseAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

     
        public class ChartData
        {
            public string Name { get; set; }
            public double TotalHours { get; set; }
        }




    }
}
