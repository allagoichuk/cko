using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Models
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
