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
    public class ContactRepository
    {
        private readonly ApplicationDbContext _context;
        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Contact, Error>> GetById(ContactId Id, CancellationToken cancellation)
        {
            var contactResult = await _context
                .Contacts
                .FirstOrDefaultAsync(c => c.Id == Id);

            if (contactResult == null)
            {
                return Errors.General.NotFound();
            }

            return contactResult;
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

        public async Task Save(Contact contact)
        {
            _context.Contacts.Attach(contact);  
            await _context.SaveChangesAsync(); 
        }

        public async Task<List<Contact>> GetAll()
        {
            return await _context.Contacts.ToListAsync();
        }


    }
}
