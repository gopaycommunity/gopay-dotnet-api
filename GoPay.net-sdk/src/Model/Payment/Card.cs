using GoPay.Model.Payments;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace gopay_dotnet_standard_api.src.Model.Payment
{
    public class Card
    {
        public enum CardStatus
        {
            ACTIVE,
            DELETED
        }

        public Card()
        {

        }

        [JsonProperty("card_id")]
        public long CardId { get; set; }

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

        [JsonProperty("card_fingerprint")]
        public string CardFingerprint { get; set; }

        [JsonProperty("card_token")]
        public string CardToken { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Nullable<CardStatus> Status;

        [JsonProperty("real_masked_pan")]
        public string RealMaskedPan { get; set; }

        [JsonProperty("card_art_url")]
        public string CardArtUrl { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
