using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;


namespace GoPay.Model.Payments
{
    public class Group
    {
        [JsonProperty("label")]
        public Dictionary<CultureInfo, string> Label { get; set; }


        public Group AddLabel(CultureInfo locale, string label)
        {
            if (this.Label == null)
            {
                this.Label = new Dictionary<CultureInfo, string>();
            }

            this.Label.Add(locale, label);
            return this;
        }

        public override string ToString()
        {
            string output = "";
            foreach(KeyValuePair<CultureInfo, string> label in Label)
            {
                output += string.Format("Group [locale={0}, description={1}]\n", label.Key.ToString(), label.Value);
            }
            return output;
        }
    }
}
