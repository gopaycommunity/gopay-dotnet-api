using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPay.Model
{
    public class OAuth
    {
        public const string GRANT_TYPE_CLIENT_CREDENTIALS = "client_credentials";
        public const string SCOPE_PAYMENT_ALL = "payment-all";
        public const string SCOPE_PAYMENT_CREATE = "payment-create";
        public const string TOKEN_TYPE_BEARER = "bearer";
    }
}
