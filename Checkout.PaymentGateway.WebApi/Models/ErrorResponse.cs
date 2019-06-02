using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Payments.Api.Models
{
    public class ErrorResponse
    {
        public ApiErrorCodes ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
