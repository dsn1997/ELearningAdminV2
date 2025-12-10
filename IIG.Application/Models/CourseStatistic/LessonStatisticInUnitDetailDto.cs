using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.CourseStatistic;
public class LessonStatisticInUnitDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int TotalQuestions { get; set; }
    public int TotalQuestionsCompleted { get; set; }
    public int TotalCorrectAnswers { get; set; }
    public EUnitLessonStepStatus Status { get; set; }
    public int CurrentScore { get; set; }
    public int TotalScore { get; set; }
    public int SortOrder { get; set; }
    public List<StepStatisticInUnitDetailDto> Steps { get; set; } = new();
}
