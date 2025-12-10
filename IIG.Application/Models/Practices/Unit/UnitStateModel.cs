namespace IIG.Application.Models.Unit;
public class UnitStateModel
{
    public Guid? CurrentUnitId { get; set; }
    public Guid? CurrentLessonId { get; set; }
    public Guid? CurrentStepId { get; set; }
    public Guid? QuestionnaireId { get; set; }
    public Guid? QuestionId { get; set; }
}
