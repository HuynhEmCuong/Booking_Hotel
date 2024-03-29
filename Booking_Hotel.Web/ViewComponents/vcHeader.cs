﻿using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookingHotel.Web.ViewComponents
{
    [ViewComponent]
    public class vcHeader : ViewComponent
    {
        #region Fields
        private readonly ILogger<vcHeader> _logging;
        private readonly IOptions<Config> _config;
        private readonly HttpClient client;
        #endregion

        #region Ctor
        public vcHeader(IHttpClientFactory clientFactory, ILogger<vcHeader> logging, IOptions<Config> config)
        {
            client = clientFactory.CreateClient("default");
            _logging = logging;
            _config = config;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.NavClass = 1 != 1 ?  "navbar navbar-white navbar-overlay" : "navbar";
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
