using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Practices.MyCourse;
public class MyCourseInfoForLearningPathModel
{
    public Guid CourseId { get; set; }
    public EProductType ProductType { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? LearningCourseExpired { get; set; }
    public bool LearnInOrder { get; set; }
}
