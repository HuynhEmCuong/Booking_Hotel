using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Booking_Hotel.Application.ViewModels;

namespace Booking_Hotel.Web.Controllers
{
    public class ContactController : Controller
    {
        #region Fields
        private readonly ILogger<RoomCategoryController> _logger;
        private readonly IOptions<Config> _config;
        private readonly HttpClient client;
        #endregion

        #region Ctor
        public ContactController(ILogger<RoomCategoryController> logger, IHttpClientFactory clientFactory, IOptions<Config> config)
        {
            client = clientFactory.CreateClient("default");
            _logger = logger;
            _config = config;
        }
        #endregion

        [Route("/lien-he", Name = "contact")]
        public async Task<IActionResult> Index()
        {

            var response = await client.GetAsync($"api/about/getfirst");

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            AboutViewModel data = JsonConvert.DeserializeObject<AboutViewModel>(json);
            ViewBag.About = data;
            return View();
        }
       
    }
}
