using System;
using Business.Models.DataVisioAPI;

namespace Business.Models.DataVisioAPI
{
    public partial class Deal
    {
        public Deal()
        {
            orders = new Order[] { };
        }


        public string id { get; set; }
        public string exchange { get; set; }
        public string @base { get; set; }
        public string coin { get; set; }
        public string quote { get; set; }
        public string type { get; set; }
        public decimal? income { get; set; }
        public decimal? outcome { get; set; }
        public decimal? fee { get; set; }
        public profit profit { get; set; }
        public DateTime opened { get; set; }
        public DateTime? closed { get; set; }
        public Order[] orders { get; set; }
    }

    public class Order
    {
        public string id { get; set; }
        public string type { get; set; }
        public string side { get; set; }
        public string number { get; set; }
        public string status { get; set; }
        public decimal price { get; set; }
        public decimal amount { get; set; }
        public decimal filled { get; set; }
        public DateTime created { get; set; }
        public DateTime? closed { get; set; }
    }
}
