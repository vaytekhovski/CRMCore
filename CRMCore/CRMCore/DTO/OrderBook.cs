using System;
using Newtonsoft.Json;

namespace CRMCore.DTO
{
    public partial class OrderBook
    {
        [JsonProperty("Time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("Situation")]
        public Situation Situation { get; set; }

        [JsonProperty("Type")]
        public OrderBookAskType Type { get; set; }

        [JsonProperty("Price")]
        public double Price { get; set; }

        [JsonProperty("Amount")]
        public double Amount { get; set; }

        [JsonProperty("Done")]
        public bool Done { get; set; }
    }
}
