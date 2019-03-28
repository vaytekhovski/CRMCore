﻿using CRMCore.Models;
using CRMCore.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMCore.Services.Data
{
    public class TradeDeltaService
    {
        private double summDelta;
        private List<TradeDeltaModel> show;

        public double SummDelta { get => summDelta;}
        public List<TradeDeltaModel> Show { get => show;  }

        public TradeDeltaService() { }

        public void Load(string coin, string startDate = "", string endDate = "", string nulldelta = "all")
        {
            using (CRMCoreContext context = new CRMCoreContext())
            {
                show = context.TradeDeltaModels.ToList();

                show = context.TradeDeltaModels.Where(z => z.CurrencyName == coin).OrderByDescending(x => x.TimeFrom).ToList();

                if (startDate != "" && endDate != "")
                {
                    DateTime SD = DateTime.Parse(startDate);
                    DateTime ED = DateTime.Parse(endDate);

                    //Session["SD"] = HomeController.DatesToSession(SD);
                    //Session["ED"] = HomeController.DatesToSession(ED);

                    show = show.Where(x => x.TimeFrom >= SD && x.TimeTo <= ED).ToList();
                }

                if (nulldelta == "notnull")
                    show = show.Where(x => x.Delta > 0 || x.Delta < 0).ToList();

                foreach (var item in Show)
                {
                    summDelta += item.Delta;
                }


                DropDownFields.Swap(coin, "", "", true);
            }
        }
    }
}
