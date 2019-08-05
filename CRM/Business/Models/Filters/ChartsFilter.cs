using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class ChartsFilter
    {
        public string Exchange { get; set; }
        public string Coin { get; set; }
        public DateTime? CalculatingStartDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Type { get; set; }
    }
}
