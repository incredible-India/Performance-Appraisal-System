using Microsoft.EntityFrameworkCore;
using performance_appraisal_system.Data;
using performance_appraisal_system.Models;
using performance_appraisal_system.Services;
using performance_appraisal_system.Enumdata;

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
                MID = emp.MID,
                password  = emp.password,
                Designation =  Enum.GetName(typeof(Designation), int.Parse(emp.Designation))


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


        public Dictionary<int, string> ManagerList()
        {
          

            //for thge validation like only hr can be the in the list
           /* _employeeContext.Employees.Where(m => m.Designation == "HR").Select(m => new { m.EID, m.Name }).ToDictionary(m => m.EID, m => m.Name);*/

            //fetching data for MID MAnager ID it is a Dictionry with EID KEY and Name as Value
            dynamic empdata = _employeeContext.Employees.Select(m => new { m.EID, m.Name,m.Designation }) .ToDictionary(m => m.EID, m => m.Name + $" - ({m.Designation + " " + m.EID})");

            return empdata;

            
        }


        //return the list of employee

        public List<Employee> EmployeeList()
        {
            return _employeeContext.Employees.ToList();
        }



        //geting employee by id
        public Employee GetEmployeeById(int empID)
        {
            return _employeeContext.Employees.Where(emp=>emp.EID == empID).FirstOrDefault();
        }

        //checking employee email after edition
        public bool EditEmployeeEmailExist(string email,int id)
        {

            //if email exist return true
            bool data=  _employeeContext.Employees.Any(m => m.email == email);

            if(data)
            {
               var isameUser = _employeeContext.Employees.Where(m=>m.email == email && m.EID == id).FirstOrDefault();
                //if email belongs to the same person
                if(isameUser != null)
                {
                    return true;
                }
                else
                {
                    //given emial belongs to different person
                    return false;
                }


            }
            else
            {
                //mean no email exist for given email
                return true;
            }
        }

        //for he editing the employee data
        public int EditEmployee(Employee e)
        {

            if (_employeeContext.Employees.Find(e.EID) != null)
            {
                _employeeContext.Entry(e).State = EntityState.Deleted;
            }
            e.Designation = Enum.GetName(typeof(Designation), int.Parse(e.Designation));

            //_employeeContext.Update(e);
            _employeeContext.Entry(e).State = EntityState.Modified ;
            int a = _employeeContext.SaveChanges();
            return a;
           
        }


        //gives the employee List

        public Dictionary<int, string> EmployeeListUnderCurrentManager(int managerID)
        {


            //for thge validation like only hr can be the in the list
            /* _employeeContext.Employees.Where(m => m.Designation == "HR").Select(m => new { m.EID, m.Name }).ToDictionary(m => m.EID, m => m.Name);*/

            //fetching data for emaployee id under the particular manager
            dynamic empdata = _employeeContext.Employees.Where(id=>id.MID == managerID).Select(m => new { m.EID, m.Name, m.Designation }).ToDictionary(m => m.EID, m => m.Name + $" - ({m.Designation + " - " + m.EID})");

            return empdata;


        }

        //give the ID of Current loged in user

        public int CurrentUserID(string email)
        {
            var user = _employeeContext.Employees.Where(mail => mail.email == email).FirstOrDefault();

            if(user != null)
            {
                return user.EID;
            }
            else
            {
                return -1;
            }
        }

  

        
    }
}
