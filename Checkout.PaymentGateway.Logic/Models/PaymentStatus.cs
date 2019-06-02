using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payments.Processor.Models
{
    public enum PaymentStatus
    {
        Requested,
        Pending,
        Authorized,
        CardVerified,
        Voided,
        Refunded,
        Declined,
        Cancelled
    }
}
