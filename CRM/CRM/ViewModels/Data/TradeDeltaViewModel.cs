using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels.Data
{
    public class TradeDeltaViewModel
    {
        public string Coin { get; set; }

        public string Situation { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string NullDelta { get; set; }

        public List<Models.Database.TradeDeltaModel> Show { get; set; }

        public double SummDelta { get; set; }
        public int CountOfPages { get; set; }
        public int FirstVisiblePage { get; set; }
        public int LastVisiblePage { get; set; }
        public int CurrentPage { get; set; }
        public string TypeOfDate { get; set; }
        public string Action { get; set; }
    }
}
