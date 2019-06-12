using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Charts
{
    public class IndicatorPointsViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string Exchange { get; set; }
        public string Base { get; set; }

        public List<long> Dates { get; set; }
        public List<string> MACDValues { get; set; }
        public List<string> SIGValues { get; set; }


        public IndicatorPointsViewModel()
        {
            MACDValues = new List<string>();
            SIGValues = new List<string>();
            Dates = new List<long>();
        }
    }
}
