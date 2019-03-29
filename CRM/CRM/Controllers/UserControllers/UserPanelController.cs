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

namespace CRM.Controllers.User
{
    public class UserPanelController : Controller
    {
        [Authorize]
        [HttpPost]
        public ActionResult UserPanel()
        {
            return View(); // TODO: после реализации авторизации заменить такие проверк на  атрибут Authorize
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangeLogin(string login)
        {
            //ChangeUserDataService.ChangeUserLogin(UserModel.Id, login);
            return View("UserPanel");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword(string password)
        {
            //ChangeUserDataService.ChangeUserPassword(Models.User.Id, password);
            return View("UserPanel");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangeDailyTrigger(string updateData, CRMContext context)
        {
            DailyTriggerService.ChangeDailyTrigger(TimeSpan.Parse(updateData));
            return View("UserPanel");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangeName(string name) //TODO создать модель пользователя и обновлять все поля вместе
        {
            //ChangeUserDataService.ChangeUserName(Models.User.Id, name);
            return View("UserPanel");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangeSurname(string surname)
        {
            //ChangeUserDataService.ChangeUserSurname(Models.User.Id, surname);
            return View("UserPanel");
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
            return View("UserPanel");
        }
        

    }
}