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

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string ExpiryDate { get; set; }

        [Required]
        public string Cvv { get; set; }

        [Required]
        public string IdempotencyKey { get; set; }

    }
}
