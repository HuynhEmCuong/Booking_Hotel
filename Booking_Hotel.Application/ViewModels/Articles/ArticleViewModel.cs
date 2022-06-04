using Booking_Hotel.Application.ViewModels.Articles;
using Booking_Hotel.Data.Enums;
using Booking_Hotel.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Booking_Hotel.Application.ViewModels
{
    public  class ArticleViewModel
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        
        public string Title { get; set; }

        
        public string Description { get; set; }

        public string Content { get; set; }

        
        public string ImagesUrl { get; set; }
        public Status Status { get; set; }

        public int ArticleCateId { get; set; }
        public string MetaTile { get; set; }
        public string MetaKeyWord { get; set; }
        public string MetaDescription { get; set; }
        public int? CreateBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ArticleCategoryViewModel ArticleCategory { get; set; }

        public virtual ICollection<ArticleFileViewModel> ArticleFile { get; set; }

    }
}
