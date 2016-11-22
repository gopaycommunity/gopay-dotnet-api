using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace GoPay.Model.Payments
{
    public class Target
    {
        public enum TargetType
        {
            ACCOUNT,
            BANK_ACCOUNT,
            COUPON
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TargetType Type { get; set; }

        [JsonProperty("goid")]
        public long GoId { get; set; }

        public override string ToString()
        {
            return string.Format("TargetParty [type={}, goId={}]", Enum.GetName(typeof(TargetType), Type), GoId);
        }

        public static Target CreateEshop(long goID)
        {
            Target created = new Target()
            {
                Type = TargetType.ACCOUNT,
                GoId = goID
            };
            return created;
        }

        public Target ToEshop(long goId)
        {
            Type = TargetType.ACCOUNT;
            GoId = goId;
            return this;
        }

    }

}
