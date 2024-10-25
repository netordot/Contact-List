using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact
{
    public interface IContactRepository
    {
        Task<Result<Guid, Error>> Create(Domain.Contact.Contact contact, CancellationToken cancellation);
        Task<Result<Guid, Error>> Delete(ContactId Id, CancellationToken cancellation);
        Task<Result<Domain.Contact.Contact, Error>> GetById(ContactId Id, CancellationToken cancellation);
        Task Save(Domain.Contact.Contact contact);
        Task<Result<Domain.Contact.Contact, Error>> GetByName(string name);
        Task<List<Domain.Contact.Contact>> Get();
    }
}
