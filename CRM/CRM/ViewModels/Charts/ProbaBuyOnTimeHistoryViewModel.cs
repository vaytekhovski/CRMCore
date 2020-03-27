using Business.Models.Charts.ProfitOnTimeHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Charts
{
    public class ProbaBuyOnTimeHistoryViewModel
    {
        public string PageName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Exchange { get; set; }
        public string Coin { get; set; }
        public string ProbaBuyMin { get; set; }
        public string ProbaBuyMax { get; set; }

        public List<IndicatorValuesModel> Indicators { get; set; }
        public List<long> ProbaBuyTimes { get; set; }
        public ProbaBuyOnTimeHistoryViewModel()
        {
            ProbaBuyTimes = new List<long>();

            Indicators = new List<IndicatorValuesModel>();
        }
    }
}
