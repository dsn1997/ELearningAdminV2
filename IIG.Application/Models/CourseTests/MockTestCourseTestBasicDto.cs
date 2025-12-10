using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.CourseTests;

public class MockTestCourseTestBasicDto
{
    public Guid CourseTestId { get; set; }
    
    public  Guid WebUserId { get; set; }

    public Guid MockTestId { get; set; }

    public EMockTestScoreType MockTestScoreType { get; set; }
}