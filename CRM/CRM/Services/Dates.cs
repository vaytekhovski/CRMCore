using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    public static class Dates
    {
        public static string MinDate = "2019-04-05";

        public static string CurrentDate()
        {
            var currentDate = DateTime.Now;

            string dd = currentDate.Day < 10 ? "0" + currentDate.Day.ToString() : currentDate.Day.ToString();
            string mm = currentDate.Month < 10 ? "0" + currentDate.Month.ToString() : currentDate.Month.ToString();
            string yy = currentDate.Year.ToString();
            string curDate = yy + "-" + mm + "-" + dd;
            return curDate;
        }
    }
}
