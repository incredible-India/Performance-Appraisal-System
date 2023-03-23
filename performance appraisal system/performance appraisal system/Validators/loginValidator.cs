using Microsoft.AspNetCore.Mvc;
using performance_appraisal_system.Data;
using performance_appraisal_system.Models;
using performance_appraisal_system.Services;

namespace performance_appraisal_system.Validators
{

    public class loginValidator : _LoginValidator
    {

        private readonly EmployeeContext _employeeContext;

        public loginValidator(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        //this method will verify the id and the password

        public Employee Validate(login logDetail)
        {


            var status = _employeeContext.Employees.Where(m => m.email == logDetail.email && m.password == logDetail.password).FirstOrDefault();


           
           return status;
            

           
        }

        //checking the number of recored in the employee table

        public int CheckNoOfData()
        {
            int data = _employeeContext.Employees.Count();

            return data;
        }



    }
}
