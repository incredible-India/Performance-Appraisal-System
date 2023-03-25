using Microsoft.EntityFrameworkCore;
using performance_appraisal_system.Data;
using performance_appraisal_system.Models;
using performance_appraisal_system.Services;

namespace performance_appraisal_system.Repository
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeService(EmployeeContext  employeeContext)
        {
            _employeeContext = employeeContext;
            
        }
        //this methid will add new employee
        public async Task<bool> AddEmployee(Employee emp)
        {
            Employee employee = new Employee()
            {
              
                Name=emp.Name,
                email=emp.email,
                Phone=emp.Phone,
                MID=emp.MID,
                password  = emp.password,
                Designation=emp.Designation,


            };

            await _employeeContext.Employees.AddAsync(employee);
            await _employeeContext.SaveChangesAsync();

            return true;
        }

        //checking the email exist or not

        public async Task<bool> EmployeeEmailExist(string email)
        {


           return  await _employeeContext.Employees.AnyAsync(m=>m.email == email);


           
           
        }
    }
}
