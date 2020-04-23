using System;

namespace Business
{
    public partial class Order
    {
        public Order()
        {

        }


        public string id { get; set; }
        public string account_id { get; set; }
        public string exchange { get; set; }
        public string @base { get; set; }
        public string quote { get; set; }
        public string number { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string side { get; set; }
        public double price { get; set; }
        public double amount { get; set; }
        public double filled { get; set; }
        public DateTime created { get; set; }
        public DateTime closed { get; set; }
    }
}
