using Microsoft.AspNetCore.Mvc;

namespace IIG.Core.Common.Models.Files
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string DisplayName { get; set; }
        public string Extension { get; set; }
        public Guid FileTypeId { get; set; }
        public FileContentResult FileByteContent { get; set; }
        //public FileContentResult ThumbnailByteContent { get; set; }
        //public FileContentResult SmallByteContent { get; set; }
        //public FileContentResult MediumByteContent { get; set; }
        //public FileContentResult LargeByteContent { get; set; }
    }
}
