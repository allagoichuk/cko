using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.PaymentGateway.Logic.Models;
using Checkout.PaymentGateway.Logic.Validators;
using Checkout.PaymentGateway.WebApi.Models;

namespace Checkout.PaymentGateway.WebApi.Mappers
{
    public class ErrorToApiErrorCodeMapper : IErrorToApiErrorCodeMapper
    {
        public ApiErrorCodes Map(ValidationErrors validationError)
        {
            switch (validationError)
            {
                case ValidationErrors.empty_currency: return ApiErrorCodes.empty_currency;
                case ValidationErrors.incorrect_currency_format: return ApiErrorCodes.incorrect_currency_format;
                case ValidationErrors.negative_or_zero__amount: return ApiErrorCodes.negative_or_zero__amount;
                case ValidationErrors.empty_request: return ApiErrorCodes.empty_request;
                case ValidationErrors.incorrect_card_number_format:
                case ValidationErrors.incorrect_cvv_format:
                case ValidationErrors.incorrect_expiry_date_format:return ApiErrorCodes.incorrect_card_data_format;
            }

            return ApiErrorCodes.unspecified;
        }
    }
}
