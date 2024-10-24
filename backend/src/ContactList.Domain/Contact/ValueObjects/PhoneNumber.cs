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
    public record PhoneNumber
    {
        private const string Pattern = "^((\\+7|8)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$";
        public string Number { get; }

        private PhoneNumber(string number)
        {
            Number = number;
        }

        public static Result<PhoneNumber, Error> Create(string number)
        {
            if (String.IsNullOrWhiteSpace(number))
            {
                return Errors.General.ValueIsRequired("PhoneNumber");
            }
            if (!Regex.IsMatch(number, Pattern))
            {
                return Errors.General.ValueIsInvalid(number);
            }

            return new PhoneNumber(number);
        }

    }
}
