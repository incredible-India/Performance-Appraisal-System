using Microsoft.EntityFrameworkCore;
using performance_appraisal_system.Models;

namespace performance_appraisal_system.Data
{
    public class EmployeeContext :DbContext
    {
      
        public EmployeeContext(DbContextOptions<EmployeeContext> options): base(options)
        {
           


        }


        public DbSet<Employee> Employees { get; set; }
   
        public DbSet<competencies> competencies { get; set; }


    }
}
