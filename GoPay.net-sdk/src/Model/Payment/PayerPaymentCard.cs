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
                    "PayerPaymentCard [cardNumber={}, cardExpiration={}, cardBrand={}, cardIssuerCountry={}, cardIssuerBank={}]",
                    CardNumber, CardExpiration, CardBrand, CardIssuerCountry, CardIssuerBank
                    );
        }

    }
}
