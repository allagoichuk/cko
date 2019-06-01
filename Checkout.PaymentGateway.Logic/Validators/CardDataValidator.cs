using Checkout.PaymentGateway.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Checkout.PaymentGateway.Logic.Validators
{
    public class CardDataValidator : ICardDataValidator
    {
        public DataValidationResult Validate(string cardNumber, string expiryDate, string cvv)
        {
            var cardRegex = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");

            if (string.IsNullOrWhiteSpace(cardNumber) || !cardRegex.IsMatch(cardNumber))
            {
                return new DataValidationResult(ValidationErrors.incorrect_card_number_format, "card number format is incorrect");
            }


            var cvvRegex = new Regex(@"\d{3}");

            if (string.IsNullOrWhiteSpace(cvv) || !cvvRegex.IsMatch(cvv))
            {
                return new DataValidationResult(ValidationErrors.incorrect_cvv_format, "cvv format is incorrect");
            }


            var expiryDateRegex = new Regex(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$");

            if (string.IsNullOrWhiteSpace(expiryDate) || !expiryDateRegex.IsMatch(expiryDate))
            {
                return new DataValidationResult(ValidationErrors.incorrect_expiry_date_format, "expiry date format is incorrect");
            }


            return new DataValidationResult(ValidationErrors.none);
        }
    }
}
