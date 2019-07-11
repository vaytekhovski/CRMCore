using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.Master
{
    public class TradeHistory
    {
        public string Exchange { get; set; }
        public string Base { get; set; }
        public string Quote { get; set; }
        public DateTime Time { get; set; }
        public string Side { get; set; }
        public decimal Amount { get; set; }
    }
}
