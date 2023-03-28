using System.ComponentModel.DataAnnotations;

namespace performance_appraisal_system.Models
{
    public class competencies
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100,MinimumLength=2,ErrorMessage ="Length should be greater than 2..")]
        public string CompetencyName { get; set; } 
        
        [Required]
        [StringLength(100,MinimumLength=2,ErrorMessage ="Length should be greater than 2..")]
        public string TypeC { get; set; }

    }
}
