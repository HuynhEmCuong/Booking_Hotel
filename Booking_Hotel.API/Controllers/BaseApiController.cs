using Booking_Hotel.Utilities.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Hotel.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseApiController : Controller
    {
        [NonAction] //Set not Tracking http method
        public ObjectResult StatusCodeResult(OperationResult result)
        {
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(result.StatusCode, result.Message);
            }
        }
    }
}
