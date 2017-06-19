using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoPay.Supercash
{
    public class SupercashBatch
    {
        [JsonProperty("coupons")]
        public List<SupercashCoupon> Coupons { get; set; }

        public override string ToString()
        {
            string output = "";
            foreach (SupercashCoupon coupon in Coupons)
            {
                output += coupon.ToString() + "\n";
            }

            return string.Format("SupercashBatch Coupons: \n" + "{0}", output);
        }
    }
}
