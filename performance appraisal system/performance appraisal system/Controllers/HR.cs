﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace performance_appraisal_system.Controllers
{
    //controller level Authentocation only HR Can Acess All the Functionalities..
    [Authorize(Policy = "BlongsToHR")]
    public class HR : Controller
    {
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
