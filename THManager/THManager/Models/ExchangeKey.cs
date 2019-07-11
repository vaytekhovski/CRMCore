using System;
using System.Collections.Generic;
using System.Text;

namespace THManager.Models
{
    public class ExchangeKey
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string AccountId { get; set; }
    }
}
