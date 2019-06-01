using Checkout.PaymentGateway.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Logic.Dal
{
    public interface IPaymentRepository
    {
        Task AddOrUpdate(Payment payment);
        Task<Payment> Get(Guid id);
    }
}
