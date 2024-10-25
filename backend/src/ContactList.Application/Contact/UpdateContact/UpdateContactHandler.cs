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

namespace ContactList.Application.Contact.UpdateContact
{
    public class UpdateContactHandler : IUpdateContactHandler
    {
        private readonly IContactRepository _repository;
        private readonly IValidator<UpdateContactCommad> _validator;

        public UpdateContactHandler(IContactRepository repository, IValidator<UpdateContactCommad> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(UpdateContactCommad command, CancellationToken cancellation)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellation);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var Id = ContactId.Create(command.Id);
            var contact = await _repository.GetById(Id, cancellation);
            if (contact.IsFailure)
            {
                return new ErrorList([contact.Error]);
            }

            var email = Domain.Contact.ValueObjects.Email.Create(command.Email).Value;
            var number = PhoneNumber.Create(command.PhoneNumber).Value;

            await _repository.Update(command.Id, number, command.Name, email, command.Description);

            return Id.Value;

        }
    }
}
