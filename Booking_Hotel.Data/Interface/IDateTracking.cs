using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Hotel.Data.Interface
{
    public interface IDateTracking
    {
        DateTime? CreateDate { get; set; }

        DateTime? ModifyDate { get; set; }
    }
}
