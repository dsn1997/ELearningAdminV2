namespace IIG.Web.Data.Models.Practices.Assessment;

public class AssessmentResponse
{
    public Guid StepId { get; set; }

    public Guid QuestionnaireId { get; set; }

    public List<AnswerChooseResponse> AnswerChooseResponse { get; set; } = new();
}