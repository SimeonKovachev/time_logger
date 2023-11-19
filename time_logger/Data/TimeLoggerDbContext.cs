using Microsoft.EntityFrameworkCore;
using time_logger.Models;

namespace time_logger.Data
{
    public class TimeLoggerDbContext : DbContext
    {
        public TimeLoggerDbContext(DbContextOptions<TimeLoggerDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure a composite key
            modelBuilder.Entity<TimeLog>().HasKey(t => t.TimeLogId);

            // Configure one-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.TimeLogs)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.TimeLogs)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

        }
    }
}
