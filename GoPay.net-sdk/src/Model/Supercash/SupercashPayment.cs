using System.Collections.Generic;
using Newtonsoft.Json;
using GoPay.Model.Payments;

namespace GoPay.Supercash
{
    public class SupercashPayment
    {

        [JsonProperty("coupon")]
        public SupercashCoupon Coupon { get; set; }

        [JsonProperty("payments")]
        public List<Payment> Payments { get; set; }

        public override string ToString()
        {
            return string.Format(
                   "SupercashPayment [{0}]", Coupon.ToString());
        }

    }
}
