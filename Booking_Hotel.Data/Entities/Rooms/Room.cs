using Booking_Hotel.Data.Enums;
using Booking_Hotel.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Booking_Hotel.Data.Entities.Rooms
{
    public class Room : IDateTracking, IUserTracking, ISeoTracking
    {
        [Key]
        public int Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        public string Description { get; set; }

        public int  Position { get; set; }

        public int Person { get; set; }

        public int Children { get; set; }

        public string UrlImage { get; set; }

        public Status Status { get; set; }

        public int RoomCateId { get; set; }

        public int RoomStatusId { get; set; }
        public string MetaTile { get; set; }
        public string MetaKeyWord { get; set; }
        public string MetaDescription { get; set; }
        public int? CreateBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }


        [ForeignKey("RoomCateId")]
        public RoomCategory RoomCategory { get; set; }

        [ForeignKey("RoomStatusId")]
        public RoomStatus RoomStatus{ get; set; }
    }
}
