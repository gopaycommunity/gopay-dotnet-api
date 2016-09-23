
using Newtonsoft.Json;

namespace GoPay.Model.Payments
{
    public class PayerContact
    {

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        public override string ToString()
        {
            return string.Format(
                    "PayerContact [firstName={}, lastName={}, email={}, phoneNumber={}, city={}, street={}, postalCode={}, countryCode={}]",
                    FirstName, LastName, Email, PhoneNumber, City, Street, PostalCode, CountryCode);
        }
        
    }
}
