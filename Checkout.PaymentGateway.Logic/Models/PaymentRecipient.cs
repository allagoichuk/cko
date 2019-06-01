using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Models
{
    public class PaymentRecipient
    {
        public string AccountNumber { get; set; }
        public string LastName { get; set; }
        public string Zip { get; set; }
    }
}
