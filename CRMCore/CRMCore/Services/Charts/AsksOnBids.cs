using CRMCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMCore.Services.Charts
{
    public class AsksOnBids
    {
        public List<long> datesAsks = new List<long>();
        public List<long> datesBids = new List<long>();
        public List<string> asksValues = new List<string>();
        public List<string> bidsValues = new List<string>();
        
        public AsksOnBids(string coin, string startDate = "", string endDate = "")
        {
            if (startDate != "" && endDate != "")
            {
                var SD = DateTime.Parse(startDate);
                var ED = DateTime.Parse(endDate);

                //Session["SD"] = HomeController.DatesToSession(SD);
                //Session["ED"] = HomeController.DatesToSession(ED);

                using (CRMCoreContext context = new CRMCoreContext())
                {
                    var Asks = context.OrderBookAsksModels
                        .Where(x => x.CurrencyName == coin)
                        .Where(x => x.Date >= SD && x.Date <= ED)
                        .OrderBy(x => x.Date);

                    var Bids = context.OrderBookBidsModels
                        .Where(x => x.CurrencyName == coin)
                        .Where(x => x.Date >= SD && x.Date <= ED)
                        .OrderBy(x => x.Date);

                    foreach (var item in Asks)
                    {
                        DateTime DatePlusTime = item.Date.DateTime;
                        string value = item.Volume.ToString();

                        datesAsks.Add(item.Date.Date.ToJavascriptTicks());
                        asksValues.Add(value.Replace(',', '.'));
                    }

                    foreach (var item in Bids)
                    {
                        DateTime DatePlusTime = item.Date.DateTime;
                        string value = item.Volume.ToString();

                        datesBids.Add(DatePlusTime.ToJavascriptTicks());
                        bidsValues.Add(value.Replace(',', '.'));
                    }
                }
            }

            DropDownFields.Swap(coin);
        }
    }
}
