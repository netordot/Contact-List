using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;

namespace ContactList.Application.Contact.GetAll
{
    public interface IGetAllContactsHandler
    {
        Task<Result<List<Domain.Contact.Contact>, Error>> Handle(CancellationToken cancellation);
    }
}