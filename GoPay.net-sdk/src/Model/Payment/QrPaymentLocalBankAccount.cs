using Newtonsoft.Json;

namespace GoPay.Model.Payments
{
    public class QrPaymentLocalBankAccount
    {
        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("bank_code")]
        public string BankCode { get; set; }

        [JsonProperty("variable_symbol")]
        public string VariableSymbol { get; set; }
    }
}
