using ContactList.Application.Validation;
using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact.UpdateContact
{
    public class UpdateCommandCommandValidator : AbstractValidator<UpdateContactCommad>
    {
        public UpdateCommandCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(Constants.MAX_SHORT_TEXT_SIZE);
            RuleFor(c => c.Email).MustBeValueObject(Email.Create);
            RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
            RuleFor(c => c.Description).MaximumLength(Constants.MAX_LONG_TEXT_SIZE);
        }
    }
}
