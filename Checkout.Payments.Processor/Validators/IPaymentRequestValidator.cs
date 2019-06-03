using Checkout.Payments.Processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payments.Processor.Validators
{
    public interface IPaymentRequestValidator
    {
        DataValidationResult Validate(PaymentRequest paymentRequest);
    }
}
