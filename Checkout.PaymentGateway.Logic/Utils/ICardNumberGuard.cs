using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payments.Processor.Utils
{
    public interface ICardNumberGuard
    {
        string MaskCardNumner(string str);
    }
}
