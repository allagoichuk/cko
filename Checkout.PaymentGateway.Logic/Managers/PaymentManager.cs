using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Checkout.Payments.Processor.Dal;
using Checkout.Payments.Processor.Models;
using Checkout.Payments.Processor.Services;
using Checkout.Payments.Processor.Utils;
using Checkout.Payments.Processor.Validators;

namespace Checkout.Payments.Processor.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBankClient _bankClient;

        public PaymentManager(IPaymentRepository paymentRepository, IBankClient bankClient)
        {
            _paymentRepository = paymentRepository;
            _bankClient = bankClient;
        }

        public async Task<Models.Payment> AddPayment(PaymentRequest paymentRequest)
        {
            Models.Payment payment = null;
            try
            {
                payment = new Models.Payment
                {
                    Amount = paymentRequest.Amount,
                    Currency = paymentRequest.Currency,
                    RequestedOn = DateTime.Now,
                    Status = PaymentStatus.Requested,
                    CardNumber = paymentRequest.CardNumber,
                    Cvv = paymentRequest.Cvv,
                    ExpiryDate = paymentRequest.ExpiryDate,
                    IdempotencyUniqueId = UniqueIdempotencyId(paymentRequest)
                };

                await _paymentRepository.Add(payment);

                var response = await _bankClient.InitiatePayment(payment);

                payment.Status = response.PaymentStatus;
                payment.BankIdentifier = response.BankIdentifier;

                await _paymentRepository.Update(payment);
            }
            catch(DuplicatePaymentRequestException)
            {
                return await _paymentRepository.GetByIdempotencyKey(payment.IdempotencyUniqueId);
            }

            return payment;
        }

        private string UniqueIdempotencyId(PaymentRequest paymentRequest)
        {
            if (paymentRequest.IdempotencyKey == null)
            {
                return null;
            }

            return $"{paymentRequest.IdempotencyKey}|{paymentRequest.CardNumber}|{paymentRequest.Currency}|{paymentRequest.Amount}|{paymentRequest.Cvv}|{paymentRequest.ExpiryDate}";
        }

        public Task<Models.Payment> GetPayment(Guid id)
        {
            return _paymentRepository.Get(id);
        }

    }
}
