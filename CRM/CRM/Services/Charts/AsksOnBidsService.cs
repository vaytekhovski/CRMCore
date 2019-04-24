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
            if (startDate == null && endDate == null) //TODO: даты доллжны парситься снаружи сервисов (в контроллерах), поменять здесь и в остальных местах
                return;

            using (CRMContext context = new CRMContext())
            {
                AsksBids = context.OrderBookModels //TODO: bids and asks -в один запрос
                    .Where(x => x.CurrencyName == coin &&
                    x.Date >= startDate && x.Date <= endDate)
                    .OrderBy(x => x.Date).ToList();
            }

            //foreach (var item in Asks)
            //{
            //    //DateTime DatePlusTime = item.Date.DateTime;
            //    string value = item.Volume.ToString();

            //    DatesAsks.Add(item.Date.Date.ToJavascriptTicks());
            //    AsksValues.Add(value.Replace(',', '.')); //TODO: сделать через Select здесь и ниже, если будет возможно
            //}

            DatesAsks = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Date.Date).ToList();
            AsksValues = AsksBids.Where(x => x.BookType == "ask").Select(x => x.Volume).ToList();

            //foreach (var item in Bids)
            //{
            //    DateTime DatePlusTime = item.Date.DateTime;
            //    string value = item.Volume.ToString();

            //    DatesBids.Add(DatePlusTime.ToJavascriptTicks());
            //    BidsValues.Add(value.Replace(',', '.'));
            //}

            DatesBids = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Date.Date).ToList();
            BidsValues = AsksBids.Where(x => x.BookType == "bid").Select(x => x.Volume).ToList();

        }

    }
}
