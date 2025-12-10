using IIG.Application.Models;

namespace IIG.Application.Models;

public class CourseTestChooseResponse : ChooseBaseModelResponse
{
    public Guid CourseTestId { get; set; }
    
    public Guid WebUserId { get; set; }

    public Guid MockTestPartId { get; set; }

    public Guid MockTestSectionId { get; set; }
}