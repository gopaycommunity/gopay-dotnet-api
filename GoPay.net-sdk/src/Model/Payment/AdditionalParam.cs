using Newtonsoft.Json;

namespace GoPay.Model.Payments
{
    public class AdditionalParam
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        public override string ToString()
        {
            return string.Format("AdditionalParam[{}={}]", Name, Value);
        }

    }
}
