using Checkout.PaymentGateway.Logic.Models;

namespace Checkout.PaymentGateway.Logic.Validators
{
    public interface IIsoCurrencyValidator
    {
        DataValidationResult Validate(string isoCode);
    }
}
