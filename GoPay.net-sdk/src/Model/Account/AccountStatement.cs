using System;
using Newtonsoft.Json;
using GoPay.Common;
using Newtonsoft.Json.Converters;

namespace GoPay.Account
{
    public class AccountStatement
    {

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("date_from")]
        public DateTime DateFrom { get; set; }
        
        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("date_to")]
        public DateTime DateTo { get; set; }

        [JsonProperty("goid")]
        public long GoID { get; set; }

        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        [JsonProperty("format")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StatementGeneratingFormat Format { get; set; }

        public override string ToString()
        {
            return string.Format(
                   "AccountStatement [dateFrom={0}, dateTo={1}, goId={2}, currency={3}, format={4}]",
                   DateFrom, DateTo, GoID, Enum.GetName(typeof(Currency), Currency), Enum.GetName(typeof(StatementGeneratingFormat), Format)
                   );
        }

    }
}


