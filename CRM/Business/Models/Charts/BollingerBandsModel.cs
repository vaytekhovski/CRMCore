using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models.Charts
{
    public class BollingerBandsModel
    {
        public List<DateTime> Dates { get; set; } = new List<DateTime>();
        public List<decimal> ProbaSellValues { get; set; } = new List<decimal>();
        public List<decimal> ProbaBuyValues { get; set; } = new List<decimal>();
        public List<decimal> BBLValues { get; set; } = new List<decimal>();
    }
}
