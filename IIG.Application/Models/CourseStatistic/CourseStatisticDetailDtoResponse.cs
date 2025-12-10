namespace IIG.Application.Models;
public class CourseStatisticDetailDtoResponse
{
    public List<UnitStatisticByCourseIdDto> Units { get; set; } = new();
    public List<CourseTestStatisticDetailByCourseIdDto> CourseTests { get; set; } = new();
}
