namespace IIG.Application.Models.Assessment;

public class AssessmentSeeAnswerRequest
{
    public Guid StepId { get; set; }

    public Guid QuestionnaireId { get; set; }
}