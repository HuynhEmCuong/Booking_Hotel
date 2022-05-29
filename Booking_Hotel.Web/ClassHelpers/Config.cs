using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Hotel.Web
{
    public class Config
    {
        public string ApiBase { get; set; }
        public string ApiUrl { get; set; }
    }
    public class PaypalSetting
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public double ExchangeRate { get; set; }
    }
}
