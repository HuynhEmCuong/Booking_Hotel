using Booking_Hotel.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace Booking_Hotel.Web.ViewComponents
{
    [ViewComponent]
    public class vcRoomCategory : ViewComponent
    {

        #region Fields
        private readonly ILogger<vcRoomCategory> _logging;
        private readonly IOptions<Config> _config;
        private readonly HttpClient client;
        #endregion
        #region Ctor
        public vcRoomCategory(IHttpClientFactory clientFactory, ILogger<vcRoomCategory> logging, IOptions<Config> config)
        {
            client = clientFactory.CreateClient("default");

            _logging = logging;
            _config = config;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string apiUrl = $"api/roomcategory/getall";

            var response = await client.GetAsync(apiUrl);

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            List<RoomCategoryViewModel> dataTemp = JsonConvert.DeserializeObject<List<RoomCategoryViewModel>>(json);
            ViewBag.Url = _config.Value.ApiUrl;
            return View(dataTemp);
        }
    }
}
