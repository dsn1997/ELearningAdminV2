using IIG.Web.Data.Models.Answers;

namespace IIG.Web.Data.Models.CourseTests;

public class CourseTestChooseResponse : ChooseBaseModelResponse
{
    public Guid CourseTestId { get; set; }
    
    public Guid WebUserId { get; set; }

    public Guid MockTestPartId { get; set; }

    public Guid MockTestSectionId { get; set; }
}