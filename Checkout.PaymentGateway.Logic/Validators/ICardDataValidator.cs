using Checkout.Payments.Processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payments.Processor.Validators
{
    public interface ICardDataValidator
    {
        DataValidationResult Validate(string cardNumber, string expiryDate, string cvv);
    }
}
