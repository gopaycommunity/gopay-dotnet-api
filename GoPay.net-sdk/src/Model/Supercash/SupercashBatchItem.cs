using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using GoPay.Common;

namespace GoPay.Supercash
{
    public class SupercashBatchItem
    {
        
        [JsonProperty("sub_type")]
        public SubType SubType { get; set; }

        [JsonProperty("custom_id")]
        public string CustomId { get; set; }

        [JsonProperty("amounts")]
        public List<long> Amounts { get; set; }

        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("order_description")]
        public string OrderDescription { get; set; }

        [JsonProperty("buyer_email")]
        public string BuyerEmail { get; set; }

        [JsonProperty("buyer_phone")]
        public string BuyerPhone { get; set; }

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("date_valid_to")]
        public DateTime DateValidTo { get; set; }

        [JsonProperty("notification_url")]
        public string NotificiationUrl { get; set; }

        public override string ToString()
        {
            return string.Format(
                   "Batch Items [subType={0}, customId={1}, amounts={2}, orderNumber={3}, orderDescription={4}, buyerEmail={5}, buyerPhone={6}, dateValidTo={7}, notificationUrl={8}]",
                   SubType, CustomId, Amounts, OrderNumber, OrderDescription, BuyerEmail, BuyerPhone, DateValidTo, NotificiationUrl
                   );
        }
    }
}
