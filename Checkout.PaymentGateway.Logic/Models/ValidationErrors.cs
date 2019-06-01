using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Models
{
    public enum ValidationErrors
    {
        none,
        empty_request,
        incorrect_currency_format,
        empty_currency,
        negative_or_zero__amount
    }
}
