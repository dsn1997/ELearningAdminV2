namespace IIG.Core.Common.Models.Files
{
    public class ImageInfoModel
    {
        public Guid? Id { get; set; }
        public string FileName { get; set; }
        public string DisplayName { get; set; }
        public string StorageLocation { get; set; }
        public string Extension { get; set; }
        public long FileSize { get; set; }
        public string Uri { get; set; }
        public EImageSizeType SizeType { get; set; }
        public Guid FileTypeId { get; set; }
        public Guid ItemId { get; set; }
        public EEntityFile? EntityFile { get; set; }
        public bool IsPublic { get; set; } = true;
        
        public bool IsMigrate { get; set; }
    }
}
