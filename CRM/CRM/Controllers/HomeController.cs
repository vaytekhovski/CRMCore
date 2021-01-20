using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace CRM.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            var browserLang = Request.Headers["Accept-Language"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();
            SetLanguage(browserLang, "Home/Home");
            return View();
        }

        public ActionResult PermissionDenied()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}