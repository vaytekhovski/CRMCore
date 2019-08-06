using Business;
using Business.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Database
{
    public static class AccountsExchangeKeysService
    {
        public static IEnumerable<SelectListItem> GetExchangeKeys(int UserId)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            using (BasicContext context = new BasicContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Id == UserId);

                lst = context.ExchangeKeys
                    .Where(x => user.RoleId == (int)UserModel.Roles.User ? x.UserId == user.Id : true)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.AccountId })
                    .ToList();
            }

            return lst;
        }

        public static IEnumerable<SelectListItem> GetExchangeKeysForBalances(int UserId)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            using (BasicContext context = new BasicContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Id == UserId);
                if (user.RoleId != 1)
                {
                    lst = context.ExchangeKeys
                      .Where(x => user.RoleId != (int)UserModel.Roles.Boss ? x.UserId == user.Id : true)
                      .Select(x => new SelectListItem { Text = x.Name, Value = x.AccountId })
                      .ToList();
                }
                else
                    lst.Add(new SelectListItem { Text = "permission denied", Value = ":(" });

            }
            return lst;
        }
    }
}
