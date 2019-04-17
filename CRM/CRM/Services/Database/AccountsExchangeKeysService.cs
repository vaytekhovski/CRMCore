using CRM.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Database
{
    public static class AccountsExchangeKeysService
    {
        public static IEnumerable<SelectListItem> GetExchangeKeys(HttpContext httpContext)
        {
            using (UserContext context = new UserContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Login == httpContext.User.Identity.Name);
                List<SelectListItem> lst = new List<SelectListItem>();
                

                lst = context.ExchangeKeys.Where(x => user.RoleId == (int)UserModel.Roles.user ? x.UserId == user.Id : true) // TODO: 1 в enum
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.AccountId })
                    .ToList();

                return lst;
            }
        }
    }
}
