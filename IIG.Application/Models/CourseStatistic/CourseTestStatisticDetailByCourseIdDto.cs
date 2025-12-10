using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.CourseStatistic;
public class CourseTestStatisticDetailByCourseIdDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid MockTestId { get; set; }
    public int TotalQuestionsFreeStyle { get; set; }
    public int TotalQuestionsNonstop { get; set; }
    public int TotalSpeakingQuestions { get; set; }
    public int TotalWritingQuestions { get; set; }
    public int NumberOfTime { get; set; }
    public bool IsTested { get; set; }
    public int SortOrder { get; set; }
    public bool CanStart { get; set; }
    public int TotalQuestions { get; set; }
    public Guid CourseId { get; set; }
    public int? RedoNumber { get; set; }
    public bool? IsSWType { get; set; }
    public ECourseScoringStatus? ScoringStatus { get; set; }
}
