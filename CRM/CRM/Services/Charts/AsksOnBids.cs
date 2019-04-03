using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Charts
{
    public class AsksOnBidsService
    {
        private List<long> datesAsks = new List<long>();
        private List<long> datesBids = new List<long>();
        private List<string> asksValues = new List<string>();
        private List<string> bidsValues = new List<string>();

        public List<long> DatesAsks { get => datesAsks; }
        public List<long> DatesBids { get => datesBids;  }
        public List<string> AsksValues { get => asksValues; }
        public List<string> BidsValues { get => bidsValues;  }


        public AsksOnBidsService() { }

        public void Load(string coin, string startDate = "", string endDate = "")
        {
            if (startDate != "" && endDate != "")
            {
                var SD = DateTime.Parse(startDate);
                var ED = DateTime.Parse(endDate);

                //Session["SD"] = HomeController.DatesToSession(SD);
                //Session["ED"] = HomeController.DatesToSession(ED);

                using (CRMContext context = new CRMContext())
                {
                    var Asks = context.OrderBookModels
                        .Where(x => x.BookType == "ask" &&  x.CurrencyName == coin)
                        .Where(x => x.Date >= SD && x.Date <= ED)
                        .OrderBy(x => x.Date);

                    var Bids = context.OrderBookModels
                        .Where(x => x.BookType == "bid" && x.CurrencyName == coin)
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

            if (coin != DropDownFields.Coins.ToArray()[0].Value)
                DropDownFields.SwapCoins(coin);
        }

    }
}
