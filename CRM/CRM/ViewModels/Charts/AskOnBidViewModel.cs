using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Charts
{
    public class AskOnBidViewModel
    {
        public string PageName { get; set; }
        public string Coin { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public List<long> DatesAsks { get; set; }
        public List<long> DatesBids { get; set; }
        public List<string> AsksValues { get; set; }
        public List<string> BidsValues { get; set; }

        public AskOnBidViewModel()
        {
            DatesAsks = new List<long>();
            DatesBids = new List<long>();
            AsksValues = new List<string>();
            BidsValues = new List<string>();
        }
    }
}
