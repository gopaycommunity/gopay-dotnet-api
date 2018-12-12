using System.Collections.Generic;
using Newtonsoft.Json;
using GoPay.EETProp;

namespace GoPay.Model.Payments
{
    public class RefundPayment
    {

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("items")]
        public List<OrderItem> Items { get; set; }

        [JsonProperty("eet")]
        public EET Eet { get; set; }

    }
}
