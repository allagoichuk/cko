using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.WebApi.Models
{
    public class CreatePaymentApiModel
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public string RecipientLastName { get; set; }

        public string RecipientZip { get; set; }

        public string RecipientAccountNumber { get; set; }
    }
}
