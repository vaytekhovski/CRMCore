using Business.Contexts;
using System;
using System.Linq;

namespace Jobs
{
    class Helper
    {
        public static DateTime FindTimeLastSell()
        {
            DateTime LastSellTime;
            using (BasicContext context = new BasicContext())
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
            using (BasicContext context = new BasicContext())
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
