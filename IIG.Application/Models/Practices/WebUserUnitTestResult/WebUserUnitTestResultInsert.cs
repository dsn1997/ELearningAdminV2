namespace IIG.Application.Models.WebUserUnitTestResult;
public class WebUserUnitTestResultInsert
{
    public Guid WebUserId { get; set; }

    public Guid UnitTestId { get; set; }

    public int TotalQuestions { get; set; }

    public int TotalCorrectAnswer { get; set; }

    public string QuestionnaireIds { get; set; }
}
