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
            RaiseDates = new List<long>();
            FallDates = new List<long>();
            RaiseValues = new List<string>();
            FallValues = new List<string>();
        }

        public string PageName { get; set; }
        public string Account { get; set; }
        public string Coin { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public List<long> RaiseDates { get; set; }
        public List<long> FallDates { get; set; }
        public List<string> RaiseValues { get; set; }
        public List<string> FallValues { get; set; }
    }
}
