using performance_appraisal_system.Models;

namespace performance_appraisal_system.Services
{
    public interface IEmployeeService
    {

        //this method will addd employee
        public Task<bool> AddEmployee(Employee emp);

        //cheking email exist or not
        public Task<bool> EmployeeEmailExist(string email);


    }
}
