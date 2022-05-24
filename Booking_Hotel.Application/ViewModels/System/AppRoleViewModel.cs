using System;
using System.ComponentModel.DataAnnotations;

namespace Booking_Hotel.Application.ViewModels.System
{
    public class AppRoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? CreateBy { get; set; }
        public int? ModifyBy { get; set; }

        public virtual AppUserViewModel UserCreate { get; set; }
        public virtual AppUserViewModel UserModify { get; set; }
    }
}
