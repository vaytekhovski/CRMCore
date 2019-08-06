﻿using Business;
using System.Collections.Generic;

namespace CRM.ViewModels.Data
{
    public class TradeHistoryViewModel
    {
        public string Coin { get; set; }

        public string Situation { get; set; }

        public string OrderType { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<Business.Data.TradeHistoryModel> Show { get; set; }

        public double SummVolume { get; set; }
        public int CountOfPages { get; set; }
        public int FirstVisiblePage { get; set; }
        public int LastVisiblePage { get; set; }
        public int CurrentPage { get; set; }
        public string TypeOfDate { get; set; }
        public string Action { get; set; }
        public string Id { get; set; }
        public int CountOfElements { get; set; }
    }
}
