using Microsoft.AspNetCore.Mvc;
using Phonebook.Data;
using Phonebook.Web.Api.Models;
using System.Linq;

namespace Phonebook.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhonebookController : ControllerBase
    {
        private readonly IContactRepository contactRepository;

        public PhonebookController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        [HttpGet]
        public string Version()
        {
            return "Phonebook API v1.0.0";
        }

        [HttpGet]
        [Route("contacts")]
        public RetrieveContactsResult RetrievePhonebookContacts([FromQuery] SimpleContactQueryModel input)
        {
            var result = contactRepository.RetrieveContacts(new string[] { }, new string[] { });
            return result;
        }

        [HttpPost]
        [Route("addcontact")]
        public AddContactResult AddContact([FromBody] NewContactRequestModel input)
        {
            var contact = new Contact
            {
                Name = input.ContactName,
                PhoneNumber = input.ContactPhoneNumber,
                AssociatedPhonebooks = input.AssociateWithPhonebooks
            };

            var result = contactRepository.AddContact(contact);
            return result;
        }
    }
}
