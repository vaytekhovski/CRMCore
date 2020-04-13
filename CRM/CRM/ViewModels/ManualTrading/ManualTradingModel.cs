using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.ManualTrading
{
    public class ManualTradingModel
    {
        public string Account { get; set; }
        public string Coin { get; set; }
        public Unit Unit { get; set; }
        public List<Unit> Units { get; set; }

    }

    public class Unit
    {
        public DateTime Time { get; set; }
        public double CountOfUnits3h { get; set; }
        public string PercentOfUnits3h { get; set; }
        public double CountOfUnits1h { get; set; }
        public string PercentOfUnits1h { get; set; }
        public double CountOfUnits30m { get; set; }
        public string PercentOfUnits30m { get; set; }
        public double CountOfUnits15m { get; set; }
        public string PercentOfUnits15m { get; set; }
        public double CountOfUnits5m { get; set; }
        public string PercentOfUnits5m { get; set; }
    }
}
