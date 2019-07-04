using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.Charts
{
    public class AskOnBidModel
    {

        public List<DateTime> DatesAsks { get; set; }
        public List<DateTime> DatesBids { get; set; }
        public List<double> AsksValues { get; set; }
        public List<double> BidsValues { get; set; }

        public AskOnBidModel()
        {
            DatesAsks = new List<DateTime>();
            DatesBids = new List<DateTime>();
            AsksValues = new List<double>();
            BidsValues = new List<double>();
        }
    }
}
