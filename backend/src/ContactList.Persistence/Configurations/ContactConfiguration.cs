using ContactList.Domain.Contact;
using ContactList.Domain.Contact.Shared;
using ContactList.Domain.Contact.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Persistence.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("contacts");

            builder.HasKey(c => c.Id);

            builder.Property(v => v.Id)
            .HasConversion(
                Id => Id.Value,
                value => ContactId.Create(value));

            builder.ComplexProperty(c => c.Email, eb => 
            {
                eb.Property(e => e.Mail)
                .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE);
            });

            builder.ComplexProperty(c => c.PhoneNumber, eb => 
            {
                eb.Property(e => e.Number).IsRequired(); 
            });

            builder.Property(c => c.Description)
                .HasMaxLength(Constants.MAX_LONG_TEXT_SIZE);

            builder.Property(c => c.Name)
                .HasMaxLength(Constants.MAX_SHORT_TEXT_SIZE);


        }
    }
}
