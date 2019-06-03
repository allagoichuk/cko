using Checkout.Payments.Processor.Models;

namespace Checkout.Payments.Processor.Validators
{
    public class IsoCurrencyValidator : IIsoCurrencyValidator
    {
        public DataValidationResult Validate(string isoCode)
        {
            if (string.IsNullOrWhiteSpace(isoCode) || isoCode.Length != 3)
                return new DataValidationResult(ValidationErrors.incorrect_currency_format, "Currency name is not ISO format");

            return new DataValidationResult(ValidationErrors.none);
        }
    }
}
