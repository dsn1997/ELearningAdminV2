using IIG.Core.Common.MongoDataModels.MockTests;

namespace IIG.Web.Data.Models.CourseTests;

public class StartedDoingCourseTestResponse
{
    public string CourseName { get; set; }

    public Guid CourseId { get; set; }

    public string CourseTestTitle { get; set; }

    public int TotalSeconds { get; set; }

    public int TotalQuestions { get; set; }

    public MgMockTestMenuModel MockTestMenu { get; set; } = new();

    public Guid? LastestMockTestPartId { get; set; }
    public Guid? LastestQuestionnaireId { get; set; }
    public Guid? LastestAnswerId { get; set; }

}

public class AnswerProgressDto
{
    public int AnsweredQuestions { get; set; }
}