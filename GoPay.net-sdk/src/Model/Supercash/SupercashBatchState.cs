using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoPay.Supercash
{
    public class SupercashBatchState
    {
        public enum SupercashState
        {
            COMPLETED,
            FAILED,
            RUNNING,
            CREATED,
            STOPPED,
            QUEUED
        };

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Nullable<SupercashState> State { get; set; }

        [JsonProperty("batch_completed")]
        public bool BatchCompleted { get; set; }

        [JsonProperty("items_finished")]
        public long ItemsFinished { get; set; }

        [JsonProperty("items_count")]
        public long ItemsCount { get; set; }

        public override string ToString()
        {
            return string.Format(
                   "SupercashBatchState [state={0}, batchCompleted={1}, itemsFinished={2}, itemsCount={3}]",
                   State, BatchCompleted, ItemsFinished, ItemsCount);
        }
    }
}
