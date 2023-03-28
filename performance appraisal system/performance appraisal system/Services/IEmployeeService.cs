using performance_appraisal_system.Models;

namespace performance_appraisal_system.Services
{
    public interface IEmployeeService
    {

        //this method will addd employee
        public Task<bool> AddEmployee(Employee emp);

        //cheking email exist or not
        public Task<bool> EmployeeEmailExist(string email);


        //getting the Manager list from the database
        public Dictionary<int, string> ManagerList();  
        
        //getting the employee list under the particular manager from the database
        public Dictionary<int, string> EmployeeListUnderCurrentManager(int managerID);

        //get employees List
        public List<Employee> EmployeeList();

        //get employee by id
        public Employee GetEmployeeById(int empID);
        //check the email id after edition
        public bool EditEmployeeEmailExist(string email,int id);

        //finally edited the information

        public int EditEmployee(Employee e);


        //give the id of current loged in user

        public int CurrentUserID(string email);


    }
}
