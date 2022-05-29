using Booking_Hotel.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Booking_Hotel.Application.ViewModels
{
    public partial class RoomCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
        public float Price { get; set; }
        public int Size { get; set; }
        public string ImageList { get; set; }
        public string UrlImage { get; set; }
        public int Person { get; set; }

        public int Children { get; set; }

        public string BedType { get; set; }

        public int Position { get; set; }

        public string MetaTile { get; set; }
        public string MetaKeyWord { get; set; }
        public string MetaDescription { get; set; }
        public int? CreateBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public List<RoomViewModel> Rooms { get; set; }
    }
}
