using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Phonebook.Data.SqlServer
{
    public class ContactRepository : IContactRepository
    {
        private readonly string connectionString;

        public ContactRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public AddContactResult AddContact(string name, string phoneNumber, string phoneBook)
        {
            var messages = new List<string> { };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("usp_add_contact", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("phone_number", phoneNumber);
                command.Parameters.AddWithValue("phonebook_name", phoneBook);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = (string)reader[0];
                        messages.Add(message);
                    }
                }
            }

            var result = new AddContactResult(messages.Count == 0, messages);
            return result;
        }

        public RetrieveContactsResult RetrieveContacts(string phoneBook)
        {
            var messages = new List<string> { };
            var contacts = new List<Contact> { };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("usp_retrieve_contacts", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("phonebook_name", phoneBook);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = (string)reader[0];
                        messages.Add(message);
                    }

                    var haveResults = reader.NextResult();

                    if (haveResults)
                    {
                        while (reader.Read())
                        {
                            var contact = new Contact((string)reader[0], (string)reader[1]);
                            contacts.Add(contact);
                        }
                    }
                }
            }

            var result = new RetrieveContactsResult(messages.Count == 0, messages, contacts);

            return result;
        }
    }
}
