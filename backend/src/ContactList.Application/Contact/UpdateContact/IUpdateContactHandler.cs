using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;

namespace ContactList.Application.Contact.UpdateContact
{
    public interface IUpdateContactHandler
    {
        Task<Result<Guid, ErrorList>> Handle(UpdateContactCommad command, CancellationToken cancellation);
    }
}