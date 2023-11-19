using Microsoft.EntityFrameworkCore;
using time_logger.Models;

namespace time_logger.Data
{
    public class DbInitializer
    {
        private readonly TimeLoggerDbContext _context;
        private readonly Random _random = new Random();

        public DbInitializer(TimeLoggerDbContext context)
        {
            _context = context;
        }

        public void SeedDatabase()
        {
            ClearTables();
            GenerateUsers();
            GenerateProjects();
            GenerateTimeLogs();
        }
        // Implementation to clear tables
        private void ClearTables()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
 
                    _context.TimeLogs.RemoveRange(_context.TimeLogs); 
                    _context.Projects.RemoveRange(_context.Projects);
                    _context.Users.RemoveRange(_context.Users);
                    _context.SaveChanges();

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Log the exception (consider using a logging framework)
                    Console.WriteLine(ex.Message);

                    // Rollback the transaction on error
                    transaction.Rollback();

                    // Optionally, rethrow the exception to handle it further up the call stack
                    throw;
                }
            }
        }

        // Implementation to generate users
        private void GenerateUsers()
        {
            // Define sample data
            var firstNames = new[] { "John", "Gringo", "Mark", "Lisa", "Maria", "Sonya", "Philip", "Jose", "Lorenzo", "George", "Justin" };
            var lastNames = new[] { "Johnson", "Lamas", "Jackson", "Brown", "Mason", "Rodriguez", "Roberts", "Thomas", "Rose", "McDonalds" };
            var domains = new[] { "hotmail.com", "gmail.com", "live.com" };

            //Generate 100 random users
            for (int i = 0; i < 100; i++)
            {
                var firstName = firstNames[_random.Next(firstNames.Length)];
                var lastName = lastNames[_random.Next(lastNames.Length)];
                var domain = domains[_random.Next(domains.Length)];
                var email = $"{firstName}.{lastName}@{domain}".ToLower();

                var newUser = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };

                // Add user to context
                _context.Users.Add(newUser);
            }

            // Save changes to the database
            try
            {
                _context.SaveChanges();
            }
            //check for errors
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        // Implementation to generate projects
        private void GenerateProjects()
        {
            // Define project names
            var projectNames = new[] { "My own", "Free Time", "Work" };

            foreach (var projectName in projectNames)
            {
                var newProject = new Project
                {
                    ProjectName = projectName
                };

                _context.Projects.Add(newProject);
            }
            // Save changes to the database
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                // Handle the exception as needed
                throw;
            }
        }

        // Implementation to generate TimeLog entries
        private void GenerateTimeLogs()
        {
            var users = _context.Users.ToList();
            var projects = _context.Projects.ToList();

            foreach (var user in users)
            {
                int numberOfEntries = _random.Next(1, 21); // Generate 1 to 20 entries

                for (int i = 0; i < numberOfEntries; i++)
                {
                    var project = projects[_random.Next(projects.Count)];
                    var hoursWorked = (float)(_random.NextDouble() * (8.00 - 0.25) + 0.25); // Generate hours between 0.25 and 8.00

                    var timeLog = new TimeLog
                    {
                        UserId = user.UserId,
                        ProjectId = project.ProjectId,
                        Date = RandomDay(),
                        HoursWorked = hoursWorked
                    };

                    _context.TimeLogs.Add(timeLog);
                }
            }
            // Save changes to the database
            try
            {
                _context.SaveChanges();    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private DateTime RandomDay()
        {
            DateTime start = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }

    }
}
