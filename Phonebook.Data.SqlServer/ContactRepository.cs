using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Phonebook.Data.SqlServer
{
    public class ContactRepository : IContactRepository
    {
        private readonly string connectionString;

        public ContactRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public AddContactResult AddContact(Contact contact)
        {
            var messages = new List<string> { };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("usp_add_contact", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("name", contact.Name);
                command.Parameters.AddWithValue("phone_number", contact.PhoneNumber);

                var associatedPhonebooksTable = new DataTable();
                associatedPhonebooksTable.Columns.Add("value", typeof(string));

                foreach (var value in contact.AssociatedPhonebooks)
                {
                    var row = associatedPhonebooksTable.NewRow();
                    row["value"] = value;
                    associatedPhonebooksTable.Rows.Add(row);
                }

                command.Parameters.AddWithValue("associated_phonebooks", associatedPhonebooksTable);

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

            var result = new AddContactResult { Success = messages.Count == 0, Messages = messages };
            return result;
        }

        public RetrieveContactsResult RetrieveContacts(IEnumerable<string> phonebookNameFilter, IEnumerable<string> contactNameFilter)
        {
            var messages = new List<string> { };
            var contacts = new List<Contact> { };

            var data = new List<Tuple<string, string, string>>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("usp_retrieve_contacts", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var phonebookNameFilterTable = new DataTable();
                phonebookNameFilterTable.Columns.Add("value", typeof(string));
                
                foreach(var value in phonebookNameFilter)
                {
                    var row = phonebookNameFilterTable.NewRow();
                    row["value"] = value;
                    phonebookNameFilterTable.Rows.Add(row);
                }

                var contactNameFilterTable = new DataTable();
                contactNameFilterTable.Columns.Add("value", typeof(string));

                foreach (var value in contactNameFilter)
                {
                    var row = contactNameFilterTable.NewRow();
                    row["value"] = value;
                    contactNameFilterTable.Rows.Add(row);
                }

                command.Parameters.AddWithValue("phonebook_name_filter", phonebookNameFilterTable);
                command.Parameters.AddWithValue("contact_name_filter", contactNameFilterTable);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(new Tuple<string, string, string>((string)reader[0], (string)reader[1], (string)reader[2]));
                    }
                }
            }

            var results = data.GroupBy(c => c.Item1, c => c.Item3, (n, a) => new Contact { Name = n, AssociatedPhonebooks = a.ToList() });            

            var result = new RetrieveContactsResult 
            { 
                Results = data.GroupBy(t => new { t.Item1, t.Item2 }, t => t.Item3)
                    .Select(gcs => new Contact{ Name = gcs.Key.Item1, PhoneNumber = gcs.Key.Item2, AssociatedPhonebooks = gcs.ToList() })
            };

            return result;
        }
    }
}
