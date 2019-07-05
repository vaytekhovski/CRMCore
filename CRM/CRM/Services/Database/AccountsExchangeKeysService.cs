using CRM.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Database
{
    public static class AccountsExchangeKeysService
    {
        public static IEnumerable<SelectListItem> GetExchangeKeys(int UserId)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            using (UserContext context = new UserContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Id == UserId);

                lst = context.ExchangeKeys
                    .Where(x => user.RoleId == (int)UserModel.Roles.User ? x.UserId == user.Id : true)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.AccountId }) // TODO: [COMPLETE] use Id as value instead of name
                    .ToList();
            }

            return lst;
        }

        public static IEnumerable<SelectListItem> GetExchangeKeysForBalances(int UserId)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            using (UserContext context = new UserContext())
            {
                UserModel user = context.UserModels.FirstOrDefault(x => x.Id == UserId);
                if (user.RoleId != 1)
                {
                    lst = context.ExchangeKeys
                      .Where(x => (user.RoleId != (int)UserModel.Roles.Boss || user.RoleId != (int)UserModel.Roles.Spectator) ? x.UserId == user.Id : true)
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
