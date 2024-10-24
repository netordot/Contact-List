using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;

namespace ContactList.Application.Contact.GetByName
{
    public interface IGetByNameHandler
    {
        Task<Result<Domain.Contact.Contact, Error>> Handle(string Name, CancellationToken cancellationT);
    }
}