using System.Collections.Generic;
using Newtonsoft.Json;

namespace GoPay.Supercash
{
    public class SupercashBatchRequest
    {
        [JsonProperty("go_id")]
        public long GoId { get; set; }

        [JsonProperty("batch_completed_notification_url")]
        public string BatchNotificationUrl { get; set; }

        [JsonProperty("defaults")]
        public SupercashBatchItem Defaults { get; set; }

        [JsonProperty("coupons")]
        public List<SupercashBatchItem> Coupons { get; set; }
    }
}
