using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Data
{
    public class TradeHistoryViewModel
    {
        public string Coin { get; set; }

        public string Situation { get; set; }

        public string OrderType { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<Models.Database.TradeHistoryModel> Show { get; set; }

        public double SummVolume { get; set; }
    }
}
