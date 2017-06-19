using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using GoPay.Common;

namespace GoPay.Supercash
{
    public class SupercashCoupon
    {
        [JsonProperty("supercash_coupon_id")]
        public string SupercashCouponId { get; set; }

        [JsonProperty("custom_id")]
        public string CustomId { get; set; }

        [JsonProperty("barcode_image_url")]
        public string BarcodeImageUrl { get; set; }

        [JsonProperty("supercash_number")]
        public string SupercashNumber { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("date_valid_to")]
        public DateTime DateValidTo { get; set; }

        [JsonProperty("payment_session_id_list")]
        public List<string> PaymentSessionIdList { get; set; }

        public override string ToString()
        {
            return string.Format(
                   "Coupon [supercashCouponId={0}, customId={1}, barcodeImageUrl={2}, supercashNumber={3}, barcode={4}, dateCreated={5}, dateValidTo={6}, paymentSessionIdList={7}]",
                   SupercashCouponId, CustomId, BarcodeImageUrl, SupercashNumber, Barcode, DateCreated, DateValidTo, PaymentSessionIdList
                   );
        }
    }
}
