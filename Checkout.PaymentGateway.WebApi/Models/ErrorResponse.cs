using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.WebApi.Models
{
    public class ErrorResponse
    {
        public ApiErrorCodes ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
