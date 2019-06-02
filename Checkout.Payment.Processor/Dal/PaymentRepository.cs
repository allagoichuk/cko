using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Checkout.Payments.Processor.Models;
using System.Collections.Concurrent;

namespace Checkout.Payments.Processor.Dal
{
    public class PaymentRepository : IPaymentRepository
    {
        private ConcurrentDictionary<Guid, Models.Payment> _storage = new ConcurrentDictionary<Guid, Models.Payment>();

        private Dictionary<string, Guid> _idempotencyKeys = new Dictionary<string, Guid>();
        private object _lockObject = new object();

        public Task<StoreNewPaymentResult> Add(Models.Payment payment)
        {
            payment.Id = Guid.NewGuid();

            if (!string.IsNullOrEmpty(payment.IdempotencyUniqueId))
            {
                lock (_lockObject)
                {
                    if (_idempotencyKeys.ContainsKey(payment.IdempotencyUniqueId))
                        return Task.FromResult(StoreNewPaymentResult.DuplicateUniqueIdempotencyId);

                    _idempotencyKeys.Add(payment.IdempotencyUniqueId, payment.Id.Value);
                }
            }

            _storage.TryAdd(payment.Id.Value, payment);
            
            return Task.FromResult(StoreNewPaymentResult.Success);
        }

        public Task<bool> Update(Models.Payment payment)
        {
            if (payment.Id.HasValue)
            {
                Task.FromResult(_storage.TryUpdate(payment.Id.Value, payment, payment));
            }
            
            return Task.FromResult(false);
        }

        public Task<Models.Payment> Get(Guid id)
        {
            Models.Payment payment = null;
            _storage.TryGetValue(id, out payment);

            return Task.FromResult(payment);
        }

        public Task<Models.Payment> GetByIdempotencyKey(string idempotemncyKey)
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
