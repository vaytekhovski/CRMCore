using Business;
using Business.Contexts;
using System;
using System.Linq;

namespace Jobs
{
    class Helper
    {

        public static AccountTradeHistory FindLastSellDayAgo()
        {
            using (BasicContext context = new BasicContext())
            {
                var lastEl = context.AccountTradeHistories.OrderBy(x => x.Time).Where(x => x.Time < DateTime.UtcNow.AddDays(-1)).LastOrDefault(x => x.Side == "sell");
                return lastEl ?? new AccountTradeHistory() { Id = 0, Time = new DateTime(1970, 1, 1, 1, 1, 1, 1) };
            }
        }
    }
}
