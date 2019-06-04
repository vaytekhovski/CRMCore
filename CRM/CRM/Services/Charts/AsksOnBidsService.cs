using CRM.Models;
using CRM.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Charts
{
    public class AsksOnBidsService
    {
        public List<DateTime> DatesAsks { get; private set; } = new List<DateTime>();
        public List<DateTime> DatesBids { get; private set; } = new List<DateTime>();
        public List<double> AsksValues { get; private set; } = new List<double>();
        public List<double> BidsValues { get; private set; } = new List<double>();

        private List<OrderBookModel> AsksBids = new List<OrderBookModel>();
        public AsksOnBidsService() { }

        public void Load(string coin, DateTime startDate, DateTime endDate)
        {
            if (startDate == null && endDate == null)
                return;

            using (CRMContext context = new CRMContext())
            {
                AsksBids = context.OrderBookModels
                    .Where(x => x.CurrencyName == coin)
                    .Where(x => x.Date >= startDate && x.Date <= endDate)
                    .OrderBy(x => x.Date).ToList();
            }

            DatesAsks = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Date.Date).ToList();
            AsksValues = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Volume).ToList();

            DatesBids = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Date.Date).ToList();
            BidsValues = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Volume).ToList();

        }

    }
}
