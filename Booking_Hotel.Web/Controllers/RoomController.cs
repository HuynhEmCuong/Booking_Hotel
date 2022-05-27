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
    public class RoomController : Controller
    {

        #region Fields
        private readonly ILogger<RoomController> _logger;
        private readonly IOptions<Config> _config;
        private readonly HttpClient client;
        #endregion

        #region Ctor
        public RoomController(ILogger<RoomController> logger, IHttpClientFactory clientFactory, IOptions<Config> config)
        {
            client = clientFactory.CreateClient("default");
            _logger = logger;
            _config = config;
        }
        #endregion

        [Route("/phong/{page?}", Name = "room-default")]
        [Route("/phong/{page?}/{catid?}/{title?}", Name = "room")]
        public async Task<IActionResult> Index(int? page, int catID = 0, string title = "")
        {  //Khai báo pageSize dùng chung
            int pageNumper = page ?? 1;
            //Khai báo url đến api cần gọi
            string apiUrl = $"api/room/getall";
            if (catID > 0)
            {
                apiUrl = $"api/room/getallbycatid?catId={catID}";
            }

            var response = await client.GetAsync(apiUrl);

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            List<RoomViewModel> dataTemp = JsonConvert.DeserializeObject<List<RoomViewModel>>(json);
            IPagedList<RoomViewModel> data = dataTemp.ToPagedList(pageNumper, 12);
            ViewBag.Url = _config.Value.ApiUrl;
            if (catID > 0)
            {
                ViewBag.CatID = catID;
                var responseRoomCategory = await client.GetAsync($"api/roomcategory/getbyid?id={catID}");

                if (responseRoomCategory == null)
                    return View();

                if (responseRoomCategory.StatusCode != System.Net.HttpStatusCode.OK)
                    return View();

                string jsonRoomCategory = responseRoomCategory.Content.ReadAsStringAsync().Result;
                RoomCategoryViewModel dataRoomCategory = JsonConvert.DeserializeObject<RoomCategoryViewModel>(jsonRoomCategory);
                ViewBag.RoomCategory = dataRoomCategory;
            }


            return View(data);
        }

        [Route("/phong/chi-tiet/{id?}/{catid?}/{title?}", Name = "room-detail")]
        public async Task<IActionResult> Detail(int ID = 0, int catID = 1, string title = "")
        {
            var response = await client.GetAsync($"api/room/getbyid?id={ID}");

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            RoomViewModel data = JsonConvert.DeserializeObject<RoomViewModel>(json);
            ViewBag.Url = _config.Value.ApiUrl;
            ViewBag.ID = ID;
            ViewBag.CatID = catID;
            return View(data);
        }

    }
}
