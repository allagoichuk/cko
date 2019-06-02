using Checkout.Payments.Processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payments.Processor.Services
{
    public class PaymentProcessingResults
    {
        public PaymentStatus PaymentStatus { get; set; }

        public PaymentProcessingErrorCodes Error { get; set; }

        public string BankIdentifier { get; set; }
    }
}
