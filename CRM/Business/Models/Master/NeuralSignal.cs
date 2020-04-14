using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models.Master
{
    public class NeuralSignal
    {
        public string Id { get; set; }
        public string Exchange { get; set; }
        public string Base { get; set; }
        public DateTime Time { get; set; }
        public string Quote { get; set; }
        public decimal ProbaSell { get; set; }
        public decimal ProbaBuy { get; set; }
        public int Value { get; set; }
        public decimal BBL { get; set; }
    }
}
