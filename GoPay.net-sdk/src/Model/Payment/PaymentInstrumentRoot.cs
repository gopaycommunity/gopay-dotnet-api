using System;
using GoPay.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoPay.Model.Payments
{
    public class PaymentInstrumentRoot
    {

        [JsonProperty("groups")]
        public Dictionary<CheckoutGroup, Group> Groups { get; set; }

        [JsonProperty("enabledPaymentInstruments")]
        public List<EnabledPaymentInstrument> EnabledPaymentInstruments { get; set; }


        public PaymentInstrumentRoot addGroup(CheckoutGroup coGroup, Group groupData)
        {

            if (this.Groups == null)
            {
                this.Groups = new Dictionary<CheckoutGroup, Group>();
            }

            this.Groups.Add(coGroup, groupData);
            return this;
        }


        public PaymentInstrumentRoot addEnabledPaymentInstrument(EnabledPaymentInstrument instrument)
        {

            if (this.EnabledPaymentInstruments == null)
            {
                this.EnabledPaymentInstruments = new List<EnabledPaymentInstrument>();
            }

            this.EnabledPaymentInstruments.Add(instrument);
            return this;
        }

    }
}
