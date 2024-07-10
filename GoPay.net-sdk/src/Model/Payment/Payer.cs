using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using System.Xml.Linq;

namespace GoPay.Model.Payments
{
    public class Payer
    {

        [JsonProperty("payment_instrument")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Nullable<PaymentInstrument> PaymentInstrument { get; set; }
        
        [JsonProperty (PropertyName = "allowed_payment_instruments", ItemConverterType = typeof(StringEnumConverter))]
        public IList<PaymentInstrument> AllowedPaymentInstruments { get; set; }
        
        [JsonProperty("allowed_swifts")]
        public IList<string> AllowedSwifts { get; set; }

        [JsonProperty("default_payment_instrument")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Nullable<PaymentInstrument> DefaultPaymentInstrument { get; set; }
        
        [JsonProperty("default_swift")]
        public string DefaultSwift { get; set; }

        [JsonProperty("allowed_bnpl_types")]
        public IList<string> AllowedBnplTypes { get; set; }

        [JsonProperty("default_bnpl_type")]
        public string DefaultBnplType { get; set; }

        [JsonProperty("contact")]
        public PayerContact Contact { get; set; }

        [JsonProperty("payment_card")]
        public PayerPaymentCard PaymendCard { get; set; }

        [JsonProperty("card_id")]
        public string CardId { get; set; }

        [JsonProperty("bank_account")]
        public BankAccount BankAccount { get; set; }

        [JsonProperty("allowed_card_token")]
        public string AllowedCardToken { get; set; }

        [JsonProperty("verify_pin")]
        public string VerifyPin { get; set; }

        [JsonProperty("request_card_token")]
        public bool RequestCardToken { get; set; }

        [JsonProperty("masked_pan")]
        public string MaskedPan { get; set; }

        public Payer()
        {
            AllowedPaymentInstruments = new List<PaymentInstrument>();
            AllowedSwifts = new List<string>();
            AllowedBnplTypes = new List<string>();
        }

        public override string ToString()
        {
            return string.Format(
                    "PayerParty [paymentInstrument={0}, allowedPaymentInstruments={1}, allowedSwifts={2}, defaultPaymentInstrument={3}, defaultSwift={4}, contact={5}, paymentCard={6}, bankAccount={7}, allowedCardToken={8}, verifyPin={9}, requestCardToken={10}, maskedPan={11}, cardId={12}]",
                    PaymentInstrument, AllowedPaymentInstruments, AllowedSwifts, DefaultPaymentInstrument, DefaultSwift, Contact, PaymendCard, BankAccount, AllowedCardToken, VerifyPin, RequestCardToken, MaskedPan, CardId);
        }

    }
}
