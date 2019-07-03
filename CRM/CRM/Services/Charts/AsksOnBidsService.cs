using CRM.Helpers;
using CRM.Models;
using CRM.Models.Database;
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

        public AskOnBidViewModel Load(string coin, DateTime startDate, DateTime endDate)
        {
            if (startDate == null && endDate == null)
                return null;

            SeparateHelper.Separator.NumberDecimalSeparator = ".";

            using (CRMContext context = new CRMContext())
            {
                AsksBids = context.OrderBookModels
                    .Where(x => x.CurrencyName == coin)
                    .Where(x => x.Date >= startDate && x.Date <= endDate)
                    .OrderBy(x => x.Date).ToList();
            }
            //TODO: use DateTime as result of service method, convert in controller/view
            DatesAsks = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Date.DateTime).ToList();
            AsksValues = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Volume).ToList();

            DatesBids = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Date.DateTime).ToList();
            BidsValues = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Volume).ToList();

            AskOnBidViewModel askOnBidViewModel = new AskOnBidViewModel
            {
                DatesAsks = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Date.DateTime).Select(x => x.ToJavascriptTicks()).ToList(),
                AsksValues = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Volume).Select(x => x.ToString(SeparateHelper.Separator)).ToList(),

                DatesBids = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Date.DateTime).Select(x => x.ToJavascriptTicks()).ToList(),
                BidsValues = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Volume).Select(x => x.ToString(SeparateHelper.Separator)).ToList()
            };

            return askOnBidViewModel;
        }

    }
}
