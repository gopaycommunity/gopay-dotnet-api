using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using GoPay.Common;

namespace GoPay.Model.Payments
{
    public class QrPayment
    {
        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        [JsonProperty("recipient")]
        public QrPaymentRecipient Recipient { get; set; }

        [JsonProperty("qr_code")]
        public QrCode QrCode { get; set; }
    }
}
