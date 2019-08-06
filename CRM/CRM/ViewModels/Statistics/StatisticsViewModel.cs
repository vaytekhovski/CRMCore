using Business;
using System.Collections.Generic;

namespace CRM.ViewModels.Statistics
{
    public class StatisticsViewModel
    {
        public StatisticsViewModel()
        {
            Statistics = new List<StatisticsElement>();
        }

        public string Id { get; set; }
        public string Action { get; set; }
        public List<StatisticsElement> Statistics { get; set; }
        public int CountOfPages { get; set; }
        public int FirstVisiblePage { get; set; }
        public int LastVisiblePage { get; set; }
        public int CurrentPage { get; set; }
        public string Coin { get; set; }
        public string Account { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TypeOfDate { get; set; }
    }
}
