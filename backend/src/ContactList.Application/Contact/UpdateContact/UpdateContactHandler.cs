using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact.UpdateContact
{
    /// <summary>
    /// Метод для обновления главной информации контакта, ищет по Id
    /// </summary>
    public class UpdateContactHandler : IUpdateContactHandler
    {
        private readonly IContactRepository _repository;
        public UpdateContactHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid, Error>> Handle(UpdateContactCommad command, CancellationToken cancellation)
        {
            // валидация 

            var Id = ContactId.Create(command.Id);
            var contact = await _repository.GetById(Id, cancellation);
            if (contact.IsFailure)
            {
                return contact.Error;
            }

            var email = Domain.Contact.ValueObjects.Email.Create(command.Email).Value;
            var number = PhoneNumber.Create(command.PhoneNumber).Value;

            await _repository.Update(command.Id, number, command.Name, email, command.Description);

            return Id.Value;

        }
    }
}
