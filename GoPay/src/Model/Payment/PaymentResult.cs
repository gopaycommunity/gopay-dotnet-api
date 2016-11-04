using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoPay.Payments
{
    public class PaymentResult
    {
        public enum PaymentResults
        {
            ACCEPTED,
            FINISHED,
            FAILED
        }
        
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("result")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentResults Result { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("PaymentResult[Id={},Result={},Description={}]", Id, Result, Description);
        }

        internal bool Error { get { return false; } }
    }
}
