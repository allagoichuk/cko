using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Utils
{
    public interface ICardNumberGuard
    {
        string MaskCardNumner(string str);
    }
}
