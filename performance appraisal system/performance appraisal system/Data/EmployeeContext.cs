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
        public DbSet<Manager> Manager { get; set; }
        public DbSet<HR> HR { get; set; }

    }
}
