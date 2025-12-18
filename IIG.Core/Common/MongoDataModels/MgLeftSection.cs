
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;
using Microsoft.AspNetCore.Mvc;

namespace IIG.Core.Common.MongoDataModels
{
    [BsonCollection("leftSections")]
    public class MgLeftSectionModel : Document
    {
        public Guid LeftSectionId { get; set; }
        public Guid QuestionnaireId { get; set; }
        public string Title { get; set; }
        public Guid? AudioFileId { get; set; }
        public Guid? ImageFileId { get; set; }
        public FileDto ImageInfo { get; set; }
        public string TextScript { get; set; }
        public string TextContent { get; set; }

        public TimeSpan? SpaceTime { get; set; }
        public string EvaluateManner { get; set; }
        public Guid? VideoId { get; set; }
        public string Sample { get; set; }
        public long? AudioDuration { get; set; }
    }

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
