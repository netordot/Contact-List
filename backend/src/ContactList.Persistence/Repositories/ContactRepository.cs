using ContactList.Application.Contact;
using ContactList.Domain.Contact;
using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Persistence.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;
        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Contact, Error>> GetById(ContactId Id, CancellationToken cancellation)
        {
            var newId = Id.Value;
            if (Id == null)
            {
                return Errors.General.ValueIsInvalid("ContactId cannot be null.");
            }

            var contactResult = await _context
                .Contacts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == newId, cancellation);

            if (contactResult == null)
            {
                return Errors.General.NotFound();
            }

            return contactResult;
        }

        public async Task<List<Contact>> Get()
        {
            var contacts = await _context.Contacts.AsNoTracking().ToListAsync();

            var result = contacts
                .Select(c => Contact.Create(c.Name, c.PhoneNumber, c.Description, c.Id, c.Email).Value)
                .ToList();

            return contacts;
        }


        public async Task<Result<Guid, Error>> Create(Contact contact, CancellationToken cancellation)
        {
            await _context.AddAsync(contact, cancellation);
            await _context.SaveChangesAsync();

            return contact.Id.Value;
        }

        public async Task<Result<Guid, Error>> Delete(ContactId Id, CancellationToken cancellation)
        {
            var contactToDelete = await GetById(Id, cancellation);
            if (contactToDelete.IsFailure)
            {
                return Id.Value;
            }

            _context.Contacts.Remove(contactToDelete.Value);
            await _context.SaveChangesAsync();
            return Id.Value;
        }

        public async Task<Result<Contact, Error>> GetByName(string name)
        {
            var result = await _context
                .Contacts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name == name);

            if (result == null)
            {
                return Errors.General.NotFound();
            }

            return result;

        }

        public async Task<Guid> Update(Guid id, PhoneNumber number, string name, Email email, string descritprion)
        {
            //await _context.Contacts
            //    .Where(c => c.Id == id)
            //    .ExecuteUpdateAsync(a => a
            //    .SetProperty(c => c.PhoneNumber, c => number)
            //    .SetProperty(c => c.Email, c => email)
            //    .SetProperty(c => c.Description, c => descritprion)
            //    .SetProperty(c => c.Name, c => name));

            var contactToUpdate = Contact.Create(name, number, descritprion, ContactId.Create(id), email);
             _context.Contacts.Update(contactToUpdate.Value);

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task Save(Contact contact)
        {
            _context.Contacts.Attach(contact);
            await _context.SaveChangesAsync();
        }
    }
}
