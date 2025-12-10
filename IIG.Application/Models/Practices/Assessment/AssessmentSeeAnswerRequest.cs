namespace IIG.Web.Data.Models.Practices.Assessment;

public class AssessmentSeeAnswerRequest
{
    public Guid StepId { get; set; }

    public Guid QuestionnaireId { get; set; }
}