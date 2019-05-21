using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THManager
{
    class Checker
    {
        public static int GetLastId()
        {
            using (MySqlContext context = new MySqlContext())
            {
                return context.Orders.Where(x => x.TimeEnded >= DateTime.Parse("2019-04-06")).FirstOrDefault().Id;
            }
        }
    }
}
