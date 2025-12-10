using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.MockTests;
using IIG.Web.Data.Models.LiveClassType;

namespace IIG.Web.Data.Models.Courses;

public class CourseInfoDto
    : IRedoSettingProperty
{
    public Guid Id { get; set; }
    public ECourseStatus Status { get; set; }
    public Guid? CategoryId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string NumberOfAssignment { get; set; }
    public string NumberOfStudent { get; set; }
    public string NumberOfVideo { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Modified { get; set; }
    public string ImageUrl { get; set; }
    public string BgImageUrl { get; set; }
    public int NumberOfRatings { get; set; }
    public decimal AverageRating { get; set; }
    public bool IsFreeLearningUnit1Lesson1 { get; set; }
    public bool IsBought { get; set; }
    public string UserGuideLink { get; set; }
    public int? RedoNumber { get; set; }
}