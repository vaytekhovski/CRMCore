using CRM.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Statistics
{
    public class StatisticsViewModel
    {
        public StatisticsViewModel()
        {
            Statistics = new List<StatisticsElement>();
        }

        public List<StatisticsElement> Statistics { get; set; }
        public int CountOfPages { get; set; }
        public int FirstVisiblePage { get; set; }
        public int LastVisiblePage { get; set; }
        public int CurrentPage { get; set; }
        public string Coin { get; set; }
        public string Account { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
