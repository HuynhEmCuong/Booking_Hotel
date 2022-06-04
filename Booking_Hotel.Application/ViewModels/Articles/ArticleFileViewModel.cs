using Booking_Hotel.Application.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Hotel.Application.ViewModels.Articles
{
    public class ArticleFileViewModel
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public int FileDataId { get; set; }

        public ArticleViewModel Article { get; set; }

        public FileDataViewModel File{ get; set; }
    }
}
