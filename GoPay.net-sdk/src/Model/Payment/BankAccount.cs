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
        public Nullable<Country> Country { get; set; }

        [JsonProperty("account_token")]
        public string AccountToken { get; set; }


        public override string ToString()
        {
            if (Country != null)
            {
                return string.Format(
                    "BankAccount [prefix={0}, accountNumber={1}, bankCode={2}, IBAN={3}, BIC={4}, accountName={5}, country={6}, accountToken={7}]",
                    Prefix, AccountNumber, BankCode, IBAN, BIC, AccountName, Enum.GetName(typeof(Country), Country), AccountToken);
            }
            else
            {
                return string.Format(
                    "BankAccount [prefix={0}, accountNumber={1}, bankCode={2}, IBAN={3}, BIC={4}, accountName={5}, country={6}, accountToken={7}]",
                    Prefix, AccountNumber, BankCode, IBAN, BIC, AccountName, Country, AccountToken);
            }
            
        }

    }
}
