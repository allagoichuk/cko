using Checkout.Payments.Processor.Models;
using Checkout.Payments.Processor.Validators;
using Checkout.Payments.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Payments.Api.Mappers
{
    public interface IErrorToApiErrorCodeMapper
    {
        ApiErrorCodes Map(ValidationErrors validationError);
    }
}
