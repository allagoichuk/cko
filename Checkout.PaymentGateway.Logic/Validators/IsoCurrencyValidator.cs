using Checkout.PaymentGateway.Logic.Models;

namespace Checkout.PaymentGateway.Logic.Validators
{
    public class IsoCurrencyValidator : IIsoCurrencyValidator
    {
        public ValidationResult Validate(string isoCode)
        {
            if (string.IsNullOrWhiteSpace(isoCode) || isoCode.Length != 3)
                return new ValidationResult(ValidationErrors.incorrect_currency_format, "Currency name is not ISO format");

            return new ValidationResult(ValidationErrors.none);
        }
    }
}
