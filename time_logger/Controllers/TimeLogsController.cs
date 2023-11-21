using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using time_logger.Data;
using time_logger.Models;

namespace time_logger.Controllers
{
    [Route("api/timelogs")]
    [ApiController]
    public class TimeLogsController : ControllerBase
    {
        private readonly TimeLoggerDbContext _context;

        public TimeLogsController(TimeLoggerDbContext context)
        {
            _context = context;
        }

        // GET: api/timelogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeLog>>> GetTimeLogs(
            int pageNumber = 1, int pageSize = 10,
            string sortBy = "Date", string sortOrder = "asc",
            DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.TimeLogs
                .Include(t => t.User)
                .Include(t => t.Project)
                .AsQueryable();

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(t => t.Date >= startDate.Value && t.Date <= endDate.Value);
            }

            switch (sortBy.ToLower())
            {
                case "username":
                    query = sortOrder.ToLower() == "asc" ? query.OrderBy(t => t.User.FirstName + " " + t.User.LastName) : query.OrderByDescending(t => t.User.FirstName + " " + t.User.LastName);
                    break;
                case "useremail":
                    query = sortOrder.ToLower() == "asc" ? query.OrderBy(t => t.User.Email) : query.OrderByDescending(t => t.User.Email);
                    break;
                case "projectname":
                    query = sortOrder.ToLower() == "asc" ? query.OrderBy(t => t.Project.ProjectName) : query.OrderByDescending(t => t.Project.ProjectName);
                    break;
                case "hoursworked":
                    query = sortOrder.ToLower() == "asc" ? query.OrderBy(t => t.HoursWorked) : query.OrderByDescending(t => t.HoursWorked);
                    break;
                // Default sorting by date
                default:
                    query = sortOrder.ToLower() == "asc" ? query.OrderBy(t => t.Date) : query.OrderByDescending(t => t.Date);
                    break;
            }

            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
