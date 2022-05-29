using Booking_Hotel.Data.Entities.Rooms;
using Booking_Hotel.Data.Enums.Reservation;
using Booking_Hotel.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Booking_Hotel.Application.ViewModels
{
    public partial class ReservationViewModel
    {
        public int Id { get; set; }
   
        public int GuestId { get; set; }

        public int RoomId { get; set; }

        public double TotalPrice { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }


        public PaymentType PaymentType { get; set; }
        public ReservationStatus ReservationStatus { get; set; }

        public int? CreateBy { get ; set ; }
        public int? ModifyBy { get ; set ; }
        public DateTime? CreateDate { get ; set ; }
        public DateTime? ModifyDate { get ; set ; }

        public GuestViewModel Guest { get; set; }

        public RoomViewModel Room { get; set; }
    }
}
