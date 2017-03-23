using Newtonsoft.Json;
using System;
using GoPay.Common;
using Newtonsoft.Json.Converters;

namespace GoPay.Model.Payments
{
    public class Recurrence
    {

        public enum RecurrenceState
        {
            REQUESTED,
            STARTED,
            STOPPED
        }
        
        [JsonProperty("recurrence_cycle")]
        public RecurrenceCycle Cycle { get; set; }
        
        [JsonProperty("recurrence_period")]
        public int Period { get; set; }

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("recurrence_date_to")]
        public DateTime DateTo { get; set; }
        
        [JsonProperty("recurrence_state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RecurrenceState State { get; set; }


        public override string ToString()
        {
            return string.Format(
                   "Recurrence [recurrenceCycle={0}, recurrencePeriod={1}, recurrenceDateTo={2}, recurrencState={3}]",
                   Cycle, Period, DateTo, Enum.GetName(typeof(RecurrenceState),State));
        }
         
        
    }
}
