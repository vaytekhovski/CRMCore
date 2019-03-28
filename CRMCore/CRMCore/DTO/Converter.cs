using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRMCore.DTO
{
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                SituationConverter.Singleton,
                OrderBookAskTypeConverter.Singleton,
                SideConverter.Singleton,
                TradeHistoryTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
