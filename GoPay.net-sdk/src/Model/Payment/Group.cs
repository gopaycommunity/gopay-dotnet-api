using System;
using Newtonsoft.Json;
using GoPay.Common;
using Newtonsoft.Json.Converters;
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
    }
}
