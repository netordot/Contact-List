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
    public class Contact : Shared.Entity<ContactId>
    {
        public string Name { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public string? Description { get; private set; }
        public ContactId Id { get; private set; }

        public Email Email { get; private set; }

        private Contact(ContactId id) : base(id)
        {
        }

        private Contact(string name, PhoneNumber phoneNumber, string description, ContactId id, Email email)
            : base(id)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Description = description;
            Email = email;
            Id = id;
        }

        public static Result<Contact, Error> Create(string name, PhoneNumber phoneNumber, string description, ContactId id, Email email)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return Errors.General.ValueIsRequired("name");
            }

            var contact = new Contact(name, phoneNumber, description, id, email);
            return contact;
        }

        public void UpdateMainInfo(Email mail, PhoneNumber number, string? description)
        {
            Email = mail;
            PhoneNumber = number;
            Description = description;  
        }
    }
}
