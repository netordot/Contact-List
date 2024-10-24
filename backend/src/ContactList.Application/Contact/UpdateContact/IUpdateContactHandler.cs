using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;

namespace ContactList.Application.Contact.UpdateContact
{
    public interface IUpdateContactHandler
    {
        Task<Result<Guid, Error>> Handle(UpdateContactCommad command, CancellationToken cancellation);
    }
}