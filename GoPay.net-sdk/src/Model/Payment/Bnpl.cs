using GoPay.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace GoPay.Model.Payments
{
    public class Bnpl
    {

        [JsonProperty("bnplType")]
        [JsonConverter(typeof(SafeJsonEnumStringConvertor), (int) BnplType.UNKNOWN)]
        public BnplType bnplType { get; set; }

        [JsonProperty("label")]
        public Dictionary<CultureInfo, string> Label { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }


        public Bnpl AddLabel(CultureInfo locale, string label)
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
            Console.WriteLine("KONZOLE");
            Console.WriteLine(bnplType);
            return string.Format(
                   "BnplType [BnplType={0}, Label={1}, Image={2}]",
                   bnplType, Label, Image
                   );
        }



    }
}
