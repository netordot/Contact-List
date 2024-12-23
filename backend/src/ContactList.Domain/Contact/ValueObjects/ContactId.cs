﻿using System;
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
        public static ContactId NewContactId => new(Guid.NewGuid());
        public static ContactId Empty => new(Guid.Empty);
        public static ContactId Create(Guid id) => new(id);
        public static implicit operator Guid(ContactId сontactId)
        {
            ArgumentNullException.ThrowIfNull(сontactId);
            return сontactId.Value;
        }
    }
}
