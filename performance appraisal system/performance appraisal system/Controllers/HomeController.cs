using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using performance_appraisal_system.Models;
using performance_appraisal_system.Services;
using System.Diagnostics;

namespace performance_appraisal_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //for the email and password validators
        private readonly _LoginValidator _loginValidator;

        public HomeController(ILogger<HomeController> logger,_LoginValidator loginValidator)
        {
            _logger = logger;
            _loginValidator = loginValidator;
        }

        //this is the default routing
     
        public IActionResult Login()
        {
            //sending the login form
           login loginForm = new login();

            return View(loginForm);
        }

        //after the clicking the login button validation process...

        [HttpPost]
        public IActionResult Login(login logDeatil)
        {
            
           if(ModelState.IsValid)
            {
                //return null if fails otherwise record and if there no data in database it returns 0
                var isCorrect = _loginValidator.Validate(logDeatil);

                if (isCorrect == null)
                {//validation fails
                    return View();
                }
                else if(isCorrect == 0)
                {
                    ViewBag.ErrorName = "Internal Server Error...";
                    return View();
                }
                else
                {
                    //validation success
                    return Content("Welcome ");
                }
            }else
            {
                return View(logDeatil);
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}