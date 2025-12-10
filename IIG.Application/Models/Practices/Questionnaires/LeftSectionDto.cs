using IIG.Core.Common.Models.Files;
using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Web.Data.Models.Practices.Questionnaires
{
    public class LeftSectionDto
        : ISpeakingWritingProperty, ISampleProperty
    {
        public string Id { get; set; }
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
    }
}
