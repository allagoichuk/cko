using Checkout.Payments.Processor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Payments.Api.Models
{
    public class PaymentApiModel
    {
        public Guid Id { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime RequestedOn { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
    }
}
