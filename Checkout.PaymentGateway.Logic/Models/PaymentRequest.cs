﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Models
{
    public class PaymentRequest
    {
        public string IdempotencyKey { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public PaymentRecipient Recipient { get; set;}
    }
}