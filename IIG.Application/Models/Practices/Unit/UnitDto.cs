using IIG.Web.Data.Models.CourseStatistic;

namespace IIG.Web.Data.Models.Practices.Unit;

public class UnitDto
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }

    public string Title { get; set; }

    public int? SortOrder { get; set; }

    public bool LearnInOrder { get; set; }

    public string TagName { get; set; }

    public string ImageUrl { get; set; }

    public decimal UnitCompletion { get; set; }

    public bool  CanStart { get; set; }

    public Guid? CurrentLessonId { get; set; }
    public Guid? CurrentStepId { get; set; }
    public Guid? QuestionnaireId { get; set; }
    public Guid? QuestionId { get; set; }

    public IEnumerable<LessonStatisticByUnitIdDto> Lessons { get; set; }

    public IEnumerable<UnitTestStatisticByUnitIdDto> UnitTests { get; set; }

    public Guid? CourseScoringId { get; set; }
    public int? WatchCount { get; set; }
}