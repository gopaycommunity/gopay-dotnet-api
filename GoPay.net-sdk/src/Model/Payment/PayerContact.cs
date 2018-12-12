using System;
using Newtonsoft.Json;
using GoPay.Common;
using Newtonsoft.Json.Converters;

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
        [JsonConverter(typeof(StringEnumConverter))]
        public Nullable<Country> CountryCode { get; set; }

        public override string ToString()
        {
            if (CountryCode != null)
            {
                return string.Format(
                    "PayerContact [firstName={0}, lastName={1}, email={2}, phoneNumber={3}, city={4}, street={5}, postalCode={6}, countryCode={7}]",
                    FirstName, LastName, Email, PhoneNumber, City, Street, PostalCode, Enum.GetName(typeof(Country), CountryCode));
            }
            else
            {
                return string.Format(
                    "PayerContact [firstName={0}, lastName={1}, email={2}, phoneNumber={3}, city={4}, street={5}, postalCode={6}, countryCode={7}]",
                    FirstName, LastName, Email, PhoneNumber, City, Street, PostalCode, CountryCode);
            }

            
        }

    }
}
