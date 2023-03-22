using System.ComponentModel.DataAnnotations;

namespace performance_appraisal_system.Models
{
    public class Manager
    {
        [Key]
        public int MID { get; set; }
        [Required]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Invalid Name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password is Too small")]
        public string password { get; set; }
    }
}
