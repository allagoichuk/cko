using Checkout.Payments.Processor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Payments.Processor.Managers
{
    public interface IPaymentManager
    {
        Task<Models.Payment> AddPayment(PaymentRequest paymentRequest);
        Task<Models.Payment> GetPayment(Guid id);
    }
}
