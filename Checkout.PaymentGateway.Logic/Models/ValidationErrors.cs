using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Models
{
    public enum ValidationErrors
    {
        none,
        empty_request,
        incorrect_card_number_format,
        incorrect_cvv_format,
        incorrect_expiry_date_format,
        incorrect_currency_format,
        empty_currency,
        negative_or_zero__amount,
    }
}
