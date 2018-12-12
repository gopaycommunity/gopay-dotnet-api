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

        public override string ToString()
        {
            string output = "PaymentInstrumentRoot {\nGroups:\n";
            foreach(KeyValuePair<CheckoutGroup, Group> dictionaryEntry in Groups)
            {
                output += string.Format("CheckoutGroup={0} : {1}", dictionaryEntry.Key, dictionaryEntry.Value);
            }
            output += "EnabledPaymentInstruments:\n";
            foreach(EnabledPaymentInstrument instrument in EnabledPaymentInstruments)
            {
                output += instrument.ToString() + "\n";
            }
            return output;
        }

    }
}
