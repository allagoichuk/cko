using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Checkout.Payments.Processor.Models;

namespace Checkout.Payments.Processor.Services
{
    public class MockBankClient : IBankClient
    {
        private Dictionary<string, string> _mockedCards;

        public MockBankClient(Dictionary<string, string> mockedCards)
        {
            _mockedCards = mockedCards;
        }

        public Task<PaymentProcessingResults> InitiatePayment(Models.Payment payment)
        {
            var status = PaymentStatus.Declined;

            if (_mockedCards != null)
            {
                foreach (var card in _mockedCards)
                {
                    if (card.Value == payment.CardNumber)
                    {
                        var statusName = card.Key.Replace("BankProvider:MockCardNumber", "");
                        Enum.TryParse(statusName, out status);

                        break;
                    }
                }
            }

            return Task.FromResult(new PaymentProcessingResults
            {
                PaymentStatus = status,
                Error = PaymentProcessingErrorCodes.none,
                BankIdentifier = Guid.NewGuid().ToString()
            });
        }
    }
}
