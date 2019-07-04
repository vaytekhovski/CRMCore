using CRM.Helpers;
using CRM.Models;
using CRM.Models.Charts;
using CRM.Models.Database;
using CRM.Models.Filters;
using CRM.ViewModels.Charts;
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

        public AskOnBidModel Load(ChartsFilter filter)
        {
            using (CRMContext context = new CRMContext())
            {
                AsksBids = context.OrderBookModels
                    .Where(x => x.CurrencyName == filter.Coin)
                    .Where(x => x.Date >= filter.StartDate && x.Date <= filter.EndDate)
                    .OrderBy(x => x.Date).ToList();
            }
            //TODO: use DateTime as result of service method, convert in controller/view
            DatesAsks = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Date.DateTime).ToList();
            AsksValues = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Volume).ToList();

            DatesBids = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Date.DateTime).ToList();
            BidsValues = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Volume).ToList();

            AskOnBidModel askOnBidModel = new AskOnBidModel
            {
                DatesAsks = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Date.DateTime).ToList(),
                AsksValues = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Volume).ToList(),

                DatesBids = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Date.DateTime).ToList(),
                BidsValues = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Volume).ToList()
            };

            return askOnBidModel;
        }

    }
}
