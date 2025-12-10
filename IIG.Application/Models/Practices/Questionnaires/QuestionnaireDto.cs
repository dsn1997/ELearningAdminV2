using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;

namespace IIG.Application.Models.Questionnaires
{
    public class QuestionnaireDto
    {
        public string Id { get; set; }

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

        public bool IsViewFullVideo { get; set; }

        public long CurrentScore { get; set; }

        public long TotalScore { get; set; }
        public int? RedoNumber { get; set; }

        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
        public List<LeftSectionDto> LeftSections { get; set; } = new List<LeftSectionDto>();

        public CourseScoringQuestionnaire CourseScoringModel { get; set; }
    }

    public class CourseScoringQuestionnaire
    {
        public Guid? ScoringId { get; set; }
        public ECourseScoringStatus? ScoringStatus { get; set; }
        public int? WatchCount { get; set; }
    }
}