using GoPay.Model;
using System;

namespace GoPay
{
    public class GPClientException : Exception
    {
        public APIError Error { get; set; }
    }
}
