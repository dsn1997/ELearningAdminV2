using IIG.Application.Models.MockTestPartQuestionnaire;
using IIG.Core.Common.Models.Paging;

namespace IIG.Application.Data;

public interface IMockTestPartQuestionnaireDA 
{
    Task<PaginationSet<MockTestPartQuestionnaireAssignedListModel>> GetQuestionnaireListByMockTestPartIdAsync(Guid mockTestPartId, SearchingMockTestPartQuestionnaireRequest request, DateTime? publishedAt = null);
    
    Task<IEnumerable<MockTestQuestionAnswerIdModel>> GetListQuestionAnswerIdByMockTestPartIdAsync(Guid mockTestPartId,
        DateTime? publishedAt = null);
}