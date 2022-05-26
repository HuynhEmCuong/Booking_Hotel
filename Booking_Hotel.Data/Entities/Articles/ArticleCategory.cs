using Booking_Hotel.Data.Enums;
using Booking_Hotel.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Booking_Hotel.Data.Entities.Articles
{
    public class ArticleCategory : IDateTracking, IUserTracking, ISeoTracking
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public Status Status { get; set; }
        public string MetaTile { get ; set ; }
        public string MetaKeyWord { get ; set ; }
        public string MetaDescription { get ; set ; }
        public int? CreateBy { get ; set ; }
        public int? ModifyBy { get ; set ; }
        public DateTime? CreateDate { get ; set ; }
        public DateTime? ModifyDate { get ; set ; }
    }
}
