using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Booking_Hotel.Data.Entities
{
   public class AppRole : IdentityRole<int>
    {
        [StringLength(250)]
        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? CreateBy { get; set; }
        public int? ModifyBy { get; set; }

        [ForeignKey(nameof(CreateBy))]
        public virtual AppUser UserCreate { get; set; }

        [ForeignKey(nameof(ModifyBy))]
        public virtual AppUser UserModify { get; set; }
    }
}
