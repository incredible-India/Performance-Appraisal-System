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

        public dynamic Validate(login logDetail)
        {


            int IsAnyData = _employeeContext.Employees.Count();

            if (IsAnyData == 0)
                return 0;
               

            var status = _employeeContext.Employees.Where(m => m.email == logDetail.email && m.password == logDetail.password).FirstOrDefault();


            return status;
           
        }

      
    }
}
