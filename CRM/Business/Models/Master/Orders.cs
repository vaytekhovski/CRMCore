using System;
using Business.Models.DataVisioAPI;

namespace Business
{
    public partial class Deal
    {
        public Deal()
        {

        }


        public string id { get; set; }
        public string exchange { get; set; }
        public string @base { get; set; }
        public string coin { get; set; }
        public string quote { get; set; }
        public decimal income { get; set; }
        public decimal outcome { get; set; }
        public profit profit { get; set; }
        public DateTime opened { get; set; }
        public DateTime closed { get; set; }
    }
}
