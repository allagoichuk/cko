using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Checkout.Payments.Processor.Models;

namespace Checkout.Payments.Processor.Services
{
    public class MockBankClient : IBankClient
    {
        private const string FAILED_CARD_NUMBER = "4111111111111111";

        public Task<PaymentProcessingResults> InitiatePayment(Models.Payment payment)
        {
            if (payment.CardNumber == FAILED_CARD_NUMBER)
            {
                return Task.FromResult(new PaymentProcessingResults
                {
                    PaymentStatus = PaymentStatus.Declined,
                    Error = PaymentProcessingErrorCodes.no_credit,
                    BankIdentifier = Guid.NewGuid().ToString()
                });
            }

            return Task.FromResult(new PaymentProcessingResults
            {
                PaymentStatus = PaymentStatus.Authorized,
                Error = PaymentProcessingErrorCodes.none,
                BankIdentifier = Guid.NewGuid().ToString()
            });
        }
    }
}
