using System;
using Newtonsoft.Json;
using GoPay.Common;

namespace GoPay.EETProp
{
    public class EETReceiptFilter
    {

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("date_from")]
        public DateTime DateFrom { get; set; }

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("date_to")]
        public DateTime DateTo { get; set; }

        [JsonProperty("id_provozovny")]
        public int IdProvoz { get; set; }


        public override string ToString()
        {
            return string.Format(
                   "EETReceiptFilter [dateFrom={0}, dateTo={1}, idProvoz={2}]",
                   DateFrom, DateTo, IdProvoz
                   );
        }

    }
}