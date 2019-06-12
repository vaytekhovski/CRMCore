using System;
using System.Collections.Generic;
using System.Text;

namespace THManager.Models
{
    public class SignalsPrivate
    {
        public string Id { get; set; }
        public string Exchange { get; set; }
        public string Base { get; set; }
        public string Quote { get; set; }
        public DateTime SourceTime { get; set; }
        public decimal Value { get; set; }
        public string Side { get; set; }
        public string ErrorMessages { get; set; }
    }
}
