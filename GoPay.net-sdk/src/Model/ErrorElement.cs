using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoPay.Model
{
    public class ErrorElement
    {
        public enum ErrorScope
        {
            G,F
        }

        [JsonProperty("scope")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ErrorScope Scope { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }

        [JsonProperty("error_name")]
        public string ErrorName { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

    }
}
