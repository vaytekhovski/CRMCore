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
    }
}
