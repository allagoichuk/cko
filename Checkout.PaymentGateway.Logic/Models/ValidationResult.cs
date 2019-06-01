using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Logic.Models
{
    public class ValidationResult
    {
        private ValidationErrors _error;
        private string _errorMessage;

        public ValidationResult(ValidationErrors error, string errorMessage = null)
        {
            _error = error;
            _errorMessage = errorMessage;
        }

        public ValidationErrors Error { get { return _error; } } 
        public string ErrorMessage { get { return _errorMessage; } }
    }
}
