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
    public class vcSlider : ViewComponent
    {

        #region Fields
        private readonly ILogger<vcSlider> _logging;
        private readonly IOptions<Config> _config;
        private readonly HttpClient client;
        #endregion
        #region Ctor
        public vcSlider(IHttpClientFactory clientFactory, ILogger<vcSlider> logging, IOptions<Config> config)
        {
            client = clientFactory.CreateClient("default");

            _logging = logging;
            _config = config;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
          
            return View();
        }
    }
}
