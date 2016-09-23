using GoPay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPay
{
    public class GPClientException : Exception
    {
        public APIError Error { get; set; }
    }
}
