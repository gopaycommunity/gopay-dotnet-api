using System;
using Newtonsoft.Json;

namespace GoPay.Model.Payments
{
    public class PayerPaymentCard
    {

        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("card_expiration")]
        public string CardExpiration { get; set; }

        [JsonProperty("card_brand")]
        public string CardBrand { get; set; }

        [JsonProperty("card_issuer_country")]
        public string CardIssuerCountry { get; set; }

        [JsonProperty("card_issuer_bank")]
        public string CardIssuerBank { get; set; }


        public override string ToString()
        {
            return string.Format(
                    "PayerPaymentCard [cardNumber={0}, cardExpiration={1}, cardBrand={2}, cardIssuerCountry={3}, cardIssuerBank={4}]",
                    CardNumber, CardExpiration, CardBrand, CardIssuerCountry, CardIssuerBank
                    );
        }

    }
}
