﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Payments.Processor.Models;
using Checkout.Payments.Processor.Utils;
using Checkout.Payments.Api.Models;

namespace Checkout.Payments.Api.Mappers
{
    public class PaymentMapper : IPaymentMapper
    {
        private readonly ICardNumberGuard _cardNumberGuard;

        public PaymentMapper(ICardNumberGuard cardNumberGuard)
        {
            _cardNumberGuard = cardNumberGuard;
        }

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
                CardNumber = createPaymentApiModel.CardNumber,
                Cvv = createPaymentApiModel.Cvv,
                ExpiryDate = createPaymentApiModel.ExpiryDate,
                IdempotencyKey = createPaymentApiModel.IdempotencyKey
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
                Id = payment.Id.Value,
                RequestedOn = payment.RequestedOn,
                Status = payment.Status,
                CardNumber = _cardNumberGuard.MaskCardNumner(payment.CardNumber),
                ExpiryDate = payment.ExpiryDate
            };
        }
    }
}
