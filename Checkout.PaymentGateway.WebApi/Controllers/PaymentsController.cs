using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.PaymentGateway.Logic.Managers;
using Checkout.PaymentGateway.Logic.Models;
using Checkout.PaymentGateway.WebApi.Models;
using Checkout.PaymentGateway.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Checkout.PaymentGateway.Logic.Validators;

namespace Checkout.PaymentGateway.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        public const string IDEMPOTENCY_HEADER = "Idempotency-Key";

        private readonly IPaymentManager _paymentManager;
        private readonly IPaymentMapper _paymentMapper;
        private readonly IPaymentRequestValidator _paymentRequestValidator;
        private readonly IErrorToApiErrorCodeMapper _errorToApiErrorCodeMapper;


        public PaymentsController(IPaymentManager paymentManager, IPaymentMapper paymentMapper, IErrorToApiErrorCodeMapper errorToApiErrorCodeMapper, IPaymentRequestValidator paymentRequestValidator)
        {
            _paymentManager = paymentManager;
            _paymentMapper = paymentMapper;
            _errorToApiErrorCodeMapper = errorToApiErrorCodeMapper;
            _paymentRequestValidator = paymentRequestValidator;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreatePaymentApiModel createPaymentApiModel)
        {
            var paymentRequest = _paymentMapper.Map(createPaymentApiModel);

            var validationResult = _paymentRequestValidator.Validate(paymentRequest);

            if (validationResult.Error != ValidationErrors.none)
            {
                return BadRequest(new ErrorResponse {
                    ErrorCode = _errorToApiErrorCodeMapper.Map(validationResult.Error),
                    ErrorMessage = validationResult.ErrorMessage});
            }

            var payment = await _paymentManager.AddPayment(paymentRequest);

            return Ok(_paymentMapper.Map(payment));
        }

        [HttpGet("{id}")]
        public void Get(int id)
        {
        }

    }
}
