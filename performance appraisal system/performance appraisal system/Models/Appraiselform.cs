using System.ComponentModel.DataAnnotations;

namespace performance_appraisal_system.Models
{
    public class Appraiselform
    {

        public int AID { get; set; }

        public int ManagerID { get; set; }

        
        public int EmployeeID { get; set; }

        public string competence { get; set; }


        public string objective { get; set; }


        [DataType(DataType.Date)]
        public string StartDate { get; set; }

        [DataType(DataType.Date)]
        public string EndDate { get; set; }
    




    }
}
