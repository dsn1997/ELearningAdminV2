namespace IIG.Web.Data.Models.CourseStatistic;

public class UnitStatisticDetailDto
{
    public Guid UnitId { get; set; }
    public List<LessonStatisticInUnitDetailDto> Lessons { get; set; } = new();
    public List<StepStatisticInUnitDetailDto> Steps { get; set; } = new();
}