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
    public class ReservationController : Controller
    {
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
        }
        [Route("/dat-phong", Name = "reservation")]
        public IActionResult Index()
        {
            return View();
        }

    
    }
}
