using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.WebApi.Models
{
    public class CreatePaymentApiModel
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        public string CardNumber { get; set; }

        public string ExpiryMonthDate { get; set; }

        public string Cvv { get; set; }
    }
}
