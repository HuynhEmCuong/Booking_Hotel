using Booking_Hotel.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Hotel.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(ILogger<ArticleController> logger)
        {
            _logger = logger;
        }
        [Route("/tin-tuc/{page?}", Name = "article-default")]
        [Route("/tin-tuc/{page?}/{catid?}/{title?}", Name = "article")]
        public IActionResult Index()
        {
            return View();
        }

     
    }
}
