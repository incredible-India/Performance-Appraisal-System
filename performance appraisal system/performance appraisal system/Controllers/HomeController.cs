using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using performance_appraisal_system.Data;
using performance_appraisal_system.Models;
using performance_appraisal_system.Services;
using System.Diagnostics;
using System.Security.Claims;
using System.Xml;

namespace performance_appraisal_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //to acess the httpContext
        private readonly IHttpContextAccessor _httpContextAccessor;
        //for the email and password validators
        private readonly _LoginValidator _loginValidator;
        //Dbcontext file
        private readonly IEmployeeService _emp;

        public HomeController(ILogger<HomeController> logger, _LoginValidator loginValidator, IHttpContextAccessor httpContextAccessor,IEmployeeService employeeContext)
        {
            _logger = logger;
            _loginValidator = loginValidator;
            _httpContextAccessor = httpContextAccessor;
            _emp = employeeContext;

        }

        //this is the default routing

        public IActionResult Login()
        {

            

            ClaimsPrincipal claimUser = HttpContext.User;
            //cheking if user is aleardy logged in or not
            if (claimUser.Identity.IsAuthenticated)
            {
                //this will check the designation of user and based on that we will continue ..
                if (User.Claims.ToList()[2].Value == "HR") return RedirectToAction("Dashboard", "HR");
                if (User.Claims.ToList()[2].Value == "Manager") return RedirectToAction("DashBoard", "Manager");
                return RedirectToAction("DashBoard", "Employee");
                 /*return RedirectToAction("NormalError", "Error", new {  message="Something WentWrong"});
*/

            }
              


            //sending the login form if user is not authorised
            login loginForm = new login();

            return View(loginForm);
        }

        //after the clicking the login button validation process...

        [HttpPost]

        public async Task<IActionResult> Login(login logDeatil)
        {

            if (ModelState.IsValid)
            {

                //first check the number of data inside
                int data = _loginValidator.CheckNoOfData();

                if (data > 0)
                {
                    //return null if fails otherwise record and if there no data in database it returns 0
                    var isCorrect = _loginValidator.Validate(logDeatil);

                    if (isCorrect != null)
                    {

                        //now  authorising the use and redirect user to the dash board 
                        _loginValidator.AuthProcess(isCorrect, logDeatil);

                        //based on Desingnation we will Redirect the page
                        if (isCorrect.Designation == "HR")
                            return RedirectToAction("DashBoard", "HR");
                        else if (isCorrect.Designation == "Manager")
                            return RedirectToAction("login");
                        
                        else return RedirectToAction("login");

                    }
                    else
                    {
                        //on unsccess validation
                        ViewBag.ErrorName = "Incorrect Username or Password";
                        return View();
                        ;
                    }

                }
                else
                {
                    //if there is no records in the database
                    ViewBag.ErrorName = "Internal Database Error...";
                    return View();

                }

            }
            else
            {
                //id model form is incorrect

                return View();
            }

        }





        //logout Routiung
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            //this methos will remove the cookie form the list
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");

        }


        //changing the password of user

        [Authorize]
        //taking the user id ....
        public IActionResult ChnageUserPassword(int id)
        {
            //verifying changin user is the current user or not
            Employee CurentEmployee = _emp.GetEmployeeById(id);

            if (CurentEmployee != null)
            {
                //if employee exist then verify with the current user 
                string  email = User.Claims.ToList()[1].Value;

                //varify the user 

                if(email == CurentEmployee.email)
                {
                    //return the login change password form..
                    changePassword cf = new changePassword(); 
                    return View(cf);
                }
                else
                {
                    //if the user is in current user
                    return RedirectToAction(actionName: "NormalError", controllerName: "Error", new
                    {
                        message = "You Have no right to perform this action"
                    }) ;
                }

            }

            return RedirectToAction(actionName: "NormalError", controllerName: "Error", new
            {
                message = "You Have no right to perform this action"
            });

        }


        //handle the post request for changing the password 
        [Authorize]
        [HttpPost]

        public IActionResult ChnageUserPassword(changePassword cp)
        {

            if (!ModelState.IsValid) return View(cp);
            else
            {
                ModelState.Clear();
                ViewBag.succ = "Password changed succefully..";
                return View(cp);
            }

         
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}