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
            var payment = new Payment
            {
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency,
                Id = Guid.NewGuid(),
                RequestedOn = DateTime.Now,
                Status = PaymentStatus.Requested,
                CardNumber = paymentRequest.CardNumber,
                Cvv = paymentRequest.Cvv,
                ExpiryDate = paymentRequest.ExpiryDate,
            };

            await _paymentRepository.AddOrUpdate(payment);

            var response = await _bankClient.InitiatePayment(payment);

            payment.Status = response.PaymentStatus;

            await _paymentRepository.AddOrUpdate(payment);

            return payment;
        }

        public Task<Payment> GetPayment(Guid id)
        {
            return _paymentRepository.Get(id);
        }

    }
}
