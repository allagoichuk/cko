using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Checkout.PaymentGateway.Logic.Dal;
using Checkout.PaymentGateway.Logic.Models;
using Checkout.PaymentGateway.Logic.Services;
using Checkout.PaymentGateway.Logic.Utils;
using Checkout.PaymentGateway.Logic.Validators;

namespace Checkout.PaymentGateway.Logic.Managers
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

        public async Task<Payment> AddPayment(PaymentRequest paymentRequest)
        {
            Payment payment = null;
            try
            {
                payment = new Payment
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

        public Task<Payment> GetPayment(Guid id)
        {
            return _paymentRepository.Get(id);
        }

    }
}
