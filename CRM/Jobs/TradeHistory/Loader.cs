using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business;
using Business.Contexts;

namespace Jobs
{
    class Loader
    {
        public Loader()
        {

        }
        public List<Orders> LoadOrders(DateTime timeToLoad)
        {
            using (MySQLContext context = new MySQLContext())
            {
                return context.Orders.Where(x => x.TimeEnded > timeToLoad && x.AccountId != "bccd3ca1-0b5e-41ac-8233-3a35209912c7").OrderBy(x => x.TimeEnded).ToList();
            }
        }
    }
}
