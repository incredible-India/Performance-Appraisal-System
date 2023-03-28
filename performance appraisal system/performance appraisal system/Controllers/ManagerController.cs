using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using performance_appraisal_system.Data;
using performance_appraisal_system.Models;
using performance_appraisal_system.Services;

namespace performance_appraisal_system.Controllers
{
    [Authorize(Policy = "BlongsToManager")]


    public class ManagerController : Controller
    {

        //employee services interface

        private readonly IEmployeeService _emp;

        //competency Services

        private readonly ICompitencies _compitencies;


        public ManagerController(IEmployeeService emp, ICompitencies compitencies)
        {

            _emp = emp;
            _compitencies = compitencies;
        }


        public IActionResult DashBoard()
        {
            return View();
        }



        //for adding new appraisel
        public IActionResult AddAppraisel()
        {
           

            Appraiselform apps = new Appraiselform();
            //getting the current user id
            int id = _emp.CurrentUserID(User.Claims.ToList()[1].Value);

            //create a dropdown in the list employee list under the manager
            DropDownEmployeeList(id);

            //dropdownform Competencey
           

            List<string> comp = _compitencies.GetCompetenciesName();


            IEnumerable <SelectListItem> selectList = comp.Select(x => new SelectListItem { Text = x, Value = x });

            ViewBag.compitency = selectList;
                          
        

            return View(apps);
        }



        //drop down for the employee list under the the current manager login

        public void DropDownEmployeeList(int id)
        {
            //this conver into list foe fetching in the dropdown..
            Dictionary<int, string> data = _emp.EmployeeListUnderCurrentManager(id);
       
            var selectList = new SelectList(data.Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }), "Value", "Text");


            ViewBag.SelectList = selectList;
        }

       

    }









}
