using System;

namespace Business.Data
{
    public class TradeDeltaModel
    {
        public long Id { get; set; }
        public string CurrencyName { get; set; }
        public DateTimeOffset TimeFrom { get; set; }
        public DateTimeOffset TimeTo { get; set; }
        public double Delta { get; set; }
       
    }
}