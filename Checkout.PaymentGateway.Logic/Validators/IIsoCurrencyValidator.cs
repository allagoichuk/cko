using Checkout.PaymentGateway.Logic.Models;

namespace Checkout.PaymentGateway.Logic.Validators
{
    public interface IIsoCurrencyValidator
    {
        ValidationResult Validate(string isoCode);
    }
}
