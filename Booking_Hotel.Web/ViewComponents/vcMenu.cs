using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Booking_Hotel.Web;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Web.Models;

namespace BookingHotel.Web.ViewComponents
{
    [ViewComponent]
    public class vcMenu : ViewComponent
    {
        #region Fields
        private readonly ILogger<vcMenu> _logging;
        private readonly IOptions<Config> _config;
        private readonly HttpClient client;
        #endregion
        #region Ctor
        public vcMenu(IHttpClientFactory clientFactory, ILogger<vcMenu> logging, IOptions<Config> config)
        {
            client = clientFactory.CreateClient("default");

            _logging = logging;
            _config = config;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var responseRoomCategory = await client.GetAsync("api/RoomCategory/getall");
            var responseArticleCategory = await client.GetAsync("api/ArticleCategory/getall");
            var response = await client.GetAsync($"api/about/getfirst");

            string json = response.Content.ReadAsStringAsync().Result;
            AboutViewModel data = JsonConvert.DeserializeObject<AboutViewModel>(json);


            string jsonRoomCategory = responseRoomCategory.Content.ReadAsStringAsync().Result;
            List<RoomCategoryViewModel> dataRoomCategory = JsonConvert.DeserializeObject<List<RoomCategoryViewModel>>(jsonRoomCategory);


            string jsonArticleCategory = responseArticleCategory.Content.ReadAsStringAsync().Result;
            List<ArticleCategoryViewModel> dataArticleCategory = JsonConvert.DeserializeObject<List<ArticleCategoryViewModel>>(jsonArticleCategory);

            var result = new MenuViewModel(dataRoomCategory, dataArticleCategory, data);

        

            return View(result);
        }
    }
}
