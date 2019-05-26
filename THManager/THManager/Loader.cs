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
                 return context.Orders.Where(x => x.TimeEnded >= DateTime.Parse("2019-04-06") && x.AccountId != "bccd3ca1-0b5e-41ac-8233-3a35209912c7").OrderBy(x => x.TimeEnded).ToList();
            }
        }
    }
}
