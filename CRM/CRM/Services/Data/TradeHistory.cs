﻿using CRM.Models;
using CRM.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Data
{
    public class TradeHistoryService
    {
        private double summVolume;
        private List<TradeHistoryModel> show;

        public double SummVolume { get => summVolume;  }
        public List<TradeHistoryModel> Show { get => show; }

        public TradeHistoryService() { }

        public void Load(string coin, string situation, string orderType, string startDate = "", string endDate = "")
        {
            using (CRMContext context = new CRMContext())
            {
                show = context.TradeHistoryModels.Where(z => z.CurrencyName == coin).OrderByDescending(x => x.Date).ToList();

                if (startDate != "" && endDate != "")
                {
                    DateTime SD = DateTime.Parse(startDate);
                    DateTime ED = DateTime.Parse(endDate);

                    //Session["SD"] = HomeController.DatesToSession(SD);
                    //Session["ED"] = HomeController.DatesToSession(ED);

                    show = Show.Where(x => x.Date >= SD && x.Date <= ED).ToList();
                }

                if (situation != "all")
                    show = show.Where(x => x.MarketSituation == situation).ToList();

                if (orderType != "all")
                    show = show.Where(x => x.Side == orderType).ToList();

                foreach (var item in Show)
                {
                    summVolume += item.Volume;
                }

            }
            
            if(coin != DropDownFields.Coins.ToArray()[0].Value)
                DropDownFields.SwapCoins(coin);

            if(situation != DropDownFields.Situation.ToArray()[0].Value)
                DropDownFields.SwapSituations(situation);

            if(orderType != DropDownFields.OrderType.ToArray()[0].Value)
                DropDownFields.SwapOrderTypes(orderType);
        }
    }
}
