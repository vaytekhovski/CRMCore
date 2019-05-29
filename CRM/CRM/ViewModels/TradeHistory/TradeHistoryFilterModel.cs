using CRM.Models.Binance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class TradeHistoryFilterModel
    {
        public int CountOfPages { get; set; }

        public int FirstVisiblePage { get; set; }
        public int LastVisiblePage { get; set; }

        public int CurrentPage { get; set; }

        public string Coin { get; set; }

        public string Account { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public ICollection<AccountTradeHistory> Orders { get; set; }

        public decimal TotalProfit { get; set; }

        public decimal DesiredTotalProfit { get; set; }

        public TradeHistoryFilterModel()
        {
            Orders = new List<AccountTradeHistory>();
        }
    }
}
