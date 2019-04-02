using CRM.Services;
using CRM.Services.Database;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using CRM.Models.Database;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using CRM.ViewModels;
using CRM.Models;

namespace CRM.Controllers.User
{
    public class UserPanelController : Controller
    {
        private UserContext db;

        public UserPanelController(UserContext context)
        {
            db = context;
        }
       
        [Authorize]
        public ActionResult UserPanel(UserPanelModel model)
        {
            UserModel user = db.UserModels.FirstOrDefault(x => x.Login == User.Identity.Name);

            ViewBag.Login = user.Login;
            ViewBag.Password = user.Password;
            ViewBag.RegistrationDate = user.RegistrationDate;

            return View(model); // TODO: [COMPLETE] после реализации авторизации заменить такие проверк на  атрибут Authorize
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> ChangeLoginAsync(UserPanelModel model)
        {
            ChangeUserDataService.ChangeUserLogin(User.Identity.Name, model.Login);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword(UserPanelModel model)
        {
            ChangeUserDataService.ChangeUserPassword(User.Identity.Name, model.Password);
            return RedirectToAction("UserPanel");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangeDailyTrigger(string updateData)
        {
            DailyTriggerService.ChangeDailyTrigger(TimeSpan.Parse(updateData));
            return RedirectToAction("UserPanel");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ExitFromAccount()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet]
        public ActionResult LoadData(string startDate, string endDate)
        {
            ViewBag.status = "Загрузка данных завершена";
            LoadDataService loadData = new LoadDataService(DateTime.Parse(startDate), DateTime.Parse(endDate));
            return RedirectToAction("UserPanel");
        }
        

    }
}