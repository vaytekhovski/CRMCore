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
    }
}
