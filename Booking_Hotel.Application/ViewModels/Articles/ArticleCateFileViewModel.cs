using Booking_Hotel.Application.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Hotel.Application.ViewModels.Articles
{
   public class ArticleCateFileViewModel
    {
        public int Id { get; set; }

        public int ArticleCateId { get; set; }

        public int FileDataId { get; set; }

        public ArticleCategoryViewModel ArticleCategory { get; set; }

        public FileDataViewModel File  { get; set; }
    }
}
