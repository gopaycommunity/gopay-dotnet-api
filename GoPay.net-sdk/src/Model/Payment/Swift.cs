using System;
using Newtonsoft.Json;
using GoPay.Common;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Globalization;


namespace GoPay.Model.Payments
{
    public class Swift
    {

        [JsonProperty("swift")]
        public string SwiftName { get; set; }

        [JsonProperty("label")]
        public Dictionary<CultureInfo, string> Label { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("isOnline")]
        public bool IsOnline { get; set; }


        public Swift AddLabel(CultureInfo locale, string label)
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
