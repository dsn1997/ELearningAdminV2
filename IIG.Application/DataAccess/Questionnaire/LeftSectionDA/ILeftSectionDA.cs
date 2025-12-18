

using IIG.Application.Models.LeftSection;
using IIG.Application.Models.Versioning;

namespace IIG.Application.Data;
public interface ILeftSectionDA 
{
    Task<IEnumerable<LeftSectionVersionInfo>> GetListVersionInfoBySubmittedDate(Guid questionnaireId, DateTime submittedDate);

    Task<IEnumerable<LeftSectionModel>> GetListByQuestionnaierIdAsync(Guid questionnaireId);
    
    Task<List<Guid>> GetListIdsByQuestionnaireIdAsync(Guid questionnaireId);
}
