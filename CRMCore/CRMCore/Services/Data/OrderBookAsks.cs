using CRMCore.Models;
using CRMCore.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMCore.Services.Data
{
    public class OrderBookAsks
    {
        public double summVolume;
        public List<OrderBookAsksModel> Show;

        public OrderBookAsks(string coin, string situation, string startDate = "", string endDate = "")
        {
            using (CRMCoreContext context = new CRMCoreContext())
            {
                Show = context.OrderBookAsksModels.Where(z => z.CurrencyName == coin).OrderByDescending(x => x.Date).ToList();

                if (startDate != "" && endDate != "")
                {
                    DateTime SD = DateTime.Parse(startDate);
                    DateTime ED = DateTime.Parse(endDate);

                    //Session["SD"] = HomeController.DatesToSession(SD);
                    //Session["ED"] = HomeController.DatesToSession(ED);

                    Show = Show.Where(x => x.Date >= SD && x.Date <= ED).ToList();
                }

                if (situation != "all")
                    Show = Show.Where(x => x.MarketSituation == situation).ToList();

                foreach (var item in Show)
                {
                    summVolume += item.Volume;
                }
                
            }

            DropDownFields.Swap(coin, situation);
        }
    }
}
