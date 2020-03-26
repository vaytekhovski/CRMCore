using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Models.Master
{
    public class ChartPoint
    {
        [Key]
        public string Exchange { get; set; }
        [Key]
        public string Base { get; set; }
        [Key]
        public string Quote { get; set; }
        [Key]
        public DateTime Time { get; set; }
        public decimal Close { get; set; }
    }
}
    

