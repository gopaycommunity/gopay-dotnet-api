using System;
using System.Collections.Generic;
using GoPay.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using GoPay.EETProp;

namespace GoPay.Model.Payments
{
    public class NextPayment
    {

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
        public IList<OrderItem> Items { get; set; }

        [JsonProperty("additional_params")]
        public IList<AdditionalParam> AdditionalParams { get; set; }

        [JsonProperty("eet")]
        public EET Eet { get; set; }


        public NextPayment()
        {
            this.Items = new List<OrderItem>();
            this.AdditionalParams = new List<AdditionalParam>();
        }


        public override string ToString()
        {
            return string.Format(
                   "CreateNextPayment [amount={0}, currency={1}, orderNumber={2}, orderDescription={3}]",
                   Amount, Enum.GetName(typeof(Currency), Currency), OrderNumber, OrderDescription
                   );
        }

    }
}