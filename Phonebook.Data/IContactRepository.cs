using System.Collections.Generic;

namespace Phonebook.Data
{
    public interface IContactRepository
    {
        AddContactResult AddContact(string name, string phoneNumber, string phoneBook);

        RetrieveContactsResult RetrieveContacts(string phoneBook);
    }

    public class AddContactResult
    {
        public AddContactResult(bool success, IEnumerable<string> messages)
        {
            Success = success;
            Messages = messages;
        }

        public bool Success { get; }

        public IEnumerable<string> Messages { get; }
    }

    public class RetrieveContactsResult
    {
        //public RetrieveContactsResult(bool success, IEnumerable<string> messages, IEnumerable<Contact> results)
        //{
        //    Success = success;
        //    Messages = messages;
        //    Results = results;
        //}

        public bool Success { get; set; }

        public IEnumerable<string> Messages { get; set; } = new List<string>();

        public IEnumerable<Contact> Results { get; set; } = new List<Contact>();
    }

    public class Contact
    {
        //public Contact(string name, string phoneNumber)
        //{
        //    Name = name;
        //    PhoneNumber = phoneNumber;
        //}

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
