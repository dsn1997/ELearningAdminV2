using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Practices.MyCourse;
public class MyCourseListForSelectionModel
{
    public Guid CourseId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public DateTime LearningCourseExpired { get; set; }
    public EProductType ProductType { get; set; }
    public bool? IsMockTestType { get; set; }
    public DateTime Created
    { get; set; }

    public int? Status { get; set; }
}
