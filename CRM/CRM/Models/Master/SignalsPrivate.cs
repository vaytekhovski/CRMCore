using System;

namespace CRM.Models.Master
{
    public class SignalsPrivate
    {
        public string Id { get; set; }
        public string Exchange { get; set; }
        public string Base { get; set; }
        public string Quote { get; set; }
        public DateTime SourceTime { get; set; }
        public decimal TrendDiff { get; set; }
        public string Side { get; set; }
        public string ErrorMessages { get; set; }
    }
}
