using Booking_Hotel.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Hotel.Web.Models
{
    public class ReservationParams
    {

        public string CheckInDate  { get; set; }
        public string CheckOutDate { get; set; }
        public int Adult { get; set; }
        public int Kids { get; set; }
        public int RoomCatId { get; set; }
    }
    public class OperationResultViewModel
    {
        public int StatusCode { set; get; }
        public string Message { set; get; }
        public bool Success { set; get; }
        public GuestReturnViewModel Data { set; get; }

    }
    public class ReservationFormViewModel
    {

        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int Adult { get; set; }
        public int Kids { get; set; }
        public int RoomCatId { get; set; }
        public string RoomCatName { get; set; }
    }
}
