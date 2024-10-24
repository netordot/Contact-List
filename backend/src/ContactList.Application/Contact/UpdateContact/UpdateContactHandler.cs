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
    /// Метод для обновления главной информации контакта, ищет по имени
    /// </summary>
    public class UpdateContactHandler
    {
        private readonly IContactRepository _repository;
        public UpdateContactHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid, Error>> Handle(UpdateContactCommad command)
        {
            // валидация 

            var contact = await _repository.GetByName(command.Name);
            if (contact.IsFailure)
            {
                return contact.Error;
            }

            var email = Domain.Contact.ValueObjects.Email.Create(command.Email).Value;
            var number = PhoneNumber.Create(command.PhoneNumber).Value;

            contact.Value.UpdateMainInfo(email, number, command.Description);
            await _repository.Save(contact.Value);

            return contact.Value.Id.Value;

        }
    }
}
