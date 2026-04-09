using Newtonsoft.Json;

namespace GoPay.Model.Payments
{
    public class QrPaymentInternationalBankAccount
    {
        [JsonProperty("bic")]
        public string Bic { get; set; }

        [JsonProperty("iban")]
        public string Iban { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}
