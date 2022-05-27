using Booking_Hotel.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Booking_Hotel.Application.ViewModels
{
    public class AboutViewModel 
    {
        public int Id { get; set; }

        
        public string  Name { get; set; }

        
        public string Email { get; set; }

        
        public string Phone{ get; set; }

        
        public string Phone2 { get; set; }


        
        public string Address { get; set; }

        public string Content{ get; set; }

        
        public string ImagesUrl{ get; set; }

        
        public string LogoUrl { get; set; }


        public DateTime? CreateDate { get ; set ; }
        public DateTime? ModifyDate { get ; set ; }
        public int? CreateBy { get ; set ; }
        public int? ModifyBy { get ; set ; }
    }
}




