using Newtonsoft.Json;

namespace YouCore.Entity.IFTTT
{
    public class IFTTTPushRequest
    {
        [JsonProperty("value1")]
        public string Value1 { get; set; }

        [JsonProperty("value2")]
        public string Value2 { get; set; }

        public string Url { get; set; }
    }

    public class IFTTTPush
    {
        [JsonProperty("value1")]
        public string Value1 { get; set; }

        [JsonProperty("value2")]
        public string Value2 { get; set; }

    }
}
