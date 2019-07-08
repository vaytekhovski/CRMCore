using System.Collections.Generic;

namespace CRM.ViewModels.Data
{
    public class OrderBookViewModel
    {
        public string Coin { get; set; }

        public string Situation { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<Models.Database.OrderBookModel> Show { get; set; }

        public double SummVolume { get; set; }
        public int CountOfElements { get; set; }
        public int CountOfPages { get; set; }
        public int FirstVisiblePage { get; set; }
        public int LastVisiblePage { get; set; }
        public int CurrentPage { get; set; }
        public string TypeOfDate { get; set; }
        public string Id { get; set; }
        public string Action { get; set; }
    }
}
