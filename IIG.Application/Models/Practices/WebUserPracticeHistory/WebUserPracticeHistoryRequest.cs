namespace IIG.Web.Data.Models.Practices.WebUserPracticeHistory;

public class WebUserPracticeHistoryRequest
{
    public Guid QuestionnaireId { get; set; }
    public Guid StepId { get; set; }
    public Guid? QuestionId { get; set; }
}