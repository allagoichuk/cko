using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Models
{
    public enum PaymentProcessingErrorCodes
    {
        none,
        no_credit,
        unathourized
    }
}
