using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Checkout.PaymentGateway.Logic.Dal;
using Checkout.PaymentGateway.Logic.Models;
using Checkout.PaymentGateway.Logic.Validators;

namespace Checkout.PaymentGateway.Logic.Managers
{
    public class PaymentManager : IPaymentManager
    {
        readonly IPaymentRepository _paymentRepository;

        public PaymentManager(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public Task<Payment> AddPayment(PaymentRequest paymentRequest)
        {
            return Task.FromResult<Payment>(new Payment
            {
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency,
                Description = paymentRequest.Description,
                Id = Guid.NewGuid(),
                RequestedOn = DateTime.Now,
                Status = PaymentStatus.Authorized
            });
        }
    }
}
