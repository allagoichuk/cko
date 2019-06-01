using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Checkout.PaymentGateway.Logic.Dal;
using Checkout.PaymentGateway.Logic.Models;
using Checkout.PaymentGateway.Logic.Utils;
using Checkout.PaymentGateway.Logic.Validators;

namespace Checkout.PaymentGateway.Logic.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICardNumberGuard _cardNumberGuard;

        public PaymentManager(IPaymentRepository paymentRepository, ICardNumberGuard cardNumberGuard)
        {
            _paymentRepository = paymentRepository;
            _cardNumberGuard = cardNumberGuard;
        }

        public Task<Payment> AddPayment(PaymentRequest paymentRequest)
        {
            return Task.FromResult<Payment>(new Payment
            {
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency,
                Id = Guid.NewGuid(),
                RequestedOn = DateTime.Now,
                Status = PaymentStatus.Authorized,
                CardNumber = paymentRequest.CardNumber,
                Cvv = paymentRequest.Cvv,
                ExpiryMonthDate = paymentRequest.ExpiryMonthDate,
                MaskedCardNumber = _cardNumberGuard.MaskCardNumner(paymentRequest.CardNumber)
            });
        }
    }
}
