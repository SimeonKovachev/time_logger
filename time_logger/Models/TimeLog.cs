using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace time_logger.Models
{
    public class TimeLog
    {
        [Key]
        public int TimeLogId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.25, 8.00)]
        public float HoursWorked { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
    }
}
