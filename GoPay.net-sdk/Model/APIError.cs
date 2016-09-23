using System;
using System.Collections.Generic;
using GoPay.Common;
using Newtonsoft.Json;

namespace GoPay.Model
{
    public class APIError
    {
        [JsonProperty("date_issued")]
        [JsonConverter(typeof(GPDateAdapter))]
        public DateTime DateIssued { get; set; }
        
        [JsonProperty("errors")]
        public IList<ErrorElement> ErrorMessages { get; set; }

        
    }
}
