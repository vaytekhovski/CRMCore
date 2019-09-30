using Business.Models.Charts.ProfitOnTimeHistory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Charts
{
    public class OrdersOnTimeHistoryViewModel
    {
        public string PageName { get; set; }
        public string Account { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Exchange { get; set; }
        public string Coin { get; set; }

        public List<IndicatorValuesModel> Indicators { get; set; }

        public List<long> GreenTimes { get; set; }
        public List<long> RedTimes { get; set; }

        public OrdersOnTimeHistoryViewModel()
        {
            Indicators = new List<IndicatorValuesModel>();
            GreenTimes = new List<long>();
            RedTimes = new List<long>();
        }
    }
}
