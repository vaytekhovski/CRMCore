using CRMCore.Models.Database;
using CRMCore.Services;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CRMCore.Services.Authorization;

namespace CRMCore.Controllers
{
    public class AuthorizationController : Controller
    {
        
        public ActionResult Registration(string user_name, string user_password, string user_confirm_password)
        {
            Registration registration = new Registration();

            if (registration.Reg(user_name, user_password, user_confirm_password))
            {
                ViewBag.status = registration.status;
                return View("~/Views/Authorization/Login.cshtml");
            }
            
            return View();
        }
        

        public ActionResult Login(string user_name, string user_password)
        {
            if (!Models.User.isAutorized)
            {
                Login login = new Login();
                if (login.Log(user_name, user_password))
                {
                    ViewBag.status = login.status;
                    return View("~/Views/Home/Index.cshtml");
                }
            }
            else
                return View("~/Views/Home/Index.cshtml");

            return View();
        }
    }
}