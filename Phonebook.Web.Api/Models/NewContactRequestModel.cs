using System.Collections.Generic;

namespace Phonebook.Web.Api.Models
{
    public class NewContactRequestModel
    {
        //TODO: Validation attributes
        public string ContactName { get; set; }

        public string ContactPhoneNumber { get; set; }

        public IEnumerable<string> AssociateWithPhonebooks { get; set; }
    }
}
