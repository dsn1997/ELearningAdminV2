using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Practices.MyCourse;
public class MyLearningPathModel
{
    public Guid CourseId { get; set; }
    public Guid WebUserId { get; set; }
    public DateTime StartDate { get; set; }
    public EDaysPerWeek DaysPerWeek { get; set; }
}
