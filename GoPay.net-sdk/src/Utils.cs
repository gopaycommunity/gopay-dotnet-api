using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace gopay_dotnet_standard_api.src
{
    public class Utils
    {
        public static string GetDefaultUserAgent()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            if (version == null)
            {
                return "GoPay .NET 0.0.0";
            }

            return "GoPay .NET " + version;
        }
    }
}
