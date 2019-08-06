using System;
using Newtonsoft.Json;

namespace CRM.DTO
{
    public partial class TradeDelta
    {
        [JsonProperty("TimeFrom")]
        public DateTimeOffset TimeFrom { get; set; }

        [JsonProperty("TimeTo")]
        public DateTimeOffset TimeTo { get; set; }

        [JsonProperty("Situation")]
        public Situation Situation { get; set; }

        [JsonProperty("Value")]
        public double Value { get; set; }
    }
}
