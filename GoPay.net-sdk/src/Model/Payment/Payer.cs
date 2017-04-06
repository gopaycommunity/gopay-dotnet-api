using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using GoPay.Common;
using Newtonsoft.Json.Converters;

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
        
        [JsonProperty("contact")]
        public PayerContact Contact { get; set; }

        [JsonProperty("payment_card")]
        public PayerPaymentCard PaymendCard { get; set; }

        [JsonProperty("bank_account")]
        public BankAccount BankAccount { get; set; }


        public Payer()
        {
            AllowedPaymentInstruments = new List<PaymentInstrument>();
            AllowedSwifts = new List<string>();
        }

        public override string ToString()
        {
            return string.Format(
                    "PayerParty [paymentInstrument={0}, allowedPaymentInstruments={1}, allowedSwifts={2}, defaultPaymentInstrument={3}, defaultSwift={4}, contact={5}, paymentCard={6}, bankAccount={7}]",
                    PaymentInstrument, AllowedPaymentInstruments, AllowedSwifts, DefaultPaymentInstrument, DefaultSwift, Contact, PaymendCard, BankAccount);
        }

    }
}
