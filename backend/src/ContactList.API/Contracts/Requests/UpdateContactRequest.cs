namespace ContactList.API.Contracts.Requests
{
    public record UpdateContactRequest(string PhoneNumber, string Name, string Email, string? Description);

}
