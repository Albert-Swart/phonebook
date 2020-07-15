using System.Collections.Generic;

namespace Phonebook.Data
{
    public class RetrieveContactsResult
    {
        public IEnumerable<Contact> Results { get; set; } = new List<Contact> { };
    }
}
