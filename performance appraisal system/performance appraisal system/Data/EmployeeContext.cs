using Microsoft.EntityFrameworkCore;
using performance_appraisal_system.Migrations;
using performance_appraisal_system.Models;

namespace performance_appraisal_system.Data
{
    public class EmployeeContext :DbContext
    {
      
        public EmployeeContext(DbContextOptions<EmployeeContext> options): base(options)
        {
           


        }

        //employee information
        public DbSet<Employee> Employees { get; set; }
   
        //for the appraisal form filled by hr
        public DbSet<competencies> competencies { get; set; }

        //appraisal form created by manager

        public DbSet<Appraiselform> AppraiselForm { get; set; }


        //appraisal context form where competencies will store when manager will create first time appraisel

        public DbSet<AspprasalAndCompetencies> AspprasalAndCompetencies { get; set; }

        //contains the Appraisal detail like comment and rating

        public DbSet<AppraisalDetails> appraisalDetails { get; set; }

        
    }
}
