using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Checkout.Payments.Api.Controllers;
using Checkout.Payments.Api.Models;
using Checkout.Payments.Api.Mappers;
using Checkout.Payments.Processor.Managers;
using Checkout.Payments.Processor.Validators;
using Checkout.Payments.Processor.Models;
using Checkout.Payments.Processor.Utils;

namespace Checkout.PaymentApi.tests
{
    public class PaymentsControllerTest
    {
        [Fact]
        public async Task Create_valid_returns_OkResponse()
        {
            var mockValidator = new Mock<IPaymentRequestValidator>();
            mockValidator.Setup(a => a.Validate(It.IsAny<PaymentRequest>())).Returns(new DataValidationResult(ValidationErrors.none));

            var mockPaymentManager = new Mock<IPaymentManager>();
            mockPaymentManager.Setup(a => a.AddPayment(It.IsAny<PaymentRequest>())).Returns(Task.FromResult(new Payment() { Id = new Guid() }));

            var controller = new PaymentsController(mockPaymentManager.Object, new PaymentMapper(new CardNumberGuard()), new ErrorToApiErrorCodeMapper(), mockValidator.Object);

            var result = await controller.Create(new CreatePaymentApiModel { }) as OkObjectResult;
            Assert.NotNull(result);

            var response = result.Value as PaymentApiModel;
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Create_invalid_returns_ErrorResponse()
        {
            var mockValidator = new Mock<IPaymentRequestValidator>();
            mockValidator.Setup(a => a.Validate(It.IsAny<PaymentRequest>())).Returns(new DataValidationResult(ValidationErrors.empty_request, "No data was sent"));

            var controller = new PaymentsController(null, new PaymentMapper(new CardNumberGuard()), new ErrorToApiErrorCodeMapper(), mockValidator.Object);

            var result = await controller.Create(new CreatePaymentApiModel { }) as BadRequestObjectResult;
            Assert.NotNull(result);

            var errorResponse = result.Value as ErrorResponse;
            Assert.NotNull(errorResponse);
            Assert.Equal(ApiErrorCodes.empty_request, errorResponse.ErrorCode);
        }
    }
}
