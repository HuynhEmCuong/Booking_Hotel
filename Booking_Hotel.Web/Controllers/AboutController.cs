using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Booking_Hotel.Application.ViewModels;

namespace Booking_Hotel.Web.Controllers
{
    public class AboutController : Controller
    {

        #region Fields
        private readonly ILogger<AboutController> _logging;
        private readonly IOptions<Config> _config;
        private readonly HttpClient client;
        #endregion
        #region Ctor
        public AboutController(IHttpClientFactory clientFactory, ILogger<AboutController> logging, IOptions<Config> config)
        {
            client = clientFactory.CreateClient("default");

            _logging = logging;
            _config = config;
        }
        #endregion
        [Route("/gioi-thieu", Name = "about")]
        public async Task<IActionResult> Index()
        {
            var response = await client.GetAsync($"api/about/getfirst");

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            AboutViewModel data = JsonConvert.DeserializeObject<AboutViewModel>(json);
            ViewBag.Url = _config.Value.ApiUrl;
            return View(data);
        }

    }
}
