using Checkout.Payments.Processor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Payments.Processor.Dal
{
    public interface IPaymentRepository
    {
        Task<StoreNewPaymentResult> Add(Models.Payment payment);
        Task<bool> Update(Models.Payment payment);
        Task<Models.Payment> Get(Guid id);
        Task<Models.Payment> GetByIdempotencyKey(string idempotemncyKey);
    }
}
