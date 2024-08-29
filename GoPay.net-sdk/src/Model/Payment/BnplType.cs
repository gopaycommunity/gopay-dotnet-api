using GoPay.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace GoPay.Model.Payments
{
    public class BnplType
    {

        [JsonProperty("bnplType")]
        public string BnplCategory { get; set; }

        [JsonProperty("label")]
        public Dictionary<CultureInfo, string> Label { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }


        public BnplType AddLabel(CultureInfo locale, string label)
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
            return string.Format(
                   "BnplType [BnplType={0}, Label={1}, Image={2}]",
                   BnplCategory, Label, Image
                   );
        }



    }
}
