namespace Phonebook.Web.Api.Models
{
    public class NewContactRequestModel
    {
        //TODO: Validation attributes
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneBook { get; set; }
    }
}
