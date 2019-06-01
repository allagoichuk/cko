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

        private Dictionary<string, Guid> _idempotencyKeys = new Dictionary<string, Guid>();
        private object _lockObject = new object();

        public Task<bool> Add(Payment payment)
        {
            payment.Id = Guid.NewGuid();

            if (!string.IsNullOrEmpty(payment.IdempotencyUniqueId))
            {
                lock (_lockObject)
                {
                    if (_idempotencyKeys.ContainsKey(payment.IdempotencyUniqueId))
                        throw new DuplicatePaymentRequestException();

                    _idempotencyKeys.Add(payment.IdempotencyUniqueId, payment.Id.Value);
                }
            }

            return Task.FromResult(_storage.TryAdd(payment.Id.Value, payment));
        }

        public Task<bool> Update(Payment payment)
        {
            if (payment.Id.HasValue)
            {
                Task.FromResult(_storage.TryUpdate(payment.Id.Value, payment, payment));
            }
            
            return Task.FromResult(false);
        }

        public Task<Payment> Get(Guid id)
        {
            Payment payment = null;
            _storage.TryGetValue(id, out payment);

            return Task.FromResult(payment);
        }

        public Task<Payment> GetByIdempotencyKey(string idempotemncyKey)
        {
            Guid guid = Guid.Empty;
            lock (_lockObject)
            {
                if (_idempotencyKeys.ContainsKey(idempotemncyKey))
                {
                    guid = _idempotencyKeys[idempotemncyKey];
                }
                else
                {
                    return Task.FromResult<Payment>(null);
                }
            }

            return Get(guid);
        }
    }
}
