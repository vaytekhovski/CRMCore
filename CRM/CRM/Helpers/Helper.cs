using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public static class DatesHelper
    {
        public static DateTime MinDate = new DateTime(2019, 4, 5);
        public static string MinDateStr = MinDate.ToString("yyyy-MM-dd");

        public static string CurrentDateStr => DateTime.Now.ToString("yyyy-MM-dd");
    }

    public static class SeparateHelper
    {
       public static NumberFormatInfo Separator = new NumberFormatInfo();
    }
}
