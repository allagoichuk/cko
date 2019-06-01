using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Models
{
    public class PaymentResponse
    {
        public PaymentStatus PaymentStatus { get; set; }

        public PaymentProcessingErrorCodes Error { get; set; }
    }
}
