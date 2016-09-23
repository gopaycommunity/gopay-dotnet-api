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
        public PaymentInstrument PaymentInstrument { get; set; }
        
        [JsonProperty (PropertyName = "allowed_payment_instruments", ItemConverterType = typeof(StringEnumConverter))]
        public IList<PaymentInstrument> AllowedPaymentInstruments { get; }
        
        [JsonProperty("allowed_swifts")]
        public IList<string> AllowedSwifts { get; }

        [JsonProperty("default_payment_instrument")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentInstrument DefaultPaymentInstrument { get; set; }
        
        [JsonProperty("default_swift")]
        public string DefaultSwift { get; set; }
        
        [JsonProperty("contact")]
        public PayerContact Contact { get; set; }
        
        public Payer()
        {
            AllowedPaymentInstruments = new List<PaymentInstrument>();
            AllowedSwifts = new List<string>();
        }

        public override string ToString()
        {
            return string.Format(
                    "PayerParty [paymentInstrument={}, allowedPaymentInstruments={}, allowedSwifts={}, defaultPaymentInstrument={}, defaultSwift={}, contact={}]",
                    Enum.GetName(typeof(PaymentInstrument),PaymentInstrument), AllowedPaymentInstruments, AllowedSwifts, DefaultPaymentInstrument, DefaultSwift, Contact);
        }

    }
}
