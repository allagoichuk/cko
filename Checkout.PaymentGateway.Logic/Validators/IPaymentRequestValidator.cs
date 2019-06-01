﻿using Checkout.PaymentGateway.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Validators
{
    public interface IPaymentRequestValidator
    {
        DataValidationResult Validate(PaymentRequest paymentRequest);
    }
}
