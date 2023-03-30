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

        //for appraisel services
        private readonly IAppraisalfromService _appraisalfromService;

        //constructor
        public ManagerController(IEmployeeService emp, ICompitencies compitencies, IAppraisalfromService appraisalfromService)
        {

            _emp = emp;
            _compitencies = compitencies;
            _appraisalfromService = appraisalfromService;
        }


        public IActionResult DashBoard()
        {
            //first get the current user id

            int userID = _emp.CurrentUserID(User.Claims.ToList()[1].Value);
            //her we will show all the appraisal form having status new

            List<Appraiselform> myform = _appraisalfromService.GetAppraisalFormHavingStatus("New", userID); 

            ViewBag.NewStatus = myform;
            
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
           

            var comp = _compitencies.ListCompetencies();


            IEnumerable <SelectListItem> selectList = comp.Select(x => new SelectListItem { Text = x.CompetencyName, Value = x.ID.ToString() });

            ViewBag.compitency = selectList;
                          
        

            return View(apps);
        }




        //http post method for adding appraisel

        [HttpPost]

        public IActionResult AddAppraisel(Appraiselform fm)
        {
            //getting the current user id
            int id = _emp.CurrentUserID(User.Claims.ToList()[1].Value);

            //create a dropdown in the list employee list under the manager
            DropDownEmployeeList(id);

            //dropdownform Competencey


            var comp = _compitencies.ListCompetencies();


            IEnumerable<SelectListItem> selectList = comp.Select(x => new SelectListItem { Text = x.CompetencyName, Value = x.ID.ToString() });

            ViewBag.compitency = selectList;

            //now adding the default Employee ID

            fm.ManagerID = id;

            //now checking the objective it is null or not
     

            if (ModelState.IsValid)
            {
                //now save all the information coming from the client side

                _appraisalfromService.SaveDataInAppraiselDB(fm);


                ViewBag.Success = "* Appraisal Created Successfully...";
                ModelState.Clear();
                return View();
            }
            else
            {
                
                return View(fm);
            }


        }

        //this action will change the appraisal form status to new 


        public IActionResult ChangeAppraisalStatus(int appid,string status)
        {
            //changing the status to created for the given appraisal id

            _appraisalfromService.ChnageAppraisalStatus(appid,"Created");

            TempData["success"] = "*status Changed successfully..";
            return RedirectToAction("DashBoard");
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
