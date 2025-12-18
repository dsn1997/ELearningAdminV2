

using IIG.Application.Models.StepQuestionnaires;

namespace IIG.Application.Data;

public interface IStepQuestionnaireDA 
{
    Task<StepQuestionnaireDto> GetInfo(Guid stepId, Guid QuestionnaireId);

    Task<IEnumerable<StepQuestionnaireDto>> GetListByStepIdAsync(Guid stepId);
}