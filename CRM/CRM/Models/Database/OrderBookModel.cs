using System;

namespace CRM.Models.Database
{
    public class OrderBookModel
    {
        public long Id { get; set; }
        public string BookType { get; set; }
        public string CurrencyName { get; set; }
        public DateTimeOffset Date { get; set; }
        public double Price { get; set; }
        public double Volume { get; set; }
        public string MarketSituation { get; set; }

    }
}