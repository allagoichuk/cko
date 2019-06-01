using Checkout.PaymentGateway.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Logic.Services
{
    public interface IBankClient
    {
        Task<PaymentResponse> InitiatePayment(Payment payment);
    }
}
