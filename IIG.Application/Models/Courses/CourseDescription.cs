using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.MockTests;
using IIG.Web.Data.Models.LiveClassType;

namespace IIG.Web.Data.Models.Courses;

public class CourseDescriptionDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int? SortOrder { get; set; }
    public short Type { get; set; }
    public List<CourseDescriptionDetailDto> CourseDescriptionDetails { get; set; }
}

public class CourseDescriptionDetailDto
{
    public Guid Id { get; set; }
    public short? CourseDescriptionType { get; set; }
    public string Content { get; set; }
    public bool? IsUrl { get; set; }
    public string Url { get; set; }
    public short? SortOrder { get; set; }
    public Guid? TteacherId { get; set; }
    public Guid? TstudentId { get; set; }
    public Guid? TstudentReviewId { get; set; }
    public Guid? TfaqId { get; set; }
    public string ImageFileUrl { get; set; }
}