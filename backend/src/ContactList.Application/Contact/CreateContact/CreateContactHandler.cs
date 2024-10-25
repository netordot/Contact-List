﻿using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact.CreateContact
{
    public class CreateContactHandler : ICreateContactHandler
    {
        // подключить fluentValidation
        private readonly IContactRepository _repository;
        public CreateContactHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid, Error>> Handle(CreateContactCommand command, CancellationToken cancellation)
        {
            // TODO: добавить валидацию
            var contactId = ContactId.NewContactId;
            var emailResult = Email.Create(command.Email);
            var numberResult = PhoneNumber.Create(command.PhoneNumber);

            if (emailResult.IsFailure || numberResult.IsFailure)
            {
                return Result.Failure<Guid, Error>(Errors.General.ValueIsInvalid("Invalid email or phone number"));
            }

            var email = emailResult.Value;
            var number = numberResult.Value;

            var contactResult = Domain.Contact.Contact.Create(command.Name, number, command.Description, contactId, email);

            if (contactResult.IsFailure)
            {
                return Result.Failure<Guid, Error>(contactResult.Error);
            }

            var result = await _repository.Create(contactResult.Value, cancellation);

            if (result.IsFailure)
            {
                return Result.Failure<Guid, Error>(result.Error);
            }

            return Result.Success<Guid, Error>(result.Value);
        }

    }
}
