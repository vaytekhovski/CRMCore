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
            gradDates = new List<DateTime>();
            logtwoDates = new List<DateTime>();
            gradValues = new List<string>();
            logtwoValues = new List<string>();
            logtwoEMA = new List<string>();
            gradEMA = new List<string>();
            signals = new List<gradlogtwoSignals>();
        }

        public string PageName { get; set; }
        public string Account { get; set; }
        public string Coin { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public List<DateTime> gradDates { get; set; }
        public List<DateTime> logtwoDates { get; set; }
        public List<string> gradValues { get; set; }
        public List<string> logtwoValues { get; set; }
        public List<string> logtwoEMA { get; set; }
        public List<string> gradEMA { get; set; }
        public List<string> BBM { get; set; }
        public List<string> BBL { get; set; }
        public List<string> BBU { get; set; }


        public List<gradlogtwoSignals> signals { get; set; }
    }

    public class gradlogtwoSignals
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal gradProba { get; set; }
        public decimal logtwoProba { get; set; }
    }
}
