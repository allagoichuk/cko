using System;
using System.Collections.Generic;
using System.Text;
using Checkout.PaymentGateway.Logic.Models;

namespace Checkout.PaymentGateway.Logic.Validators
{
    public class PaymentRequestValidator : IPaymentRequestValidator
    {
        IIsoCurrencyValidator _currencyValidator;
        ICardNumberValidator _cardNumberValidator;
        public PaymentRequestValidator(IIsoCurrencyValidator currencyValidator, ICardNumberValidator cardNumberValidator)
        {
            _currencyValidator = currencyValidator;
            _cardNumberValidator = cardNumberValidator;
        }

        public DataValidationResult Validate(PaymentRequest paymentRequest)
        {
            if (paymentRequest == null)
            {
                return new DataValidationResult(ValidationErrors.empty_request, "No data was sent");
            }

            if (paymentRequest.Amount <= 0)
            {
                return new DataValidationResult(ValidationErrors.negative_or_zero__amount, "Amount should be more then 0");
            }

            var cardNumberValidationResults = _cardNumberValidator.Validate(paymentRequest.CardNumber);
            if (cardNumberValidationResults.Error != ValidationErrors.none)
            {
                return cardNumberValidationResults;
            }

            var currencyValidationResult = _currencyValidator.Validate(paymentRequest.Currency);
            if (currencyValidationResult.Error != ValidationErrors.none)
            {
                return currencyValidationResult;
            }

            return new DataValidationResult(ValidationErrors.none);
        }
    }
}
