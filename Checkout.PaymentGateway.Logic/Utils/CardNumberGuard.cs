using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Checkout.PaymentGateway.Logic.Utils
{
    public class CardNumberGuard : ICardNumberGuard
    {
        public string MaskCardNumner(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return Regex.Replace(str, "[0-9](?=[0-9]{4})", "*");
        }
    }
}
