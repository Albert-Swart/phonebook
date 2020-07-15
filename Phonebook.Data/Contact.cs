using System.Collections.Generic;

namespace Phonebook.Data
{
    public class Contact
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<string> AssociatedPhonebooks { get; set; } // Contextual Value
    }
}
