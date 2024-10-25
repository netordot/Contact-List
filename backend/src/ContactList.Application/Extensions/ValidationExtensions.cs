using ContactList.Domain.Contact.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Extensions
{
    public static class ValidationExtensions
    {
        public static ErrorList ToErrorList(this FluentValidation.Results.ValidationResult validationResult)
        {
            var validationErrors = validationResult.Errors;

            var errors = from validationError in validationErrors
                         let errorMessage = validationError.ErrorMessage
                         let error = Error.Deserialize(errorMessage)
                         select Error.Validation(error.Code, error.Message, validationError.PropertyName);

            return new ErrorList(errors);
        }

    }
}
