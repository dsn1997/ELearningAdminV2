using IIG.Core.Common.Enums;

namespace IIG.Application.Models;

public class MockTestCourseTestBasicDto
{
    public Guid CourseTestId { get; set; }
    
    public  Guid WebUserId { get; set; }

    public Guid MockTestId { get; set; }

    public EMockTestScoreType MockTestScoreType { get; set; }
}