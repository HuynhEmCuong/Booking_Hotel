using Booking_Hotel.Data.Entities.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Booking_Hotel.Data.Entities.Rooms.Files
{
    public class RoomCateFile
    {
        [Key]
        public int Id { get; set; }

        public int RoomCateId { get; set; }

        public int FileDataId { get; set; }

        [ForeignKey("RoomCateId")]
        public virtual RoomCategory RoomCategory { get; set; }

        [ForeignKey("FileDataId")]
        public virtual FileData FileData { get; set; }
    }
}


