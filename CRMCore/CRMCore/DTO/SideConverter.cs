using System;
using Newtonsoft.Json;

namespace CRMCore.DTO
{
    internal class SideConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Side) || t == typeof(Side?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "buy":
                    return Side.Buy;
                case "sell":
                    return Side.Sell;
            }
            throw new Exception("Cannot unmarshal type Side");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Side)untypedValue;
            switch (value)
            {
                case Side.Buy:
                    serializer.Serialize(writer, "buy");
                    return;
                case Side.Sell:
                    serializer.Serialize(writer, "sell");
                    return;
            }
            throw new Exception("Cannot marshal type Side");
        }

        public static readonly SideConverter Singleton = new SideConverter();
    }
}
