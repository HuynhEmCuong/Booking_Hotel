using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Booking_Hotel.Data.Entities.System
{
  public  class FileData
    {
        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string FileFullPath { get; set; }

        [StringLength(256)]
        public string FileOriginalName { get; set; }

        [StringLength(256)]
        public string FileLocalName { get; set; }

        [StringLength(50)]
        public string FileExtension { get; set; }

        [StringLength(256)]
        public string FileType { get; set; }

        [StringLength(256)]
        public string Path { get; set; }

        public int? Position { get; set; }
        public bool IsImage { get; set; }
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
