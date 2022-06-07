using Booking_Hotel.Application.ViewModels.System;
using Booking_Hotel.Data.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Hotel.Application.ViewModels.Rooms.Files
{
    public class RoomCateFileViewModel
    {
        public int Id { get; set; }

        public int RoomCateId { get; set; }

        public int FileDataId { get; set; }

        public RoomCategoryViewModel RoomCategory{ get; set; }

        public FileDataViewModel File { get; set; }
    }
}
