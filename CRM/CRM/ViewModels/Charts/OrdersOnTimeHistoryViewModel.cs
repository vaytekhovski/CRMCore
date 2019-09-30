using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Charts
{
    public class OrdersOnTimeHistoryViewModel
    {
        public string PageName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string Exchange { get; set; }
        public string Base { get; set; }

        public List<long> Dates { get; set; }
        public List<string> RSIValues { get; set; }


        public OrdersOnTimeHistoryViewModel()
        {
            RSIValues = new List<string>();
            Dates = new List<long>();
        }
    }
}
