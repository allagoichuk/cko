using System;
using System.Collections.Generic;
using System.Text;
using Checkout.Payments.Processor.Models;

namespace Checkout.Payments.Processor.Validators
{
    public class PaymentRequestValidator : IPaymentRequestValidator
    {
        IIsoCurrencyValidator _currencyValidator;
        ICardDataValidator _cardNumberValidator;
        public PaymentRequestValidator(IIsoCurrencyValidator currencyValidator, ICardDataValidator cardNumberValidator)
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
                return new DataValidationResult(ValidationErrors.negative_or_zero__amount, "Amount should be greater than 0");
            }

            var cardNumberValidationResults = _cardNumberValidator.Validate(paymentRequest.CardNumber, paymentRequest.ExpiryDate, paymentRequest.Cvv);
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
