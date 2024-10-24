namespace ContactList.API.Contracts.Requests
{
    public record CreateContactRequest(string PhoneNumber, string Name, string Email, string? Description);
    
}
