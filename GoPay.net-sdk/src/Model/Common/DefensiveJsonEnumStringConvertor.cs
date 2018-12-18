using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Reflection;

namespace GoPay.Common
{
    public class SafeJsonEnumStringConvertor : StringEnumConverter
    {
        private readonly int unknownValue;

        public SafeJsonEnumStringConvertor(int u)
        {
            unknownValue = u;
        }

        public override bool CanConvert(Type objectType)
        {
            return base.CanConvert(objectType) && Enum.IsDefined(objectType, unknownValue) && objectType.GetTypeInfo().IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch
            {
                return Enum.Parse(objectType, $"{unknownValue}");
            }
        }
    }
}
