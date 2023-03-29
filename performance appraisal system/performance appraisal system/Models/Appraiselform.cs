using Microsoft.EntityFrameworkCore.Metadata.Internal;
using performance_appraisal_system.Migrations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace performance_appraisal_system.Models
{
    public class Appraiselform
    {
        [Key]
        public int AID { get; set; }

        [Required]
       
        public int ManagerID { get; set; }

        [Required]
        public string Status { get; set; } = "New";

        [Required]
        [DisplayName("Employee ID")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Please select the compitency..")]





        [DisplayName("Objective")]
        public string objective { get; set; } = "No objectives";

        
        [DisplayName("Appraisal Start Date")]
        [Required (ErrorMessage ="Start Date is required..")]
        [DataType(DataType.Date)]
        public string StartDate { get; set; }

        
        [DisplayName("Last Evaluation Date")]
        [Required(ErrorMessage = "End Date is required..")]
        [DataType(DataType.Date)]
        public string EndDate { get; set; }




        [NotMapped]
        [Required(ErrorMessage = "Please select the compitency..")]

        [DisplayName("Select Competencies")]

        public int[] tempCompetency { get; set; }


    }
}
