using Microsoft.AspNetCore.Mvc;

namespace performance_appraisal_system.Controllers
{
    public class Error : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult NormalError(string message) {

            ViewBag.Error=message;
            return View();
        

        }
    }
}
