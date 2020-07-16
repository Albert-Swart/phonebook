using System.Collections.Generic;

namespace Phonebook.Web.UI.Models
{
    public class ContactDetailsViewModel
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<string> AssociatedPhonebooks { get; set; }

        public IEnumerable<string> ExixtingPhonebooks { get; set; }
    }
}
