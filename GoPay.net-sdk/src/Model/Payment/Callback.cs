using Newtonsoft.Json;

namespace GoPay.Model.Payments
{
    public class Callback
    {

        [JsonProperty("return_url")]
        public string ReturnUrl { get; set; }

        [JsonProperty("notification_url")]
        public string NotificationUrl { get; set; }

        public override string ToString()
        {
            return string.Format("Callback [ returnUrl={0}, notificationUrl={1}]", ReturnUrl, NotificationUrl);
        }
    }
}
