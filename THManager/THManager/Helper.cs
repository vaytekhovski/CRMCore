using Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jobs
{
    class Helper
    {
        public static DateTime FindTimeLastSell()
        {
            DateTime LastSellTime;
            using (CRMContext context = new CRMContext())
            {
                try
                {
                    LastSellTime = context.AccountTradeHistories.LastOrDefault(x =>
                        x.Time < DateTime.Now.AddDays(-2) &&
                        x.Side == "sell" &&
                        x.Profit != 0).Time;
                }
                catch
                {
                    LastSellTime = new DateTime(1999, 01, 01);
                }
            }
            return LastSellTime;
        }

        public static int FindLastSell()
        {
            int LastSellId;
            using (CRMContext context = new CRMContext())
            {
                try
                {
                    LastSellId = context.AccountTradeHistories.LastOrDefault(x =>
                        x.Time < DateTime.Now.AddDays(-2) &&
                        x.Side == "sell" &&
                        x.Profit != 0).Id;
                }
                catch
                {
                    LastSellId = 0;
                }
            }
            return LastSellId;
        }
    }
}
