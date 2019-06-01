﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Checkout.PaymentGateway.Logic.Models;

namespace Checkout.PaymentGateway.Logic.Services
{
    public class BankClient : IBankClient
    {
        private const string FAILED_CARD_NUMBER = "4111111111111111";

        public Task<PaymentResponse> InitiatePayment(Payment payment)
        {
            if (payment.CardNumber == FAILED_CARD_NUMBER)
            {
                return Task.FromResult(new PaymentResponse
                {
                    PaymentStatus = PaymentStatus.Declined,
                    Error = PaymentProcessingErrorCodes.no_credit
                });
            }

            return Task.FromResult(new PaymentResponse
            {
                PaymentStatus = PaymentStatus.Authorized,
                Error = PaymentProcessingErrorCodes.none,
            });
        }
    }
}
