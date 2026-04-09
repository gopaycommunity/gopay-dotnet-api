using Newtonsoft.Json;

namespace GoPay.Model.Payments
{
    public class QrPaymentRecipient
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("bank_account")]
        public QrPaymentBankAccount BankAccount { get; set; }
    }
}
