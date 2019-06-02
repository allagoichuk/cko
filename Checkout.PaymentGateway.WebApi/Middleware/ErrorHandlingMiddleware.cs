using Checkout.PaymentGateway.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private IOptions<MvcJsonOptions> _optionsAccessor;
        public ErrorHandlingMiddleware(RequestDelegate next, IOptions<MvcJsonOptions> optionsAccessor)
        {
            this.next = next;
            _optionsAccessor = optionsAccessor;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new ErrorResponse
            {
                ErrorCode = ApiErrorCodes.unknown,
                ErrorMessage = "Current operation has resulted in error. For further information please log in to our portal",
            }, _optionsAccessor.Value.SerializerSettings);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
