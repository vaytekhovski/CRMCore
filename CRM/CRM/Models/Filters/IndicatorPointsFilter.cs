using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.Filters
{
    public class IndicatorPointsFilter
    {
        public string Exchange { get; set; }
        public string Coin { get; set; }
        public int CurrentPage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Type { get; set; }
    }
}
