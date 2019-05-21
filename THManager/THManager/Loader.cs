using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THManager
{
    class Loader
    {
        public static List<Orders> LoadOrders()
        {
            using (MySqlContext context = new MySqlContext())
            {
                 return context.Orders.Where(x => x.Id >= Checker.GetLastId()).ToList();
            }
        }
    }
}
