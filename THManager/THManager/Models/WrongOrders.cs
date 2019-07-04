using System;
using System.Collections.Generic;
using System.Text;

namespace THManager.Models
{
    class WrongOrders
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
