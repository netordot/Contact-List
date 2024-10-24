using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using CSharpFunctionalExtensions;
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
        public ContactId Id { get; private set; }

        private Contact(string name, PhoneNumber phoneNumber, string description, ContactId id)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Description = description;
            Id = id;
        }

        public static Result<Contact,Error> Create(string name, PhoneNumber phoneNumber, string description, ContactId id)
        {
            var contact = new Contact(name, phoneNumber, description, id);
            return contact;
        }
    }
}
