using IIG.Application.Models.Questionnaires;
using IIG.Application.Models.Versioning;

namespace IIG.Application.Data;
public interface IAnswerDA
{
    Task<IEnumerable<AnswerVersionInfo>> GetListVersionInfoBySubmittedDate(List<Guid> questionIds, DateTime submittedDate);

    Task<IEnumerable<AnswerDto>> GetListAnswerByListQuestionIdAsync(IEnumerable<Guid> questionIds);
}
