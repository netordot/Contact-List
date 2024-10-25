using ContactList.Application.Validation;
using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact.CreateContact
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(Constants.MAX_SHORT_TEXT_SIZE);
            RuleFor(c => c.Email).MustBeValueObject(Email.Create);
            RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
            RuleFor(c => c.Description).MaximumLength(Constants.MAX_LONG_TEXT_SIZE);
        }
    }
}
