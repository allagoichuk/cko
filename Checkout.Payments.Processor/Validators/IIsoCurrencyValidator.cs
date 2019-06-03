using Checkout.Payments.Processor.Models;

namespace Checkout.Payments.Processor.Validators
{
    public interface IIsoCurrencyValidator
    {
        DataValidationResult Validate(string isoCode);
    }
}
