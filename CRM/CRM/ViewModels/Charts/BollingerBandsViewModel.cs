using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Charts
{
    public class BollingerBandsViewModel
    {
        public string PageName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public List<long> Dates { get; set; }
        public List<string> ProbaSellValues { get; set; }
        public List<string> ProbaBuyValues { get; set; }
        public List<string> BBLValues { get; set; }

        public BollingerBandsViewModel()
        {
            Dates = new List<long>();
            ProbaSellValues = new List<string>();
            ProbaBuyValues = new List<string>();
            BBLValues = new List<string>();
        }
    }
}
