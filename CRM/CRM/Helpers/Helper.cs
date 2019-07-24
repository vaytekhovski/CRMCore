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
        public static DateTime MinDate = DateTime.Now.AddMonths(-1).AddHours(3);

        public static string MinDateTimeStr = MinDate.ToString("yyyy-MM-ddTHH:mm");
        public static string MinDateStr = MinDate.ToString("yyyy-MM-dd");

        public static string CurrentDateTimeStr => DateTime.Now.AddHours(3).ToString("yyyy-MM-ddTHH:mm");
        public static string CurrentDateStr => DateTime.Now.AddHours(3).ToString("yyyy-MM-dd");
    }

    public static class SeparateHelper
    {
       public static NumberFormatInfo Separator = new NumberFormatInfo();
    }

    public static class AccountExchangeKeys
    {
        public static List<ExchangeKey> ExchangeKeys;


        static AccountExchangeKeys()
        {
            using (UserContext db = new UserContext())
            {
                ExchangeKeys = db.ExchangeKeys.ToList();
            }
        }
        
    }
}
