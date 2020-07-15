using System.Collections.Generic;

namespace Phonebook.Data
{
    public class AddContactResult
    {
        public bool Success { get; set; }

        public IEnumerable<string> Messages { get; set; } = new List<string> { };
    }
}
