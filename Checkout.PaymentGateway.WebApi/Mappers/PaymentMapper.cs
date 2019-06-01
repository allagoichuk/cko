using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.PaymentGateway.Logic.Models;
using Checkout.PaymentGateway.WebApi.Models;

namespace Checkout.PaymentGateway.WebApi.Mappers
{
    public class PaymentMapper : IPaymentMapper
    {
        public PaymentRequest Map(CreatePaymentApiModel createPaymentApiModel)
        {
            if (createPaymentApiModel == null)
            {
                return null;
            }

            return new PaymentRequest
            {
                Amount = createPaymentApiModel.Amount,
                Currency = createPaymentApiModel.Currency,
                Description = createPaymentApiModel.Description,
                Recipient = new PaymentRecipient
                {
                    AccountNumber = createPaymentApiModel.RecipientAccountNumber,
                    LastName = createPaymentApiModel.RecipientLastName,
                    Zip = createPaymentApiModel.RecipientZip
                }
            };
        }

        public PaymentApiModel Map(Payment payment)
        {
            if (payment == null)
            {
                return null;
            }

            return new PaymentApiModel
            {
                Amount = payment.Amount,
                Currency = payment.Currency,
                Description = payment.Description,
                Id = payment.Id,
                RequestedOn = payment.RequestedOn,
                Status = payment.Status
            };
        }
    }
}
