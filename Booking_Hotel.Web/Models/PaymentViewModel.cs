using Booking_Hotel.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Hotel.Web.Models
{
    public class PaymentViewModel
    {

        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string RoomCatName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public int RoomCatID { get; set; }
        public int RoomID { get; set; }
        public int Adult { get; set; }
        public int Kids { get; set; }
        public double TotalPrice { get; set; }
    }
    public class PaypalViewModel
    {

        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string RoomCatName { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoomCatID { get; set; }
        public int RoomID { get; set; }
        public int Adult { get; set; }
        public int Kids { get; set; }
        public double TotalPrice { get; set; }
    }
}
