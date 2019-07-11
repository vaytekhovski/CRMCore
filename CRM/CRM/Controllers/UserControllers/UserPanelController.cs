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

namespace CRM.Controllers.User
{
    [Authorize] 
    public class UserPanelController : Controller
    {
        private readonly ChangeUserDataService _changeUserDataService;
        private UserContext db;

        public UserPanelController(UserContext context)
        {
            db = context;

            _changeUserDataService = new ChangeUserDataService();
        }
       
        [HttpGet]
        public ActionResult UserPanel(UserPanelModel model)
        {
            UserModel user = db.UserModels.FirstOrDefault(x => x.Id == Convert.ToInt32(User.Identity.Name));

            model.Login = user.Login;
            model.Password = user.Password;
            model.RegistrationDate = user.RegistrationDate;
            ViewBag.isUser = user.RoleId != 1 ? true : false;
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> ChangeLoginAsync(UserPanelModel model)
        {
            _changeUserDataService.ChangeUserLogin(User.Identity.Name, model.Login);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        
        [HttpGet]
        public ActionResult ChangePassword(UserPanelModel model)
        {
            _changeUserDataService.ChangeUserPassword(User.Identity.Name, model.Password);
            return RedirectToAction("UserPanel");
        }
        
        [HttpGet]
        public ActionResult ChangeDailyTrigger(string updateData)
        {
            DailyTriggerService.ChangeDailyTrigger(TimeSpan.Parse(updateData));
            return RedirectToAction("UserPanel");
        }
        
        [HttpGet]
        public async Task<IActionResult> ExitFromAccount()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult LoadData(string startDate, string endDate)
        {
            ViewBag.status = "Загрузка данных завершена";
            LoadDataService loadData = new LoadDataService(DateTime.Parse(startDate), DateTime.Parse(endDate));
            return RedirectToAction("UserPanel");
        }

        [HttpGet]
        public ActionResult RestartTHManager(string startDate, string endDate)
        {
            //restart
            THManager.Program.Restart();
            return RedirectToAction("UserPanel");
        }
    }
}