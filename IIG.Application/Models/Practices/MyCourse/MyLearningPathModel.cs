using IIG.Core.Common.Enums;

namespace IIG.Application.Models.MyCourse;
public class MyLearningPathModel
{
    public Guid CourseId { get; set; }
    public Guid WebUserId { get; set; }
    public DateTime StartDate { get; set; }
    public EDaysPerWeek DaysPerWeek { get; set; }
}
