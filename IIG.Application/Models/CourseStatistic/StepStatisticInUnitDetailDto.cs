using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.CourseStatistic;
public class StepStatisticInUnitDetailDto
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public string Title { get; set; }
    public int TotalQuestions { get; set; }
    public int TotalQuestionsCompleted { get; set; }
    public int TotalCorrectAnswers { get; set; }
    public decimal CompletionPercentage { get; set; }
    public decimal CorrectPercentage { get; set; }
    public EUnitLessonStepStatus Status { get; set; }
    public int SortOrder { get; set; }
    public bool IsOnlySW { get; set; } = false;
}
