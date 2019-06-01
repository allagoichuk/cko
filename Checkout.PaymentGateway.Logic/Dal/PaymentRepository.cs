using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Checkout.PaymentGateway.Logic.Models;
using System.Collections.Concurrent;

namespace Checkout.PaymentGateway.Logic.Dal
{
    public class PaymentRepository : IPaymentRepository
    {
        private ConcurrentDictionary<Guid, Payment> _storage = new ConcurrentDictionary<Guid, Payment>(); 

        public Task AddOrUpdate(Payment payment)
        {
            return Task.FromResult(_storage.AddOrUpdate(payment.Id, payment, (k, v) => payment));
        }

        public Task<Payment> Get(Guid id)
        {
            Payment payment = null;
            _storage.TryGetValue(id, out payment);

            return Task.FromResult(payment);
        }
    }
}
