using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Models
{
    public class Payment
    {
        public Guid? Id { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime RequestedOn { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
        public string IdempotencyUniqueId { get; set; }
        public string BankIdentifier { get; set; }
    }
}
