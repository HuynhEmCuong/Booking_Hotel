using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Hotel.Data.Interface
{
   public interface IUserTracking
    {
        int? CreateBy { get; set; }

        int? ModifyBy { get; set; }
    }
}
