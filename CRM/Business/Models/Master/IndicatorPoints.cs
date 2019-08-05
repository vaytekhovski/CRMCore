using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class IndicatorPoints
    {
        [Key]
        public string Exchange { get; set; }
        [Key]
        public string Base { get; set; }
        [Key]
        public string Quote { get; set; }
        [Key]
        public DateTime Time { get; set; }
        public decimal? MACD { get; set; }
        public decimal? SIG { get; set; }
    }
}
