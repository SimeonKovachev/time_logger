using System.ComponentModel.DataAnnotations;

namespace time_logger.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        // Navigation property
        public virtual ICollection<TimeLog> TimeLogs { get; set; }
    }
}
