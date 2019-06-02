﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Payments.Api.Models
{
    public enum ApiErrorCodes
    {
        empty_request,
        incorrect_currency_format,
        incorrect_card_data_format,
        empty_currency,
        negative_or_zero__amount,
        unknown
    }
}
