
namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    public class SpeakingWritingResultViewModel
    {
        public Guid Id { get; set; }
        public Guid? ChooseId { get; set; }
        public decimal? TotalScore { get; set; }
        public decimal? PronounciationScore { get; set; }
        public decimal? IntonationStressScore { get; set; }
        public decimal? VocabularyScore { get; set; }
        public decimal? GrammarScore { get; set; }
        public decimal? CohesionScore { get; set; }
        public decimal? RelevanceScore { get; set; }
        public decimal? CompletenessScore { get; set; }
        public string Comment { get; set; }
        public Guid? ApiSpeakingResponseId { get; set; }
        public Guid? ApiWritingResponseId { get; set; }

        /// <summary>
        /// Người comment
        /// </summary>
        public Guid? ScoringUserId { get; set; }
        public string MistakeExplain { get; set; }

        public decimal? AccuracyScore { get; set; }
        public decimal? FluencyScore { get; set; }
        public decimal? TopicScore { get; set; }

        /// <summary>
        /// json format
        /// </summary>
        public string TeacherEvaluation { get; set; }
        public ELastestSwitchType? LastestSwitchType { get; set; }
    }

    public enum ELastestSwitchType
    {
        FiveCriteria = 1,
        SevenCriteria = 2
    }
}
