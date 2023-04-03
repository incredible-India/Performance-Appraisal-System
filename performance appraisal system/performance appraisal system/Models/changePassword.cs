using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace performance_appraisal_system.Models
{
    public class changePassword
    {

        [Required(ErrorMessage ="Old password is required")]
        [DisplayName("Old Password")]
        [DataType(DataType.Password)]
        public string? oldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain at least one lowercase letter, one uppercase letter, one digit, and one special character")]
        [DisplayName("New  Password")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is required")]
        [DisplayName("Confirm New Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain at least one lowercase letter, one uppercase letter, one digit, and one special character")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Confirm password does not match with the new password...")]
        public string? CnfNewPassword { get; set; }
    }
}
