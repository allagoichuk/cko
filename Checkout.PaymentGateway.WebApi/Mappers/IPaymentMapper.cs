using Checkout.PaymentGateway.Logic.Models;
using Checkout.PaymentGateway.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.WebApi.Mappers
{
    public interface IPaymentMapper
    {
        PaymentRequest Map(CreatePaymentApiModel createPaymentApiModel);

        PaymentApiModel Map(Payment payment);
    }
}
