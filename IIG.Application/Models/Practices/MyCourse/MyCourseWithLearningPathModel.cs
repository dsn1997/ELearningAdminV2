using IIG.Core.Common.Enums;

namespace IIG.Application.Models.MyCourse;

public class MyCourseWithLearningPathModel
{
    public int Week { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public List<UnitWithLearningPathModel> Units { get; set; } = new();
}

public class UnitWithLearningPathModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public EUnitWithLearningPathStatus UnitStatus { get; set; }

    public int TotalScore { get; set; }

    public int CurrentScore { get; set; }

    public bool CanStart { get; set; }

    public List<CourseTestWithLearningPathModel> CourseTests { get; set; } = new();
}

public class CourseTestWithLearningPathModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public ECourseTestWithLearningPathStatus CourseTestStatus { get; set; }

    public int TotalScore { get; set; }

    public string CurrentScore { get; set; }

    public bool CanStart { get; set; }
}