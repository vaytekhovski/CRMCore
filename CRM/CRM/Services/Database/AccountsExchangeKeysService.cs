using CRM.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Database
{
    public static class AccountsExchangeKeysService
    {
        public static IEnumerable<SelectListItem> GetExchangeKeys(string UserName) // TODO: передавать только Id пользователя, + переделать запись в Identity (Id вместо имени пользователя)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            using (UserContext context = new UserContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Login == UserName);

                lst = context.ExchangeKeys
                    .Where(x => user.RoleId == (int)UserModel.Roles.User ? x.UserId == user.Id : true)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.AccountId })
                    .ToList();
            }

            return lst;
        }

        public static IEnumerable<SelectListItem> GetExchangeKeysForBalances(string UserName)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            using (UserContext context = new UserContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Login == UserName);
                if (user.RoleId != 1)
                {
                    lst = context.ExchangeKeys
                      .Where(x => user.RoleId != (int)UserModel.Roles.Boss ? x.UserId == user.Id : true &&
                        x.AccountId != "all")
                      .Select(x => new SelectListItem { Text = x.Name, Value = x.AccountId })
                      .ToList();
                }
                else
                    lst.Add(new SelectListItem { Text = "permission denied", Value = "404" });

            }
            return lst;
        }
    }
}
