using Newtonsoft.Json;
using GoPay.Common;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Globalization;

namespace GoPay.Model.Payments
{
    public class EnabledPaymentInstrument
    {

        [JsonProperty("paymentInstrument")]
        [JsonConverter(typeof(SafeJsonEnumStringConvertor), (int) PaymentInstrument.UNKNOWN)]
        public PaymentInstrument PaymentInstrument { get; set; }

        [JsonProperty("label")]
        public Dictionary<CultureInfo, string> Label { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("enabledSwifts")]
        public List<Swift> EnabledSwifts { get; set; }

        [JsonProperty("enabledBnplTypes")]
        public List<BnplType> EnabledBnplTypes { get; set; }


        public EnabledPaymentInstrument AddLabel(CultureInfo locale, string label)
        {
            if (this.Label == null)
            {
                this.Label = new Dictionary<CultureInfo, string>();
            }

            this.Label.Add(locale, label);
            return this;
        }

        public EnabledPaymentInstrument WithGroup(CheckoutGroup coGroup)
        {
            this.Group = coGroup.GetCaption();
            return this;
        }

        public EnabledPaymentInstrument AddEnabledSwift(Swift swift)
        {

            if (this.EnabledSwifts == null)
            {
                this.EnabledSwifts = new List<Swift>();
            }

            this.EnabledSwifts.Add(swift);
            return this;
        }

        public EnabledPaymentInstrument AddEnabledBnplType(BnplType bnplType)
        {

            if (this.EnabledBnplTypes == null)
            {
                this.EnabledBnplTypes = new List<BnplType>();
            }

            this.EnabledBnplTypes.Add(bnplType);
            return this;
        }

        public override string ToString()
        {
            return string.Format(
                   "EnabledPaymentInstrument: [paymentInstrument={0}, label={1}, image={2}, group={3}, enabledSwifts={4}, enabledBnplTypes={5}]",
                   PaymentInstrument, Label, Image, Group, EnabledSwifts, EnabledBnplTypes
                   );
        }

    }
}