using Newtonsoft.Json;

namespace GoPay.Supercash
{
    public class SupercashBatchResult
    {
        [JsonProperty("batch_request_id")]
        public long BatchRequestId { get; set; }

        public override string ToString()
        {
            return string.Format("SupercashBatchResult [batchRequestId={0}]", BatchRequestId);
        }
    }
}
