using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace performance_appraisal_system.Models
{
    //this table will store the information of appraisal and competency when manager will create the first appraisal form......
    //and he select the multi compitencies...
    public class AspprasalAndCompetencies
    {

        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Appraiselform")]
        public int AppID { get; set; }
        

        [Required]
        public int Compitency { get; set; }
    }
}
