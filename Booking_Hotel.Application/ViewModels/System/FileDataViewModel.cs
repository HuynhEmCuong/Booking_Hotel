using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Hotel.Application.ViewModels.System
{
    public class FileDataViewModel
    {
        public int Id { get; set; }
        public string FileFullPath { get; set; }

        public string FileOriginalName { get; set; }

        public string FileLocalName { get; set; }

        public string FileExtension { get; set; }

        public string FileType { get; set; }

        public string Path { get; set; }

        public int? Position { get; set; }
        public bool IsImage { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? CreateBy { get; set; }
        public int? ModifyBy { get; set; }

        public virtual AppUserViewModel UserCreate { get; set; }

        public virtual AppUserViewModel UserModify { get; set; }
    }
}
