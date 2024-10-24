using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;

namespace ContactList.Application.Contact.Delete
{
    public interface IDeleteContactHandler
    {
        Task<Result<Error, Guid>> Handle(Guid Id, CancellationToken cancellation);
    }
}