namespace IIG.Web.Data.Models.Practices.UnitTest;
public class UnitTestQuestionnaireDto
{
    public Guid QuestionnaireId { get; set; }
    public Guid UnitTestId { get; set; }
    public Guid CourseId { get; set; }
    public int SortOrder { get; set; }
}
