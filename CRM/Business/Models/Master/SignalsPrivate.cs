using System;

namespace Business
{
    public class SignalsPrivate
    {
        public string Id { get; set; }
        public string Exchange { get; set; }
        public string Base { get; set; }
        public string Type { get; set; }
        public string Quote { get; set; }
        public DateTime SourceTime { get; set; }
        public decimal Value { get; set; }
        public string Side { get; set; }
        public string ErrorMessages { get; set; }
        
        public decimal? TotalToDecide { get; set; }
    }
}
