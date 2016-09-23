using RestSharp;
using RestSharp.Authenticators;

namespace GoPay
{
    internal class GPAuthenticator : HttpBasicAuthenticator
    {
        
        public GPAuthenticator(string UserId, string UserSecret) : base(UserId,UserSecret) {}

        public new void Authenticate(IRestClient client, IRestRequest request)
        {
            base.Authenticate(client, request);
        }
    }
}
