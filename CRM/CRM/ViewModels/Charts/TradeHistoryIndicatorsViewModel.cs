using Business.Models.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Charts
{
    public class TradeHistoryIndicatorsViewModel
    {
        public TradeHistoryIndicatorsViewModel()
        {
            Indicators = new List<ViewIndicator>();
        }
        public string PageName { get; set; }
        public string Account { get; set; }
        public string Coin { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }


        public List<ViewIndicator> Indicators { get; set; }
    }

    public class ViewIndicator
    {
        public long Date { get; set; }
        public string TP { get; set; }
        public string TL { get; set; }
        public string TR { get; set; }
        public string NP { get; set; }
        public string NL { get; set; }
        public string N { get; set; }
        public string RPL { get; set; }
        public string AP { get; set; }
        public string AL { get; set; }
        public string AR { get; set; }
        public string RAPAL { get; set; }
        public string MIDD { get; set; }
        public string Dmin { get; set; }
        public string R { get; set; }
        public string RF { get; set; }
        public string PF { get; set; }
        public string APF { get; set; }
        public string SharpeRatio { get; set; }
    }
}
