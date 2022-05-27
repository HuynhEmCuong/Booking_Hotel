using Booking_Hotel.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Booking_Hotel.Data.Entities.Contacts
{
    public class Contact : IDateTracking, IUserTracking
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(255)]
        public string Email { get; set; }


        [StringLength(15)]
        public string Phone { get; set; }

        
        public string Note { get; set; }


        public int? CreateBy { get ; set ; }
        public int? ModifyBy { get ; set ; }
        public DateTime? CreateDate { get ; set ; }
        public DateTime? ModifyDate { get ; set ; }
    }
}
