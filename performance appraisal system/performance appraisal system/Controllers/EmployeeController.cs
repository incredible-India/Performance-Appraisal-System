﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using performance_appraisal_system.Data;
using performance_appraisal_system.Models;
using performance_appraisal_system.Repository;
using performance_appraisal_system.Services;

namespace performance_appraisal_system.Controllers
{
    //Authentication filter
    [Authorize]
    public class EmployeeController : Controller
    {
        //to access employee related services..
        private  readonly IEmployeeService _emp;

        //to access Appraisal related services..
        private readonly IAppraisalfromService _app;

        public EmployeeController(IEmployeeService employeeService , IAppraisalfromService appraisalfromService)
        {
            _emp = employeeService;
            _app = appraisalfromService;
        }

        //actions start from here
        public IActionResult DashBoard()
        {

            //first checking any new appraisal form..
            //this will return current userid
            int UserId = _emp.CurrentUserID(User.Claims.ToList()[1].Value);

            //checking appraisla form having status created

            var MyAppraisalForm = _app.CheckNewAppraisalForEmployee(UserId,"Created");

            //CHECKING FOR THE RATED APPRAISAL FOR FINAL APPROVAL

            var FinalApp = _app.CheckNewAppraisalForEmployee(UserId, "Rated");


            //cheking the complted Appraisal...

            var completedAppraisal = _app.CheckNewAppraisalForEmployee(UserId, "Completed");

            if (MyAppraisalForm != null) { ViewBag.status = MyAppraisalForm; ViewBag.final = FinalApp; ViewBag.ca = completedAppraisal; }




            return View();
        }


        //when user click first appraisal form

        public IActionResult FirstAppraisalDetail(int appID)
        {
            int UserId = _emp.CurrentUserID(User.Claims.ToList()[1].Value);

            //return appraisal form based on given id if exist other wise null..

            var MyAppraisalForm = _app.GetCurrentAppraisalForm(appID);

            //now get the list of the competencies

            var Comps = _app.GetCompetencies(appID);
            //appraisal form
            ViewBag.status = MyAppraisalForm;   
            //list of competencies
             List<competencies> mycomps = new List<competencies>();

            foreach (var comp in Comps)
            {
                mycomps.Add(_app.GetCompitencyName(comp.Compitency));
            }

            ViewBag.comps = mycomps;
      
            //getting the manager name from the manger ID in Appraisal form

            ViewBag.Manager = _emp.GetEmployeeById(MyAppraisalForm.ManagerID).Name;

            //for the appraisal detail we will send that object

            AppraisalDetails ad = new AppraisalDetails();


            return View(ad);
        }

        //after emloyee adding comment and rated himself

        [HttpPost]
        public IActionResult FirstAppraisalDetail(AppraisalDetails ap, int appID)
        {
         
        
            
    
            if (ModelState.IsValid)
            {
                _app.SaveFirstAppraisalFormDetails(ap,"Self Rated",appID);

                return RedirectToAction("DashBoard", "Employee");
            }
            else
            {
                TempData["Error"] = "Some thing Went wrong Please Check All the comment box it should not be empty and Rating box";

                return RedirectToAction("FirstAppraisalDetail",new { appID = appID});
            }
        }


        //for the employee approval final

        public IActionResult EmployeeApproval(int appID)
        {
            var MyAppraisalForm = _app.GetCurrentAppraisalForm(appID);


            ViewBag.fm = MyAppraisalForm;

            //for the compitencies and the comment
            var Comps = _app.GetAppraisalFormDetails(appID);

          

            ViewBag.comps = Comps;

            return View();

        }


        //when Employee Accept the Appraisal

        public IActionResult AcceptAppraisal(int appID)
        {
            TempData["success"] = "*You Have Accepted You Appraisal..";
            _app.ChnageAppraisalStatus(appID, "Completed");
            return RedirectToAction("DashBoard");
        }




        //See Appraisal Details for  everyonecan Access...
        public IActionResult SeeAppraisalDetails(int appID)
        {
            int UserId = _emp.CurrentUserID(User.Claims.ToList()[1].Value);

          
            //give the appraisal form only for current login user or the user belongs to this form

            var MyAppraisalForm = _app.GetCurrentAppraisalFormForCurrentEmployee(appID, UserId, "Completed");    


            ViewBag.fm = MyAppraisalForm;

            //for the compitencies and the comment
            var Comps = _app.GetAppraisalFormDetails(appID);



            ViewBag.comps = Comps;

            return View();
        }
    }
}
