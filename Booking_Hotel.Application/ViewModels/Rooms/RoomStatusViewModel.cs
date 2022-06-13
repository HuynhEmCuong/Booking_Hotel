using Booking_Hotel.Data.Enums;
using Booking_Hotel.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Booking_Hotel.Application.ViewModels
{
    public  class RoomStatusViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public int Position { get; set; }

        public int? CreateBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
