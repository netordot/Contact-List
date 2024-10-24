using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contact.CreateContact
{
    public record CreateContactCommand(string PhoneNumber, string Name, string Email, string? Description);

}
