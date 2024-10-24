using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact.CreateContact
{
    public class CreateContactHandler
    {
        // подключить fluentValidation
        private readonly IContactRepository _repository;
        public CreateContactHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Error, Guid>> Handle(CreateContactCommand command, CancellationToken cancellation)
        {
            // TODO: добавить валидацию
            var contactId = ContactId.NewContactId;

            var email = Email.Create(command.Email).Value;
            var number = PhoneNumber.Create(command.PhoneNumber).Value;

            var contactResult = Domain.Contact.Contact.Create(command.Name, number, command.Description, contactId, email);

            var result =await _repository.Create(contactResult.Value, cancellation);
            if(result.IsFailure)
            {
                return result.Error;
            }

            return result.Value;
        }
    }
}
