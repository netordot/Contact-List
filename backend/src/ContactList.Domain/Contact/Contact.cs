using ContactList.Domain.Contact.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Domain.Contact
{
    public class Contact
    {
        public string Name { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public string Description { get; private set; }
        public Guid Id { get; private set; }

        private Contact(string name, PhoneNumber phoneNumber, string description, Guid id)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Description = description;
            Id = id;
        }

        public static Contact Create(string name, PhoneNumber phoneNumber, string description, Guid id)
        {
            var contact = new Contact(name, phoneNumber, description, id);
            return contact;
        }
    }
}
