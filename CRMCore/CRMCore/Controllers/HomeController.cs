using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRMCore.Models;
using CRMCore.Models;

namespace CRMCore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Models.User.isAutorized)
            {
                return View();
            }

            return View("~/Views/Authorization/Login.cshtml");
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = "228" });
        }

    }
}