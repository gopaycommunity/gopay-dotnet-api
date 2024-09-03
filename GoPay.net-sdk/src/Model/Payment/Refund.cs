using GoPay.Account;
using GoPay.Common;
using GoPay.Model.Payments;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using static gopay_dotnet_standard_api.src.Model.Payment.Card;

namespace gopay_dotnet_standard_api.src.Model.Payment
{
    public class Refund
    {
        public enum RefundState
        {
            SUCCESS,
            FAILED,
            REQUESTED
        }

        public Refund()
        {

        }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RefundState RefundStatus { get; set; }

        [JsonProperty("amount")]
        public long Ammount { get; set; }

        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("date_requested")]
        public DateTime DateRequested { get; set; }

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("date_last_change")]
        public Nullable<DateTime> DateLastChange { get; set; }

    }
}
