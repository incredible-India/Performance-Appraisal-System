using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using performance_appraisal_system.Models;
using System.Security.Claims;


using Microsoft.AspNetCore.Authorization;
namespace performance_appraisal_system.Services
{
    public class basicAppInfo : _basicAppInfo
    {
        //dependency injection for the 
        private readonly IConfiguration _configuration;

        public basicAppInfo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string AppName()
        {

            return _configuration["AppName"];

        }


        
    }
}
