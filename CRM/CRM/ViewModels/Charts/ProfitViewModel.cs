using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Charts
{
    public class ProfitViewModel
    {
        public string PageName { get; set; }
        public string Account { get; set; }
        public string Coin { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public List<long?> Dates { get; set; }
        public List<string> Values { get; set; }

        public int CountOfZero { get; set; }
        public int CountOfMore { get; set; }
        public int CountOfLess { get; set; }

        public string VolumeOfMore { get; set; }
        public string VolumeOfLess { get; set; }

        public ProfitViewModel()
        {
            Dates = new List<long?>();
            Values = new List<string>();
        }
    }
}
