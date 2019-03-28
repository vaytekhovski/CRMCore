using System;
using Newtonsoft.Json;

namespace CRMCore.DTO
{
    internal class TradeHistoryTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TradeHistoryType) || t == typeof(TradeHistoryType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "market")
            {
                return TradeHistoryType.Market;
            }
            throw new Exception("Cannot unmarshal type TradeHistoryType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TradeHistoryType)untypedValue;
            if (value == TradeHistoryType.Market)
            {
                serializer.Serialize(writer, "market");
                return;
            }
            throw new Exception("Cannot marshal type TradeHistoryType");
        }

        public static readonly TradeHistoryTypeConverter Singleton = new TradeHistoryTypeConverter();
    }
}
