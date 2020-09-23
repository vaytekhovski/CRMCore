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
            boostDates = new List<DateTime>();
            logregDates = new List<DateTime>();
            boostValues = new List<string>();
            logregValues = new List<string>();
            logregEMA = new List<string>();
            boostEMA = new List<string>();
            signals = new List<boostlogregSignals>();
        }

        public string PageName { get; set; }
        public string Account { get; set; }
        public string Coin { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public List<DateTime> boostDates { get; set; }
        public List<DateTime> logregDates { get; set; }
        public List<string> boostValues { get; set; }
        public List<string> logregValues { get; set; }
        public List<string> logregEMA { get; set; }
        public List<string> boostEMA { get; set; }
        public List<string> BBM { get; set; }
        public List<string> BBL { get; set; }
        public List<string> BBU { get; set; }


        public List<boostlogregSignals> signals { get; set; }
    }

    public class boostlogregSignals
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal boostProba { get; set; }
        public decimal logregProba { get; set; }
    }
}
