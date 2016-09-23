using Newtonsoft.Json;

namespace GoPay.Model.Payments
{
    public class OrderItem
    {
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("amount")]
        public long Amount { get; set; }
        
        [JsonProperty("fee")]
        public long Fee { get; set; }
        
        [JsonProperty("count")]
        public long Count { get; set; }
        
        public override string ToString()
        {
            return string.Format("ROrderItem [name=%s, amount=%s, fee=%s, count=%s]", Name, Amount, Fee, Count);
        }

    }
}
