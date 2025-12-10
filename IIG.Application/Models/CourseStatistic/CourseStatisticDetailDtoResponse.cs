namespace IIG.Web.Data.Models.CourseStatistic;
public class CourseStatisticDetailDtoResponse
{
    public List<UnitStatisticByCourseIdDto> Units { get; set; } = new();
    public List<CourseTestStatisticDetailByCourseIdDto> CourseTests { get; set; } = new();
}
