using Checkout.PaymentGateway.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Checkout.PaymentGateway.Logic.Validators
{
    public class CardNumberValidator : ICardNumberValidator
    {
        public DataValidationResult Validate(string cardNumber)
        {
            var incorrectCardNumber = "card number format is incorrect";
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                return new DataValidationResult(ValidationErrors.incorrect_card_number_format, incorrectCardNumber);
            }

            var regex = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");

            var matches = regex.Matches(cardNumber);

            if (matches.Count == 0)
            {
                return new DataValidationResult(ValidationErrors.incorrect_card_number_format, incorrectCardNumber);
            }
            return new DataValidationResult(ValidationErrors.none);
        }
    }
}
