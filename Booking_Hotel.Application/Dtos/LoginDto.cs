﻿namespace Booking_Hotel.Application.Dtos
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
