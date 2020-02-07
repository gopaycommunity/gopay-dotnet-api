using GoPay.Model;
using System;

namespace GoPay
{
    public class GPClientException : Exception
    {

        public GPClientException() : base() { }

        public GPClientException(string message) : base(message) {}

        public APIError Error { get; set; }
    }
}
