using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.ViewModels.Balances;

namespace CRM.ViewModels.ManualTrading
{
    public class ManualTradingModel
    {
        public ManualTradingModel()
        {
            Unit = new Unit();
            Units = new List<Unit>();
            CoinPrices = new List<string>();
        }

        public string Account { get; set; }
        public string Coin { get; set; }
        public Unit Unit { get; set; }
        public List<Unit> Units { get; set; }
        public List<string> CoinPrices { get; set; }
        public BalancesModel balancesModel { get; set; }
    }

    public class Unit
    {
        public string Time { get; set; }
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
