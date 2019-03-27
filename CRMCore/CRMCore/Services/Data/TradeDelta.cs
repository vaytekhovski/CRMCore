using CRMCore.Models;
using CRMCore.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMCore.Services.Data
{
    public class TradeDelta
    {
        public double summDelta;
        public List<TradeDeltaModel> Show;

        public TradeDelta(string coin, string startDate = "", string endDate = "", string nulldelta = "all")
        {
            using (CRMCoreContext context = new CRMCoreContext())
            {
                Show = context.TradeDeltaModels.ToList();

                Show = context.TradeDeltaModels.Where(z => z.CurrencyName == coin).OrderByDescending(x => x.TimeFrom).ToList();

                if (startDate != "" && endDate != "")
                {
                    DateTime SD = DateTime.Parse(startDate);
                    DateTime ED = DateTime.Parse(endDate);

                    //Session["SD"] = HomeController.DatesToSession(SD);
                    //Session["ED"] = HomeController.DatesToSession(ED);

                    Show = Show.Where(x => x.TimeFrom >= SD && x.TimeTo <= ED).ToList();
                }

                if (nulldelta == "notnull")
                    Show = Show.Where(x => x.Delta > 0 || x.Delta < 0).ToList();

                foreach (var item in Show)
                {
                    summDelta += item.Delta;
                }


                DropDownFields.Swap(coin, "", "", true);
            }
        }
    }
}
