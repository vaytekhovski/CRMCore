using CRMCore.Services;
using CRMCore.Services.Database;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace CRMCore.Controllers.User
{
    public class UserPanelController : Controller
    {
        [HttpPost]
        public ActionResult UserPanel()
        {
            return Models.User.isAutorized ? View() : View("~/Views/Authorization/Login.cshtml"); // TODO: после реализации авторизации заменить такие проверк на  атрибут Authorize
        }


        [HttpGet]
        public ActionResult ChangeLogin(string login)
        {
            if (Models.User.isAutorized)
            {
                ChangeUserDataService.ChangeUserLogin(Models.User.Id, login);
                return View("UserPanel");
            }

            return View("~/Views/Authorization/Login.cshtml");
            
        }

        [HttpGet]
        public ActionResult ChangePassword(string password)
        {
            if (Models.User.isAutorized)
            {
                ChangeUserDataService.ChangeUserPassword(Models.User.Id, password);
                return View("UserPanel");
            }

            return View("~/Views/Authorization/Login.cshtml");
            
        }

        [HttpGet]
        //[Authorize] use froms auithentification
        public ActionResult ChangeDailyTrigger(string updateData, CRMCoreContext context)
        {
            if (Models.User.isAutorized)
            {
                DailyTriggerService.ChangeDailyTrigger(TimeSpan.Parse(updateData));
                return View("UserPanel");
            }

            return View("~/Views/Authorization/Login.cshtml");
            
        }

        [HttpGet]
        public ActionResult ChangeName(string name) //TODO создать модель пользователя и обновлять все поля вместе
        {
            if (Models.User.isAutorized)
            {
                ChangeUserDataService.ChangeUserName(Models.User.Id, name);
                return View("UserPanel");
            }

            return View("~/Views/Authorization/Login.cshtml");
            
        }

        [HttpGet]
        public ActionResult ChangeSurname(string surname)
        {
            if (Models.User.isAutorized)
            {
                ChangeUserDataService.ChangeUserSurname(Models.User.Id, surname);
                return View("UserPanel");
            }

            return View("~/Views/Authorization/Login.cshtml");
            
        }
        [HttpGet]
        public ActionResult ExitFromAccount()
        {
            Models.User.isAutorized = false;
            return View("~/Views/Authorization/Login.cshtml");

        }
        
        [HttpGet]
        public ActionResult LoadData(string startDate, string endDate)
        {
            if (Models.User.isAutorized)
            {
                ViewBag.status = "Загрузка данных завершена";
                LoadDataService loadData = new LoadDataService(DateTime.Parse(startDate), DateTime.Parse(endDate));
                return View("UserPanel");
            }
            return View("~/Views/Authorization/Login.cshtml");
        }
        

    }
}