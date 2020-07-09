using Microsoft.AspNetCore.Mvc;
using Phonebook.Data;
using Phonebook.Web.Api.Models;

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
        public RetrieveContactsResult AllEntries([FromQuery] ContactsQueryModel input)
        {
            var result = contactRepository.RetrieveContacts(input.PhoneBookName);
            return result;
        }

        [HttpPost]
        [Route("addcontact")]
        public AddContactResult AddContact([FromBody] NewContactRequestModel input)
        {
            var result = contactRepository.AddContact(input.Name, input.PhoneNumber, input.PhoneBook);
            return result;
        }
    }
}
