﻿using System.Collections.Generic;

namespace Booking_Hotel.Utilities.Dtos
{
    public class AuthenticationResult
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public object Permission { get; set; }

        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}