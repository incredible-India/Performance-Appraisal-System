using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using performance_appraisal_system.Models;
using performance_appraisal_system.Services;
using System.Diagnostics;
using System.Security.Claims;

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


            ClaimsPrincipal claimUser = HttpContext.User;
            //cheking if user is aleardy logged in or not
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Dashboard");


            //sending the login form
            login loginForm = new login();

            return View(loginForm);
        }

        //after the clicking the login button validation process...

        [HttpPost]

        public async Task<IActionResult> Login(login logDeatil)
        {
        
           if(ModelState.IsValid)
            {

                //first check the number of data inside
                int data = _loginValidator.CheckNoOfData();

                if (data > 0)
                {
                    //return null if fails otherwise record and if there no data in database it returns 0
                    var isCorrect =_loginValidator.Validate(logDeatil);

                    if(isCorrect !=null)
                    {
                        //on success validation
                        //creating cookies  list and adding parameters
                        List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, isCorrect.Name),
                    new Claim(ClaimTypes.Role,isCorrect.Designation),
                    new Claim(ClaimTypes.Email,isCorrect.email),

                };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties()
                        {

                            AllowRefresh = true,
                            IsPersistent = logDeatil.KeepLoggedIn
                        };


                        //saving cookies on the client side in Encrypted form
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), properties);

                        return RedirectToAction("Dashboard");
                    }else
                    {
                        //on unsccess validation
                        ViewBag.ErrorName = "Incorrect Username or Password";
                        return View();
;                    }

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
               

                return View();
            }
         
        }






        //her we will show the dash board if user is Authenticated
        [Authorize]
        public IActionResult Dashboard()
        {

            //saving the information in session...
/*            HttpContext.Session.SetString("UserName",HttpContext.User.FindFirstValue(ClaimTypes.Name));
            HttpContext.Session.SetString("Role",HttpContext.User.FindFirstValue(ClaimTypes.Role));
            HttpContext.Session.SetString("Email",HttpContext.User.FindFirstValue(ClaimTypes.Email));
*/
            return View();
        }



        //logout Routiung
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            //this methos will remove the cookie form the list
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return  RedirectToAction("Login");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}