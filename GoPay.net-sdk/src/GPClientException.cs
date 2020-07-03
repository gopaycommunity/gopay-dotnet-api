using GoPay.Model;
using System;

namespace GoPay
{
    public class GPClientException : Exception
    {

        public GPClientException() : base() { }

        public GPClientException(string message) : base(message) { }

        public GPClientException(APIError error) : base()
        {
            Error = error;
        }

        public APIError Error { get; set; }
    }
}
