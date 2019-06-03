using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Payments.Processor.Managers;
using Checkout.Payments.Processor.Models;
using Checkout.Payments.Api.Models;
using Checkout.Payments.Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Checkout.Payments.Processor.Validators;
using System.Net;
namespace Checkout.Payments.Api.Controllers
{
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
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
        [ProducesResponseType(typeof(PaymentApiModel), 200)]
        [ProducesResponseType(400)]
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
        [ProducesResponseType(typeof(PaymentApiModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid id)
        {
            var payment = await _paymentManager.GetPayment(id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(_paymentMapper.Map(payment));
        }

    }
}
