using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using X.PagedList;

namespace Booking_Hotel.Web.Controllers
{
    public class RoomCategoryController : Controller
    {

        #region Fields
        private readonly ILogger<RoomCategoryController> _logger;
        private readonly IOptions<Config> _config;
        private readonly HttpClient client;
        #endregion

        #region Ctor
        public RoomCategoryController(ILogger<RoomCategoryController> logger, IHttpClientFactory clientFactory, IOptions<Config> config)
        {
            client = clientFactory.CreateClient("default");
            _logger = logger;
            _config = config;
        }
        #endregion

        [Route("/loai-phong/{page?}", Name = "room-category-default")]
        [Route("/loai-phong/{page?}/{title?}", Name = "room-category")]
        public async Task<IActionResult> Index(int? page, string title = "")
        {  //Khai báo pageSize dùng chung
            int pageNumper = page ?? 1;
            //Khai báo url đến api cần gọi
            string apiUrl = $"api/roomcategory/getall";
          
            var response = await client.GetAsync(apiUrl);

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            List<RoomCategoryViewModel> dataTemp = JsonConvert.DeserializeObject<List<RoomCategoryViewModel>>(json);
            //IPagedList<RoomCategoryViewModel> data = dataTemp.ToPagedList(pageNumper, 12);
            ViewBag.Url = _config.Value.ApiUrl;
            ViewBag.RoomCategory = dataTemp;
            ViewBag.PopularRooms = dataTemp;

            return View();
        }

        [Route("/loai-phong/chi-tiet/{id?}/{title?}", Name = "room-category-detail")]
        public async Task<IActionResult> Detail(int ID = 0, int catID = 1, string title = "")
        {
            var response = await client.GetAsync($"/api/RoomCategory/FindById?id={ID}");

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            RoomCategoryViewModel data = JsonConvert.DeserializeObject<RoomCategoryViewModel>(json);
            ViewBag.Url = _config.Value.ApiUrl;
            ViewBag.ID = ID;
            ViewBag.RoomCategory = data;

            return View();
        }

    }
}
