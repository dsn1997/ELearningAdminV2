using IIG.Core.Common.Models.Files;

namespace IIG.Web.Data.Models.Practices.Unit;

public class UnitModel
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int? SortOrder { get; set; }

    public bool LearnInOrder { get; set; }

    public int? WeekLearningPlan { get; set; }

    public string TagName { get; set; }

    public string ImageUrl { get; set; }

    public bool CanStart { get; set; }
    public Guid? CourseScoringId { get; set; }
    public int? WatchCount { get; set; }
}