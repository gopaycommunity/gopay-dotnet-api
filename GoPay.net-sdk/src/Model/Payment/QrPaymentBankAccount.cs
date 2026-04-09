using Newtonsoft.Json;

namespace GoPay.Model.Payments
{
    public class QrPaymentBankAccount
    {
        [JsonProperty("local")]
        public QrPaymentLocalBankAccount Local { get; set; }

        [JsonProperty("international")]
        public QrPaymentInternationalBankAccount International { get; set; }
    }
}
