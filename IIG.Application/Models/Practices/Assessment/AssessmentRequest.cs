namespace IIG.Web.Data.Models.Practices.Assessment;

public class AssessmentRequest
{
    public Guid StepId { get; set; }

    public Guid QuestionnaireId { get; set; }

    public List<AnswerChooseRequest> AnswerChooseRequest { get; set; }
}