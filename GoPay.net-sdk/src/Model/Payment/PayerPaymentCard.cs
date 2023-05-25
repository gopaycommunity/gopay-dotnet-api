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

        [JsonProperty("card_token")]
        public string CardToken { get; set; }

        [JsonProperty("3ds_result")]
        public string ThreeDResult { get; set; }

        [JsonProperty("card_fingerprint")]
        public string CardFingerPrint { get; set; }


        public override string ToString()
        {
            return string.Format(
                    "PayerPaymentCard [cardNumber={0}, cardExpiration={1}, cardBrand={2}, cardIssuerCountry={3}, cardIssuerBank={4}, cardToken={5}, threeDResult={6}, cardFingerPrint={7}]",
                    CardNumber, CardExpiration, CardBrand, CardIssuerCountry, CardIssuerBank, CardToken, ThreeDResult, CardFingerPrint
                    );
        }

    }
}
