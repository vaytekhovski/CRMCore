using System;
using Newtonsoft.Json;

namespace CRMCore.DTO
{
    internal class OrderBookAskTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(OrderBookAskType) || t == typeof(OrderBookAskType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "ask":
                    return OrderBookAskType.Ask;
                case "bid":
                    return OrderBookAskType.Bid;
            }
            throw new Exception("Cannot unmarshal type OrderBookAskType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (OrderBookAskType)untypedValue;
            switch (value)
            {
                case OrderBookAskType.Ask:
                    serializer.Serialize(writer, "ask");
                    return;
                case OrderBookAskType.Bid:
                    serializer.Serialize(writer, "bid");
                    return;
            }
            throw new Exception("Cannot marshal type OrderBookAskType");
        }

        public static readonly OrderBookAskTypeConverter Singleton = new OrderBookAskTypeConverter();
    }
}
