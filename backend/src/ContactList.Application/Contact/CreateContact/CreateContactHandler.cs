using ContactList.Application.Extensions;
using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact.CreateContact
{
    public class CreateContactHandler : ICreateContactHandler
    {
        private readonly IContactRepository _repository;
        private readonly IValidator<CreateContactCommand> _validator;

        public CreateContactHandler(IContactRepository repository, IValidator<CreateContactCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(CreateContactCommand command, CancellationToken cancellation)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellation);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }
            var contactId = ContactId.NewContactId;
            var emailResult = Email.Create(command.Email);

            var numberResult = PhoneNumber.Create(command.PhoneNumber);

            var email = emailResult.Value;
            var number = numberResult.Value;

            var contactResult = Domain.Contact.Contact.Create(command.Name, number, command.Description, contactId, email);

            var result = await _repository.Create(contactResult.Value, cancellation);

            return result.Value;
        }

    }
}
