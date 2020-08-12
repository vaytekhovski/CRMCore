using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Charts
{
    public class SignalsModel
    {
        public SignalsModel()
        {
            RaiseDates = new List<DateTime>();
            FallDates = new List<DateTime>();
            RaiseValues = new List<string>();
            FallValues = new List<string>();
            FallEMA = new List<string>();
            RaiseEMA = new List<string>();
            signals = new List<RaiseFallSignals>();
        }

        public string PageName { get; set; }
        public string Account { get; set; }
        public string Coin { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public List<DateTime> RaiseDates { get; set; }
        public List<DateTime> FallDates { get; set; }
        public List<string> RaiseValues { get; set; }
        public List<string> FallValues { get; set; }
        public List<string> FallEMA { get; set; }
        public List<string> RaiseEMA { get; set; }
        public List<string> BBM { get; set; }
        public List<string> BBL { get; set; }
        public List<string> BBU { get; set; }


        public List<RaiseFallSignals> signals { get; set; }
    }

    public class RaiseFallSignals
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal RaiseProba { get; set; }
        public decimal FallProba { get; set; }
    }
}
