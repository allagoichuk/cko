using Checkout.PaymentGateway.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Logic.Managers
{
    public interface IPaymentManager
    {
        Task<Payment> AddPayment(PaymentRequest paymentRequest);
    }
}
