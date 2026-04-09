using Newtonsoft.Json;

namespace GoPay.Model.Payments
{
    public class QrCode
    {
        /// <summary>Czech payment QR code (SPAYD) - only available for the CZK currency</summary>
        [JsonProperty("spayd")]
        public string Spayd { get; set; }

        /// <summary>Slovak payment QR code (PayBySquare) - only available for the EUR currency</summary>
        [JsonProperty("paybysquare")]
        public string PayBySquare { get; set; }

        /// <summary>SEPA payment QR code (specified by EPC) - only available for the EUR currency</summary>
        [JsonProperty("sepa")]
        public string Sepa { get; set; }

        /// <summary>Hungarian payment QR code (specified by MNB) - only available for the HUF currency</summary>
        [JsonProperty("mnb_qr")]
        public string MnbQr { get; set; }
    }
}
