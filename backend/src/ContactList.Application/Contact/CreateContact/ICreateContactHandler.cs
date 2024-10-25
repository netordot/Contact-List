using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;

namespace ContactList.Application.Contact.CreateContact
{
    public interface ICreateContactHandler
    {
        Task<Result<Guid, Error>> Handle(CreateContactCommand command, CancellationToken cancellation);
            
    }
}