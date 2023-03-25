using performance_appraisal_system.Data;
using performance_appraisal_system.Enum;
using performance_appraisal_system.Repository;
using performance_appraisal_system.Validators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace performance_appraisal_system.Models
{

    
    public class Employee
    {
      
        [Key]
        [Required]
        public int EID { get; set; }


        [Required]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Name should grater than 3 char and less than 50 char")]
        [DisplayName("Employee Name")]
        public string Name { get; set; }


        [Required(ErrorMessage ="Designation is Required")]
        [DisplayName("Employee Designation")]
        public String Designation { get; set; }


        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        [DisplayName("Email")]

        [CustomEmailValidator]
        public string email { get; set; }


        [Required (ErrorMessage ="Mobile number is required.")]
        [StringLength(15,MinimumLength =10,ErrorMessage ="Invalid Number")]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Mobile Numer")]
        public string Phone { get; set; }


        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password is Too small")]
        [DisplayName("Password ")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain at least one lowercase letter, one uppercase letter, one digit, and one special character")]
        public string password { get; set; }

      

        [Required(ErrorMessage ="ManagerId is Required..")]
        [DisplayName("Manager ID")]
        public int? MID { get; set; }


  

            
    }



}
