using ContactList.Application.Contact.CreateContact;
using ContactList.Application.Contact.UpdateContact;

namespace ContactList.API.Contracts.Response
{
    public record GetAllContactsResponse(List<UpdateContactCommad> contacts);
    
}
