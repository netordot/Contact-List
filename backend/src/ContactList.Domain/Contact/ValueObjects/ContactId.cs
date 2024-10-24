using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Domain.Contact.ValueObjects
{
    public record ContactId
    {
        public Guid Value { get; }

        private ContactId(Guid value)
        {
            Value = value;
        }

        public static ContactId NewVolunteerId => new(Guid.NewGuid());
        public static ContactId Empty => new(Guid.Empty);
        public static ContactId Create(Guid id) => new(id);
        public static implicit operator Guid(ContactId volunteerId)
        {
            ArgumentNullException.ThrowIfNull(volunteerId);
            return volunteerId.Value;
        }
    }
}
