using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;


namespace BookingHotel.Web.ViewComponents
{
    [ViewComponent]
    public class vcFooter : ViewComponent
    {
        #region Fields
        private readonly ILogger<vcFooter> _logging;
        private readonly IOptions<Config> _config;
        private readonly HttpClient client;
        #endregion
        #region Ctor
        public vcFooter(IHttpClientFactory clientFactory, ILogger<vcFooter> logging, IOptions<Config> config)
        {
            client = clientFactory.CreateClient("default");

            _logging = logging;
            _config = config;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
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
