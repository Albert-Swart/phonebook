using System.Collections.Generic;

namespace Phonebook.Web.Api.Models
{
    public class ContactsQueryModel
    {
        public IEnumerable<string> PhoneookNameFilter { get; set; }

        public IEnumerable<string> ContactNameFilter { get; set; }
    }
}
