using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;

namespace ContactList.Application.Contact.GetByName
{
    public interface IGetByIdHandler
    {
        Task<Result<Domain.Contact.Contact, Error>> Handle(Guid Id, CancellationToken cancellationT);
    }
}