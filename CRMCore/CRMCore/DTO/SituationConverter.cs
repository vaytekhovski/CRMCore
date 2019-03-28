using System;
using Newtonsoft.Json;

namespace CRMCore.DTO
{
    internal class SituationConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Situation) || t == typeof(Situation?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "flat":
                    return Situation.Flat;
                case "trend":
                    return Situation.Trend;
                case "unknown":
                    return Situation.Unknown;
            }
            throw new Exception("Cannot unmarshal type Situation");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Situation)untypedValue;
            switch (value)
            {
                case Situation.Flat:
                    serializer.Serialize(writer, "flat");
                    return;
                case Situation.Trend:
                    serializer.Serialize(writer, "trend");
                    return;
                case Situation.Unknown:
                    serializer.Serialize(writer, "unknown");
                    return;
            }
            throw new Exception("Cannot marshal type Situation");
        }

        public static readonly SituationConverter Singleton = new SituationConverter();
    }
}
