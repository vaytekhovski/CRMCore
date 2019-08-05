using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class TradeHistoryDelta
    {
        public int Id { get; set; }
        public string Exchange { get; set; }
        public string Base { get; set; }
        public string Quote { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string Situation { get; set; }
        public decimal Value { get; set; }
        public decimal TickerAsk { get; set; }
        public decimal? MinLongDiff { get; set; }
        public decimal? MaxLongDiff { get; set; }

    }
}
