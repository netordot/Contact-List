using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact.GetByName
{
    public class GetByNameHandler : IGetByNameHandler
    {
        private readonly IContactRepository _repository;

        public GetByNameHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Domain.Contact.Contact, Error>> Handle(string Name, CancellationToken cancellationT)
        {
            var contact = await _repository.GetByName(Name);
            if (contact.IsFailure)
            {
                return contact.Error;
            }

            return contact;
        }
    }
}
