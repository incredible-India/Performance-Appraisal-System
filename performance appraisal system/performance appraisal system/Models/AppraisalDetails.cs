using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace performance_appraisal_system.Models
{
    public class AppraisalDetails
    {
        [Key]
        public int DetialID { get; set; }

        [Required]
        public int AppraisalID { get; set; }

        [Required(ErrorMessage = "Employee Comment is required")]


        public string? EmployeeComment { get; set; } = "no comments";

        [Required(ErrorMessage = "Employee Rating is required")]
        public int EmployeeRating { get; set; } = 0;

        [Required(ErrorMessage = "Manager Comment is required")]
        public string? ManagerComment { get; set; } = "no comments";

        [Required(ErrorMessage = "Employee Rating is required")]
        public int ManagerRating { get; set; } = 0;

        [Required]
        public int Competency { get; set; }



        [NotMapped]
        [Required(ErrorMessage = "Employee comment is required")]

        public string[]? EComments { get; set; }

        [NotMapped]
        public string[]? MComments { get; set; } = { "no comments" };

        [NotMapped]

        public int[] ERating { get; set; }  


        [NotMapped]

        public int[] MRating { get; set; } = { 0 };


    }
}
