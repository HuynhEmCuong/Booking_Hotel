using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Hotel.Data.Interface
{
    public interface ISeoTracking
    {
        string MetaTile { get; set; }
        string MetaKeyWord { get; set; }

        string MetaDescription { get; set; }
    }
}
