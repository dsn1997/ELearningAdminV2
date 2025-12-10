using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;

namespace IIG.Core.Common.MongoDataModels
{
    [BsonCollection("questionnaires")]
    public class MgQuestionnaireModel : Document
    {
        public Guid QuestionnaireId { get; set; }

        public string Name { get; set; }

        public EQuestionnaireType Type { get; set; }

        public Guid? GroupId { get; set; }

        public bool IsActive { get; set; }

        public string TitleLeftSection { get; set; }

        public Guid? VideoFileId { get; set; }

        public Guid? SubtitleFileId { get; set; }

        public Guid? SlideFileId { get; set; }

        public FileDto SubtitleInfo { get; set; }

        public FileDto SlideInfo { get; set; }

        public string IntroContent { get; set; }

        public string Identifier { get; set; }

        public long TotalScore { get; set; } = 0;
    }
}