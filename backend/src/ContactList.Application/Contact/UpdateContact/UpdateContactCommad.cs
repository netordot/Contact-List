namespace ContactList.Application.Contact.UpdateContact
{
    public record UpdateContactCommad(Guid Id,string PhoneNumber, string Name, string Email, string? Description);

}