using CRM.Models.Database;
using CRM.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public static class DatesHelper
    {
        public static DateTime MinDate = new DateTime(2019, 4, 5, 0, 0, 0);
        public static string MinDateStr = MinDate.ToString("yyyy-MM-ddTHH:mm");

        public static string CurrentDateStr => DateTime.Now.AddHours(3).ToString("yyyy-MM-ddTHH:mm");
    }

    public static class SeparateHelper
    {
       public static NumberFormatInfo Separator = new NumberFormatInfo();
    }

    public static class AccountExchangeKeys
    {
        public static List<ExchangeKey> ExchangeKeys;

        public static void InitializeExchangeKeys()
        {
            using(UserContext db =new UserContext())
            {
                ExchangeKeys = db.ExchangeKeys.ToList();
            }
        }

        public static string AccountName(string accountId)
        {
            using (UserContext db = new UserContext())
            {
                return ExchangeKeys.FirstOrDefault(x => x.AccountId == accountId).Name;
            }
        }
    }
}
