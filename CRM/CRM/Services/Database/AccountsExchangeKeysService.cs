using CRM.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Database
{
    public static class AccountsExchangeKeysService
    {
        public static IEnumerable<SelectListItem> GetExchangeKeys(HttpContext httpContext) // передавать только имя пользователя, + переделать запись в Identity (Id вместо имени пользователя)
        {
            using (UserContext context = new UserContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Login == httpContext.User.Identity.Name);
                List<SelectListItem> lst = new List<SelectListItem>();
                

                lst = context.ExchangeKeys
                    .Where(x => user.RoleId == (int)UserModel.Roles.User ? x.UserId == user.Id : true)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.AccountId })
                    .ToList();

                return lst;
            }
        }
    }
}
