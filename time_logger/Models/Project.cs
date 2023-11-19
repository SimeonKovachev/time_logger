using System.ComponentModel.DataAnnotations;

namespace time_logger.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProjectName { get; set; }

        // Navigation property
        public virtual ICollection<TimeLog> TimeLogs { get; set; }
    }
}
