using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact.Delete
{
    public class DeleteContactHandler : IDeleteContactHandler
    {
        private readonly IContactRepository _repository;

        public DeleteContactHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Error, Guid>> Handle(Guid Id, CancellationToken cancellation)
        {
            var contact = await _repository.GetById(ContactId.Create(Id), cancellation);

            contact.Value.Delete();

            return contact.Value.Id.Value;

        }
    }
}
