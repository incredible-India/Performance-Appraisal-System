using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using performance_appraisal_system.Data;
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

            //checking appraisla form new 

            var MyAppraisalForm = _app.CheckNewAppraisalForEmployee(UserId);

            if (MyAppraisalForm != null) ViewBag.status = MyAppraisalForm;




            return View();
        }
    }
}
