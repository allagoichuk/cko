using Checkout.PaymentGateway.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Services
{
    public class PaymentProcessingResults
    {
        public PaymentStatus PaymentStatus { get; set; }

        public PaymentProcessingErrorCodes Error { get; set; }

        public string BankIdentifier { get; set; }
    }
}
