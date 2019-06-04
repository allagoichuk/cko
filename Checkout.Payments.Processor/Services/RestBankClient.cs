using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Checkout.Payments.Processor.Models;

namespace Checkout.Payments.Processor.Services
{
    public class RestBankClient : IBankClient
    {
        public Task<PaymentProcessingResults> InitiatePayment(Models.Payment payment)
        {
            throw new NotImplementedException();
        }

    }
}
