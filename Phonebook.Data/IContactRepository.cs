using System.Collections.Generic;

namespace Phonebook.Data
{
    public interface IContactRepository
    {
        AddContactResult AddContact(Contact contact);

        RetrieveContactsResult RetrieveContacts(IEnumerable<string> phonebookNameFilter, IEnumerable<string> contactNameFilter);
    }
}
