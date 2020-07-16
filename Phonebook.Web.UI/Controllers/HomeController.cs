using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Phonebook.Data;
using Phonebook.Web.UI.Models;

namespace Phonebook.Web.UI.Controllers
{
    public class PhoneBookEntryViewModel
    {
        public string Name { get; set; }

        public string Number { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = new RetrieveContactsResult();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:61781/phonebook/contacts?phonebookname=my+phonebook"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            model = JsonConvert.DeserializeObject<RetrieveContactsResult>(apiResponse);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddContacts()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddContacts(ContactDetailsViewModel model)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
