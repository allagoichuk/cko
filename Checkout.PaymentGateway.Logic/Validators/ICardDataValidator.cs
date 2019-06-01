using Checkout.PaymentGateway.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Validators
{
    public interface ICardDataValidator
    {
        DataValidationResult Validate(string cardNumber, string expiryDate, string cvv);
    }
}
