using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using GoPay.Common;
using Newtonsoft.Json.Converters;

namespace GoPay.Model.Payments
{
    public class Payment
    {
        public enum SessionState
        {
            CREATED,
            PAYMENT_METHOD_CHOSEN,
            PAID,
            AUTHORIZED,
            CANCELED,
            TIMEOUTED,
            REFUNDED,
            PARTIALLY_REFUNDED
        }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("parent_id")]
        public long ParentId { get; set; }

        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SessionState State { get; set; }

        [JsonProperty("payment_instrument")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentInstrument PaymentInstrument { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        [JsonProperty("payer")]
        public Payer Payer { get; set; }

        [JsonProperty("target")]
        public Target Target { get; set; }

        [JsonProperty("recurrence")]
        public Recurrence Recurrence { get; set; }

        [JsonProperty("preauthorization")]
        public PreAuthorization PreAuthorization { get; set; }

        [JsonProperty("additional_params")]
        public IList<AdditionalParam> AdditionalParams { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("gw_url")]
        public string GwUrl { get; set; }

        public Payment()
        {
            AdditionalParams = new List<AdditionalParam>();
        }
      
        public bool Error { get { return false; } }
    }
}
