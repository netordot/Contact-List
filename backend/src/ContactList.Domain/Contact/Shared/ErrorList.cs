using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Domain.Contact.Shared
{
    public class ErrorList : IEnumerable<Error>
    {
        public readonly List<Error> _errors;

        public ErrorList(IEnumerable<Error> errors)
        {
            _errors = errors.ToList();
        }
        public IEnumerator<Error> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static implicit operator ErrorList(List<Error> errors) => new(errors);

        public static implicit operator ErrorList(Error error) => new([error]);
    }
}
