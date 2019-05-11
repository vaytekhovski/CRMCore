using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.SignalsAnalytics
{
    public class SignalsAnalyticsModel
    {
        public string Situation { get; set; }
        public decimal Value { get; set; }
        public decimal MinLongDiff { get; set; }
        public decimal MaxLongDiff { get; set; }
        public decimal TrendDiff { get; set; }
        public string ErrorMessages { get; set; }
        public DateTime TimeFrom { get; internal set; }
        public DateTime TimeTo { get; internal set; }
    }
}
