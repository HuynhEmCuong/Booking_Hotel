using Booking_Hotel.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Booking_Hotel.Data.Entities.Aboutes
{
    public class About : IDateTracking, IUserTracking
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string  Name { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(15)]
        public string Phone{ get; set; }

        [StringLength(15)]
        public string Phone2 { get; set; }


        [StringLength(255)]
        public string Address { get; set; }

        public string Content{ get; set; }

        [StringLength(255)]
        public string ImagesUrl{ get; set; }

        [StringLength(255)]
        public string LogoUrl { get; set; }


        public DateTime? CreateDate { get ; set ; }
        public DateTime? ModifyDate { get ; set ; }
        public int? CreateBy { get ; set ; }
        public int? ModifyBy { get ; set ; }
    }
}




