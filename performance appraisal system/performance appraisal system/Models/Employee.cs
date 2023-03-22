using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace performance_appraisal_system.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Name should grater than 3 char and less than 50 char")]
        public string Name { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [StringLength(15,MinimumLength =10,ErrorMessage ="Invalid Number")]

        public string Phone { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password is Too small")]
        public string password { get; set; }
        [Required]
        
        public int MID { get; set; }


  

            
    }
}
