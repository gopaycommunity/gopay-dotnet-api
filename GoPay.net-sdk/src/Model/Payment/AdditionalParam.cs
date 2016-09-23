using Newtonsoft.Json;

namespace GoPay.Model.Payments
{
    public class AdditionalParam
    {

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }

        public override string ToString()
        {
            return string.Format("AdditionalParam[{}={}]", name, value);
        }

    }
}
