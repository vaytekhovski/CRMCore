using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.Models.Master
{
    public class IndicatorValues
    {
        [Key]
        public string Exchange { get; set; }
        [Key]
        public string Base { get; set; }
        [Key]
        public string Quote { get; set; }
        [Key]
        public DateTime Time { get; set; }
        public decimal? RSI { get; set; }

    }
}
