﻿using CRM.Models;
using CRM.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Data
{
    public class OrderBookService
    {
        private double summVolume;
        private List<OrderBookModel> show;

        public double SummVolume { get => summVolume; }
        public List<OrderBookModel> Show { get => show; }

        public OrderBookService() { }

        public void Load(string bookType, string coin, string situation, string startDate = "", string endDate = "")
        {
            using (CRMContext context = new CRMContext())
            {
                show = context.OrderBookModels.Where(z => z.BookType == bookType && z.CurrencyName == coin).OrderByDescending(x => x.Date).ToList();

                if (startDate != "" && endDate != "")
                {
                    DateTime SD = DateTime.Parse(startDate);
                    DateTime ED = DateTime.Parse(endDate);

                    //Session["SD"] = HomeController.DatesToSession(SD);
                    //Session["ED"] = HomeController.DatesToSession(ED);

                    show = show.Where(x => x.Date >= SD && x.Date <= ED).ToList();
                }

                if (situation != "all")
                    show = show.Where(x => x.MarketSituation == situation).ToList();

                foreach (var item in Show)
                {
                    summVolume += item.Volume;
                }
            }

            if (coin != DropDownFields.Coins.First().Value)
                DropDownFields.SwapCoins(coin);

            if (situation != DropDownFields.Situation.First().Value)
                DropDownFields.SwapSituations(situation);
        }
    }
}
