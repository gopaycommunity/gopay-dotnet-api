using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoPay.Model.Payments
{
    public class PreAuthorization
    {
        public enum PreAuthState
        {
            REQUESTED,
            AUTHORIZED,
            CAPTURED,
            CANCELED
        }

        [JsonProperty("requested")]
        public bool Requested { get; set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PreAuthState State { get; set; }

        public override string ToString()
        {
            return string.Format("Preauthorization [requested={}, preAuthState={}]", Requested, Enum.GetName(typeof(PreAuthState), State));
        }
   
    }
}
