using Checkout.PaymentGateway.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Validators
{
    public interface ICardNumberValidator
    {
        DataValidationResult Validate(string cardNumber);
    }
}
