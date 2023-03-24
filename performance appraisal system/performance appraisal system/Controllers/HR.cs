using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace performance_appraisal_system.Controllers
{
    //controller level Authentocation only HR Can Acess All the Functionalities..
    [Authorize(Policy = "BlongsToHR")]
    public class HR : Controller
    {

        //for Simple DashBoard
        public IActionResult DashBoard()
        {
            return View();
        }

        public IActionResult AddEmployees()
        {
            return View();
        }
    }
}
