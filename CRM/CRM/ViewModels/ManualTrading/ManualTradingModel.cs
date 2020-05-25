using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models.DataVisioAPI;
using CRM.ViewModels.Balances;

namespace CRM.ViewModels.ManualTrading
{
    public class ManualTradingModel
    {
        public ManualTradingModel()
        {
            Unit = new Unit();
            Units = new List<Unit>();
            CoinPrices = new List<decimal>();
        }

        public string Account { get; set; }
        public string Coin { get; set; }
        public Unit Unit { get; set; }
        public List<Unit> Units { get; set; }
        public List<decimal> CoinPrices { get; set; }
        public List<Graph> Graphs { get; set; }
        public BalancesModel balancesModel { get; set; }
        public string BuyAmount { get; set; }
        public string SellAmount { get; set; }
        public string PlaceOrderResponse { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimeRange { get; set; }
        public ListDeals Deals { get; set; }
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
