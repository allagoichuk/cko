using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payments.Processor.Models
{
    public enum StoreNewPaymentResult
    {
        Success,
        DuplicateUniqueIdempotencyId
    }
}
