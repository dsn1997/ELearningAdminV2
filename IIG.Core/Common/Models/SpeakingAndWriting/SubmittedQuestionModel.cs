using IIG.Core.Common.Enums;

namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    public class SubmittedQuestionModel
    {
        public Guid? QuestionId { get; set; }
        public string AnsweredText { get; set; }
        public ECourseScoringStatus? Status { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public Guid? AudioFileId { get; set; }

        public SpeakingWritingResultViewModel? ResultModel { get; set; }
    }
}
