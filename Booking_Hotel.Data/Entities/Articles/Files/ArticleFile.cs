using Booking_Hotel.Data.Entities.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Booking_Hotel.Data.Entities.Articles.Files
{
    public class ArticleFile
    {
        [Key]
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public int FileDataId { get; set; }

        [ForeignKey("ArticleId")]
        public Article Article { get; set; }

        [ForeignKey("FileDataId")]
        public FileData FileData{ get; set; }
    }
}
