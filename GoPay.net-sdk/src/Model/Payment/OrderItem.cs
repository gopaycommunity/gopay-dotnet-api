using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoPay.Model.Payments
{
    public class OrderItem
    {
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("amount")]
        public long Amount { get; set; }
        
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("vat_rate")]
        public int VatRate { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType ItemType { get; set; }

        [JsonProperty("product_url")]
        public string ProductURL { get; set; }

        [JsonProperty("ean")]
        public string Ean { get; set; }


        public override string ToString()
        {
            return string.Format("OrderItem [name={}, amount={}, count={}, vatRate={}, type={}, ean={}, url={}]", 
                Name, Amount, Count, VatRate, ItemType, Ean, ProductURL
                );
        }
    }
}
