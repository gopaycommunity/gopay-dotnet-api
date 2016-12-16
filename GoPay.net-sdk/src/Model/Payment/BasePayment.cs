using System.Collections.Generic;
using Newtonsoft.Json;
using GoPay.Model.Payments;
using GoPay.Common;
using Newtonsoft.Json.Converters;

namespace GoPay.Model.Payment
{

    public class BasePayment
    {
        [JsonProperty("payer")]
        public Payer Payer { get; set; }

        [JsonProperty("target")]
        public Target Target { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("order_description")]
        public string OrderDescription { get; set; }

        [JsonProperty("items")]
        public List<OrderItem> Items { get; set; }

        [JsonProperty("callback")]
        public Callback Callback { get; set; }

        [JsonProperty("recurrence")]
        public Recurrence Recurrence { get; set; }

        [JsonProperty("preauthorization")]
        public bool PreAuthorization { get; set; }

        [JsonProperty("additional_params")]
        public List<AdditionalParam> AdditionalParams { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        public BasePayment()
        {
            Items = new List<OrderItem>();
            AdditionalParams = new List<AdditionalParam>();
        }

        public void AddAdditionalParams(string key,string value)
        {
            AdditionalParams.Add(new AdditionalParam()
            {
                Name = key,
                Value = value
            });
        }

        public void AddOrderItem(string name, long amount, long count)
        {
            Items.Add(new OrderItem()
            {
                Amount = amount,
                Count = count,
                Name = name
            });
        }
    }
}
