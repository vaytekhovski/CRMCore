using System;
using Newtonsoft.Json;

namespace CRM.DTO
{
    public partial class TradeHistory
    {
        [JsonProperty("Time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("Situation")]
        public Situation Situation { get; set; }

        [JsonProperty("Id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("Type")]
        public TradeHistoryType Type { get; set; }

        [JsonProperty("Side")]
        public Side Side { get; set; }

        [JsonProperty("OrderTime")]
        public DateTimeOffset OrderTime { get; set; }

        [JsonProperty("Price")]
        public double Price { get; set; }

        [JsonProperty("Amount")]
        public double Amount { get; set; }
    }
}
