using System;
using Newtonsoft.Json;
using GoPay.Common;
using Newtonsoft.Json.Converters;


namespace GoPay.Model.Payments
{
    public class BankAccount
    {

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("bank_code")]
        public string BankCode { get; set; }

        [JsonProperty("iban")]
        public string IBAN { get; set; }

        [JsonProperty("bic")]
        public string BIC { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("country")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Country Country { get; set; }

        public override string ToString()
        {
            return string.Format(
                    "BankAccount [prefix={}, accountNumber={}, bankCode={}, IBAN={}, BIC={}, accountName={}, country={}]",
                    Prefix, AccountNumber, BankCode, IBAN, BIC, AccountName, Enum.GetName(typeof(Country), Country));
        }

    }
}
