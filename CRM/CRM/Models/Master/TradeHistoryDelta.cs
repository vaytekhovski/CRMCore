using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.Master
{
    public class TradeHistoryDelta
    {
        public string Id { get; set; }
        public string Exchange { get; set; }
        public string Base { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string Situation { get; set; }
        public decimal Value { get; set; }
        public decimal MinLongDiff { get; set; }
        public decimal MaxLongDiff { get; set; }

    }
}
