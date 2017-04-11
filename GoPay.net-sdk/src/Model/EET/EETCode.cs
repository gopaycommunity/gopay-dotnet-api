using System;
using Newtonsoft.Json;

namespace GoPay.EETProp
{
    public class EETCode
    {

        [JsonProperty("fik")]
        public string Fik { get; set; }

        [JsonProperty("bkp")]
        public string Bkp { get; set; }

        [JsonProperty("pkp")]
        public string Pkp { get; set; }

        public override string ToString()
        {
            return string.Format(
                   "EETCode [fik={0}, bkp={1}, pkp={2}]",
                   Fik, Bkp, Pkp
                   );
        }

    }
}
