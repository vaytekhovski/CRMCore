using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class DataFilter
        {
            public string BookType { get; set; }
            public string Situation { get; set; }
            public string Coin { get; set; }
            public int CurrentPage { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string OrderType { get; set; }
            public string nulldelta { get; set; }
    }
}
