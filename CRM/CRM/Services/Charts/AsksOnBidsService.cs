using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Charts
{
    public class AsksOnBidsService
    {
        public List<long> DatesAsks { get; private set; } = new List<long>();
        public List<long> DatesBids { get; private set; } = new List<long>();
        public List<string> AsksValues { get; private set; } = new List<string>();
        public List<string> BidsValues { get; private set; } = new List<string>();


        public AsksOnBidsService() { }

        public void Load(string coin, string startDate, string endDate)
        {
            if (startDate == null && endDate == null)
                return;

            var SD = DateTime.Parse(startDate);
            var ED = DateTime.Parse(endDate);

            using (CRMContext context = new CRMContext())
            {
                var Asks = context.OrderBookModels
                    .Where(x => x.BookType == "ask" && x.CurrencyName == coin)
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

                    DatesAsks.Add(item.Date.Date.ToJavascriptTicks());
                    AsksValues.Add(value.Replace(',', '.'));
                }

                foreach (var item in Bids)
                {
                    DateTime DatePlusTime = item.Date.DateTime;
                    string value = item.Volume.ToString();

                    DatesBids.Add(DatePlusTime.ToJavascriptTicks());
                    BidsValues.Add(value.Replace(',', '.'));
                }
            }

        }

    }
}
