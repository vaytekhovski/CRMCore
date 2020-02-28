using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models.Charts
{
    public class TradeHistoryIndicatorsModel
    {
        public TradeHistoryIndicatorsModel()
        {
            Indicators = new List<Indicator>();
        }

        public List<Indicator> Indicators { get; set; }
    }

    public class Indicator
    {
        public DateTime Date { get; set; }

        public decimal TP { get; set; }
        public decimal TL { get; set; }
        public decimal TR { get; set; }
        public decimal NP { get; set; }
        public decimal NL { get; set; }
        public decimal N { get; set; }
        public decimal RPL { get; set; }
        public decimal AP { get; set; }
        public decimal AL { get; set; }
        public decimal AR { get; set; }
        public decimal RAPAL { get; set; }
        public decimal MIDD { get; set; }
        public decimal Dmin { get; set; }
        public decimal R { get; set; }
        public decimal RF { get; set; }
        public decimal PF { get; set; }
        public decimal APF { get; set; }
        public decimal SharpeRatio { get; set; }
    }
}
