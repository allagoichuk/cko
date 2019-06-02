using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payments.Processor.Models
{
    public enum PaymentProcessingErrorCodes
    {
        none,
        no_credit,
        unathourized
    }
}
