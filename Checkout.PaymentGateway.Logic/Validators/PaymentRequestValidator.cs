using System;
using System.Collections.Generic;
using System.Text;
using Checkout.PaymentGateway.Logic.Models;

namespace Checkout.PaymentGateway.Logic.Validators
{
    public class PaymentRequestValidator : IPaymentRequestValidator
    {
        IIsoCurrencyValidator _currencyValidator;
        public PaymentRequestValidator(IIsoCurrencyValidator currencyValidator)
        {
            _currencyValidator = currencyValidator;
        }

        public ValidationResult Validate(PaymentRequest paymentRequest)
        {
            if (paymentRequest == null)
            {
                return new ValidationResult(ValidationErrors.empty_request, "No data was sent");
            }

            if (paymentRequest.Amount <= 0)
            {
                return new ValidationResult(ValidationErrors.negative_or_zero__amount, "Amount should be more then 0");
            }

            var currencyValidationResult = _currencyValidator.Validate(paymentRequest.Currency);

            if (currencyValidationResult.Error != ValidationErrors.none)
            {
                return currencyValidationResult;
            }

            return new ValidationResult(ValidationErrors.none);
        }
    }
}
