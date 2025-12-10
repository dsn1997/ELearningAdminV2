using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Web.Data.Models
{
    public class FileDataDto
    {
        public Guid? Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string StorageLocation { get; set; }
        public string DisplayName { get; set; }
        public Guid FileTypeId { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CourseTeacherId { get; set; }
        public string ThumbnailStorageLocation { get; set; }
        public string SmallStorageLocation { get; set; }
        public string MediumStorageLocation { get; set; }
        public string LargeStorageLocation { get; set; }
        public int ImageOrder { get; set; }
        public long FileSize { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public Guid? NewsId { get; set; }
        public long? AudioDuration { get; set; }
        public bool IsMigrate { get; set; }
    }

}
