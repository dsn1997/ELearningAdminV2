
using IIG.Core.Common.Models.Files;
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

}
