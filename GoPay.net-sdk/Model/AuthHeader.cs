using System;

namespace GoPay.Model
{
    public class AuthHeader
    {

        public string Auhorization { get; set; }

        public static AuthHeader Build(string clientId, string clientSecret)
        {
            string toEncode = clientId + ":" + clientSecret;
            byte[] bytes = new byte[toEncode.Length * sizeof(char)];
            Buffer.BlockCopy(toEncode.ToCharArray(), 0, bytes, 0, bytes.Length);
            var base64 = "Basic " + Convert.ToBase64String(bytes);
            AuthHeader result = new AuthHeader()
            {
                Auhorization = base64
            };
            return result;

        }

        public static AuthHeader Build(string accessToken)
        {
            AuthHeader result = new AuthHeader()
            {
                Auhorization = "Bearer " + accessToken
            };

            return result;
        }

    }

}
