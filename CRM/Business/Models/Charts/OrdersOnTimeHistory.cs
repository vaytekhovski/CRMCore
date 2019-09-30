using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class OrdersInTimeHistory
    {
        public List<DateTime> Dates { get; set; }
        public List<double> MACDValues { get; set; }
        public List<double> SIGValues { get; set; }


        public OrdersInTimeHistory()
        {
            MACDValues = new List<double>();
            SIGValues = new List<double>();
            Dates = new List<DateTime>();
        }
    }
}
