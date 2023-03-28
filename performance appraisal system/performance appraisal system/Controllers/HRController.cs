using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using performance_appraisal_system.Models;
using performance_appraisal_system.Data;
using performance_appraisal_system.Services;

using System.Drawing.Text;

using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using performance_appraisal_system.Validators;

namespace performance_appraisal_system.Controllers
{
    //controller level Authentocation only HR Can Acess All the Functionalities..
    [Authorize(Policy = "BlongsToHR")]
    public class HRController : Controller
    {

        //Employee service Interface 

        private readonly IEmployeeService _emp;

        //competency related service

        private readonly ICompitencies _comp;

        //login related services
        private readonly _LoginValidator _loginValidator;

        public HRController(IEmployeeService employeeService,_LoginValidator loginValidator,ICompitencies comp)
        {
            _emp = employeeService;
            _loginValidator = loginValidator;
            _comp = comp;
      
            
        }

        //for Simple DashBoard
        public IActionResult DashBoard()
        {
            //first cheking is there any data exist or not
           int noOfdata = _loginValidator.CheckNoOfData();
            if(noOfdata != 0)
            {
                //this will return the no of employees
                ViewBag.EmployeeList = _emp.EmployeeList();
            }
           
            return View();
        }
    

        //adding employee form
        public IActionResult AddEmployees()
        {

            //drop down for the Manager ID..
            DropDown();
         

            Employee emp =new Employee();
          
            return View(emp);
        }


        //for the post request add employee form
        [HttpPost]
       
        public async Task<IActionResult> AddEmployees(Employee emp)
        {

            //Drop Down for The Manager ID in case if fails for validation...
                DropDown();

            if (ModelState.IsValid)
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



        //for editing the employee list

        public IActionResult EditEmployee(int empID)
        {
            DropDown();

            var employee =  _emp.GetEmployeeById(empID);
            if (employee != null)
            {

                return View(employee);
            }
            else
            {
                return Content("Employee Does not Exist");
            }
            
           
        }

        //Employee Post Request

        [HttpPost]
        public IActionResult EditEmployee(Employee emp)
        {
            //for the drop down
            DropDown();
         
   

                if (ModelState.IsValid)
                {
                    //cheking email already exist or not
                    bool isExist = _emp.EditEmployeeEmailExist(emp.email,emp.EID);

                    if (isExist == false)
                    {
                        ViewBag.Error = "Email Already Exist..";

                        return View();
                    }
                    else
                    {
                  
                        int isEdited = _emp.EditEmployee(emp);
                        //if no error in editing
                        if(isEdited > 0)
                        {
                            TempData["Edited"] = $"*Employee {emp.Name} is updated successfully";
                            return RedirectToAction("DashBoard");
                        }
                        else
                        {
                            return Content("Something went wrong..");

                        }
                       
                    }


                }
                else
                {
                return View();
                }

          

          
        }


        //for adding the AddCompetencies

        public IActionResult AddCompetencies()
        {
            competencies competencies = new competencies();

            ViewBag.Competencies = _comp.ListCompetencies();

            return View(competencies);
        }


        [HttpPost]
        public IActionResult AddCompetencies(competencies c)
        {
            if(ModelState.IsValid)
            {
                _comp.AddCompitency(c);
                TempData["Success"] = "*Competency Added Successfully";
                return RedirectToAction("AddCompetencies");

            }
            else
            {
                return View();
            }
        }


        //for creating the drop down  and fetching the data form the database....
        private void DropDown()
        {
            //this conver into list foe fetching in the dropdown..
            Dictionary<int, string> data = _emp.ManagerList();
            data.Add(0, "None");
            var selectList = new SelectList(data.Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }), "Value", "Text");


            ViewBag.SelectList = selectList;
        }
    }
}
