using Booking_Hotel.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Booking_Hotel.Application.ViewModels
{
    public partial class GuestViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string  Phone { get; set; }

        public string Email { get; set; }

        public DateTime? CreateDate { get ; set ; }
        public DateTime? ModifyDate { get ; set ; }
        public int? CreateBy { get ; set ; }
        public int? ModifyBy { get ; set ; }
    }
}
