using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactList.Domain.Contact.ValueObjects
{
    public record Email
    {
        private const string Pattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
        public string Mail { get; }

        private Email(string mail)
        {
            Mail = mail;
        }

        public static Result<Email, Error> Create(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
            {
                return Errors.General.ValueIsRequired("Email");
            }
            if (!Regex.IsMatch(mail, Pattern))
            {
                return Errors.General.ValueIsInvalid(mail);
            }

            return new Email(mail);
        }

    }
}
