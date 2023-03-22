using System.ComponentModel.DataAnnotations;

namespace performance_appraisal_system.Models
{
    public class HR
    {
        [Key]
        public int HRID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should grater than 3 char and less than 50 char")]

        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password is Too small")]
        public string password { get; set; }
    }
}
