using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using performance_appraisal_system.Models;
using performance_appraisal_system.Data;
using performance_appraisal_system.Services;
using System.Drawing.Text;
using System;
using performance_appraisal_system.Enum;

namespace performance_appraisal_system.Controllers
{
    //controller level Authentocation only HR Can Acess All the Functionalities..
    [Authorize(Policy = "BlongsToHR")]
    public class HRController : Controller
    {

        //Employee service Interface 

        private readonly IEmployeeService _emp;


        public HRController(IEmployeeService employeeService)
        {
            _emp = employeeService;
      
            
        }

        //for Simple DashBoard
        public IActionResult DashBoard()
        {
            return View();
        }


        //adding employee form
        public IActionResult AddEmployees()
        {
           // List<Designation> enumList = Enum.GetValues(typeof(Designation)).Cast<Designation>().ToList();

            //return the employee form

            Employee emp =new Employee();
            return View(emp);
        }


        //for the post request add employee form
        [HttpPost]
        public async Task<IActionResult> AddEmployees(Employee emp)
        {
            if(ModelState.IsValid)
            {
                //cheking email already exist or not
                bool isExist = await _emp.EmployeeEmailExist(emp.email);
                if (isExist) { ViewBag.error = "Email Already Exist.."; return View(emp); }
                //Adding new employee return true if successfully added otherwise false
               bool status = await _emp.AddEmployee(emp);
                if(status) { ViewBag.success = "Employee Added Successfully.."; ModelState.Clear(); return  View(); }
                else { ViewBag.error = "Something Went Wrong"; return View(); }
            }
            else
            {
                return View();
            }
        }
    }
}
