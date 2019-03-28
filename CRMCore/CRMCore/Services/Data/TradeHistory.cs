using CRMCore.Models;
using CRMCore.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMCore.Services.Data
{
    public class TradeHistoryService
    {
        private double summVolume;
        private List<TradeHistoryModel> show;

        public double SummVolume { get => summVolume;  }
        public List<TradeHistoryModel> Show { get => show; }

        public TradeHistoryService() { }

        public void Load(string coin, string situation, string orderType, string startDate = "", string endDate = "")
        {
            using (CRMCoreContext context = new CRMCoreContext())
            {
                show = context.TradeHistoryModels.Where(z => z.CurrencyName == coin).OrderByDescending(x => x.Date).ToList();

                if (startDate != "" && endDate != "")
                {
                    DateTime SD = DateTime.Parse(startDate);
                    DateTime ED = DateTime.Parse(endDate);

                    //Session["SD"] = HomeController.DatesToSession(SD);
                    //Session["ED"] = HomeController.DatesToSession(ED);

                    show = Show.Where(x => x.Date >= SD && x.Date <= ED).ToList();
                }

                if (situation != "all")
                    show = show.Where(x => x.MarketSituation == situation).ToList();

                if (orderType != "all")
                    show = show.Where(x => x.Side == orderType).ToList();

                foreach (var item in Show)
                {
                    summVolume += item.Volume;
                }

            }
            DropDownFields.Swap(coin, situation, orderType);
        }
    }
}
