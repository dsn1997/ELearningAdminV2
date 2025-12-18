
using IIG.Application.Models.Questionnaires;
using IIG.Application.Models.UnitTest;
using IIG.Application.Models.Versioning;

namespace IIG.Application.Data;
public interface IQuestionnaireDA 
{
    Task<IEnumerable<UnitTestVersionMenuWithStatusListModel>> GetMenuVersionByListQuestionnaireIdAsync(List<Guid> questionnaireIds, DateTime submittedDate);

    Task<QuestionnaireVersionInfo> GetVersionInfoBySubmittedDate(Guid questionnaireId, DateTime submittedDate);

    Task<QuestionnaireModel> GetByIdAsync(Guid id);
}
