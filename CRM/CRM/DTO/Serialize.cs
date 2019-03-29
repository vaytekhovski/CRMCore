using Newtonsoft.Json;

namespace CRM.DTO
{
    public static class Serialize
    {
        public static string ToJson(this Ticker self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
