using ContactList.Domain.Contact;
using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact.GetAll
{
    public class GetAllContactsHandler : IGetAllContactsHandler
    {
        private readonly IContactRepository _repository;

        public GetAllContactsHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<Domain.Contact.Contact>, Error>> Handle(CancellationToken cancellation)
        {
            var result = await _repository.GetAll();
            if (result == null)
            {
                return Errors.General.NotFound();
            }

            return result;
        }
    }
}
