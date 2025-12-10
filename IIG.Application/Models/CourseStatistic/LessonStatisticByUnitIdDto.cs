namespace IIG.Web.Data.Models.CourseStatistic;

public class LessonStatisticByUnitIdDto
{
    public Guid Id { get; set; }

    public Guid UnitId { get; set; }

    public string Title { get; set; }

    public int? SortOrder { get; set; }

    public decimal? LessonCompletion { get; set; }

    public int TotalQuestions { get; set; }

    public int TotalQuestionsCompleted { get; set; }

    public Guid? LatestStepId { get;set; }

    public Guid? LatestQuestionnaireId { get;set; }

    public bool CanStart { get; set; }

    public int? WatchCount { get; set; }
}