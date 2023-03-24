
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using performance_appraisal_system.Data;
using performance_appraisal_system.Models;
using performance_appraisal_system.Services;
using System.Security.Claims;

namespace performance_appraisal_system.Validators
{

    public class loginValidator :Controller, _LoginValidator
    {

        private readonly EmployeeContext _employeeContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

 

        public loginValidator(EmployeeContext employeeContext, IHttpContextAccessor httpContextAccessor)
        {
            _employeeContext = employeeContext;
            _httpContextAccessor = httpContextAccessor;
        }

        //this method will verify the id and the password

        public Employee Validate(login logDetail)
        {


            var status = _employeeContext.Employees.Where(m => m.email == logDetail.email && m.password == logDetail.password).FirstOrDefault();


           
           return status;
            

           
        }

        //checking the number of recored in the employee table

        public int CheckNoOfData()
        {
            int data = _employeeContext.Employees.Count();

            return data;
        }

        public  void AuthProcess(Employee emp,login logDetails)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,emp.Name),
                new Claim(ClaimTypes.Email,emp.email),
                new Claim("Designation",emp.Designation),
                
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                          CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            AuthenticationProperties properties = new AuthenticationProperties()
            {

                AllowRefresh = true,
              
            };


            _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                       new ClaimsPrincipal(claimsIdentity), properties);


        }


   


    }
}
