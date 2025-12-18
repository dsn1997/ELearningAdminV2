

using IIG.Application.Models.Questionnaires;
using IIG.Application.Models.Versioning;

namespace IIG.Application.Data;
public interface IQuestionDA
{
    Task<IEnumerable<QuestionVersionInfo>> GetListVersionInfoBySubmittedDate(Guid questionnaireId, DateTime submittedDate);

    Task<IEnumerable<QuestionTranslationDto>> GetListTranslationByListQuestionIdAndAndSubmittedDateAsync(IEnumerable<Guid> questionIds, DateTime submittedDate);

    Task<FlashCardQuestionModel> GetFlashCardQuestionInfoByIdAsync(Guid id);

    Task<IEnumerable<QuestionTranslationDto>> GetListTranslationByQuestionIdAsync(Guid questionId);

    Task<IEnumerable<QuestionDto>> GetListQuestionByQuestionnaireIdAsync(Guid questionnaireId);

    Task<IEnumerable<QuestionTranslationDto>> GetListTranslationByListQuestionIdAsync(IEnumerable<Guid> questionIds);
}
