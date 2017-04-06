using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using GoPay.Common;
using Newtonsoft.Json.Converters;
using GoPay.EETProp;

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
        public Nullable<SessionState> State { get; set; }

        [JsonProperty("sub_state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Nullable<SessionSubState> SubState { get; set; }

        [JsonProperty("payment_instrument")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Nullable<PaymentInstrument> PaymentInstrument { get; set; }

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

        [JsonProperty("eet_code")]
        public EETCode EetCode { get; set; }


        public Payment()
        {
            AdditionalParams = new List<AdditionalParam>();
        }
      
        public bool Error { get { return false; } }


        public void AddAdditionalParams(string key, string value)
        {
            AdditionalParams.Add(new AdditionalParam()
            {
                Name = key,
                Value = value
            });
        }

        public override string ToString()
        {
            return string.Format(
                   "Payment [id={0}, parentId={1}, state={2}, paymentInstrument={3}, amount={4}, currency={5}, payer={6}, target={7}, recurrence={8}, aditionalParams={9}, preAuthorization={10}, lang={11}]",
                   Id, ParentId, State, PaymentInstrument, Amount, Enum.GetName(typeof(Currency), Currency), Payer, Target, Recurrence, AdditionalParams, PreAuthorization, Lang
                   );
        }

    }
}