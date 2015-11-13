using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JordoCore.Entity.IFTTT
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
