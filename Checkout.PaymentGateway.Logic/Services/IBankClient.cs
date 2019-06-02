using Checkout.Payments.Processor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Payments.Processor.Services
{
    public interface IBankClient
    {
        Task<PaymentProcessingResults> InitiatePayment(Models.Payment payment);
    }
}
