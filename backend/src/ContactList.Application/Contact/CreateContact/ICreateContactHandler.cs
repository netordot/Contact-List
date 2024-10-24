using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;

namespace ContactList.Application.Contact.CreateContact
{
    public interface ICreateContactHandler
    {
        Task<Result<Error, Guid>> Handle(CreateContactCommand command, CancellationToken cancellation);
    }
}