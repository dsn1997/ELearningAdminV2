namespace IIG.Application.Models.Assessment;

public class AssessmentRedoRequest
{
    public Guid StepId { get; set; }
    
    public Guid QuestionnaireId { get; set; }
}