using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Charts
{
    public class IndicatorPointsModel
    {
        public List<DateTime> Dates { get; set; }
        public List<double> MACDValues { get; set; }
        public List<double> SIGValues { get; set; }


        public IndicatorPointsModel()
        {
            MACDValues = new List<double>();
            SIGValues = new List<double>();
            Dates = new List<DateTime>();
        }
    }
}
