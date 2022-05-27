using Booking_Hotel.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Hotel.Web.Models
{
    public class MenuViewModel
    {
        public MenuViewModel(List<RoomCategoryViewModel> roomCategory, List<ArticleCategoryViewModel> articleCategory, AboutViewModel about)
        {
            RoomCategory = roomCategory;
            ArticleCategory = articleCategory;
            About = about;
        }

        public List<RoomCategoryViewModel> RoomCategory { get; set; }
        public List<ArticleCategoryViewModel> ArticleCategory { get; set; }
        public AboutViewModel About { get; set; }
    }
}
