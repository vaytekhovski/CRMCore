using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Database
{
    public class TradeHistoryModel
    {
        public long Id { get; set; }
        public string CurrencyName { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Side { get; set; }
        public DateTimeOffset OrderTime { get; set; }
        public double Price { get; set; }
        public double Volume { get; set; }
        public string MarketSituation { get; set; }
    }
}