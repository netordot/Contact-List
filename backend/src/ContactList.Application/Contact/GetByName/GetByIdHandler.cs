using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact.GetByName
{
    public class GetByIdHandler : IGetByIdHandler
    {
        private readonly IContactRepository _repository;

        public GetByIdHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Domain.Contact.Contact, Error>> Handle(Guid contactId, CancellationToken cancellation)
        {
            var Id = ContactId.Create(contactId);
            var contact = await _repository.GetById(Id, cancellation);
            if (contact.IsFailure)
            {
                return contact.Error;
            }

            return contact;
        }
    }
}
