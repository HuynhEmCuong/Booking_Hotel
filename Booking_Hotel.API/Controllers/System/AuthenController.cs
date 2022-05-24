using Booking_Hotel.Application.Dtos;
using Booking_Hotel.Application.Services.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Booking_Hotel.Controllers.System
{
    public class AuthenController: BaseApiController
    {
        private IAuthService _service;

        public AuthenController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginDto model)
        {
            return StatusCodeResult(await _service.LoginAsync(model));
        }

        [HttpGet]
        public async Task<IActionResult> ResetPasswordAsync(int id)
        {
            return StatusCodeResult(await _service.ResetPasswordAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePasswordAsync(int id, string password)
        {
            return StatusCodeResult(await _service.ChangePasswordAsync(id, password));
        }
    }
}
