using Newtonsoft.Json.Converters;

namespace GoPay.Common
{
    public class GPDateAdapter : IsoDateTimeConverter
    {
        public GPDateAdapter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
