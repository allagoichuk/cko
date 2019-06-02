using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Checkout.Payments.Processor.Dal;
using Checkout.Payments.Processor.Managers;
using Checkout.Payments.Api.Mappers;
using Checkout.Payments.Processor.Validators;
using Checkout.Payments.Processor.Utils;
using Checkout.Payments.Processor.Services;
using Checkout.Payments.Api.Middleware;
using Newtonsoft.Json;

namespace Checkout.Payments.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Payments API", Version = "v1" });
            });

            services.AddScoped<IPaymentMapper, PaymentMapper>();
            services.AddScoped<IErrorToApiErrorCodeMapper, ErrorToApiErrorCodeMapper>();

            services.AddScoped<IIsoCurrencyValidator, IsoCurrencyValidator>();
            services.AddScoped<IPaymentRequestValidator, PaymentRequestValidator>();
            services.AddScoped<IPaymentManager, PaymentManager>();
            services.AddScoped<ICardDataValidator, CardDataValidator>();
            services.AddScoped<ICardNumberGuard, CardNumberGuard>();
            services.AddScoped<IBankClient, BankClient>();

            //as it is in-memory storage right now it should be singleton, one DB is plugged, it should Scoped
            services.AddSingleton<IPaymentRepository, PaymentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payments V1");
            });

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMvc();
        }
    }
}
