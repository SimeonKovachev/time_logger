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

        // Asynchronously seed the database
        public async Task SeedDatabaseAsync()
        {
            await ClearTablesAsync();
            await GenerateUsersAsync();
            await GenerateProjectsAsync();
            await GenerateTimeLogsAsync();
        }

        // Asynchronously clear all tables
        private async Task ClearTablesAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Remove all entries from tables
                    _context.TimeLogs.RemoveRange(_context.TimeLogs);
                    _context.Projects.RemoveRange(_context.Projects);
                    _context.Users.RemoveRange(_context.Users);
                    await _context.SaveChangesAsync();

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // Log the exception and rollback the transaction
                    Console.WriteLine(ex.Message);
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        // Asynchronously generate users
        private async Task GenerateUsersAsync()
        {
            // Sample data for user generation
            var firstNames = new[] { "John", "Gringo", "Mark", "Lisa", "Maria", "Sonya", "Philip", "Jose", "Lorenzo", "George", "Justin" };
            var lastNames = new[] { "Johnson", "Lamas", "Jackson", "Brown", "Mason", "Rodriguez", "Roberts", "Thomas", "Rose", "McDonalds" };
            var domains = new[] { "hotmail.com", "gmail.com", "live.com" };

            // Generate 100 random users
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
                await _context.Users.AddAsync(newUser);
            }

            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Asynchronously generate projects
        private async Task GenerateProjectsAsync()
        {
            // Sample project names
            var projectNames = new[] { "My own", "Free Time", "Work" };

            // Generate projects
            foreach (var projectName in projectNames)
            {
                var newProject = new Project
                {
                    ProjectName = projectName
                };

                // Add project to context
                await _context.Projects.AddAsync(newProject);
            }

            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Asynchronously generate TimeLog entries
        private async Task GenerateTimeLogsAsync()
        {
            // Retrieve all users and projects
            var users = await _context.Users.ToListAsync();
            var projects = await _context.Projects.ToListAsync();

            // Generate TimeLog entries for each user
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

                    // Add TimeLog to context
                    await _context.TimeLogs.AddAsync(timeLog);
                }
            }

            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Generate a random date for TimeLog entries
        private DateTime RandomDay()
        {
            DateTime start = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }
    }
}
