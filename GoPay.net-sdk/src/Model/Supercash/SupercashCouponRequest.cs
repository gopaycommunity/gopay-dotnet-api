using System;
using Newtonsoft.Json;
using GoPay.Common;

namespace GoPay.Supercash
{
    public class SupercashCouponRequest
    {
        [JsonProperty("go_id")]
        public long GoId { get; set; }

        [JsonProperty("sub_type")]
        public SubType SubType { get; set; }

        [JsonProperty("custom_id")]
        public string CustomId { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

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
                   "Coupon Request [goid={0}, subType={1}, customId={2}, amount={3}, orderNumber={4}, orderDescription={5}, buyerEmail={6}, buyerPhone={7}, dateValidTo={8}, notificationUrl={9}]",
                   GoId, SubType, CustomId, Amount, OrderNumber, OrderDescription, BuyerEmail, BuyerPhone, DateValidTo, NotificiationUrl
                   );
        }
    }
}
