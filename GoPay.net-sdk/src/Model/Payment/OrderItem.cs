using System;
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
        public Nullable<ItemType> ItemType { get; set; }

        [JsonProperty("product_url")]
        public string ProductURL { get; set; }

        [JsonProperty("ean")]
        public string Ean { get; set; }


        public override string ToString()
        {
            return string.Format("OrderItem [name={0}, amount={1}, count={2}, vatRate={3}, type={4}, ean={5}, url={6}]", 
                Name, Amount, Count, VatRate, ItemType, Ean, ProductURL
                );
        }
    }
}
